using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject skin;

    public void SetEnable(bool b)
    {
        gameObject.SetActive(b);
    }
}
