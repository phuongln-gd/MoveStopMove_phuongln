using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicatorManager : MonoBehaviour
{
    private bool flag;

    public void SetEnable(bool b)
    {
        flag = b;
        gameObject.SetActive(flag);
    }
}
