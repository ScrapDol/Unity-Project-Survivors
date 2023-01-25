using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _offset;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _startRateOfFire;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _shoot;
    
    private Movement _playerMovement;
    private float _rotationZ;
    private float _rateOfFire = 0;

    private void Awake()
    {
        _playerMovement = _playerTransform.GetComponent<Movement>();
    }

    private void Update()
    {
        transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _offset);
        
        if (_playerMovement.CurrentControlType == ControlType.PC)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        
        else if (_playerMovement.CurrentControlType == ControlType.mobile && Mathf.Abs(_joystick.Vertical) > 0.1f || Mathf.Abs(_joystick.Vertical) > 0.1f)
        {
            _rotationZ = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
        }

        if (_rateOfFire <= 0)
        {
            if (_playerMovement.CurrentControlType == ControlType.PC && Input.GetMouseButton(0))
            {
                Shoot();
            }
            else if (_playerMovement.CurrentControlType == ControlType.mobile && _joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                Shoot();
            }

            _rateOfFire = _startRateOfFire;
        }
        else
        {
            _rateOfFire -= Time.deltaTime;
        }
        
        

        transform.rotation = Quaternion.Euler(0f,0f,_rotationZ + _offset);
    }

    private void Shoot()
    {
        _audio.PlayOneShot(_shoot);
        GameObject newBullet = Instantiate(_bullet, shotPoint.position, shotPoint.rotation);
        newBullet.GetComponent<Bullet>().Damage = _bulletDamage;
        newBullet.GetComponent<Bullet>().Speed = _bulletSpeed;
    }

}
