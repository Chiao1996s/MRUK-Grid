using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class GetFloorPlaneBoundary2D : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    private float tileSize;

    public void GetMyFloor()
    {
        var room = MRUK.Instance.GetCurrentRoom();

        // 获取地板锚点
        var floorAnchor = room.FloorAnchor;
        Debug.Log("Yes, Got the floor!");

        if (floorAnchor.PlaneRect.HasValue)
        {
            List<Vector2> planeBoundary = floorAnchor.PlaneBoundary2D;  // 這裡取得的是 Vector2 的點位資料
            // 處理2D邊界點
            List<Vector3> boundaryPoints = new List<Vector3>();  //轉換成 Vector3 的點位資料, 垂直高度用 floorAnchor的
            foreach (var point in planeBoundary)
            {
                Vector3 point3D = new Vector3(point.x, floorAnchor.transform.position.y, point.y);
                boundaryPoints.Add(point3D);
                Debug.Log($"2D Boundary Point: {point}");
            }

            // 計算包含所有點的最小邊界長方形
            Bounds bounds = GetBoundingBox(boundaryPoints);

            Renderer renderer = tilePrefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                tileSize = renderer.bounds.size.x; // 假設瓷磚是正方形或矩形，這裡選擇 x 軸的尺寸作為 tileSize
            }

            // 在邊界長方形內生成網格瓷磚
            for (float x = bounds.min.x; x < bounds.max.x; x += tileSize)
            {
                for (float z = bounds.min.z; z < bounds.max.z; z += tileSize)
                {
                    Vector3 tilePosition = new Vector3(x + tileSize / 2, bounds.min.y, z + tileSize / 2);
                    var spawnedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {z}";

                    // 根據需要初始化瓷磚屬性
                    var isOffset = ((int)(x / tileSize) % 2 == 0 && (int)(z / tileSize) % 2 != 0) || ((int)(x / tileSize) % 2 != 0 && (int)(z / tileSize) % 2 == 0);
                    spawnedTile.Init(isOffset);
                }
            }
        }
    }

    Bounds GetBoundingBox(List<Vector3> points)
    {
        if (points.Count == 0)
        {
            return new Bounds();
        }

        float minX = float.MaxValue, minZ = float.MaxValue;
        float maxX = float.MinValue, maxZ = float.MinValue;
        float y = points[0].y;

        foreach (Vector3 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.z < minZ) minZ = point.z;
            if (point.x > maxX) maxX = point.x;
            if (point.z > maxZ) maxZ = point.z;
        }

        Vector3 min = new Vector3(minX, y, minZ);
        Vector3 max = new Vector3(maxX, y, maxZ);

        // 打印 min 和 max 值
        Debug.Log($"Min bounds: {min}");
        Debug.Log($"Max bounds: {max}");

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
    
}


        
        