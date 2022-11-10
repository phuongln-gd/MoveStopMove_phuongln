using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{
    
    public void ChangeStateActive(bool state)
    {
        gameObject.SetActive(state);
    }

    public bool IsStateActive()
    {
        return gameObject.activeInHierarchy;
    }
}
