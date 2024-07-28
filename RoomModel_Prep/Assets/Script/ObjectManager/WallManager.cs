using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class WallManager : MonoBehaviour
{
    public List<MRUKAnchor> walls = new List<MRUKAnchor>();
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;
    public Transform cameraRig;
    public float yOffset = 1.5f;

    // Start is called before the first frame update
    public void WallBloc()
    {
        // Get the current room
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        // Get the room anchors
        List<MRUKAnchor> anchors = room.GetRoomAnchors();

        // Filter the table anchors
        foreach (var anchor in anchors)
        {
            if (anchor.name == "WALL_FACE")
            {
                walls.Add(anchor);
            }
        }

        // Instantiate prefabs on the tables
        for (int i = 0; i < walls.Count; i++)
        {
            Vector3[] points = walls[i].GetBoundsFaceCenters();
            
            GameObject prefabToInstantiate = null;

            // Choose the appropriate prefab based on the table index
            switch (i)
            {
                case 0:
                    prefabToInstantiate = prefab1;
                    break;
                case 1:
                    prefabToInstantiate = prefab2;
                    break;
                case 2:
                    prefabToInstantiate = prefab3;
                    break;
                case 3:
                    prefabToInstantiate = prefab4;
                    break;
                case 4:
                    prefabToInstantiate = prefab5;
                    break;
                case 5:
                    prefabToInstantiate = prefab6;
                    break;
                default:
                    Debug.LogWarning("More than 6 walls found, no prefab assigned for wall index: " + i);
                    break;
            }

            if (prefabToInstantiate != null && points.Length > 0)
            {
                // Calculate the position with an upward offset
                Vector3 offsetPosition = points[0] + new Vector3(0, yOffset, 0);

                // Use the wall's rotation
                Quaternion wallRotation = walls[i].transform.rotation;

                // Instantiate the prefab with the wall's rotation
                GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, offsetPosition, wallRotation);
            }
        }
    }
    
    
}