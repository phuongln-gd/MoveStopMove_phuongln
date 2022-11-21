using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        LevelManager.Ins.OnStartGame();
        UIManager.Ins.OpenUI<GamePlay>();
        Close();
    }

    public void ChangeWeaponButton()
    {
        //UIManager.Ins.OpenUI<ChangeWeaponUI>();
        //Close();
    }

    public void ChangeSkinButton()
    {
        //UIManager.Ins.OpenUI<ChangeWeaponUI>();
        //Close();
    }

    public void ZombieModeButton()
    {

    }

    public void SoundButton()
    {
        
    }

    public void PhoneButton()
    {
       
    }

    public void AdsButton()
    {
        
    }
}
