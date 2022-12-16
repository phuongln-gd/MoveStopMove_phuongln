using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHatUI : MonoBehaviour
{
    [SerializeField] private List<Image> items;

    private int currentItem;
    private void Awake()
    {
        currentItem = 0;
        int i = GameManager.Ins.userData.lastUsedHat;
        ChangeCurrentItem(i);
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
        if ( currentItem != i)
        {
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            items[currentItem].color = Color.blue;
            currentItem = i;
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeHat(currentItem);
        }
    }

}
