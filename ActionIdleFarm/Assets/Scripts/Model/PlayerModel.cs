using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PlayerModel : MonoBehaviour
    {
        public event Action UnloadBagAction;
        public Transform CornBagPos => _cornBagPos;
        public int CapacityCornBag => _capacityCornBag;
        public int MyCorn => _myCorn;

        public CornBag CornBag => _cornBag;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private CornBag _cornBag;
        [SerializeField] private Transform _cornBagPos;
        [SerializeField] private Transform _wareHousePos;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private GameObject _pickaxe;

        [SerializeField] private int _capacityCornBag;

        private CornBag _cornBagg;
        private int _myCorn;

        private List<CornBag> _cornBags = new List<CornBag>();
        private void Start()
        {
            _cornBagg = Instantiate(_cornBag, _cornBagPos);
            _cornBagg.ShakeAnimation();
            _playerAnimator.SetBool("Idle", true);
        }

        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _rigidbody.velocity = direction * _moveSpeed;
        }
        private void Update()
        {
            Vector3 direction = new Vector3(_joystick.Horizontal * _moveSpeed, 0, _joystick.Vertical * _moveSpeed);

            if (direction.magnitude > 0)
            {
                _playerAnimator.SetBool("Idle", false);
                _playerAnimator.SetBool("Walk", true);

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
            }
            else
            {
                _playerAnimator.SetBool("Idle", true);
                _playerAnimator.SetBool("Walk", false);
            }
        }

        public void AddCorn(int corn)
        {
            _myCorn += corn;
        }

        public void PlayMowAnimation()
        {
            _playerAnimator.SetBool("Mow", true);
            _playerAnimator.SetBool("Idle", false);
        }

        public void PlayIdleAnimation()
        {
            _playerAnimator.SetBool("Idle", true);
            _playerAnimator.SetBool("Mow", false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "WareHouse")
            {
                Debug.LogError("WareHouse");
                _myCorn = 0;

                for (int i = 0; i < _cornBags.Count; i++)
                {
                    _cornBags[i].MoveCubeToStorage(_wareHousePos.transform.position);
                }
                //UnloadBagAction?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_myCorn == _capacityCornBag)
                return;

            if (other.gameObject.TryGetComponent(out CornBag cornBag))
            {
                AddCorn(1);
                _cornBags.Add(cornBag);
                cornBag.CornHarvestedAnimation(_cornBagg);
                cornBag.ShakeAnimation();
            }
        }

        //private void OnCollisionStay(Collision collision)
        //{
        //    if (collision.gameObject.tag == "Corn")
        //    {
        //        int index = 0;

        //        if (index <= 1)
        //        {
        //            index++;
        //            PlayMowAnimation();
        //            _pickaxe.gameObject.SetActive(true);
        //        }
        //    }
        //    else
        //    {
        //        //_playerAnimator.SetBool("Mow", false);
        //        _pickaxe.gameObject.SetActive(false);
        //    }
        //}
    }
}
