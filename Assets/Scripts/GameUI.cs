using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameUI : MonoBehaviour
    {
        public GameObject pickUpBtn;
        public GameObject dropBtn;
        public GameObject operateBtn;
        private void Start()
        {
            pickUpBtn.SetActive(true);
            dropBtn.SetActive(false);
        }
        public void OnPickUp()
        {
            if (GameManager.instance.OnPickUp())
            {
                pickUpBtn.SetActive(false);
                dropBtn.SetActive(true);
            }
        }
        public void OnDrop()
        {
            if (GameManager.instance.OnDrop())
            {
                pickUpBtn.SetActive(true);
                dropBtn.SetActive(false);
            }
        }
        public void OnOperate()
        {
            GameManager.instance.OnOperate();
        }
        public void OnStopOperate()
        {
            GameManager.instance.OnStopOperate();
        }
    }
}
