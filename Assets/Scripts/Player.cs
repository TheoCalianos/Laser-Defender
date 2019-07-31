using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [Header("Player")]
    [SerializeField] float speed = 100f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = .75f;
    [SerializeField] AudioClip deathSFX;

    [Header("Projectile")]
    [SerializeField] [Range(0, 1)] float fireSFXVolume = .2f;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.05f;

    Coroutine FireCoroutine;
    // Start is called before the first frame update
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();


    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
        GameObject laser = Instantiate(
               projectile,
               transform.position,
               Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);
        yield return new WaitForSeconds(projectileFiringPeriod);

        }
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireCoroutine = StartCoroutine(FireContinuously());

        } 
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FireCoroutine);
        }
    }

    private void Move()
    {
        //var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");


        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector3(newXPos, newYPos, transform.position.z);


    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
    public int GetHealth()
    {
        return health;
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

    }
}
