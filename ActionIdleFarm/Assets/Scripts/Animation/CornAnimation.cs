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

            _sequence.Append(transform.DOScale(new Vector3(0.6f,0.6f,0.6f), _timeGrow));

            _sequence.AppendCallback(() => { CallBack(); });
        }

        private void CallBack()
        {
            HarvestedAction?.Invoke(true);
        }


        private void OnDestroy()
        {
            _sequence?.Kill();
            _harvestedSequence?.Kill();
        }
    }
}
