using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Knife,Hammer,Boomerang
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponThrow> weaponThrowPrefabs;
    [SerializeField] List<GameObject> weaponHandSkins;

    public GameObject GetWeaponHand(WeaponType weaponType)
    {
        return weaponHandSkins[(int)weaponType];
    }

    public WeaponThrow GetWeaponThrow(WeaponType weaponType)
    {
        return weaponThrowPrefabs[(int)weaponType];
    }
}
