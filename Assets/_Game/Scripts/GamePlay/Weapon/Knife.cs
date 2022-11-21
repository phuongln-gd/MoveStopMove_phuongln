using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponThrow
{

    public override void OnInit()
    {
        base.OnInit();
        speed = 10;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Throw()
    {
        tf.position = Vector3.MoveTowards(tf.position, targetPos, speed * Time.deltaTime);
    }
}
