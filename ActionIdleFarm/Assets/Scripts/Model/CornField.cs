using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class CornField : MonoBehaviour
    {
        public event Action<CornPlant> CornHarvestedAction;

        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private CornPlant _cornPlant;
        [SerializeField] private int _countCornPlant;

        private List<CornPlant> _cornPlants = new List<CornPlant>();

        private void Start()
        {
            for (int i = 0; i < _countCornPlant; i++)
            {
                InstantiateCorn(CornPosition());
            }
        }

        private void OnDead(CornPlant cornPlant)
        {
            Debug.LogError("COUNT: " + _playerModel.MyCorn + _playerModel.CapacityCornBag);

            if (cornPlant.IsHarvested && _playerModel.MyCorn <= _playerModel.CapacityCornBag)
            {
                if (_cornPlants.Contains(cornPlant))
                {
                    CornHarvestedAction?.Invoke(cornPlant);
                    StartCoroutine(SpawnCorn(cornPlant.transform.position));
                    cornPlant.DeadAction -= OnDead;
                    _cornPlants.Remove(cornPlant);
                    Destroy(cornPlant.gameObject, 0.5f);
                }
            }
        }
        private IEnumerator SpawnCorn(Vector3 position)
        {
            yield return new WaitForSeconds(5f);

            InstantiateCorn(position);
        }

        private void InstantiateCorn(Vector3 pos)
        {
            var corn = Instantiate(_cornPlant, pos, Quaternion.identity);
            corn.transform.parent = transform;
            corn.GrowAnimation();
            corn.DeadAction += OnDead;
            _cornPlants.Add(corn);
        }

        private Vector3 CornPosition()
        {
            var cornPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(6f, 11f));
            return cornPosition;
        }
    }
}
