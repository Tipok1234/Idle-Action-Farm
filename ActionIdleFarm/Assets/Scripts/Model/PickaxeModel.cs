using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PickaxeModel : MonoBehaviour
    {
        public event Action ExitMowAction;
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Corn")
            {
                Debug.LogError("EXIT;");

                ExitMowAction?.Invoke();
                SetActiveObject(false);
            }
        }

        public void SetActiveObject(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
