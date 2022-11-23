using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
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
            if(Vector3.Distance(tf.position,startPoint) > attacker.attackRange)
            {
                hasTarget = false;
                OnDespawn();
            }
        }
    }
    public virtual void OnInit()
    {
        hasTarget = false;
        startPoint = tf.position;
    }
    public virtual void OnDespawn()
    {
        Destroy(gameObject);
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
            target.OnHit();
            LevelManager.Ins.currentLevel.RemoveBot(target);
            OnDespawn();
            int amountBotAlive = LevelManager.Ins.currentLevel.aliveBot;
            UIManager.Ins.GetUI<GamePlay>().SetAliveText("Alive: "+ amountBotAlive);
        }
    }
}
