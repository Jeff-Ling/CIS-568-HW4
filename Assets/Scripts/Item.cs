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

        public ItemType Type { get { return type; } }
        public virtual void OnPickUp()
        {
        }
        public virtual void OnDrop(Platform platform)
        {
        }
        public virtual void UpdateByPlatform(Platform platform)
        {

        }
    }
}
