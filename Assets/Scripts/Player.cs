using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _lose;
    
    private Animator _animator;
    private float _health = 3;
    private bool _isDeath = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isDeath)
        {
            _deadPanel.SetActive(true);
        }
        else
        {
            _deadPanel.SetActive(false);
        }
        _animator.SetBool("isDeath", _isDeath);
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _health -= damage;
        }
        
        if (_health == 0)
        {
            Death();
        }
    }
    
    private void Death()
    {
        _isDeath = true;
        _audio.PlayOneShot(_lose);
        Invoke("Pause", 1f);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }
}
