using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI alive_text;
    public void Settingbutton()
    {
        UIManager.Ins.OpenUI<Setting>();
        Close();
    }

    public void SetAliveText(string s)
    {
        alive_text.text = s;
    }
}
