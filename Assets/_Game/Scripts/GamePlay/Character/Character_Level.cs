using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Level : MonoBehaviour
{
    [SerializeField] Text text_name; 
    [SerializeField] Text text_level;
    private Transform mainCameraTrans;

    [SerializeField] RectTransform tf;

    private void Awake()
    {
        mainCameraTrans = Camera.main.transform;
    }
    private void LateUpdate()
    {
        tf.LookAt(tf.position + mainCameraTrans.rotation * Vector3.forward, mainCameraTrans.rotation * Vector3.up);
    }
    public void SetTextName(string name)
    {
        text_name.text = name;
    }

    public void SetTextLevel(int level)
    {
        text_level.text = level.ToString();
    }
}
