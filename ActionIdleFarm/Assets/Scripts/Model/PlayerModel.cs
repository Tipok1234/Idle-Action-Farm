using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PlayerModel : MonoBehaviour
    {
        public Transform CornBag => _cornBag;
        public int CapacityCornBag => _capacityCornBag;
        public int MyCorn => _myCorn;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _cornBag;
        [SerializeField] private float _moveSpeed;

        [SerializeField] private int _capacityCornBag;

        private int _myCorn;

        private bool _isWalk;  
        private bool _isIdle;
        private bool _isMow;

        private void Start()
        {
            _playerAnimator.SetBool("Idle",true);
        }
        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

            if(_joystick.Horizontal !=0 || _joystick.Vertical != 0)
            {
                _isWalk = true;
                _isIdle = false;
                _playerAnimator.SetBool("Idle",false);
                _playerAnimator.SetBool("Walk",true);
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
            else
            {
                _isWalk = false;
                _isIdle = true;
                _playerAnimator.SetBool("Idle", true);
                _playerAnimator.SetBool("Walk", false);
            }
        }

        public void AddCorn(int corn)
        {
            _myCorn += corn;

            if (_myCorn == _capacityCornBag)
                return;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "WareHouse")
            {
                _myCorn = 0;
            }
        }
    }
}
