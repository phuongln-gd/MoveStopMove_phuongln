using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    
    [SerializeField] protected Transform tf;

    public GameObject skin;

    public float speed;
    protected Vector3 targetPos;
    protected Character attacker;

    protected bool hasTarget;
    public bool IsDestination => Vector3.Distance(tf.position.x * Vector3.right + tf.position.z * Vector3.forward
        , targetPos) < 0.1f;
    public virtual void Update()
    {
        if (hasTarget)
        {
            Throw();
            if (IsDestination)
            {
                hasTarget = false;
                OnDespawn();
            }
        }
    }
    public virtual void OnInit()
    {
        hasTarget = false;
    }
    public virtual void OnDespawn()
    {
        Destroy(gameObject);
    }
    public virtual void Throw()
    {
    }
    public void SetTargetPosition(Vector3 pos)
    {
        hasTarget = true;
        targetPos = pos;
        targetPos.y = 0;
    }

    public void SetCharacter(Character character)
    {
        this.attacker = character;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            attacker.LevelUp();
            Character target = other.GetComponent<Character>();
            target.OnDespawn();
            OnDespawn();
        }
    }
}
