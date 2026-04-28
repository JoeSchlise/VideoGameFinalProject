using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float range = 300f;
    public int damage = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Visual bullet only
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, playerCamera.transform.rotation);
        bullet.transform.Rotate(0, 90, 0);
        Destroy(bullet, 1f);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = playerCamera.transform.forward * bulletSpeed;
        }

        RaycastHit hit;
        Vector3 rayStart = playerCamera.transform.position + playerCamera.transform.forward * 1f;

        Debug.DrawRay(rayStart, playerCamera.transform.forward * range, Color.red, 2f);

        if (Physics.Raycast(rayStart, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Ray hit: " + hit.transform.name);

            EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}