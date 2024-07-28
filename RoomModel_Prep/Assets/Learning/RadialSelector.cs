using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSelector : MonoBehaviour
{
    [Range(2,10)]public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnRadialPart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRadialPart()
    {
        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = i * 360 / (float)numberOfRadialPart;  // was show: possible loss of fraction, so I add (float) 
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRaidialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRaidialPart.transform.position = radialPartCanvas.position;
            spawnedRaidialPart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedRaidialPart.GetComponent<Image>().fillAmount = 1 / (float)numberOfRadialPart; // remember to add UI 
            


        }
    }
}
