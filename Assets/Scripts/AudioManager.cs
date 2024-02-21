using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        go.tag = "GameController";
                        _instance = go.AddComponent<AudioManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
        public AudioSource wasingAudioClip;
        public AudioSource cookingAudioClip;
        // Start is called before the first frame update
        void Start()
        {
            wasingAudioClip.volume = 0f;
            cookingAudioClip.volume = 0f;
            wasingAudioClip.Play();
            cookingAudioClip.Play();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
