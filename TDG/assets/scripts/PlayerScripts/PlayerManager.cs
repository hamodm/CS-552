using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    public bool hasPlacedCastle;
    public Vector3 castlePosition;
    //holds the list of all roads, which are each a list of vertices
    public List<List<Vector3>> roads;
    public List<List<Vector3>> resourceRoads;
    public int activeRoad;
    public List<LineRenderer> roadLines;
	public List<GameObject> units;
    public List<GameObject> towers;
    public List<GameObject> movingUnits;
    public int gold;
    public int wood;
    public int stone;

    private Color playerColor;
    private Color activeRoadColor;
    private Color highlightedRoadColor;
	// Use this for initialization
	void Start () 
    {
        hasPlacedCastle = false;
        castlePosition = new Vector3(0, 0, 0);
        roads = new List<List<Vector3>>();
        resourceRoads = new List<List<Vector3>>();
        activeRoad = 0;
        roadLines = new List<LineRenderer>();
        units = new List<GameObject>();
        towers = new List<GameObject>();
        movingUnits = new List<GameObject>();
        gold = 100;
        wood = 0;
        stone = 0;

        
        playerColor = Color.blue;
        activeRoadColor = Color.yellow;
        highlightedRoadColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void SetPlayerColor(Color c)
    {
        playerColor = c;
    }

    public void MakeRoadActive(int i)
    {
        roadLines[activeRoad].renderer.material.color = playerColor;
        roadLines[i].renderer.material.color = activeRoadColor;
        activeRoad = i;
    }

    public void MoveUnit(int unitNumber)
    {
        units[unitNumber].GetComponent<UnitManager>().MoveAlongRoad(activeRoad);
    }
}
