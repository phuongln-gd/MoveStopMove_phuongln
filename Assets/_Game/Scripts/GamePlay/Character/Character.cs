using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : GameUnit, IHit
{
    public Transform tf;
    public string nameCharacter;
    [SerializeField] protected float moveSpeed;
    public Transform weakPoint;

    // anim
    [SerializeField] protected Animator anim;
    //weapon hand
    [SerializeField] protected GameObject weaponHand;
    protected GameObject skinWeaponHand;
    protected GameObject skinHat;
    protected GameObject skinShield;
    protected WeaponHand wh;
    // weapon throw
    [SerializeField] protected GameObject weaponThrow;
    protected WeaponThrow wt;
    [SerializeField] protected Transform attackPoint;
    // skin
    public Transform skin;
    //attack range
    [SerializeField] public GameObject attackRangeGO;
    [SerializeField] protected GameObject hatPos;
    [SerializeField] protected GameObject shieldPos;

    protected List<Character> listEnemyInAreaAttack = new List<Character>();

    public WeaponData weaponData;
    public SkinData skinData;
    public NameData nameData;
    public SkinDataPlayer skinDataPlayer;

    public GameObject skin_pant;
    public GameObject skin_color;
    public ColorType colorType;

    protected WeaponType currentWeapon;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool hasEnemyInAreaAttack;

    private string currentAnimName;
    public float attackRange;
    public int level_in_game;
    [SerializeField] protected Character_Level character_Level;
    [HideInInspector] public bool isDead;
    private int upgradeCount;
    [SerializeField] private ParticleSystem levelupVFXPrefab;
    public override void OnInit()
    {
        isDead = false;
        level_in_game = 1;
        attackRange = 5f;
        wh = weaponHand.GetComponent<WeaponHand>();
        ChangeAnim(Constant.IDLE_ANIM);
        character_Level.SetTextLevel(level_in_game);
        character_Level.SetTextName(nameCharacter);
        upgradeCount = 0;
    }

    public override void OnDespawn()
    {
        ChangeAnim(Constant.DEAD_ANIM);
        isDead = true;
    }

    public void LevelUp()
    {
        int up = Random.Range(1, 3);
        level_in_game += up;
        character_Level.SetTextLevel(level_in_game);

        if (level_in_game >= 3 && upgradeCount < 1)
        {
            UpgradeScale();
            upgradeCount += 1;
        }
        if (level_in_game >= 5 && upgradeCount < 2)
        {
            UpgradeScale();
            upgradeCount += 1;
        }
        if (level_in_game >= 9 && upgradeCount < 3)
        {
            UpgradeScale();
            upgradeCount += 1;
        }
    }

    public void UpgradeScale()
    {
        skin.localScale = skin.localScale * 1.1f;
        attackRange *= 1.5f;
        ParticlePool.Play(levelupVFXPrefab,tf.position,tf.rotation);
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

    public virtual void ResetAttack()
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

    public void ChangeSkinPantRandom()
    {
        skin_pant.GetComponent<SkinnedMeshRenderer>().material = skinData.RandomPants();
    }

    public void GetRandomHat()
    {
        GameObject hat = Instantiate(skinData.RandomHat(), hatPos.transform);
    }

    public void ChangeSkinColor(ColorType color)
    {
        colorType = color;
        skin_color.GetComponent<SkinnedMeshRenderer>().material = skinData.GetColor(color);
    }

    public void RandomName()
    {
        nameCharacter = nameData.RandomName();
        character_Level.SetTextName(nameCharacter);
    }
    
    public virtual void OnHit()
    {
        OnDespawn();
    }

    
}
