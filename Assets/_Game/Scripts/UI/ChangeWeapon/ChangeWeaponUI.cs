using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponUI : UICanvas
{
    [SerializeField] private List<GameObject> weapons;
    private int currentIndex;
    public override void Open()
    {
        base.Open();
        currentIndex = 0;
    }


    public void NextItemButton()
    {
        currentIndex++;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if(currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenPanelChildren(currentIndex);
        closeOtherPanelChildren(currentIndex);
    }

    public void BackItemButton()
    {
        currentIndex--;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenPanelChildren(currentIndex);
        closeOtherPanelChildren(currentIndex);
    }

    public void EquipButton()
    {
        if (weapons[currentIndex].GetComponent<EquipWeaponText>().GetEquipText() == "equip")
        {
            LevelManager.Ins.player.ChangeWeapon((WeaponType)currentIndex);
            SetUnEquipWeaponAll();
            weapons[currentIndex].GetComponent<EquipWeaponText>().SetEquipText();
        }
    }

    public void SetUnEquipWeaponAll()
    {
        for(int i =0;i < weapons.Count; i++)
        {
            weapons[i].GetComponent<EquipWeaponText>().SetUnEquipText();
        }
    }
    public void X_Button()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void OpenPanelChildren(int i)
    {
        weapons[i].SetActive(true);
    }
    public void closeOtherPanelChildren(int index)
    {
        for(int i = 0; i<weapons.Count;i++)
        {
            if (index != i)
            {
                closePanelChildren(i);
            }
        }
    }

    public void closePanelChildren(int i)
    {
        weapons[i].SetActive(false);
    }
}
