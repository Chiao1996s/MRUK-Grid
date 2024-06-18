using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayManipulation : MonoBehaviour
{
    private Vector3 originalScale;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private Vector3 offset; // Reference to the prefab

    void Start()
    {
        // Store the original scale
        originalScale = transform.localScale;
    }

    public void ScaleMe()  //hover
    {
        // Double the height (y-axis) every time this function is called
        Vector3 newScale = transform.localScale;
        newScale.y *= 5;
        gameObject.transform.localScale = newScale;
    }

    public void ScaleDown()
    {
        // Change back to the original scale
        gameObject.transform.localScale = originalScale;
    }

    public void SetNewY()  //selected
    {
        Vector3 newScale = transform.localScale;
        gameObject.transform.localScale = newScale;

        // Update the original scale to the new scale
        originalScale = newScale;
    }

    public void AddBox()
    {
        // Define the offset position
        Vector3 newPosition = transform.position + offset;

        // Instantiate a new box at the offset position
        GameObject newBox = Instantiate(boxPrefab, newPosition, Quaternion.identity);
        BoxManager.Instance.AddBox(newBox);
    }

    public void ClearBoxes()
    {
        BoxManager.Instance.ClearBoxes();
    }

    public void ChangeBoxColor(Color newColor)
    {
        BoxManager.Instance.ChangeBoxColor(newColor);
    }
}