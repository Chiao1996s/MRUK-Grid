using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class RaycastHitSpawner : MonoBehaviour
{
    public GameObject prefabBox; // Assign the prefab box in the inspector
    public Transform rayStartPoint; // Assign the starting point of the ray
    public float rayLength = 10f; // Set the ray length
    public LayerMask labelFilter; // Set the label filter

    
}