using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
	Color stoneColor = Color.gray;
    int resourceType = 1;
	// Use this for initialization
	void Start () 
    {
        gameObject.renderer.material.color = stoneColor;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
