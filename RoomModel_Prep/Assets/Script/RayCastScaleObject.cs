using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScaleObject : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 originalScale;

    void Start()
    {
        // Store the original scale
        originalScale = transform.localScale;
    }

    public void ScaleMe()
    {
        // Change the scale to the target scale
        gameObject.transform.localScale = targetScale;
    }

    public void ScaleDown()
    {
        // Change back to the original scale
        gameObject.transform.localScale = originalScale;
    }
}
