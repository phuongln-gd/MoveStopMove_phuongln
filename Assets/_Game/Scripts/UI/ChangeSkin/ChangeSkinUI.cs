using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopState
{
    Hat,Pant,Shield,Set
}
public class ChangeSkinUI : UICanvas
{
    private ShopState currentState;
    [SerializeField] private List<Image> shopStates;
    public override void Open()
    {
        base.Open();
        currentState = ShopState.Hat;
        OpenShopState(ShopState.Hat);
        shopStates[(int)currentState].gameObject.SetActive(true);
    }


    public void X_Button()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        CloseCurrentShopState();
        Close();
    }

    public void ChangeHatButton()
    {
        OpenShopState(ShopState.Hat);
    }

    public void ChangePantButton()
    {
        OpenShopState(ShopState.Pant);
    }

    public void ChangeShieldButton()
    {
        OpenShopState(ShopState.Shield);
    }

    public void ChangeSetButton()
    {
        OpenShopState(ShopState.Set);
    }

    public void OpenShopState(ShopState shopState)
    {
        if (currentState != shopState)
        {
            CloseCurrentShopState();
            currentState = shopState;
            shopStates[(int)shopState].gameObject.SetActive(true);
        }
    }

    public void CloseCurrentShopState()
    {
        shopStates[(int)currentState].gameObject.SetActive(false);
    }

}
