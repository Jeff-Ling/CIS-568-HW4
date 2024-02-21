using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class Refrigirator : Platform
    {
        public GameObject steakPrefab;
        public float spawnInterval = 2f;
        public GameObject steak;

        float timer = 0f;

        public override bool AddOneItem(Item item)
        {
            return false;
        }
        public override Item OnPickUp()
        {
            var ret = base.OnPickUp();
            if (ret != null)
            {
                steak = null;
            }
            return ret;
        }

        // Update is called once per frame
        void Update()
        {
            if (steak == null)
            {
                if (timer > spawnInterval)
                {
                    steak = Instantiate(steakPrefab, itemAnchor);
                    this.item = steak.GetComponent<Steak>();
                    timer = 0;
                }
                else timer += Time.deltaTime;
            }
            else timer = 0;
        }
    }
}
