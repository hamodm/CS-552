using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawRoad : MonoBehaviour 
{
    private LineRenderer line;
    private int counter;
    private List<Vector3> vertices;

    private float terrainWidth;
    private float terrainLength;

	// Use this for initialization
	void Start () 
    {
        terrainWidth = Terrain.activeTerrain.terrainData.size.x;
        terrainLength = Terrain.activeTerrain.terrainData.size.z;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void FindDestination()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 cameraPosition = Camera.main.transform.position;
        //this controls the altitude of the object, and setting it equal to the camera's height (y axis) puts it on the terrain
        mousePosition.z = cameraPosition.y;
        //get the mouse position in relation to the word instead of the screen
        Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);
        line.SetPosition(counter, destination);
    }

    public void SetOrigin(Vector3 origin)
    {
        line = GetComponent<LineRenderer>();
		line.SetWidth(1f, 1f);
        origin.y = 0.1f;
        vertices = new List<Vector3>();
        line.SetPosition(0, origin);
        vertices.Add(origin);
        counter = 1;
    }

    bool InBounds(Vector3 position)
    {
        if (position.x > 0 && position.x < terrainWidth && position.z > 0 && position.z < terrainLength)
            return true;
        return false;
    }

    public void AddVertex(Vector3 destination)
    {
        destination.y = 0.1f;
        line.SetVertexCount(counter + 1);
        if (InBounds(destination))
        {
            line.SetPosition(counter, destination);
            vertices.Add(destination);
            print("Added to vertices: " + destination.x + ", " + destination.z);
            counter++;
        }
        else
            line.SetVertexCount(counter);

    }

    public void ClearTempVertices()
    {
        line.SetVertexCount(counter);
    }

    public void TemporarilyAddVertex(Vector3 destination)
    {
        if (InBounds(destination))
        {
            destination.y = 0.1f;
            line.SetVertexCount(counter + 1);
            line.SetPosition(counter, destination);
        }
        else
            line.SetVertexCount(counter);
    }
	
	public void AddRoadToPlayerManager(PlayerManager playerManager)
	{
		playerManager.roads.Add(vertices);
        playerManager.roadLines.Add(line);
	}
	
	public Vector3 LastVertex()
	{
		return vertices[vertices.Count - 1];
	}
	
	public List<Vector3> GetVertices()
	{
		List<Vector3> returnList = vertices;
		return returnList;
	}
}
