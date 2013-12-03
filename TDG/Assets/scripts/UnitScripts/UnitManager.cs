using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {
    public float moveSpeed = 1;
    private PlayerManager playerManager;
    private bool isMoving;
    public bool canMove;
    private List<Vector3> destinations;
	private int health = 10;
    //0 for worker, 1 for attacker
    //plan to add more unit types eventually
    private int unitType;
	// Use this for initialization
	void Start () 
    {
        playerManager = gameObject.transform.parent.GetComponent<PlayerManager>();
        isMoving = false;
        canMove = true;
        destinations = new List<Vector3>();
        unitType = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (health <= 0)
		{
			playerManager.units.Remove(gameObject);
			playerManager.movingUnits.Remove(gameObject);
			Destroy(gameObject);	
		}
        if (isMoving && destinations.Count > 0)
            Move();
        else if (isMoving)
        {
            isMoving = false;
            playerManager.movingUnits.Remove(gameObject);
        }

	}

    void OnGUI()
    {
		try
		{
			Vector3 screenPosition = Camera.current.WorldToScreenPoint(gameObject.transform.position);	
			screenPosition.y = Screen.height - (screenPosition.y + 1);
			Rect rect = new Rect(screenPosition.x - 50, screenPosition.y - 12, 100, 24);
			GUI.Box(new Rect(screenPosition.x-10,screenPosition.y-40,100,20),getHealth()+"/"+10);
		}
		catch(System.Exception e)
		{
		}
		
    }

	public void OnTriggerEnter(Collider col)
	{
        if(col.gameObject.tag == "Arrow")
		{
			DecrHealth ();	
		}
    }
	/*
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Arrow")
		{
			DecrHealth ();
		}
	}*/

    public void DecrHealth()
    {
		this.health -= 1;
        gameObject.renderer.material.color = Color.red;
    }

    public void StopDecrHealth()
    {
        gameObject.renderer.material.color = Color.white;
    }

    public void MoveAlongRoad(int road)
    {
        print(playerManager.roads[road].Count);
        for (int i = 0; i < playerManager.roads[road].Count; i++)
        {
            print(i);
            print(playerManager.roads[road][i].z);
            isMoving = true;
            playerManager.movingUnits.Add(gameObject);
            destinations.Add(playerManager.roads[road][i]);
        }
    }

    float GetDistance(Vector3 origin, Vector3 destination)
    {
        return (float)Math.Sqrt(Math.Pow(origin.x - destination.x, 2) + Math.Pow(origin.y - destination.y, 2) + Math.Pow(origin.z - destination.z, 2));
    }

    void Move()
    {
        print("From: " + gameObject.transform.position.x + ", " + gameObject.transform.position.z + " To: " + destinations[0].x + ", " + destinations[0].z);
        float rate = moveSpeed / GetDistance(gameObject.transform.position, destinations[0]);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, destinations[0], rate);
        if (gameObject.transform.position == destinations[0])
            destinations.Remove(destinations[0]);
        canMove = false;
        print(gameObject.transform.position.z);
    }
	
	public float getHealth()
	{
		return this.health;	
	}
}
