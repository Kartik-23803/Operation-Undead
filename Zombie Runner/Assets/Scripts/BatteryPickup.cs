using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 50f;
    [SerializeField] float addIntensity = 5f;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<FlashLight>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<FlashLight>().RestoreLightIntensity(addIntensity);
            Destroy(gameObject);
        }
    }
}