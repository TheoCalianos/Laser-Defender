﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    [SerializeField] float shoutCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Effects")]
    [SerializeField] float ExplosionLifeSpan = .2f;
    [SerializeField] [Range(0,1)] float deathSFXVolume = .75f;
    [SerializeField] [Range(0,1)] float fireSFXVolume = .75f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject deathVFX;
    // Start is called before the first frame update
    void Start()
    {
        shoutCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }
    private void CountDownAndShoot()
    {
        shoutCounter -= Time.deltaTime;
        if (shoutCounter <= 0f)
        {
            Fire();
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);
            shoutCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

    }
    private void Fire()
    {
        GameObject laser = Instantiate(
           projectile,
           transform.position,
           Quaternion.identity) as GameObject;
           laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }
private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, ExplosionLifeSpan);        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
