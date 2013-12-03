using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
    Color foodColor = Color.green;
    int resourceType = 2;
	// Use this for initialization
	void Start () 
    {
        gameObject.renderer.material.color = foodColor;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
