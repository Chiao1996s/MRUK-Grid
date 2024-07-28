using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class PlaceTrashcan : MonoBehaviour
{
    public GameObject trashcanPrefab;  // The trashcan prefab to place
    public float zOffset = 0.3f;       // X offset for placing the trashcan

    public void PlaceTrashcanOnFirstTable()
    {
        // Get the current room
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        // Get the room anchors
        List<MRUKAnchor> anchors = room.GetRoomAnchors();

        // Find the first table anchor
        MRUKAnchor firstTable = null;
        foreach (var anchor in anchors)
        {
            if (anchor.HasLabel("TABLE"))
            {
                firstTable = anchor;
                break;
            }
        }

        // Place the trashcan on the first table found
        if (firstTable != null)
        {
            Vector3[] faceCenters = firstTable.GetBoundsFaceCenters();

            if (faceCenters.Length > 0)
            {
                Vector3 placementPosition = faceCenters[0] + new Vector3(0, 0, zOffset);

                // Instantiate the trashcan prefab at the offset position
                Instantiate(trashcanPrefab, placementPosition, Quaternion.identity);

                Debug.Log("Trashcan placed on the first table surface with offset.");
            }
            else
            {
                Debug.LogError("No face centers found for the first table.");
            }
        }
        else
        {
            Debug.LogError("No tables found in the room.");
        }
    }
}
