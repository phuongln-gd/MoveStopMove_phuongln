using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponUI : UICanvas
{
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private TextMeshProUGUI text_gold;

    private int currentIndex;
    private int currentItem;
    public override void Open()
    {
        base.Open();
        text_gold.text = GameManager.Ins.userData.Gold + "";
        currentIndex = 0;
        currentItem = GameManager.Ins.userData.lastUsedWeapon;
        if (currentItem != 0)
        {
            weapons[0].GetComponent<EquipWeaponText>().SetUnEquipText(); ;
            weapons[currentItem].GetComponent<EquipWeaponText>().SetEquipText();
        }
    }

    public void ChangeCurrentItem(int i)
    {
        if (currentItem != i)
        {
            weapons[currentItem].GetComponent<EquipWeaponText>().SetUnEquipText(); ;
            currentItem = i;
            weapons[currentItem].GetComponent<EquipWeaponText>().SetEquipText();
            LevelManager.Ins.player.ChangeWeapon((WeaponType)currentItem);
        }
    }
    public void EquipButton()
    {
        if (currentItem != currentIndex)
        {
            ChangeCurrentItem(currentIndex);
        }
    }
   
    public void NextItemButton()
    {
        CloseCurrentPanel();
        currentIndex++;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if(currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenCurrentPanel();
    }

    public void BackItemButton()
    {
        CloseCurrentPanel();
        currentIndex--;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenCurrentPanel();
    }

    public void X_Button()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void OpenCurrentPanel()
    {
        weapons[currentIndex].SetActive(true);
    }

    public void CloseCurrentPanel()
    {
        weapons[currentIndex].SetActive(false);
    }
}
