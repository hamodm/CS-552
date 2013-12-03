using UnityEngine;
using System.Collections;

public class PlaceTower : MonoBehaviour {
    public GameObject tower;

    private PlayerManager playerManager;
    private bool isPlacing;
    private GameObject newTower;
	// Use this for initialization
	void Start () 
    {
        playerManager = GetComponent<PlayerManager>();
        isPlacing = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isPlacing)
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
        if (Input.GetMouseButtonDown(0))
        {
            isPlacing = false;
            playerManager.towers.Add(newTower);
            newTower.GetComponent<TowerManager>().isPlaced = true;
        }
        else
        {
            newTower.transform.position = GetMousePosition();
        }
    }

    //public function called by GUI button
    public void TowerPlace()
    {
        isPlacing = true;
        newTower = Network.Instantiate(tower, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
        newTower.transform.parent = gameObject.transform;
        playerManager.towers.Add(newTower);
    }
}
