using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public enum ItemType { Steak, Plate };
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private ItemType type;
        [SerializeField]
        public PhotonView photonView;

        public ItemType Type { get { return type; } }
        public virtual void Start()
        {
            if (photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
        }
        [PunRPC]
        public virtual void OnPickUp()
        {
        }
        [PunRPC]
        public virtual void OnDrop(int platformViewId)
        {
        }
        [PunRPC]
        public virtual void UpdateByPlatform(int platformViewId)
        {

        }
    }
}
