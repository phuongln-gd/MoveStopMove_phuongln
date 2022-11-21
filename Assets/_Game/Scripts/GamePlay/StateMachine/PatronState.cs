using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatronState : IState<Enemy>
{
    
    public void OnEnter(Enemy t)
    {
        Vector3 pos = t.RandomPositionInGround();
        t.SetDestination(pos);
        t.ChangeAnim(Constant.RUN_ANIM);
    }

    public void OnExecute(Enemy t)
    {
        if (!t.isDead)
        {
            if (t.IsDestination())
            {
                t.StopMoving();
                t.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Enemy t)
    {
    }
}
