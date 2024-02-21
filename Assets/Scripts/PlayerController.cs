using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class PlayerController : MonoBehaviour
    {
        public Transform itemAnchor;
        public float rayCastDistance = 1f;

        [SerializeField]
        public PhotonView photonView;
        public int ViewID
        {
            get => photonView.ViewID;
        }
        [SerializeField]
        public Platform operatingPlatform;
        [SerializeField]
        public Platform rayCastingPlatform;
        [SerializeField]
        public Item holdingItem;
        private void Start()
        {        
            if (photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
            if (itemAnchor == null) itemAnchor = transform;
            if (photonView != null)
            {
                if (GameManager.instance.playerController.ViewID != ViewID)
                {
                    this.enabled = false;
                }
            }
        }
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastDistance, LayerMask.GetMask("Platform")))
            {
                //Debug.Log("Find Platform!");
                GameManager.instance.RayCastingPlatform = hit.collider.GetComponent<Platform>();
            }
            else
            {
                GameManager.instance.RayCastingPlatform = null;
            }
        }
    }
}
