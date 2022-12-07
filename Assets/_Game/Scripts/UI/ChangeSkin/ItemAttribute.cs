using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffAttribute
{
    Range,
    MoveSpeed,
    Gold,
    AttackSpeed
}
public class ItemAttribute : MonoBehaviour
{
    public string description;
    public BuffAttribute buff;
    public int increase;

    public void SetStringDescription()
    {
        description = increase +"% "+ buff.ToString();
    }
}
