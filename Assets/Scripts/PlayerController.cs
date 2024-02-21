using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class PlayerController : MonoBehaviour
    {
        public Transform itemAnchor;
        public float rayCastDistance = 1f;
        public Item holdingItem;
        private void Start()
        {
            if (itemAnchor == null) itemAnchor = transform;
        }
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastDistance, LayerMask.GetMask("Platform")))
            {
                //Debug.Log("Find Platform!");
                GameManager.instance.RayCastingPlatform = hit.collider.GetComponent<Platform>();
            }
            else
            {
                GameManager.instance.RayCastingPlatform = null;
            }
        }
    }
}
