using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float Damage;

    [SerializeField] private float _lifeTime;

    private void Start()
    {
        Invoke("DestroyBullet", _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            col.GetComponent<IDamageble>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
