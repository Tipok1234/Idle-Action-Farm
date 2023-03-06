using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI
{
    public class GameWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _myCornText;

        public void ShowScoreCorn(int myCorn, int capacityCorn)
        {
            _myCornText.text = "Corn: " + myCorn + "/" + capacityCorn;
        }
    }
}