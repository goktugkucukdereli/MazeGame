using System;
using UnityEngine;

namespace MTEK.Scripts.GameObject
{
    public class DestroyOutOfMtekBase : MonoBehaviour
    {
        private void Awake()
        {
#if MTEKBASE
            DestroyImmediate(gameObject);
#endif
        }
    }
}