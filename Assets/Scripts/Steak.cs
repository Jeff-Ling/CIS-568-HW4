using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Steak : Item
    {
        public float progress = 0f;
        public float cookingSpeed = 0.1f;
        public GameObject rawSteak;
        public GameObject welldoneSteak;
        public override void Start()
        {
            base.Start();
            rawSteak.SetActive(true);
            welldoneSteak.SetActive(false);
        }
        [PunRPC]
        public override void UpdateByPlatform(int platformViewId)
        {
            var go = PhotonView.Find(platformViewId).gameObject;
            var platform = go.GetComponent<Platform>();
            if (progress < 1f)
            {
                if (platform.Type == PlatformType.Hob)
                {
                    Debug.Log($"Cooking Steak... {progress * 100f}%.");
                    progress += cookingSpeed * Time.deltaTime;
                    if (progress >= 1f)
                    {
                        Debug.Log("Steak Done.");
                        //rawSteak.SetActive(false);
                        //welldoneSteak.SetActive(true);
                        photonView.RPC("CookedSync", RpcTarget.AllBuffered, ViewID);
                    }
                }
            }
        }
        [PunRPC]
        public void CookedSync(int steakId)
        {
            if (steakId == ViewID)
            {
                progress = 1f;
                rawSteak.SetActive(false);
                welldoneSteak.SetActive(true);
            }
        }

    }
}
