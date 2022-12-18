using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHatUI : MonoBehaviour
{
    [SerializeField] private List<Image> items;

    private int currentItem;
    private int equipedindex;

    private void Awake()
    {
        currentItem = 0;
        equipedindex = 0;
        int i = GameManager.Ins.userData.lastUsedHat;
        OnInit();
        if (Pref.GetBool(PrefConst.HAT_PREF + i))
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
        for (int i = 0; i < items.Count; i++)
        {
            if (Pref.GetBool(PrefConst.HAT_PREF + i))
            {
                items[i].GetComponent<Item>().lockimage.gameObject.SetActive(false); //tat lock_image
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
        if (items[currentItem].GetComponent<Item>().currentState == ItemState.buy)
        {
            LevelManager.Ins.player.ChangeHat(equipedindex);
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
    public void ChangeItem3()
    {
        ChangeCurrentItem(2);
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
            if (Pref.GetBool(PrefConst.HAT_PREF + currentItem))
            {
                items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(false); // tat nut buy
            }
            else
            {
                items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(true); //mo nut buy
                items[currentItem].GetComponent<Item>().amount.text = items[currentItem].GetComponent<Item>().price+"";
            }
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeHat(currentItem);
            if (Pref.GetBool(PrefConst.HAT_PREF + currentItem))
            {
                items[currentItem].GetComponent<Item>().currentState = ItemState.equiped;
                equipedindex = currentItem;
            }
        }
    }
    public void ClickBuyButton()
    {
        if (GameManager.Ins.userData.Gold >= items[currentItem].GetComponent<Item>().price)
        {
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            items[currentItem].GetComponent<Item>().lockimage.gameObject.SetActive(false);
            items[currentItem].GetComponent<Item>().buyButton.gameObject.SetActive(false);
            items[currentItem].GetComponent<Item>().currentState = ItemState.equiped;
            Pref.SetBool(PrefConst.HAT_PREF + currentItem, true);
            equipedindex = currentItem;

            // change UI coin_text 
            int coin_player = GameManager.Ins.userData.Gold - items[currentItem].GetComponent<Item>().price;
            GameManager.Ins.userData.SetIntData(UserData.Key_Gold, ref GameManager.Ins.userData.Gold, coin_player);
            UIManager.Ins.GetUI<ChangeSkinUI>().text_gold.text = GameManager.Ins.userData.Gold + "";
        }
    }

}
