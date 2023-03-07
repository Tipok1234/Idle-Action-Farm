using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Animation
{
    public class BagShakeAnimation : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private Transform _backpackTransform;
        [SerializeField] private float _duration;
        [SerializeField] private float _angle;

        private Sequence _sequence;

        public void ShakeBagAnimation()
        {

            var axis = Vector3.forward;

            _sequence = DOTween.Sequence();

            _sequence.Append(_backpackTransform.DORotate(axis * _angle, _duration / 2f));
            _sequence.Append(_backpackTransform.DORotate(axis * -_angle, _duration));
            _sequence.Append(_backpackTransform.DORotate(Vector3.zero, _duration / 2f));

            _sequence.SetLoops(-1);
        }

        public void CollectCube(CornBag cornBag)
        {
            if (cornBag == null)
                return;

            _boxCollider.enabled = false;

            Vector3 backpackPosition = cornBag.transform.position;
            gameObject.transform.DOMove(backpackPosition, _duration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _boxCollider.enabled = true;
                    gameObject.transform.SetParent(cornBag.transform);

                });
        }

        public void MovevCubeToStorage(Vector3 pos)
        {
            gameObject.transform.DOMove(pos, _duration).SetEase(Ease.InOutQuad);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
