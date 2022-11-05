using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    public GameObject skin;

    public float speed;
    protected Vector3 targetPos;
    [SerializeField] protected Transform tf;

    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
    }

    private void Update()
    {

    }
    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
    public virtual void Throw()
    {
    }
    public void SetTargetPosition(Vector3 pos)
    {
        targetPos = pos;
    }
}
