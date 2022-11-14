using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    public Transform tf;
    [SerializeField] protected string name;
    [SerializeField] protected float moveSpeed;
    public Transform weakPoint;

    // anim
    [SerializeField] protected Animator anim;
    //weapon hand
    [SerializeField] protected GameObject weaponHand;
    protected GameObject skinWeaponHand;
    protected WeaponHand wh;
    // weapon throw
    [SerializeField] protected GameObject weaponThrow;
    protected WeaponThrow wt;
    [SerializeField] protected Transform attackPoint;
    // skin
    [SerializeField] protected Transform skin;
    //attack range
    [SerializeField] public GameObject attackRangeGO;
    
    protected List<Character> listEnemyInAreaAttack = new List<Character>();

    public WeaponData weaponData;

    protected WeaponType currentWeapon;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool hasEnemyInAreaAttack;

    private string currentAnimName;
    [SerializeField]protected float attackRange;
    public int level_in_game;
    [HideInInspector] public bool isDead;
    protected virtual void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        isDead = false;
        level_in_game = 1;
        attackRange = 5f;
        wh = weaponHand.GetComponent<WeaponHand>();
        ChangeAnim(Constant.IDLE_ANIM);
    }

    public virtual void OnDespawn()
    {
        ChangeAnim(Constant.DEAD_ANIM);
        isDead = true;
    }
    public void LevelUp()
    {
        int up = Random.Range(1, 3);
        level_in_game += up;
        skin.localScale = skin.localScale + up * skin.localScale * 0.05f;
        attackRangeGO.transform.localScale = attackRangeGO.transform.localScale + up * attackRangeGO.transform.localScale * 0.05f;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            // cai dat lai thong so trigger
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            // cai dat lai trigger theo ten anim hien tai
            anim.SetTrigger(currentAnimName);
        }
    }

    public void CheckEnemyInAttackArea()
    {
        listEnemyInAreaAttack.Clear();
        hasEnemyInAreaAttack = false;
        Collider[] colliders = Physics.OverlapSphere(tf.position, attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(Constant.CHARACTER_TAG) &&
                Vector3.Distance(tf.position, colliders[i].gameObject.transform.position) > 0.5f)
            {
                hasEnemyInAreaAttack = true;
                listEnemyInAreaAttack.Add(colliders[i].GetComponent<Character>());
            }
        }
    }
    public Character FindNearestEnemy()
    {
        if (listEnemyInAreaAttack.Count > 0)
        {
            float minDistance = Vector3.Distance(tf.position, listEnemyInAreaAttack[0].gameObject.transform.position);
            Character rsEnemy = listEnemyInAreaAttack[0];
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
        return null;
    }

    public virtual void Attack(Character target)
    {
        
    }

    public void ResetAttack()
    {
        wh.ChangeStateActive(true);
        isAttacking = false;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        if (skinWeaponHand != null && currentWeapon != weaponType)
        {
            Destroy(skinWeaponHand);
        }
        currentWeapon = weaponType;
        skinWeaponHand = Instantiate(weaponData.GetWeaponHand(weaponType), weaponHand.transform);
        weaponThrow = weaponData.GetWeaponThrow(weaponType).gameObject;
    }

    public virtual void OnHit()
    {
        OnDespawn();
    }
}
