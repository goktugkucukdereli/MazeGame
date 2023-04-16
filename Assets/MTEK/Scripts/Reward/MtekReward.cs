using System;
using UnityEngine;
#if MTEKBASE
using KidzJungle;
#endif

namespace MTEK.Scripts.Reward
{
    public class MtekReward : MonoBehaviour
    {
        public static Action<int, RewardType> Gain;
        public static MtekReward instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Gain += GainReward;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            
        }

        public void GainStar(int amount, RewardType rewardType = RewardType.Star)
        {
            Gain?.Invoke(amount, rewardType);
        }

        public void GainDiamond(int amount, RewardType rewardType = RewardType.Diamond)
        {
            Gain?.Invoke(amount, rewardType);
        }

        private void GainReward(int amount, RewardType rewardType)
        {
            Debug.Log(amount + " adet " + ((rewardType.Equals(RewardType.Star)) ? " Yıldız" : " Kristal") +
                      " kazanıldı.");
            
#if MTEKBASE            
                switch (rewardType)
                {
                    case RewardType.Star:
                        GameState.GainStar?.Invoke(amount);
                        break;
                    case RewardType.Diamond:
                        GameState.GainDiamond?.Invoke(amount);
                        break;
                }
#endif
        }
    }

    public enum RewardType
    {
        Star,
        Diamond
    }
}