using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.UI;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CornField _cornField;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private CornBag _cornBag;
        [SerializeField] private GameWindow _gameWindow;
        [SerializeField] private Transform _wareHouse;

        private void Start()
        {
            _cornField.CornHarvestedAction += OnCornHarvested;
            _playerModel.UnloadBagAction += OnUnloadBag;
            _gameWindow.ShowScoreCorn(_playerModel.MyCorn,_playerModel.CapacityCornBag);
        }

        private void OnDestroy()
        {
            _cornField.CornHarvestedAction -= OnCornHarvested;
            _playerModel.UnloadBagAction -= OnUnloadBag;
        }

        private void OnCornHarvested(CornPlant corn)
        {
            _gameWindow.ShowScoreCorn(_playerModel.MyCorn, _playerModel.CapacityCornBag);
            _playerModel.PlayIdleAnimation();
            _cornBag.InstantiateBag(corn.transform.position + new Vector3(0f,0.1f,0f));
        }

        private void OnUnloadBag()
        {
            _gameWindow.ShowScoreCorn(_playerModel.MyCorn, _playerModel.CapacityCornBag);

        }
    }
}
