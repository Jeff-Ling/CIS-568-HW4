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

        public PlatformType Type { get { return type; } }
        //public Grid grid;
        //public int capacity;
        //public List<Item> items;
        //public List<Vector2Int> availablePosition;
        public Item item;
        public virtual void Start()
        {
            if (itemAnchor == null) itemAnchor = transform;
        }
        public virtual bool AddOneItem(Item item)
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
                    GameObject.Destroy(item.gameObject);
                    return true;
                }
            }
            return false;
        }
        public virtual Item OnPickUp()
        {
            Item ret = null;
            if (item != null)
            {
                Debug.Log($"Pick up from {this}");
                ret = item;
                item = null;
            }
            return ret;
        }
        public virtual void Update()
        {
            if (GameManager.instance.OperatingPlatform == this)
            {
                if (item != null)
                {
                    item.UpdateByPlatform(this);
                }
            }
        }
    }
}
