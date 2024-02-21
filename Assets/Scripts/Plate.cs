using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Plate : Item
    {
        [Tooltip("How clean is this plate after usage.")]
        public float washProgress = 0f;
        public float washSpeed = 0.1f;
        public GameObject cleanPlate;
        public GameObject dirtyPlate;
        public GameObject steakOnPlate;
        //public bool Consumable { get; private set; }
        public override void Start()
        {
            base.Start();
            cleanPlate.SetActive(true);
            dirtyPlate.SetActive(false);
            steakOnPlate.SetActive(false);
        }
        [PunRPC]
        public override void UpdateByPlatform(int platformViewId)
        {
            var go = PhotonView.Find(platformViewId).gameObject;
            var platform = go.GetComponent<Platform>();
            if (dirtyPlate.active)
            {
                if (platform.Type == PlatformType.Sink)
                {
                    Debug.Log($"Cleaning Plate... {washProgress * 100f}%.");
                    washProgress += washSpeed * Time.deltaTime;
                    if (washProgress > 1f)
                    {
                        Debug.Log("Clean Done.");
                        photonView.RPC("CleanSync", RpcTarget.AllBuffered, ViewID);
                    }
                }
            }
        }
        //public override void OnDrop(Platform platform)
        //{

        //}
        public bool AddSteak()
        {
            if (!cleanPlate.active) return false;
            photonView.RPC("SteakSync", RpcTarget.AllBuffered, ViewID);
            //Consumable = true;
            return true;
        }
        public bool FinishedEating()
        {
            if (!steakOnPlate.active) return false;
            photonView.RPC("DirtySync", RpcTarget.AllBuffered, ViewID);
            return true;
        }

        [PunRPC]
        public void CleanSync(int plateId)
        {
            if (plateId == ViewID)
            {
                cleanPlate.SetActive(true);
                dirtyPlate.SetActive(false);
                steakOnPlate.SetActive(false);
                washProgress = 0f;
            }
        }
        [PunRPC]
        public void DirtySync(int plateId)
        {
            if (plateId == ViewID)
            {
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(true);
                steakOnPlate.SetActive(false);
            }
        }
        [PunRPC]
        public void SteakSync(int plateId)
        {
            if (plateId == ViewID)
            {
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                steakOnPlate.SetActive(true);
            }
        }
    }
}
