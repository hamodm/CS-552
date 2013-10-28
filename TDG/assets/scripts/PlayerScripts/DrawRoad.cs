﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawRoad : MonoBehaviour 
{
    private LineRenderer line;
    private int counter;
    private List<Vector3> vertices;

	// Use this for initialization
	void Start () 
    {
        line = GetComponent<LineRenderer>();
        line.SetWidth(0.5f, 0.5f);
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
        vertices = new List<Vector3>();
        line.SetPosition(0, origin);
        vertices.Add(origin);
        counter = 1;
    }

    public void AddVertex(Vector3 destination)
    {
        line.SetVertexCount(counter + 1);
        line.SetPosition(counter, destination);
        counter++;
    }

    public void TemporarilyAddVertex(Vector3 destination)
    {
        print("TemporarilyAddVertex(" + destination.x + ", " + destination.y + ", " + destination.z + ")");
        line.SetVertexCount(counter + 1);
        line.SetPosition(counter, destination);
    }
}