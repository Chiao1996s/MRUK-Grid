using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;


public class GetRoomOutline : MonoBehaviour
{
    public GameObject tilePrefab;  // 在Inspector中分配瓦片预制件
    private List<GameObject> spawnedTiles = new List<GameObject>();
    

 

    public void GenerateTiles()
    {
        var room = MRUK.Instance.GetCurrentRoom();
        if (room == null)
        {
            Debug.LogError("Room instance is not set.");
            return;
        }

        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab is not assigned.");
            return;
        }

        // 获取tilePrefab的尺寸
        MeshRenderer meshRenderer = tilePrefab.GetComponent<MeshRenderer>();
        Vector3 tileSize = meshRenderer.bounds.size;
        List<Vector3> floorPoints = room.GetRoomOutline();
        if (floorPoints == null || floorPoints.Count == 0)
        {
            Debug.LogError("No floor points provided.");
            return;
        }

        // 计算包围盒
        Vector3 min = floorPoints[0];
        Vector3 max = floorPoints[0];

        foreach (var point in floorPoints)
        {
            if (point.x < min.x) min.x = point.x;
            if (point.y < min.y) min.y = point.y;
            if (point.z < min.z) min.z = point.z;

            if (point.x > max.x) max.x = point.x;
            if (point.y > max.y) max.y = point.y;
            if (point.z > max.z) max.z = point.z;
        }

        int row = 0;
        for (float x = min.x; x <= max.x; x += tileSize.x, row++)
        {
            int column = 0;
            for (float z = min.z; z <= max.z; z += tileSize.z, column++)
            {
                Vector3 tilePosition = new Vector3(x, min.y, z);
                GameObject spawnedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                spawnedTile.name = $"Tile ({row},{column})";
                
                // 确保瓦片对象被激活
                spawnedTile.SetActive(true); 
                spawnedTiles.Add(spawnedTile);
                
            }
        }
    }
    
    public void ClearTiles()
    {
        foreach (GameObject tile in spawnedTiles)
        {
            Destroy(tile);
        }
        spawnedTiles.Clear();
    }
    
}


