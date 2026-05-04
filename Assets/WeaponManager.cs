using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject pistol;
    public GameObject rifle;

    void Start()
    {
        pistol.SetActive(true);
        rifle.SetActive(false);
    }

    public void SwitchToRifle()
    {
        Debug.Log("SWITCH CALLED");
        pistol.SetActive(false);
        rifle.SetActive(true);

        Debug.Log("Switched to rifle!");
    }
}