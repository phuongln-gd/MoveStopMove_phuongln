using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum ItemState
{
    equiped,
    own,
    buy
}
public class Item : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Image lockimage;

    public ItemState currentState;

    public void OnInit()
    {
        if (currentState == ItemState.buy)
        {
            SetEnableBuyButton(true);
        }
    }

    public void OnDespawn()
    {
        if (currentState == ItemState.buy)
        {
            SetEnableBuyButton(false);
        }
    }

    public void ClickBuyButton()
    {
        if (GameManager.Ins.userData.Gold >= Int32.Parse(amount.text))
        {
            lockimage.gameObject.SetActive(false);
            SetEnableBuyButton(false);
            currentState = ItemState.equiped;
            Debug.Log("bought");
        }
    }

    public ItemState CheckCurrentState()
    {
        return currentState;
    }
    public void SetEnableBuyButton(bool flag)
    {
        buyButton.gameObject.SetActive(flag);
    }
}
