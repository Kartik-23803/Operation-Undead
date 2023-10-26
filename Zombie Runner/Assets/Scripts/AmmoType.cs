// public enum AmmoType
// {
//     Bullets,
//     Shells,
//     Rounds
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ammo Type", fileName = "New Ammo")]
public class AmmoType : ScriptableObject
{
    [SerializeField] string type = "Enter the type of ammo";
    
    public string GetAmmoType()
    {
        return type;
    }
}