using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Hob : Platform
    {
        public override void Update()
        {
            //base.Update();
            if (this.item != null && this.item.Type == ItemType.Steak)
            {
                item.UpdateByPlatform(this);
            }
        }
    }
}
