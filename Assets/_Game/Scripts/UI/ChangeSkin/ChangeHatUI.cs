using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHatUI : MonoBehaviour
{
    [SerializeField] private List<Image> items;
    [SerializeField] private TextMeshProUGUI description_item;

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
    
    public void ChangeCurrentItem(int i)
    {
        if ( currentItem != i)
        {
            items[currentItem].color = Color.blue;
            currentItem = i;
            items[currentItem].color = Color.yellow;
            LevelManager.Ins.player.ChangeHat(currentItem);
            items[i].GetComponent<ItemAttribute>().SetStringDescription();
            SetDescriptionItem(items[i].GetComponent<ItemAttribute>().description);
        }
    }

    public void SetDescriptionItem(string s)
    {
        description_item.text = s;
    }
}
