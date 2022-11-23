using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType
{
    Black,Green,Orange,Red,White,Yellow
}


[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    [SerializeField] List<Material> pantSkins;
    [SerializeField] List<Material> colorType;
    [SerializeField] List<GameObject> hatPrefabs;
    public Material RandomPants()
    {
        int i = Random.Range(0,pantSkins.Count);
        return pantSkins[i];
    }

    public Material GetColor(ColorType color)
    {
        return colorType[(int)color];
    }

    public GameObject RandomHat()
    {
        int i = Random.Range(0, hatPrefabs.Count);
        return hatPrefabs[i];
    }
}
