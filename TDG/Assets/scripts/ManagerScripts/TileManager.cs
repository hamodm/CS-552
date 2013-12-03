using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
    public int terrainWidth;
    public int terrainLength;

    //stores the number of castles or resources on the tile
    public int[,] resources;
    //stores the number of roads on the tile
    public int[,] roads;
    //stores the number of towers on the tile
    public int[,] towers;

    public int maxResources = 1;
    public int maxRoads = 1;
    public int maxTowers = 2;

    public int tileSize = 50;
	// Use this for initialization
	void Start () 
    {
        terrainWidth = 10;
        terrainLength = 10;
        resources = new int[terrainWidth, terrainLength];
        roads = new int[terrainWidth, terrainLength];
        towers = new int[terrainWidth, terrainLength];
	}

    // Update is called once per frame
    void Update() 
    {
	
	}
}
