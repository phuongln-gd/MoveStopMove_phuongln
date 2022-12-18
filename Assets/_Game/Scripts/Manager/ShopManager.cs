using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public List<GameObject> listShopSkins;
    public GameObject shopWeapon;

    private void Start()
    {
        if (listShopSkins == null || listShopSkins.Count <= 0 || shopWeapon == null) return;

        for(int i = 0; i < listShopSkins.Count; i++)
        {
            listShopSkins[i].GetComponent<ShopSkin>().OnInit();
        }
    }
}
