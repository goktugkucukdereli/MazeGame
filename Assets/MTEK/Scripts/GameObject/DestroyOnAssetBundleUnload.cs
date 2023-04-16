using System;
using System.Collections;
using UnityEngine;
#if MTEKBASE
using KidzJungle;
#endif

namespace MTEK.Scripts.GameObject
{
    public class DestroyOnAssetBundleUnload : MonoBehaviour
    {
#if MTEKBASE
        private AbstractGameManager _mtekGameManager;
        private AssetBundleDefinition _assetBundleDefinition;

        private void Awake()
        {
            _mtekGameManager = FindObjectOfType<AbstractGameManager>();
            _assetBundleDefinition = _mtekGameManager.nextBundle;
        }

        private void LateUpdate()
        {
            StartCoroutine(Check());
        }

        private IEnumerator Check()
        {
            if (_assetBundleDefinition.SceneName != _mtekGameManager.nextBundle.SceneName)
            {
                Debug.Log("Another asset bundle loaded, I Destroy myself");
                DestroyImmediate(gameObject);
            }

            yield return null;

        }

        private void OnDestroy()
        {
            Debug.Log(this.name + " Destroyed!");
        }
#endif
    }
}