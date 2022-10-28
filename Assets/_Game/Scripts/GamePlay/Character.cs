using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Transform tf;
    [SerializeField] protected string name;
    [SerializeField] protected int level_in_game;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float sight;
    [SerializeField] protected Transform weakPoint;
    [SerializeField] protected Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInit()
    {
    }

    public virtual void OnDespawn()
    {

    }
}
