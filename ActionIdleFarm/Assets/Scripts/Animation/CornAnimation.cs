using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Assets.Scripts.Animation
{
    public class CornAnimation : MonoBehaviour
    {
        public event Action<bool> HarvestedAction;
        [SerializeField] private float _timeGrow;

        private Sequence _sequence;
        private Sequence _harvestedSequence;
        public void GrowAnimation()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOScale(Vector3.one, _timeGrow));

            _sequence.AppendCallback(() => { CallBack(); });
        }

        private void CallBack()
        {
            HarvestedAction?.Invoke(true);
        }
        public void Harvested(Vector3 pos)
        {
            _harvestedSequence = DOTween.Sequence();

            _harvestedSequence.Append(transform.DOMoveY(4f, 0.25f));
            _harvestedSequence.Join(transform.DOScale(0.2f, 0.2f));
            _harvestedSequence.Append(transform.DOMove(pos, 0.25f));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
            _harvestedSequence?.Kill();
        }
    }
}
