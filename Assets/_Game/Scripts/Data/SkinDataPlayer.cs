using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SkinDataPlayer", menuName = "ScriptableObjects/SkinDataPlayer")]
public class SkinDataPlayer : ScriptableObject
{
    [SerializeField] List<GameObject> hatPrefabs;
    [SerializeField] List<Material> pantPrefabs;
    [SerializeField] List<GameObject> shieldPrefabs;

    public GameObject ChangeHatPlayer(int i)
    {
        return hatPrefabs[i];
    }

    public GameObject ChangeShieldPlayer(int i)
    {
        return shieldPrefabs[i];
    }

    public Material ChangePantPlayer(int i)
    {
        return pantPrefabs[i];
    }
}
