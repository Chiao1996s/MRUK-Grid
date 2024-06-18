using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction;

public class RoomFloorTiles : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;  // Assign in Inspector
    [SerializeField]
    private GameObject menuPrefab;  // Assign in Inspector

    private MRUKRoom roomInstance;

    void Start()
    {
        GenerateTiles();
        MountMenuToWall();
    }

    public void GenerateTiles()
    {
        roomInstance = MRUK.Instance.GetCurrentRoom()?.GetComponent<MRUKRoom>();
        if (roomInstance == null)
        {
            Debug.LogError("Room instance or MRUKRoom component is not set.");
            return;
        }

        if (tilePrefab == null || menuPrefab == null)
        {
            Debug.LogError("Tile or Menu prefab is not assigned.");
            return;
        }
        
        // Get tilePrefab size
        MeshRenderer meshRenderer = tilePrefab.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("Tile prefab does not have a MeshRenderer component.");
            return;
        }
        Vector3 tileSize = meshRenderer.bounds.size;

        // Get floor points
        List<Vector3> floorPoints = roomInstance.GetRoomOutline();
        if (floorPoints == null || floorPoints.Count == 0)
        {
            Debug.LogError("No floor points provided.");
            return;
        }

        // Calculate bounding box
        Vector3 min = floorPoints[0];
        Vector3 max = floorPoints[0];

        foreach (var point in floorPoints)
        {
            min = Vector3.Min(min, point);
            max = Vector3.Max(max, point);
        }

        // Generate tiles within the bounding box
        int row = 0;
        for (float x = min.x; x <= max.x; x += tileSize.x, row++)
        {
            int column = 0;
            for (float z = min.z; z <= max.z; z += tileSize.z, column++)
            {
                Vector3 tilePosition = new Vector3(x, min.y, z);
                GameObject spawnedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                spawnedTile.name = $"Tile ({row},{column})";
                spawnedTile.SetActive(true); // Ensure tile object is active
            }
        }
    }

    public void MountMenuToWall()
    {
        roomInstance = MRUK.Instance.GetCurrentRoom()?.GetComponent<MRUKRoom>();
        if (roomInstance == null)
        {
            Debug.LogError("Room instance or MRUKRoom component is not set.");
            return;
        }

        if (tilePrefab == null || menuPrefab == null)
        {
            Debug.LogError("Tile or Menu prefab is not assigned.");
            return;
        }
        
        // Get key wall and its scale
        var keyWall = roomInstance.GetKeyWall(out Vector2 wallScale, tolerance: 1.0f);
        if (keyWall == null)
        {
            Debug.LogError("Key wall not found.");
            return;
        }

        // Calculate center position and rotation to face the room
        Vector3 wallCenter = keyWall.transform.position 
                             + keyWall.transform.forward * wallScale.y * 0.5f 
                             + keyWall.transform.right * wallScale.x * 0.5f;
        Quaternion rotationToFaceRoom = Quaternion.LookRotation(-keyWall.transform.forward);

        // Instantiate the menu prefab at the adjusted position with the correct rotation
        Instantiate(menuPrefab, wallCenter, rotationToFaceRoom);
    }
}