using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private JoystickControl joystickControl;

    private float timer;
    private bool isMove;
    public string namekiller = "";
    public int rank = 100;
    
    public int killCount;
    public Enemy currentTarget;

    public override void OnInit()
    {
        base.OnInit();
        ChangeHat(0);
        ChangePant(0);
        ChangeShield(0);
        killCount = 0;
        currentTarget = null;
    }
    void Update()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (!isDead)
            {
                CheckBarrierInAttackArea();
                timer += Time.deltaTime;
                hasEnemyInAreaAttack = false;
                if (!isMove)
                {
                    CheckEnemyInAttackArea();
                    Character target = FindNearestEnemy();
                    if (hasEnemyInAreaAttack)
                    {
                        Attack(target);
                        timer = 0;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 nextPoint = JoystickControl.direct * moveSpeed * Time.deltaTime + transform.position;
                    transform.position = MoveGround(nextPoint);
                    skin.forward = JoystickControl.direct;
                    CheckEnemyInAttackArea();
                    Character target = FindNearestEnemy();
                    if (hasEnemyInAreaAttack)
                    {
                        ChangeCurrentTarget(target.GetComponent<Enemy>());
                    }
                    else
                    {
                        if (currentTarget != null)
                        {
                            currentTarget.targetCircle.SetEnable(false);
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (!hasEnemyInAreaAttack)
                    {
                        StopMove();
                    }
                }
            }
        }
    }
    
    public void ChangeCurrentTarget(Enemy enemy)
    {
        if(currentTarget!= null && currentTarget != enemy)
        {
            currentTarget.targetCircle.SetEnable(false);
        }
        currentTarget = enemy;
        currentTarget.targetCircle.SetEnable(true);
    } 


    public override void OnDespawn()
    {
        base.OnDespawn();
        GameManager.Ins.ChangeState(GameState.Lose);
        UIManager.Ins.OpenUI<Lose>();
        UIManager.Ins.CloseUI<GamePlay>();
    }

    public void StopMove()
    {
        isMove = false;
        JoystickControl.direct = Vector3.zero;
        ChangeAnim(Constant.IDLE_ANIM);
    }
    public Vector3 MoveGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if ( Physics.Raycast(nextPoint, Vector3.down,out hit, 5f, groundLayer))
        {
            ChangeAnim(Constant.RUN_ANIM);
            isMove = true;
            return hit.point + Vector3.up * 1.1f;
        }
        else
        {
            ChangeAnim(Constant.IDLE_ANIM);
            isMove = false;
            return tf.position;
        }
    }

    public override void OnHit()
    {
        base.OnHit();
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
            skin.forward = toRotation; // xoay mat ra huong tan cong
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
            wt.skin.transform.forward = toRotation - Vector3.up * -90f; // vu khi huong ra phia muc tieu
            wt.SetCharacter(this);
            wt.SetTargetPosition(pos);
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    public void ChangeHat(int i)
    {
        if (skinHat != null)
        {
            Destroy(skinHat);
        }
        skinHat = Instantiate(skinDataPlayer.ChangeHatPlayer(i), hatPos.transform);
    }

    public void ChangePant(int i)
    {
        skin_pant.GetComponent<SkinnedMeshRenderer>().material = skinDataPlayer.ChangePantPlayer(i);
    }

    public void ChangeShield(int i)
    {
        if (skinShield != null)
        {
            Destroy(skinShield);
        }
        skinShield = Instantiate(skinDataPlayer.ChangeShieldPlayer(i), shieldPos.transform);
    }

    public void CheckBarrierInAttackArea()
    {
        Collider[] colliders = Physics.OverlapSphere(tf.position, attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(Constant.BARRIER_TAG) )
            {
                colliders[i].GetComponent<Barrier>().ChangeMaterial(1);
            }
        }
    }
}
