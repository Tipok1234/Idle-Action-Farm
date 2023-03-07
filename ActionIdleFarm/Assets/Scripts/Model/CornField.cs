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
        [SerializeField] private PickaxeModel _pickaxeModel;

        private List<CornPlant> _cornPlants = new List<CornPlant>();
        private CornPlant _corn;

        private void Start()
        {
            _pickaxeModel.ExitMowAction += OnExitMow;

            for (int i = 0; i < _countCornPlant; i++)
            {
                InstantiateCorn(CornPosition());
            }
        }

        private void OnDestroy()
        {
            _pickaxeModel.ExitMowAction += OnExitMow;
        }

        private void OnExitMow()
        {
            _corn.DeadAction -= OnDead;
            CornHarvestedAction?.Invoke(_corn);
            StartCoroutine(SpawnCorn(_corn.transform.position));
            _cornPlants.Remove(_corn);
            Destroy(_corn.gameObject);
        }


        private void OnDead(CornPlant cornPlant)
        {
            _corn = cornPlant;

            if(_corn.IsHarvested && _playerModel.MyCorn <= _playerModel.CapacityCornBag)
            {
                if(_cornPlants.Contains(_corn))
                {
                    _pickaxeModel.SetActiveObject(true);
                    _playerModel.PlayMowAnimation();
                }
            }
        }
        private IEnumerator SpawnCorn(Vector3 position)
        {
            yield return new WaitForSeconds(10f);

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
