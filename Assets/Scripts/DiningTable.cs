using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class DiningTable : Platform
    {
        public GameObject customerPrefab;
        public Transform customerAnchor;
        public float spawnInterval = 5f;
        public GameObject customer;
        public float eatProgress = 0f;
        public float eatSpeed = 0.1f;

        float timer = 0f;

        public override void Start()
        {
            base.Start();
            customer = Instantiate(customerPrefab, customerAnchor);
        }

        public override bool AddOneItem(Item item)
        {
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
        public override Item OnPickUp()
        {
            var ret = base.OnPickUp();
            if (ret != null)
            {
                Plate plate = ret as Plate;
                if (plate == null || !plate.dirtyPlate.active)
                {
                    ret = null;
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
                        //GameManager.instance.Score++;
                        // TODO: add score
                        eatProgress = 0f;
                        customer.active = false;
                    }
                }
            }

            base.Update();
        }
    }
}
