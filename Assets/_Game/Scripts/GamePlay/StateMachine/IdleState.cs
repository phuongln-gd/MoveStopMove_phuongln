using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constant.IDLE_ANIM);
        timer = 0;
        randomTime = Random.Range(1f,2f);
    }

    public void OnExecute(Enemy t)
    {
        if (!t.isDead)
        {
            timer += Time.deltaTime;
            if (timer >= randomTime)
            {
                t.ChangeState(new PatronState());
            }
        }
    }

    public void OnExit(Enemy t)
    {
    }
}
