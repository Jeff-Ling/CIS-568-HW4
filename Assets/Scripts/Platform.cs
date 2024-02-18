using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    enum PlatformType { Hob, DinningTable, Sink, PlateRack, Refrigerator, Counter };

    public class Platform : MonoBehaviour
    {
        public Grid grid;
        public int capacity;
        public List<Item> items;
        public virtual void OnPickUp()
        {

        }

        public virtual void OnOperate()
        {

        }

        // Start is called before the first frame update
        public virtual void Start()
        {
        
        }

        // Update is called once per frame
        public virtual void Update()
        {
        
        }
    }
}
