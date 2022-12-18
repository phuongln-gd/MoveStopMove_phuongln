using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum ItemState
{
    equiped,
    own,
    buy
}
public class Item : MonoBehaviour
{
    public Button buyButton;
    public TextMeshProUGUI amount;
    public Image lockimage;
    public int price;

    public ItemState currentState;

}
