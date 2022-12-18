using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShieldUI : MonoBehaviour
{
    [SerializeField] private List<Image> items;
    private int currentItem;
    private int equipedindex;
    private void Awake()
    {
        currentItem = 0;
        equipedindex = 0;
        int i = GameManager.Ins.userData.lastUsedShield;
        OnInit();
        if (Pref.GetBool(PrefConst.SHIELD_PREF + i))
        {
            items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(false);
        }
        else
        {
            items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(true);
        }
        ChangeCurrentItem(i);
    }

    public void OnInit()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(Pref.GetBool(PrefConst.SHIELD_PREF + i))
            {
                items[i].GetComponent<Item>().lockimage.gameObject.SetActive(false);
            }
            else
            {
                items[i].GetComponent<Item>().lockimage.gameObject.SetActive(true);
            }
        }
    }
    public void OnDespawn()
    {
        ChangeCurrentItem(equipedindex);
        if (items[currentItem].GetComponent<Item>().currentState== ItemState.buy)
        {
            LevelManager.Ins.player.ChangeShield(equipedindex);
        }
    }
    public void ChangeItem1()
    {
        ChangeCurrentItem(0);
    }

    public void ChangeItem2()
    {
        ChangeCurrentItem(1);
    }

    public void ChangeCurrentItem(int i)
    {
        if (currentItem != i)
        {
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            items[currentItem].color = Color.blue;
            if (items[currentItem].GetComponent<Item>().currentState == ItemState.equiped)
            {
                items[currentItem].GetComponent<Item>().currentState = ItemState.own;
                equipedindex = currentItem;
            }
            currentItem = i;
            if (Pref.GetBool(PrefConst.SHIELD_PREF + currentItem))
            {
                items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(false);
            }
            else
            {
                items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(true);
                items[currentItem].GetComponent<Item>().amount.text = items[currentItem].GetComponent<Item>().price + "";
            }
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeShield(currentItem);
            if(Pref.GetBool(PrefConst.SHIELD_PREF + currentItem))
            {
                items[currentItem].GetComponent<Item>().currentState = ItemState.equiped;
                equipedindex = currentItem;
            }
        }
    }

    public void ClickBuyButton()
    {
        if (GameManager.Ins.userData.Gold >= Int32.Parse(items[currentItem].GetComponent<Item>().amount.text))
        {
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            items[currentItem].GetComponent<Item>().lockimage.gameObject.SetActive(false);
            items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(false);
            items[currentItem].GetComponent<Item>().currentState = ItemState.equiped;
            Pref.SetBool(PrefConst.SHIELD_PREF + currentItem, true);
            equipedindex = currentItem;

            int coin_player = GameManager.Ins.userData.Gold - Int32.Parse(items[currentItem].GetComponent<Item>().amount.text);
            GameManager.Ins.userData.SetIntData(UserData.Key_Gold, ref GameManager.Ins.userData.Gold, coin_player);
            UIManager.Ins.GetUI<ChangeSkinUI>().text_gold.text = GameManager.Ins.userData.Gold + "";
        }
    }

}
