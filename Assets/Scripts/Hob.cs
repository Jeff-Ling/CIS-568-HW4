using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Hob : Platform
    {
        static HashSet<Hob> HobsInCooking = new HashSet<Hob>();
        public AudioSource cookingAudioClip;
        public override void Update()
        {
            //base.Update();
            if (this.item != null && this.item.Type == ItemType.Steak)
            {
                item.UpdateByPlatform(photonView.ViewID);
                if (HobsInCooking.Count == 0)
                    cookingAudioClip.Play();
                HobsInCooking.Add(this);
            }
            else
            {
                if (HobsInCooking.Contains(this))
                {
                    HobsInCooking.Remove(this);
                    if (HobsInCooking.Count == 0)
                    {
                        cookingAudioClip.Stop();
                    }
                }
            }
        }
    }
}
