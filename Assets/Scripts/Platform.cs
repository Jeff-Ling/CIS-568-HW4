using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public enum PlatformType { Hob, DinningTable, Sink, PlateRack, Refrigerator, Counter };

    public class Platform : MonoBehaviour
    {
        public Transform itemAnchor;
        [SerializeField]
        private PlatformType type;
        [SerializeField]
        public PhotonView photonView;

        public PlatformType Type { get { return type; } }
        //public Grid grid;
        //public int capacity;
        //public List<Item> items;
        //public List<Vector2Int> availablePosition;
        public Item item;
        public virtual void Start()
        {
            if (photonView  == null)
            {
                photonView = GetComponent<PhotonView>();
            }
            if (itemAnchor == null) itemAnchor = transform;
        }
        [PunRPC]
        public virtual bool AddOneItem(int itemViewId)
        {
            var go = PhotonView.Find(itemViewId).gameObject;
            var item = go.GetComponent<Item>();
            if (go != null)
            {
                if (this.item == null)
                {
                    this.item = item;
                    item.transform.parent = itemAnchor;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    Debug.Log($"{item} added to {this}");
                    return true;
                }
                else if (this.item.Type == ItemType.Plate && item.Type == ItemType.Steak)
                {
                    Plate plate = this.item as Plate;
                    if (plate.AddSteak())
                    {
                        //GameObject.Destroy(item.gameObject);
                        PhotonNetwork.Destroy(item.gameObject);
                        return true;
                    }
                }
            }
            return false;
        }
        [PunRPC]
        public virtual int OnPickUp()
        {
            Item ret = null;
            if (item != null)
            {
                Debug.Log($"Pick up from {this}");
                ret = item;
                item = null;
            }
            if (ret != null) return ret.photonView.ViewID;
            else return -1;
        }
        [PunRPC]
        public virtual void Update()
        {
            if (GameManager.instance.OperatingPlatform == this)
            {
                if (item != null)
                {
                    item.UpdateByPlatform(photonView.ViewID);
                }
            }
        }
    }
}
