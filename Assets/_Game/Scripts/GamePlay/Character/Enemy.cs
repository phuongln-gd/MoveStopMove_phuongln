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
    public float timer;
  
    void Update()
    {
        timer += Time.deltaTime;
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
        timer = 0;
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

    public override void OnHit()
    {
        base.OnHit();
        agent.SetDestination(tf.position);
    }

    public override void Attack(Character target)
    {
        base.Attack(target);
        StartCoroutine(DelayAttack(target));
    }

    public IEnumerator DelayAttack(Character target)
    {
        if (!isAttacking && !target.isDead)
        {
            isAttacking = true;
            ChangeAnim(Constant.ATTACK_ANIM);
            wh.ChangeStateActive(false); // tat vu khi tren tay
            Vector3 pos = target.weakPoint.position;
            Vector3 toRotation = pos - tf.position;
            transform.forward = toRotation; // xoay mat ra huong tan cong
            yield return new WaitForSeconds(0.5f); // doi 0.5s
            GameObject newBullet = Instantiate(weaponThrow, attackPoint.position, Quaternion.identity);
            if (currentWeapon == WeaponType.Knife)
            {
                wt = newBullet.GetComponent<Knife>();
            }
            else if (currentWeapon == WeaponType.Hammer)
            {
                wt = newBullet.GetComponent<Hummer>();
            }
            wt.skin.transform.forward = toRotation - Vector3.up * -90f; // vu khi huong ra phia muc tieu
            wt.SetCharacter(this);
            wt.SetTargetPosition(pos);
            Invoke(nameof(ResetAttack), 3f);
        }
    }

    public override void ResetAttack()
    {
        base.ResetAttack();

    }
}
