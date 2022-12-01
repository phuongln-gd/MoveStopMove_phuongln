using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : GameUnit
{
    
    [SerializeField] protected Transform tf;
    [SerializeField] protected Rigidbody rb;

    public GameObject skin;

    public float speed;
    protected Vector3 targetDir;
    protected Character attacker;

    protected bool hasTarget;
    protected Vector3 startPoint;
    public virtual void Update()
    {
        if (hasTarget)
        {
            Throw();
            if(Vector3.Distance(tf.position,startPoint) > attacker.attackRange*1.5f)
            {
                hasTarget = false;
                OnDespawn();
            }
        }
    }
    public override void OnInit()
    {
        hasTarget = false;
        startPoint = tf.position;
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public virtual void Throw()
    {
    }
    public virtual void SetTargetPosition(Vector3 pos)
    {
        hasTarget = true;
        targetDir = pos - tf.position;
    }

    public void SetCharacter(Character character)
    {
        this.attacker = character;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG) && Cache.GetCharacter(other) != attacker) {
            attacker.LevelUp();
            Character target = Cache.GetCharacter(other);
            if (target.TryGetComponent<Enemy>(out Enemy enemy))
            {
                LevelManager.Ins.currentLevel.RemoveBot(enemy);
                if (attacker.TryGetComponent<Player>(out Player player))
                {
                    player.killCount += 1;
                    if(LevelManager.Ins.currentLevel.aliveBot == 0)
                    {
                        GameManager.Ins.ChangeState(GameState.Victory);
                        UIManager.Ins.OpenUI<Win>();
                        UIManager.Ins.CloseUI<GamePlay>();
                        player.ChangeAnim(Constant.DANCE_ANIM);
                    }
                }
            }
            else if (target.TryGetComponent<Player>(out Player player))
            {
                LevelManager.Ins.player.namekiller = attacker.nameCharacter;
                LevelManager.Ins.player.rank = LevelManager.Ins.currentLevel.aliveBot + 1;
            }
            target.OnHit();
            OnDespawn();
        }
        if (other.CompareTag(Constant.BARRIER_TAG))
        {
            rb.velocity = Vector3.zero;
            hasTarget = false;
            Invoke(nameof(OnDespawn), 2f);
        }
    }

    
}
