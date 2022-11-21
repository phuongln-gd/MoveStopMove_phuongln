using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : WeaponThrow
{
    public override void OnInit()
    {
        base.OnInit();
        speed = 8;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Throw()
    {
        tf.position = Vector3.MoveTowards(tf.position, targetPos, speed * Time.deltaTime);
        tf.RotateAround(tf.position,Vector3.up, 720 * Time.deltaTime);
    }

}
