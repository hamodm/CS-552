using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    int resourceCount = 3;
    TileManager tileManager;

    bool isFinishedCreating;
    GameObject newResource;

    public GameObject wood;
    public GameObject food;
    public GameObject stone;
	
	enum Resources { Wood, Food, Stone };
	// Use this for initialization
	void Start ()
    {
        newResource = null;
        tileManager = GetComponent<TileManager>();
        CreateResource((int)Resources.Wood);
		CreateResource((int)Resources.Food);
		CreateResource((int)Resources.Stone);
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }

    bool TileIsAvailable(int x, int z)
    {
		//make sure the current tile is available, and there are no adjacent tiles with a resource
		for(int i = -1; i <= 1; i++)
		{
			if(x+i >= 0 && x+i < tileManager.terrainWidth)
			{
				for(int j = -1; j <= 1; j++)
				{
					if(z+j >= 0 && z+j < tileManager.terrainLength)
					{
        				if (tileManager.resources[x+i, z+j] == 1)
						{
							Vector3 p = FindTilePosition(x, z);
							print ("Collision at " + p.x + "," + p.z + ", finding new tile for resource");
							return false;
						}
					}
				}
			}
		}
            
		return true;
    }

    Vector3 FindTilePosition(int x, int z)
    {
        Vector3 position;
        //get the tile size from the drawGrid script when it's added
        int tileSize = tileManager.tileSize;
        //find the tile the resource should be drawn to, then move it to the middle of that tile
        position.x = tileSize * x  + tileSize / 2;
        position.z = tileSize * z  + tileSize / 2;
        position.y = 0;
        return position;
    }

    void CreateResource(int resourceType)
    {
        int x = 0;
        int z = 0;
        int count = 0;
        while (count < resourceCount)
        {
            x = Random.Range(0, tileManager.terrainWidth);
            z = Random.Range(0, tileManager.terrainLength);
            if (TileIsAvailable(x, z))
            {
				tileManager.resources[x,z] = 1;
				switch(resourceType)
				{
				case (int)Resources.Wood:
					CreateWoodInstance(FindTilePosition(x, z));
					break;
				case (int)Resources.Food:
					CreateFoodInstance(FindTilePosition(x, z));
					break;
				case (int)Resources.Stone:
					CreateStoneInstance(FindTilePosition(x, z));
					break;
				default:
					break;
				}
                count++;
            }
        }
    }

    void CreateWoodInstance(Vector3 position)
    {
        print("Creating tree at " + position.x + "," + position.z);
        newResource = Network.Instantiate(wood, position, Quaternion.identity, 0) as GameObject;
    }
	
	void CreateFoodInstance(Vector3 position)
	{
        print("Creating food at " + position.x + "," + position.z);
        newResource = Network.Instantiate(food, position, Quaternion.identity, 0) as GameObject;
	}
	
	void CreateStoneInstance(Vector3 position)
	{
        print("Creating rock at " + position.x + "," + position.z);
        newResource = Network.Instantiate(stone, position, Quaternion.identity, 0) as GameObject;
	}
	
}
