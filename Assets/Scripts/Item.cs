using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    enum ItemType { Steak, Plate };
    public class Item : MonoBehaviour
    {
        public virtual void OnDrop(Platform platform)
        {

        }
        public virtual void OnDrop(Item item)
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
