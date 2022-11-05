using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    void Update()
    {
        
    }

    public override void OnDespawn()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        base.OnDespawn();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
