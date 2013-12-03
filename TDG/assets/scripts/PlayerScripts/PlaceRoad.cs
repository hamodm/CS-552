using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceRoad : MonoBehaviour {
    public GameObject road;
    
    private GameObject newRoad;
    private bool isPlacing;
    private bool isValid;
    private bool isInitialized;
    private Camera camera;
    private DrawRoad drawRoad;
    private float terrainWidth;
    private float terrainLength;
    private PlayerManager playerManager;
    private TileManager tileManager;

	// Use this for initialization
	void Start () 
    {
        isPlacing = false;
        isInitialized = false;
        isValid = false;
        camera = Camera.main;
        terrainWidth = Terrain.activeTerrain.terrainData.size.x;
        terrainLength = Terrain.activeTerrain.terrainData.size.z;
        playerManager = GetComponent<PlayerManager>();
        tileManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TileManager>();
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
	
	Vector3 FindTile(Vector3 position)
	{
		//get the tile size from the drawGrid script when it's added
		int tileSize = 50;	
		//find the tile the road should be drawn to, then move it to the middle of that tile
		position.x = tileSize * ((int) position.x / tileSize) + tileSize / 2;
		position.z = tileSize * ((int) position.z / tileSize) + tileSize / 2;
		return position;
	}

    List<int> TileNumber(Vector3 position)
    {
        List<int> tile = new List<int>();

        //get the tile size from the drawGrid script when it's added
        int tileSize = 50;
        //find the tile the road should be drawn to, then move it to the middle of that tile
        int x = (int)position.x / tileSize;
        int z = (int)position.z / tileSize;
        tile.Add(x);
        tile.Add(z);
        return tile;
    }

    //makes sure that a location is on the map
    bool InBounds(Vector3 position)
    {
        if (position.x > 0 && position.x < terrainWidth && position.z > 0 && position.z < terrainLength)
            return true;
        return false;
    }

    bool CanBePlaced(Vector3 position)
    {
        //List<int> tile = TileNumber(position);
        //if (tileManager.roads[tile[0]][tile[1]] == 0)
        //    return true;
         return false;
    }

    void Place()
    {
        Vector3 mousePosition = GetMousePosition();
        //If the beginning position for the road hasn't been selected, place it where the player clicks
        if (!isInitialized)
        {
            drawRoad.SetOrigin(playerManager.castlePosition);
            isInitialized = true;
        }
        else if (isInitialized && Input.GetMouseButtonDown(0) && InBounds(GetMousePosition()))
        {
			mousePosition = FindTile(GetMousePosition());
            drawRoad.AddVertex(mousePosition);
        }
        else if (isInitialized && InBounds(GetMousePosition()))
        {
			mousePosition = FindTile(GetMousePosition());
            drawRoad.TemporarilyAddVertex(mousePosition);
        }
        else
            drawRoad.ClearTempVertices();
    }


    public void StartPlacingRoad()
    {
        isPlacing = true;
        newRoad = Network.Instantiate(road, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
        newRoad.transform.parent = gameObject.transform;
        drawRoad = newRoad.GetComponent<DrawRoad>();
    }

    public void CreateRoad()
    {
        isPlacing = false;
        isInitialized = false;
        isValid = false;
        drawRoad.AddRoadToPlayerManager(playerManager);
    }
}
