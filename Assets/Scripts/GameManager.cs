using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        go.tag = "GameController";
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        [SerializeField]
        public PhotonView photonView;
        [SerializeField]
        protected PlayerController playerController;
        [SerializeField]
        protected Platform operatingPlatform;
        [SerializeField]
        protected Platform rayCastingPlatform;
        [SerializeField]
        protected Item holdingItem;
        //[SerializeField]
        //protected int score = 0;
        //public int Score
        //{
        //    get => score; set => score = value;
        //}
        public Platform OperatingPlatform {
            get { return operatingPlatform; } 
            set
            {
                operatingPlatform = value;
            }
        }
        public Platform RayCastingPlatform { 
            get { return rayCastingPlatform; }

            set
            {
                rayCastingPlatform = value;
            }
        }
        public Item HoldingItem { 
            get { return holdingItem; }
            set
            {
                holdingItem = value;
            }
        }
        protected void Start()
        {

            if (photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
        }
        public bool OnPickUp()
        {
            if (holdingItem != null) return false;
            if (rayCastingPlatform == null) return false;
            int id = rayCastingPlatform.OnPickUp();
            if (id == -1) holdingItem = null;
            else holdingItem = PhotonView.Find(id).GetComponent<Item>();
            if (holdingItem != null)
            {
                holdingItem.transform.parent = playerController.transform;
                holdingItem.transform.localPosition = Vector3.zero;
                holdingItem.transform.localRotation = Quaternion.identity;
                holdingItem.OnPickUp();
                
            }
            return holdingItem != null;
        }
        public bool OnDrop()
        {
            if (holdingItem == null) return false;
            if (rayCastingPlatform == null) return false;
            if (rayCastingPlatform.AddOneItem(holdingItem.photonView.ViewID))
            {
                holdingItem.OnDrop(rayCastingPlatform.photonView.ViewID);
                holdingItem = null;
                return true;
            }
            return false;
        }
        public bool OnOperate()
        {
            if (rayCastingPlatform == null) return false;
            operatingPlatform = rayCastingPlatform;
            return true;
        }
        public void OnStopOperate()
        {
            operatingPlatform = null;
        }
    }
}
