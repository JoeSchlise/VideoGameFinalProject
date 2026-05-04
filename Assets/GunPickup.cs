using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pickup touched by: " + other.name);

        if (!other.CompareTag("Player"))
        {
            return;
        }

        WeaponManager wm = other.GetComponentInParent<WeaponManager>();

        if (wm != null)
        {
            wm.SwitchToRifle();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Player touched pickup, but no WeaponManager found.");
        }
    }
}