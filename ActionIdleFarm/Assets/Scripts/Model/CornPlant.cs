using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Animation;
using System;

namespace Assets.Scripts.Model
{
    public class CornPlant : MonoBehaviour
    {
        public event Action<CornPlant> DeadAction;
        public bool IsHarvested => _isHarvested;
        [SerializeField] private CornAnimation _cornAnimation;

        private bool _isHarvested = false;
        private float _growthTime = 10f;
        private float _currentGrowth = 0f;


        private void Start()
        {
            _cornAnimation.HarvestedAction += OnHarvested;
        }

        private void OnDestroy()
        {
            _cornAnimation.HarvestedAction -= OnHarvested;
        }
        //private void FixedUpdate()
        //{
        //    if(_isHarvested)
        //    {
        //        _currentGrowth += Time.deltaTime;

        //        if (_currentGrowth >= _growthTime)
        //        {
        //            _isHarvested = false;
        //            _currentGrowth = 0f;
        //        }
        //    }
        //}

        private void OnHarvested(bool harvested) 
        {
            _isHarvested = harvested;
        }

        public void GrowAnimation()
        {
            _cornAnimation.GrowAnimation();
        }

        public void HarvestedAnimation(Vector3 pos)
        {
            _cornAnimation.Harvested(pos);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                DeadAction?.Invoke(this);
            }
        }
    }
}
