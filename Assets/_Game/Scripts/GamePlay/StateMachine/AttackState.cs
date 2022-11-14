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
        if (t.timer > 5f)
        {
            t.CheckEnemyInAttackArea();
            if (t.hasEnemyInAreaAttack)
            {
                Character target = t.FindNearestEnemy();
                t.Attack(target);
            }
            else
            {
                t.StopMoving();
            }
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
