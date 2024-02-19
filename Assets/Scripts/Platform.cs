using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public enum PlatformType { Hob, DinningTable, Sink, PlateRack, Refrigerator, Counter };

    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private PlatformType type;

        public PlatformType Type { get { return type; } }
        public Grid grid;
        public int capacity;
        public List<Item> items;
        public virtual void OnPickUp()
        {

        }

        public virtual void OnOperate()
        {

        }
    }
}
