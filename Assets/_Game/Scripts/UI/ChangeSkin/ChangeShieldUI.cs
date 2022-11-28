using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShieldUI : MonoBehaviour
{
    [SerializeField] private List<Image> items;
    private int currentItem = 0;
    private void Awake()
    {
        currentItem = 0;
    }
    public void ChangeItem1()
    {
        ChangeCurrentItem(0);
    }

    public void ChangeCurrentItem(int i)
    {
        if (currentItem != i)
        {
            items[currentItem].color = Color.blue;
            currentItem = i;
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeShield(currentItem);
        }
    }
}
