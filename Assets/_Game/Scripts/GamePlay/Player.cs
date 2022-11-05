using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private JoystickControl joystickControl;
    [SerializeField] private Transform skin;
    [SerializeField] private GameObject attackRange;

    private bool isAttack;
    private bool isMove;
    private bool hasEnemyInAreaAttack;
    private List<Enemy> listEnemyInAreaAttack = new List<Enemy>();
    private WeaponHand wh;
    private void Awake()
    {
        OnInit();
        // vu khi default : knife
        weaponType = WeaponType.Knife;
        Instantiate(weaponData.GetWeaponHand(weaponType),weaponHand.transform);
        weaponThrowPrefab = weaponData.GetWeaponThrow(weaponType).gameObject;
    }

    
    void Update()
    {
        if (!isMove)
        {
            CheckEnemyInAttackArea();
            if (hasEnemyInAreaAttack)
            {
                Enemy target = FindNearestEnemy();
                StartCoroutine(Attack(target));
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPoint = JoystickControl.direct * moveSpeed * Time.fixedDeltaTime + transform.position;
            transform.position = CheckGround(nextPoint);
            skin.forward = JoystickControl.direct;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
            CheckEnemyInAttackArea();
            if (hasEnemyInAreaAttack && !isAttack)
            {
                Enemy target = FindNearestEnemy();
                StartCoroutine(Attack(target));
            }
            else
            {
                ChangeAnim(Constant.IDLE_ANIM);
            }
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        level_in_game = 0;
        ChangeAnim(Constant.IDLE_ANIM);
        wh = weaponHand.GetComponent<WeaponHand>();
    }

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if ( Physics.Raycast(nextPoint, Vector3.down,out hit, 5f, groundLayer))
        {
            ChangeAnim(Constant.RUN_ANIM);
            isMove = true;
            return hit.point + Vector3.up * 1.1f;
        }
        return tf.position;
    }

    private void CheckEnemyInAttackArea()
    {
        listEnemyInAreaAttack.Clear();
        hasEnemyInAreaAttack = false;
        Collider[] colliders = Physics.OverlapSphere(tf.position, 8f);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].CompareTag(Constant.ENEMY_TAG))
            {
                hasEnemyInAreaAttack = true;
                if (colliders[i].TryGetComponent<Enemy>(out Enemy enemy))
                {
                    listEnemyInAreaAttack.Add(enemy);
                }
            }
        }
    }
    IEnumerator Attack(Enemy target)
    {
        if (!isAttack)
        {
            Vector3 pos = target.weakPoint.position;
            Vector3 toRotation = pos - tf.position;
            skin.forward = toRotation;
            isAttack = true;
            wh.ChangeStateActive(false);
            ChangeAnim(Constant.ATTACK_ANIM);
            yield return new WaitForSeconds(0.3f);
            GameObject weaponThrow = Instantiate(weaponThrowPrefab,
                attackPoint.position, Quaternion.identity);
            weaponThrow.GetComponentInParent<WeaponThrow>().skin.transform.forward = toRotation - new Vector3(0, -90, 0);
            if (weaponType == WeaponType.Knife)
            {
                weaponThrow.GetComponent<Knife>().SetTargetPosition(pos);
            }
            Invoke(nameof(ResetAttack), 3f);
        }
    }

    public void ResetAttack()
    {
        wh.ChangeStateActive(true);
        isAttack = false;
    }

    public Enemy FindNearestEnemy()
    {
        float minDistance = Vector3.Distance(tf.position, listEnemyInAreaAttack[0].gameObject.transform.position);
        Enemy rsEnemy = listEnemyInAreaAttack[0];
        for (int i = 1; i < listEnemyInAreaAttack.Count; i++)
        {
            if (Vector3.Distance(tf.position, listEnemyInAreaAttack[i].gameObject.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(tf.position, listEnemyInAreaAttack[i].gameObject.transform.position);
                rsEnemy = listEnemyInAreaAttack[i];
            }
        }
        return rsEnemy;
    }

    public void LevelUp(int level)
    {
        level_in_game += level;
        skin.localScale = skin.localScale + level * skin.localScale * 0.05f;
        attackRange.transform.localScale = attackRange.transform.localScale + level * attackRange.transform.localScale * 0.05f;
    }
}
