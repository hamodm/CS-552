using UnityEngine;
using System.Collections;

public class CreateUnit : MonoBehaviour {
	public GameObject unit;
	public int maxUnits = 5;
	public float unitSpeed = 5.0f;
	
	private int unitCount;
	private GameObject newUnit;
	private PlayerManager playerManager;
	
	// Use this for initialization
	void Start () 
	{
		unitCount = 0;
		playerManager = GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.R))
			DecrHealth();
		else
			StopDecrHealth();
	}
	
	void OnGUI()
	{
		if(unitCount < maxUnits)
			PromptForUnitCreation();
	}
	
	void PromptForUnitCreation()
	{
		if (GUI.Button(new Rect(0, 70, 100, 20), "Create Unit"))
        {
			newUnit = Network.Instantiate(unit, playerManager.castlePosition, Quaternion.identity, 0) as GameObject;
			newUnit.transform.parent = gameObject.transform;
			playerManager.units.Add(newUnit);
        }
	}
	
	public void DecrHealth()
	{
		unit.renderer.material.color = Color.red;	
	}
	
	public void StopDecrHealth()
	{
		unit.renderer.material.color = Color.white;	
	}
	
	public void MoveAlongRoad(DrawRoad road)
	{
		for(int i = 0; i < road.GetVertices().Count; i++)
			MoveTo(road.GetVertices()[i]);	
	}
	
	void MoveTo(Vector3 destination)
	{
		return;	
	}
}
