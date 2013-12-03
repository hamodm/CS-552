using UnityEngine;
using System.Collections;

public class InitializeManagers : MonoBehaviour 
{
	GameObject manager;

	// Use this for initialization
	void Start () 
	{
		manager = GameObject.FindGameObjectWithTag("GameManager");
		manager.GetComponent<TileManager>().enabled = true;
		manager.GetComponent<ResourceManager>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
