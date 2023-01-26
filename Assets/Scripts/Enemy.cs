using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private AudioClip _deadAudioClip;
    [SerializeField] private float _speed;
    public AudioSource Audio;
    public Transform TargetDestination;
    public Action OnDead;
    

    private Rigidbody2D _rigidbody;
    private bool _isDead = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            Vector3 direction = (TargetDestination.position - transform.position).normalized;
            _rigidbody.velocity = direction * _speed * Time.fixedDeltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _health -= damage;
        }

        if (_health <= 0)
        {
            Audio.PlayOneShot(_deadAudioClip);
            Death();
        }
    }

    private void Death()
    {
        OnDead.Invoke();
        _isDead = true;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            col.gameObject.GetComponent<IDamageble>().TakeDamage(_damage);
        }
    }
}
