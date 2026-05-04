using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpin : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.25f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered pickup trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Picked up better gun!");
            Destroy(gameObject);
        }
    }
}
