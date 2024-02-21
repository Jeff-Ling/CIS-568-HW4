using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameUI : MonoBehaviour
    {
        public GameObject pickUpBtn;
        public GameObject dropBtn;
        public GameObject operateBtn;
        public TMP_Text scoreField;
        private void Start()
        {
            pickUpBtn.SetActive(true);
            dropBtn.SetActive(false);
        }
        private void Update()
        {
            scoreField.text = $"$ {GameManager.instance.Score}";
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
