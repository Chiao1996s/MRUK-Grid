using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Oculus.Interaction;

public class SetTargetTransform : MonoBehaviour
{
    public Grabbable grabbable; // Reference to Grabbable

    // Method to set the target transform of the Grabbable component using reflection
    public void SetTarget(Transform newTarget)
    {
        if (grabbable != null)
        {
            FieldInfo targetTransformField = typeof(Grabbable).GetField("_targetTransform", BindingFlags.NonPublic | BindingFlags.Instance);
            if (targetTransformField != null)
            {
                targetTransformField.SetValue(grabbable, newTarget);
                Debug.Log("Target transform set to new prefab.");
            }
            else
            {
                Debug.LogError("Could not find _targetTransform field in Grabbable.");
            }
        }
        else
        {
            Debug.LogError("Grabbable component is not set.");
        }
    }
}