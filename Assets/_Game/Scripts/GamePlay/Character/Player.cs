using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private JoystickControl joystickControl;

    private float timer;
    private bool justAttack = false;
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
                    StartCoroutine(Attack(target));
                    justAttack = true;
                    timer = 0;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if ((!justAttack) || (justAttack && timer > 0.3f))
                {
                    Vector3 nextPoint = JoystickControl.direct * moveSpeed * Time.deltaTime + transform.position;
                    transform.position = MoveGround(nextPoint);
                    skin.forward = JoystickControl.direct;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMove = false;
                if (!hasEnemyInAreaAttack)
                {
                    ChangeAnim(Constant.IDLE_ANIM);
                }
            }
        }
       
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
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
}
