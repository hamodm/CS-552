using UnityEngine;
using System.Collections;

public class PlaceCastle : MonoBehaviour {

    public GameObject castle;
	GameObject newCastle;
    int castleCount;
	bool isPlacing;

    private PlayerManager player;

    // Use this for initialization
    void Start()
    {
		castleCount = 0;
		isPlacing = false;
        player = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
		if(isPlacing)
        	Place();
    }

    Vector3 GetMousePosition()
    {
        //while the player is placing the castle, update its location so that the player can see where it will be placed
        Vector3 mousePosition = Input.mousePosition;
        Vector3 cameraPosition = Camera.main.transform.position;
        //this controls the altitude of the object, and setting it equal to the camera's height (y axis) puts it on the terrain
        mousePosition.z = cameraPosition.y;
        //get the mouse position in relation to the word instead of the screen
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
	
    void Place()
    {
		
        //each player should only have one castle
        if(Input.GetMouseButtonDown(0) && castleCount == 0)
		{
			castleCount++;
			isPlacing = false;
            player.hasPlacedCastle = true;
            player.castlePosition = newCastle.transform.position;
		}
		else if (castleCount == 0)
        {
			newCastle.transform.position = GetMousePosition();
        }
    }
	
	void OnGUI()
	{	
		//if the player hasnt placed a castle yet and isn't currently placing one, then display a button prompting them to place one
		if(!isPlacing && castleCount == 0)
			PromptForCastlePlacement();
	}
	
	void PromptForCastlePlacement()
	{
		if (GUI.Button(new Rect(0, 0, 100, 20), "Place Castle"))
		{
			isPlacing = true;
			newCastle = Network.Instantiate(castle, new Vector3(0,0,0), Quaternion.identity, 0) as GameObject;
            newCastle.transform.parent = gameObject.transform;
		}
	}
}
