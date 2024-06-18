using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.UI;


public class MountToWall : MonoBehaviour
{
    public GameObject menuPrefab;
    public RayManipulation rayManipulation; // Reference to the RayManipulation script
    public GetRoomOutline roomOutline; 

    public void MountMenuToWall()
    {
        // Get the current room instance
        var room = MRUK.Instance.GetCurrentRoom();
        if (room == null)
        {
            Debug.LogError("Current room not found");
            return;
        }

        // Get the MRUKRoom component from the current room
        MRUKRoom roomInstance = room.GetComponent<MRUKRoom>();
        if (roomInstance == null)
        {
            Debug.LogError("MRUKRoom component not found on the current room");
            return;
        }

        // Get the key wall and its scale
        var keyWall = roomInstance.GetKeyWall(out Vector2 wallScale, tolerance: 1.0f);
        if (keyWall == null)
        {
            Debug.LogError("Key wall not found");
            return;
        }

        // Calculate the center position of the key wall
        Vector3 wallCenter = keyWall.transform.position
                             + keyWall.transform.forward * wallScale.y * 0.5f
                             + keyWall.transform.right * wallScale.x * 0.5f;

        // Calculate the rotation to face the room
        Quaternion rotationToFaceRoom = Quaternion.LookRotation(-keyWall.transform.forward);

        // Instantiate the menu prefab at the adjusted position with the correct rotation
        GameObject panelInstance = Instantiate(menuPrefab, wallCenter, rotationToFaceRoom);
        AssignButtonListeners(panelInstance);
    }

    void AssignButtonListeners(GameObject panel)
    {
        // Find the clear boxes button and assign the listener
        Button clearBoxesButton = panel.transform.Find("Canvas/ClearBoxesButton").GetComponent<Button>();
        if (clearBoxesButton != null)
        {
            clearBoxesButton.onClick.AddListener(BoxManager.Instance.ClearBoxes);
            Debug.Log("ClearBoxesButton listener assigned");
        }
        else
        {
            Debug.LogError("ClearBoxesButton not found in the panel.");
        }
        
        // Find the clear tiles button and assign the listener
        Button clearTilesButton = panel.transform.Find("Canvas/ClearTilesButton").GetComponent<Button>();
        if (clearTilesButton != null)
        {
            clearTilesButton.onClick.AddListener(roomOutline.ClearTiles);
            Debug.Log("ClearTilesButton listener assigned");
        }
        else
        {
            Debug.LogError("ClearTilesButton not found in the panel.");
        }

        // Find the change box color button and assign the listener
        Button changeBoxColorButton = panel.transform.Find("Canvas/ChangeBoxColorButton").GetComponent<Button>();
        if (changeBoxColorButton != null)
        {
            // Assuming you want to change to a specific color, e.g., red
            changeBoxColorButton.onClick.AddListener(() => BoxManager.Instance.ChangeBoxColor(Color.white));
            Debug.Log("ChangeBoxColorButton listener assigned");
        }
        else
        {
            Debug.LogError("ChangeBoxColorButton not found in the panel.");
        }
        
    }
    

}