using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class DistanceGrabManipulation : MonoBehaviour
{
    private Vector3 originalScale;
    public GameObject prefabToSpawn; // 要生成的預製體
    public SetTargetTransform setTargetTransform; // Reference to SetTargetTransform

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        Debug.Log("Original scale set to: " + originalScale);
    }

    public void ScaleDown()
    {
        Debug.Log("ScaleDown function called");
        Vector3 newScale = transform.localScale;
        newScale *= 0.5f; // 將縮放比例減半
        transform.localScale = newScale;
        Debug.Log("Scaled down to: " + newScale);
    }

    public void ScaleBack()
    {
        transform.localScale = originalScale;
        Debug.Log("Scaled back to original: " + originalScale);
    }
    
    public void ScaleUp()
    {
        Debug.Log("ScaleUp function called");
        Vector3 newScale = transform.localScale;
        newScale *= 2;
        transform.localScale = newScale;
        Debug.Log("Scaled up to: " + newScale);
    }
    
    public void Spawn()
    {
        Debug.Log("Spawn function called");
        if (prefabToSpawn != null)
        {
            // Instantiate the new prefab
            GameObject newObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // Log the original scale of the new prefab
            Debug.Log("Original scale of new prefab: " + newObject.transform.localScale);

            // Set the scale of the new prefab to half the size of the original object
            Vector3 newScale = transform.localScale * 0.5f;
            newObject.transform.localScale = newScale;

            // Log the new scale to confirm it has been set
            Debug.Log("New scale of new prefab: " + newObject.transform.localScale);

            // Update the target transform of the SetTargetTransform component
            if (setTargetTransform != null)
            {
                setTargetTransform.SetTarget(newObject.transform); // Update the target transform
            }
        }
        else
        {
            Debug.LogError("Prefab to spawn is not set.");
        }
    }
}