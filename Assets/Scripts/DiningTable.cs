using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFirstARGame
{
    public class DiningTable : Platform
    {
        //public GameObject customerPrefab;
        public Transform customerAnchor;
        public float spawnInterval = 5f;
        public GameObject customer;
        public float eatProgress = 0f;
        public float eatSpeed = 0.1f;

        public Slider patienceSlider;
        [SerializeField]
        float maxPatience = 100f;
        float currentPatience;
        [SerializeField]
        float decreaseRate = 5f;

        float timer = 0f;

        public override void Start()
        {
            base.Start();
            //if (customer == null) customer = Instantiate(customerPrefab, customerAnchor);
        }
        [PunRPC]
        public override bool AddOneItem(int itemViewId)
        {
            var go = PhotonView.Find(itemViewId).gameObject;
            var item = go.GetComponent<Item>();
            if (this.item == null)
            {
                Plate plate = item as Plate;
                if (plate)
                {
                    if (plate.steakOnPlate.active)
                    {
                        this.item = item;
                        item.transform.parent = itemAnchor;
                        item.transform.localPosition = Vector3.zero;
                        item.transform.localRotation = Quaternion.identity;
                        return true;
                    }
                }
            }
            return false;
        }
        [PunRPC]
        public override int OnPickUp()
        {
            int ret = base.OnPickUp();
            if (ret != -1)
            {
                var go = PhotonView.Find(ret).gameObject;
                var item = go.GetComponent<Item>();
                Plate plate = item as Plate;
                if (plate == null || !plate.dirtyPlate.active)
                {
                    ret = -1;
                }
            }
            return ret;
        }

        // Update is called once per frame
        public override void Update()
        {
            if (!customer.active)
            {
                if (timer > spawnInterval)
                {
                    customer.SetActive(true);
                    currentPatience = maxPatience;
                    timer = 0;
                }
                else timer += Time.deltaTime;
            }
            else timer = 0;

            Plate plate = item as Plate;
            if (plate != null)
            {
                if (plate.steakOnPlate.active)
                {
                    Debug.Log($"Eating Steak... {eatProgress * 100f}%.");
                    eatProgress += eatSpeed * Time.deltaTime;
                    if (eatProgress > 1f)
                    {
                        Debug.Log("Eating Done.");
                        plate.FinishedEating();
                        GameManager.instance.IncrementScore();
                        eatProgress = 0f;
                        customer.active = false;
                    }
                }
            }

            if (customer.active)
            {
                if (plate == null)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (currentPatience > 0)
                        {
                            Debug.Log(currentPatience);
                            currentPatience -= decreaseRate * Time.deltaTime;
                            this.photonView.RPC("UpdatePatienceUI", RpcTarget.All, currentPatience);
                            // UpdatePatienceUI();
                        }
                        else
                        {
                            GameManager.instance.GameOver();
                        }
                    }
                }
            }


            base.Update();
        }

        [PunRPC]
        void UpdatePatienceUI(float curPatience)
        {
            patienceSlider.value = curPatience / maxPatience;
        }
    }
}
