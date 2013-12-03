using UnityEngine;
using System.Collections;

public class PlaceCastle : MonoBehaviour {

    public GameObject castle;
	GameObject newCastle;
    int castleCount;
	bool isPlacing;

    private PlayerManager playerManager;
	private TileManager tileManager;
    
    // Use this for initialization
    void Start()
    {
		castleCount = 0;
		isPlacing = false;
        playerManager = GetComponent<PlayerManager>();
		tileManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TileManager>();
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

    Vector3 FindTile(Vector3 position)
    {
        int tileSize = 50;
        position.x = tileSize * ((int)position.x / tileSize) + tileSize / 2;
        position.z = tileSize * ((int)position.z / tileSize) + tileSize / 2;
        return position;
    }
	
	bool TileIsAvailable(Vector3 p)
	{
		int x = (int)p.x /tileManager.tileSize;
		int z = (int)p.z / tileManager.tileSize;
		if(tileManager.resources[x, z] == 1)
			return false;
		else
			return true;
	}
	
    void Place()
    {
		
        //each player should only have one castle
        if(Input.GetMouseButtonDown(0) && castleCount == 0 && TileIsAvailable(newCastle.transform.position))
		{
			castleCount++;
			isPlacing = false;
            playerManager.hasPlacedCastle = true;
            playerManager.castlePosition = newCastle.transform.position;
		}
		else if (castleCount == 0)
        {
			newCastle.transform.position = FindTile(GetMousePosition());
        }
    }
	
	void OnGUI()
	{	
		//if the player hasnt placed a castle yet and isn't currently placing one, then display a button prompting them to place one
		//if(!isPlacing && castleCount == 0)
		//	PromptForCastlePlacement();
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

    public void StartPlacingCastle()
    {
        isPlacing = true;
        newCastle = Network.Instantiate(castle, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
        newCastle.transform.parent = gameObject.transform;
    }
}
