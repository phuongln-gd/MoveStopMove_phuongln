using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : WeaponThrow
{
    private void Update()
    {
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void Throw()
    {
        base.Throw();

    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
