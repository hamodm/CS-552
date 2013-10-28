using UnityEngine;
using System.Collections;

public class PlaceRoad : MonoBehaviour {
    public GameObject road;
    
    private GameObject newRoad;
    private bool isPlacing;
    private bool isInitialized;
    private Camera camera;
    private DrawRoad drawRoad;
    private float terrainWidth;
    private float terrain;

	// Use this for initialization
	void Start () 
    {
        isPlacing = false;
        isInitialized = false;
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isPlacing)
            Place();
	}

    Vector3 GetMousePosition()
    {

        Vector3 mousePosition = Input.mousePosition;
        Vector3 cameraPosition = Camera.main.transform.position;
        //this controls the altitude of the object, and setting it equal to the camera's height (y axis) puts it on the terrain
        mousePosition.z = cameraPosition.y;
        //get the mouse position in relation to the word instead of the screen
        return camera.ScreenToWorldPoint(mousePosition);
    }

    void Place()
    {
        //If the beginning position for the road hasn't been selected, place it where the player clicks
        if (!isInitialized && Input.GetMouseButtonDown(0))
        {
            drawRoad.SetOrigin(GetMousePosition());
            isInitialized = true;
        }
        else if(isInitialized && Input.GetMouseButtonDown(0))
        {
            drawRoad.AddVertex(GetMousePosition());
        }
        else if (isInitialized)
        {
            drawRoad.TemporarilyAddVertex(GetMousePosition());
        }
    }

    void OnGUI()
    {
        //can change to only show when the player clicks on their castle
        if (!isPlacing)
            PromptForRoadPlacement();
        else
            PromptForRoadPlacementEnd();
    }

    void PromptForRoadPlacement()
    {
        if (GUI.Button(new Rect(0, 110, 100, 100), "Place Road"))
        {
            isPlacing = true;
            newRoad = Network.Instantiate(road, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
            drawRoad = newRoad.GetComponent<DrawRoad>();
        }
    }

    void PromptForRoadPlacementEnd()
    {
        if (GUI.Button(new Rect(0, 110, 100, 100), "Stop Placing Road"))
        {
            isPlacing = false;
            isInitialized = false;
        }
    }
}
