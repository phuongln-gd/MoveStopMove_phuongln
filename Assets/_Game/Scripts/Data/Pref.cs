using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref 
{
    public static int CurHatId
    {
        set => PlayerPrefs.SetInt(PrefConst.HAT_PREF, value);
        get => PlayerPrefs.GetInt(PrefConst.HAT_PREF);
    }

    public static int CurPantId
    {
        set => PlayerPrefs.SetInt(PrefConst.PANT_PREF, value);
        get => PlayerPrefs.GetInt(PrefConst.PANT_PREF);
    }

    public static int CurShieldId
    {
        set => PlayerPrefs.SetInt(PrefConst.SHIELD_PREF, value);
        get => PlayerPrefs.GetInt(PrefConst.SHIELD_PREF);
    }
    public static void SetBool(string key, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
