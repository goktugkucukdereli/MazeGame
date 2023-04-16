using System;
using MTEK.Scripts.Reward;
using UnityEngine;
using UnityEngine.Serialization;

namespace MTEK.Scripts
{
    [RequireComponent(typeof(Audio.MtekAudio))]
    public class MtekManager : MonoBehaviour
    {
        public static MtekManager instance;
        public Audio.MtekAudio mtekAudio;
        public Reward.MtekReward mtekReward;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                mtekAudio = GetComponent<Audio.MtekAudio>();
                mtekReward = GetComponent<MtekReward>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            
        }
        private void Reset()
        {
            int numMtekAudioComponent = GetComponents<Audio.MtekAudio>().Length;
            if(numMtekAudioComponent.Equals(0))
                gameObject.AddComponent<Audio.MtekAudio>();
            
            int numMtekRewardComponent = GetComponents<MtekReward>().Length;
            if(numMtekRewardComponent.Equals(0))
                gameObject.AddComponent<MtekReward>();
        }
    }
}