using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    public Transform ground;
    private Enemy target;
    public Enemy Target => target;
    [HideInInspector] public Vector3 destionation;
    //Vector3.Distance(Vector3.right * tf.position.x + Vector3.forward * tf.position.z, destionation) < 0.1f;
    public bool IsDestination()
    {
        if (Mathf.Abs(destionation.x- tf.position.x) < 0.1f && Mathf.Abs(destionation.z- tf.position.z)< 0.1f
            && Mathf.Abs(destionation.y- tf.position.y)<0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    public void SetDestination(Vector3 position)
    {
        destionation = position;
        agent.SetDestination(position);
    }

    private IState<Enemy> currentState;

  
    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeWeapon((WeaponType)Random.Range(0, 2));
        ChangeState(new IdleState());
        ground = LevelManager.Ins.currentLevel.ground;
    }

    public void ChangeState(IState<Enemy> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnDespawn()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        base.OnDespawn();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


    /*
     - random position
     */
  
    public Vector3 RandomPositionInGround()
    {
        float posX = Random.Range(-ground.localPosition.x / 2, ground.localPosition.x / 2 + 1);
        float posZ = Random.Range(-ground.localPosition.y / 2, ground.localPosition.y / 2 + 1);
        return new Vector3(posX,0,posZ);
    }

    public void StopMoving()
    {
        agent.SetDestination(tf.position);
        ChangeAnim(Constant.IDLE_ANIM);
    }
    internal void setTarget(Enemy target)
    {
        this.target = target;
    }
}
