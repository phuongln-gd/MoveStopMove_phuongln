using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI text_gold;
    public override void Open()
    {
        base.Open();
        currentState = ShopState.Hat;
        OpenShopState(ShopState.Hat);
        shopStates[(int)currentState].gameObject.SetActive(true);
        LevelManager.Ins.player.ChangeAnim(Constant.DANCE_ANIM);
        text_gold.text = GameManager.Ins.userData.Gold + "";
    }

    public override void Close()
    {
        base.Close();
        LevelManager.Ins.player.ChangeAnim(Constant.IDLE_ANIM);
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
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            CloseCurrentShopState();
            currentState = shopState;
            shopStates[(int)shopState].gameObject.SetActive(true);
        }
    }

    public void CloseCurrentShopState()
    {
        shopStates[(int)currentState].gameObject.SetActive(false);
        if (currentState == ShopState.Shield)
        {
            shopStates[(int)currentState].GetComponent<ChangeShieldUI>().OnDespawn();
        }
        if (currentState == ShopState.Hat)
        {
            shopStates[(int)currentState].GetComponent<ChangeHatUI>().OnDespawn();
        }
        if (currentState == ShopState.Pant)
        {
            shopStates[(int)currentState].GetComponent<ChangePantUI>().OnDespawn();
        }

    }

}
