using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Refrigirator : Platform
    {
        public GameObject steakPrefab;
        public float spawnInterval = 2f;
        public GameObject steak;

        float timer = 0f;

        public override void Start()
        {
            base.Start();
            //if (item == null)
            //{
            //    steak = PhotonNetwork.Instantiate(steakPrefab.name, itemAnchor.transform.position, itemAnchor.transform.rotation);
            //    this.item = steak.GetComponent<Steak>();
            //    timer = 0;
            //}
        }
        [PunRPC]
        public override bool AddOneItem(int id)
        {
            return false;
        }
        [PunRPC]
        public override int OnPickUp()
        {
            var ret = base.OnPickUp();
            if (ret != -1)
            {
                steak = null;
            }
            return ret;
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (steak == null)
                {
                    if (timer > spawnInterval)
                    {
                        steak = PhotonNetwork.Instantiate(steakPrefab.name, itemAnchor.transform.position, itemAnchor.transform.rotation);
                        steak.transform.parent = itemAnchor; steak.transform.localScale = Vector3.one;
                        this.item = steak.GetComponent<Steak>();
                        timer = 0;
                        photonView.RPC("OnSteakInstantiated", RpcTarget.AllBuffered, ViewID, item.photonView.ViewID);

                    }
                    else timer += Time.deltaTime;
                }
                else timer = 0;
            }
        }
        [PunRPC]
        void OnSteakInstantiated(int refrigeratorId, int steakId)
        {
            if (refrigeratorId == photonView.ViewID)
            {
                if (item == null)
                {
                    var steak = PhotonView.Find(steakId).GetComponent<Steak>();
                    steak.transform.parent = itemAnchor; steak.transform.localScale = Vector3.one;
                    this.item = steak.GetComponent<Steak>();
                }
            }
        }
    }
}
