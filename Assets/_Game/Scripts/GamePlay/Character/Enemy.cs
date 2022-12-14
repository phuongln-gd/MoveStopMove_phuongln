using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    private Enemy target;
    public Enemy Target => target;
    private Vector3 destionation;
    //Vector3.Distance(Vector3.right * tf.position.x + Vector3.forward * tf.position.z, destionation) < 0.1f;

    private IState<Enemy> currentState;
    public float timer;
    public TargetCircleEnemy targetCircle;
    
    void Update()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            timer += Time.deltaTime;
            if (currentState != null && !isDead)
            {
                currentState.OnExecute(this);
            }
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeWeapon((WeaponType)Random.Range(0, 3));
        ChangeSkinPantRandom();
        ChangeSkinColor((ColorType)(Random.Range(0, 6)));
        RandomName();
        GetRandomHat();

        ChangeState(new IdleState());
        timer = 0;

        targetCircle.SetEnable(false);
    }

    public bool IsDestination()
    {
        if (Vector3.Distance(tf.position.x * Vector3.right + tf.position.z * Vector3.forward, destionation) < 2f)
        {
            return true;
        }
        return false;
    }
    public void SetDestination(Vector3 position)
    {
        destionation = position;
        destionation.y = 0;
        agent.enabled = true;
        agent.SetDestination(position);
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
        SimplePool.Despawn(this);
    }

    public Vector3 RandomPositionInGround()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 15f + tf.position;
        NavMeshHit hit;
        Vector3 finalPosition = tf.position;
        if (NavMesh.SamplePosition(randomDirection, out hit, 15f, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
            finalPosition.y = 0;
            return finalPosition;
        }
        else
        {
            return RandomPositionInGround();
        }
    }

    public void StopMoving()
    {
        agent.enabled = false;
        ChangeAnim(Constant.IDLE_ANIM);
    }
    internal void setTarget(Enemy target)
    {
        this.target = target;
    }

    public override void OnHit()
    {
        base.OnHit();
        agent.enabled = false;
    }

    public override void Attack(Character target)
    {
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
            tf.forward = toRotation; // xoay mat ra huong tan cong
            yield return new WaitForSeconds(0.5f); // doi 0.5s
            if (currentWeapon == WeaponType.Knife)
            {
                Knife newBullet = SimplePool.Spawn<Knife>(PoolType.KnifeBulllet, attackPoint.position, Quaternion.identity);
                wt = newBullet;
                wt.OnInit();
            }
            else if (currentWeapon == WeaponType.Hammer)
            {
                Hummer newBullet = SimplePool.Spawn<Hummer>(PoolType.HammerBullet, attackPoint.position, Quaternion.identity);
                wt = newBullet;
                wt.OnInit();
            }
            else if (currentWeapon == WeaponType.Candy)
            {
                Candy newBullet = SimplePool.Spawn<Candy>(PoolType.CandyBullet, attackPoint.position, Quaternion.identity);
                wt = newBullet;
                wt.OnInit();
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
