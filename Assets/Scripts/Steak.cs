using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace MyFirstARGame
{
    public class Steak : Item
    {
        public float progress = 0f;
        public float cookingSpeed = 0.1f;
        public GameObject rawSteak;
        public GameObject welldoneSteak;
        private void Start()
        {
            rawSteak.SetActive(true);
            welldoneSteak.SetActive(false);
        }

        public override void UpdateByPlatform(Platform platform)
        {
            if (progress < 1f)
            {
                if (platform.Type == PlatformType.Hob)
                {
                    Debug.Log($"Cooking Steak... {progress * 100f}%.");
                    progress += cookingSpeed * Time.deltaTime;
                    if (progress > 1f)
                    {
                        Debug.Log("Steak Done.");
                        rawSteak.SetActive(false);
                        welldoneSteak.SetActive(true);
                    }
                }
            }
        }

    }
}
