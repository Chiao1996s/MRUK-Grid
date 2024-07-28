using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;


public class LampManager : MonoBehaviour
{
    public List<MRUKAnchor> lamps = new List<MRUKAnchor>();
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public Transform cameraRig;

    // Start is called before the first frame update
    public void LampBloc()
    {
        // Get the current room
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        // Get the room anchors
        List<MRUKAnchor> anchors = room.GetRoomAnchors();

        // Filter the table anchors
        foreach (var anchor in anchors)
        {
            if (anchor.name == "LAMP")
            {
                lamps.Add(anchor);
            }
        }

        // Instantiate prefabs on the tables
        for (int i = 0; i < lamps.Count; i++)
        {
            Vector3[] points = lamps[i].GetBoundsFaceCenters();
            
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
                default:
                    Debug.LogWarning("More than 3 lamps found, no prefab assigned for lamp index: " + i);
                    break;
            }

            if (prefabToInstantiate != null && points.Length > 0)
            {
                // Calculate the horizontal direction to the camera rig
                Vector3 directionToCamera = cameraRig.position - points[0];
                directionToCamera.y = 0;  // Ignore the vertical component

                // Calculate the rotation to face the camera rig horizontally
                Quaternion rotationToCamera = Quaternion.LookRotation(directionToCamera);

                // Instantiate the prefab facing the camera rig horizontally
                GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, points[0], rotationToCamera);

                // Add the FaceCamera component to the instantiated prefab
                FaceCamera faceCameraComponent = instantiatedPrefab.AddComponent<FaceCamera>();
                faceCameraComponent.cameraRig = cameraRig;
            }
        }
    }
    
    
}