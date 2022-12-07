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
        ChangeCurrentItem(i);
    }
    
    public void OnDespawn()
    {
        ChangeCurrentItem(equipedindex);
        if (items[currentItem].GetComponent<Item>().CheckCurrentState() == ItemState.buy)
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
            items[currentItem].color = Color.blue;
            items[currentItem].GetComponent<Item>().OnDespawn();
            if (items[currentItem].GetComponent<Item>().currentState == ItemState.equiped)
            {
                items[currentItem].GetComponent<Item>().currentState = ItemState.own;
                equipedindex = currentItem;
            }
            currentItem = i;
            items[currentItem].GetComponent<Item>().OnInit();
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeShield(currentItem);
            if(items[currentItem].GetComponent<Item>().currentState == ItemState.own)
            {
                items[currentItem].GetComponent<Item>().currentState = ItemState.equiped;
                equipedindex = currentItem;
            }
        }
    }
}
