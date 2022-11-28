using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePantUI : MonoBehaviour
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
    public void ChangeItem2()
    {
        ChangeCurrentItem(1);
    }
    public void ChangeItem3()
    {
        ChangeCurrentItem(2);
    }

    public void ChangeItem4()
    {
        ChangeCurrentItem(3);
    }

    public void ChangeCurrentItem(int i)
    {
        if (currentItem != i)
        {
            items[currentItem].color = Color.blue;
            currentItem = i;
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangePant(currentItem);
        }
    }
}
