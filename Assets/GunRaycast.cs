using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float range = 300f;
    public int damage = 1;

    public bool isAutomatic = false;   // 🔥 NEW
    public float fireRate = 10f;       // bullets per second

    private float nextFireTime = 0f;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isAutomatic)
        {
            // HOLD to fire
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            // CLICK to fire
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
       
        if (playerCamera == null || firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("GunRaycast missing setup on " + gameObject.name);
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            playerCamera.transform.rotation
        );

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