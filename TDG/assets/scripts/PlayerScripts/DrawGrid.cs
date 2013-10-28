using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawGrid : MonoBehaviour
{
    public int gridSize = 50;

    private LineRenderer line;
    private Camera camera;
    private List<LineRenderer> lines;
    void Start()
    {
        camera = Camera.main;
        line = GetComponent<LineRenderer>();
        line.SetWidth(0.2f, 0.2f);

        line.SetPosition(0, new Vector3(10, 0, 10));
        line.SetPosition(1, new Vector3(30, 0, 30));
        line.SetPosition(0, new Vector3(30, 0, 30));
        line.SetPosition(1, new Vector3(30, 0, 50));
    }
}