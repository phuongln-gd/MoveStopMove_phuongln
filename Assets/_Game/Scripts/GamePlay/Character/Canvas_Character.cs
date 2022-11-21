using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Character : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_alive;
    
    public void SetText_Alive(string text)
    {
        text_alive.text = text;
    }
}
