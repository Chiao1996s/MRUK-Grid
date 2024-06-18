using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private static BoxManager instance;
    public static BoxManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoxManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("BoxManager");
                    instance = go.AddComponent<BoxManager>();
                }
            }
            return instance;
        }
    }

    private List<GameObject> boxes = new List<GameObject>();

    public void AddBox(GameObject box)
    {
        boxes.Add(box);
    }

    public void ClearBoxes()
    {
        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }
        boxes.Clear();
    }

    public void ChangeBoxColor(Color newColor)
    {
        foreach (GameObject box in boxes)
        {
            Renderer boxRenderer = box.GetComponent<Renderer>();
            if (boxRenderer != null)
            {
                boxRenderer.material.color = newColor;
            }
        }
    }
}
