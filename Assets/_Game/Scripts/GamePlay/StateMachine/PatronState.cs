using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /* Neu tim thay muc tieu thi chuyen sang attack state
             neu khong tim duoc tiep tuc di chuyen den dich
             +/ neu den dich --> chuyen sang state idle
         */
        if (!t.isDead)
        {
            t.CheckEnemyInAttackArea();
            if (t.hasEnemyInAreaAttack)
            {
                t.StopMoving();
                t.ChangeState(new AttackState());
            }
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
