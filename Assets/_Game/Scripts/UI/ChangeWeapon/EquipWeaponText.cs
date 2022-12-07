using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeaponText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private Image background;
    public string GetEquipText()
    {
        return equipText.text;
    }
    public void SetEquipText()
    {
        equipText.text = Constant.EQUIPPED_TEXT;
        SetBackGroundEquiped();
    }

    public void SetUnEquipText()
    {
        equipText.text = Constant.EQUIP_TEXT;
        SetBackGroundUnEquip();
    }

    public void SetBackGroundEquiped()
    {
        background.color = Color.gray;
    }

    public void SetBackGroundUnEquip()
    {
        background.color = Color.green;
    }
}
