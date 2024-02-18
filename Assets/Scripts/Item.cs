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

        }s
    }
}
