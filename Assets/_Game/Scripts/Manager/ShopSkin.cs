using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkin : MonoBehaviour
{
    public List<bool> items;
    public int indexShop;
    public void OnInit()
    {
        if (items == null || items.Count <= 0) return;
        for(int i= 0; i < items.Count; i++)
        {
            var item = items[i];
            switch (indexShop)
            {
                case 0:
                    if (i == 0)
                    {
                        Pref.SetBool(PrefConst.HAT_PREF + i, true); // hatPref_0
                    }
                    else
                    {
                        if (!PlayerPrefs.HasKey(PrefConst.HAT_PREF + i))
                        {
                            Pref.SetBool(PrefConst.HAT_PREF + i, false);
                        }
                    }
                    break;
                case 1:
                    if (i == 0)
                    {
                        Pref.SetBool(PrefConst.PANT_PREF + i, true); 
                    }
                    else
                    {
                        if (!PlayerPrefs.HasKey(PrefConst.PANT_PREF + i))
                        {
                            Pref.SetBool(PrefConst.PANT_PREF + i, false);
                        }
                    }
                    break;
                case 2:
                    if (i == 0)
                    {
                        Pref.SetBool(PrefConst.SHIELD_PREF + i, true); 
                    }
                    else
                    {
                        if (!PlayerPrefs.HasKey(PrefConst.SHIELD_PREF + i))
                        {
                            Pref.SetBool(PrefConst.SHIELD_PREF + i, false);
                        }
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}
