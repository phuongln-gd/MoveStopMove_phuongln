using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponThrow
{
    private void Awake()
    {
        speed = 10;
    }
    private void Update()
    {
        if (Vector3.Distance(targetPos,tf.position) > 0.1f)
        {
            tf.position = Vector3.MoveTowards(tf.position, targetPos, speed * Time.deltaTime);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.ENEMY_TAG))
        {
            OnDestroy();
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnDespawn();
            }
        }
    }
}
