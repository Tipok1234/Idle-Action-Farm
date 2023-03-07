using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Animation;

namespace Assets.Scripts.Model
{
    public class CornBag : MonoBehaviour
    {
        [SerializeField] private BagShakeAnimation _shakeAnimation;
        [SerializeField] private BoxCollider _boxCollider;

        public void InstantiateBag(Vector3 pos)
        {
            Instantiate(gameObject, pos, Quaternion.identity);
        }

        public void ShakeAnimation()
        {
            _shakeAnimation.ShakeBagAnimation();
        }

        public void CornHarvestedAnimation(CornBag cornBag)
        {
            _shakeAnimation.CollectCube(cornBag);
        }

        public void MoveCubeToStorage(Vector3 pos)
        {
            _shakeAnimation.MovevCubeToStorage(pos);
        }

        public void OffCollider()
        {
            _boxCollider.enabled = false;
        }
        public void OnCollider()
        {
            _boxCollider.enabled = true;
        }

    }
}
