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
    private void Awake()
    {
        ChangeWeapon(WeaponType.Knife);
    }
    void Update()
    {
        if (!isDead)
        {
            timer += Time.deltaTime;
            hasEnemyInAreaAttack = false;
            if (!isMove)
            {
                CheckEnemyInAttackArea();
                if (hasEnemyInAreaAttack)
                {
                    Character target = FindNearestEnemy();
                    Attack(target);
                    timer = 0;
                }
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 nextPoint = JoystickControl.direct * moveSpeed * Time.deltaTime + transform.position;
                transform.position = MoveGround(nextPoint);
                skin.forward = JoystickControl.direct;
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
    
    public override void OnDespawn()
    {
        base.OnDespawn();
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
            skin.forward = toRotation; // xoay mat ra huong tan cong
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
}
