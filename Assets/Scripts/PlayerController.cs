using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class PlayerController : MonoBehaviour
    {
        public float rayCastDistance = 1f;
        public Item holdingItem;
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastDistance, LayerMask.GetMask("Platform")))
            {
                Debug.Log("Find Platform!");
            }
        }
    }
}
