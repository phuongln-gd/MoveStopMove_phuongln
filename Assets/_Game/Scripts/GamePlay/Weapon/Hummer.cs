using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : WeaponThrow
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Throw()
    {
        rb.velocity = targetDir * speed / 5;
        tf.RotateAround(tf.position,Vector3.up, 720 * Time.deltaTime);
    }

    public override void SetTargetPosition(Vector3 pos)
    {
        base.SetTargetPosition(pos);
    }

}
