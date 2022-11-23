using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NameData", menuName = "ScriptableObjects/NameData", order = 2)]
public class NameData : ScriptableObject
{
    [SerializeField] List<string> names;

    public string RandomName()
    {
        int i = Random.Range(0, names.Count);
        return names[i];
    }

}
