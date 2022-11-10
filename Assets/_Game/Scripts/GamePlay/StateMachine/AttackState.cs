using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy t)
    {
        timer = 0;
        randomTime = Random.Range(0.2f, 1f);
        if (t.hasEnemyInAreaAttack)
        {
            Character target = t.FindNearestEnemy();
            t.StartCoroutine(t.Attack(target));
        }
    }

    public void OnExecute(Enemy t)
    {
        if (!t.isDead)
        {
            timer += Time.deltaTime;
            if(timer >= randomTime)
            {
                t.ChangeState(new PatronState());
            }
        }
    }

    public void OnExit(Enemy t)
    {
    }
}
