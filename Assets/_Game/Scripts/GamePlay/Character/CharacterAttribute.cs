using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float goldBuff;

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float GoldBuff
    {
        get { return goldBuff; }
        set { goldBuff = value; }
    }
}
