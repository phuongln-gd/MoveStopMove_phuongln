using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Transform tf;
    [SerializeField] protected string name;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float sight;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject weaponHand;
    [SerializeField] protected GameObject weaponThrowPrefab;

    public WeaponData weaponData;
    public WeaponType weaponType;

    private string currentAnimName;
    public Transform weakPoint;

    public int level_in_game;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInit()
    {
        isDead = false;
    }

    public virtual void OnDespawn()
    {
        ChangeAnim(Constant.DEAD_ANIM);
    }

    protected void ChangeAnim(string animName)
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
}
