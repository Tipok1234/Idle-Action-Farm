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
        [SerializeField] private GameWindow _gameWindow;

        private void Start()
        {
            _cornField.CornHarvestedAction += OnCornHarvested;
            _gameWindow.ShowScoreCorn(_playerModel.MyCorn,_playerModel.CapacityCornBag);
        }

        private void OnCornHarvested(CornPlant corn)
        {
            corn.HarvestedAnimation(_playerModel.CornBag.position);
            _playerModel.AddCorn(1);
            _gameWindow.ShowScoreCorn(_playerModel.MyCorn, _playerModel.CapacityCornBag);
        }
    }
}
