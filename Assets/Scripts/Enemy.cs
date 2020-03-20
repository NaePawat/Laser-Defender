using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stat")]
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 0.5f;

    [Header("EnemyCompo")]
    [SerializeField] GameObject enemyLaserPrefabs;
    [SerializeField] float projectileSpeed = 0.5f;
    [SerializeField] GameObject DeathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume=0.5f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.25f;
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter = shotCounter - Time.deltaTime;
        if(shotCounter<=0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaserPrefabs,transform.position,Quaternion.identity) as GameObject; 
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health = health - damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            GameObject VFX =Instantiate(DeathVFX, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(VFX, durationOfExplosion);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position,deathSFXVolume);
        }
    }
}
