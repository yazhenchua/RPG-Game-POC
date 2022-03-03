using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 1f;
    [SerializeField] float firingRate = 0.5f;
    public Vector3 offsetPosition = new Vector3(1, 0, 0);
    //[SerializeField] ParticleSystem explosionEffect;

    public bool isFiring;

    Coroutine firingCoroutine;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position + offsetPosition, Quaternion.identity);

            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.velocity = transform.right * projectileSpeed * -1;
            }
            //ParticleSystem particleSystem = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //Destroy(particleSystem, particleSystem.main.duration);
            Destroy(instance, projectileLifetime);

            yield return new WaitForSeconds(firingRate);

        }
    }
}
