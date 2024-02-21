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
        public PlayerController playerController;

        [SerializeField]
        public PhotonView photonView;
        public int ViewID
        {
            get => photonView.ViewID;
        }
        //[SerializeField]
        //protected Platform operatingPlatform;
        //[SerializeField]
        //protected Platform rayCastingPlatform;
        //[SerializeField]
        //protected Item holdingItem;
        [SerializeField]
        protected int score = 0;
        public int Score
        {
            get => score; 
        }
        public void IncrementScore()
        {
            photonView.RPC("ScoreSync", RpcTarget.AllBuffered, score+1);
        }
        [PunRPC]
        public void ScoreSync(int score)
        {
            this.score = score;
        }
        public Platform OperatingPlatform {
            get { return playerController.operatingPlatform; } 
            set
            {
                playerController.operatingPlatform = value;
            }
        }
        public Platform RayCastingPlatform { 
            get { return playerController.rayCastingPlatform; }

            set
            {
                playerController.rayCastingPlatform = value;
            }
        }
        public Item holdingItem { 
            get { return playerController.holdingItem; }
            set
            {
                playerController.holdingItem = value;
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
            if (playerController.holdingItem != null) return false;
            if (playerController.rayCastingPlatform == null) return false;
            int id = playerController.rayCastingPlatform.OnPickUp();

            if (id != -1)
            {
                playerController.holdingItem = PhotonView.Find(id).GetComponent<Item>();
                playerController.holdingItem.transform.parent = playerController.transform;
                playerController.holdingItem.transform.localPosition = Vector3.zero;
                playerController.holdingItem.transform.localRotation = Quaternion.identity;
                playerController.holdingItem.OnPickUp();
                photonView.RPC("OnPickUpSync", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, playerController.ViewID, playerController.rayCastingPlatform.ViewID) ;
            }

            return playerController.holdingItem != null;
        }
        [PunRPC]
        public void OnPickUpSync(int callerId, int playerControllerId, int platformId)
        {
            if (callerId == PhotonNetwork.LocalPlayer.ActorNumber) return;

            var playerController = PhotonView.Find(playerControllerId).GetComponent<PlayerController>();
            var platform = PhotonView.Find(platformId).GetComponent<Platform>();
            int id = platform.OnPickUp();

            if (id != -1)
            {
                playerController.holdingItem = PhotonView.Find(id).GetComponent<Item>();
                playerController.holdingItem.transform.parent = playerController.transform;
                playerController.holdingItem.transform.localPosition = Vector3.zero;
                playerController.holdingItem.transform.localRotation = Quaternion.identity;
                playerController.holdingItem.OnPickUp();
            }
        }
        public bool OnDrop()
        {
            if (playerController.holdingItem == null) return false;
            if (playerController.rayCastingPlatform == null) return false;
            if (playerController.rayCastingPlatform.AddOneItem(playerController.holdingItem.ViewID))
            {
                playerController.holdingItem.OnDrop(playerController.rayCastingPlatform.ViewID);
                playerController.holdingItem = null;
                photonView.RPC("OnDropSync", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, playerController.ViewID, playerController.rayCastingPlatform.ViewID);
                return true;
            }
            return false;
        }
        [PunRPC]
        public void OnDropSync(int callerId, int playerControllerId, int platformId)
        {
            if (callerId == PhotonNetwork.LocalPlayer.ActorNumber) return;
            var playerController = PhotonView.Find(playerControllerId).GetComponent<PlayerController>();
            var platform = PhotonView.Find(platformId).GetComponent<Platform>();
            if (platform.AddOneItem(playerController.holdingItem.ViewID))
            {
                playerController.holdingItem.OnDrop(platform.ViewID);
                playerController.holdingItem = null;
            }
        }
        public bool OnOperate()
        {
            if (playerController.rayCastingPlatform == null) return false;
            playerController.operatingPlatform = playerController.rayCastingPlatform;
            photonView.RPC("OnOperateSync", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, playerController.photonView.ViewID, playerController.rayCastingPlatform.photonView.ViewID);
            return true;
        }
        [PunRPC]
        public void OnOperateSync(int callerId, int playerControllerId, int platformId)
        {
            if (callerId == PhotonNetwork.LocalPlayer.ActorNumber) return;
            var playerController = PhotonView.Find(playerControllerId).GetComponent<PlayerController>();
            var platform = PhotonView.Find(platformId).GetComponent<Platform>();
            playerController.operatingPlatform = platform;
        }
        public void OnStopOperate()
        {
            playerController.operatingPlatform = null;
            photonView.RPC("OnStopOperateSync", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, playerController.ViewID);
        }
        [PunRPC]
        public void OnStopOperateSync(int callerId, int playerControllerId)
        {
            if (callerId == PhotonNetwork.LocalPlayer.ActorNumber) return;
            var playerController = PhotonView.Find(playerControllerId).GetComponent<PlayerController>();
            playerController.operatingPlatform = null;
        }
    }
}
