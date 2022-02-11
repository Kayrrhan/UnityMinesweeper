using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeColor : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color nonActiveColor;
    //private Renderer r;
    private void Start()
    {
     //   r = text.GetComponent<Renderer>();
        Debug.Log(text.text);
    }
    
    private void OnMouseEnter(){
        Debug.Log("entered");
        text.color = activeColor;
    }

    private void OnMouseDown()
    {
        text.outlineColor = nonActiveColor;
        text.color = activeColor;
    }

    private void OnMouseExit()
    {
        text.color = nonActiveColor;
    }


}
