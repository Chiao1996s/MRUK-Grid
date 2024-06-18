using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Color originalColor;

    public void Init(bool isOffset)
    {
        // Initialize the tile's properties based on isOffset
        if (isOffset)
        {
            // For example, change color or material
            GetComponent<Renderer>().material.color = Color.gray;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        
        // Store the initialized color as the original color
        originalColor = GetComponent<Renderer>().material.color;
    }

    

}