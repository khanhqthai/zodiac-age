﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjects : MonoBehaviour
{
    private Color startcolor;
    void OnMouseEnter()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
        Debug.Log("Highlighting" );
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startcolor;
    }
}
