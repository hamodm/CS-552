using UnityEngine;
using System.Collections;

//this class will be applied to the camera itself
public class CameraController : MonoBehaviour {
    public float moveSpeed = 50;
	public float maxCamHeight = 250;
	public float minCamHeight = 20;

    private int numberOfUnitTypes;

    //other scripts necessary
    private PlaceCastle castlePlacer;
    private PlaceRoad roadPlacer;
    private CreateUnit unitCreator;
    private PlaceTower towerPlacer;
    private PlayerManager playerManager;

    //positions for GUI buttons
    private int buttonWidth;
    private int buttonHeight;
    private int castleX;
    private int roadsX;
    private int unitX;
    private int towerX;

    //y position for top GUI options
    private int topLayer;

    //bools to handle which options to show
    bool isManagingCastle;
    bool isPlacingCastle;
    bool isManagingRoad;
    bool isPlacingRoad;
    bool isSelectingRoad;
    bool isManagingUnit;
    bool isCreatingUnit;
    bool isMovingUnit;
    bool isManagingTower;
    bool isPlacingTower;

    // Use this for initialization
    void Start()
    {
        numberOfUnitTypes = 2;

        castlePlacer = transform.parent.GetComponent<PlaceCastle>();
        roadPlacer = transform.parent.GetComponent<PlaceRoad>();
        unitCreator = transform.parent.GetComponent<CreateUnit>();
        towerPlacer = transform.parent.GetComponent<PlaceTower>();
        playerManager = transform.parent.GetComponent<PlayerManager>();

        buttonWidth = Screen.width / 4;
        buttonHeight = 30;

        castleX = 0;
        roadsX = castleX + buttonWidth;
        unitX = roadsX + buttonWidth;
        towerX = unitX + buttonWidth;

        topLayer = Screen.height - buttonHeight;

        isManagingCastle = false;
        isPlacingCastle = false;
        isManagingRoad = false;
        isPlacingRoad = false;
        isSelectingRoad = false;
        isManagingUnit = false;
        isCreatingUnit = false;
        isMovingUnit = false;
        isManagingTower = false;
        isPlacingTower = false;
    }

    // Update is called once per frame
    void Update()
    {
        KeyMove();
    }
    void KeyMove()
    {
        //Because of its rotation, transform.up now moves the camera forward
        transform.Translate(transform.up * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
        transform.Translate(transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
		if (Input.GetKey(KeyCode.RightShift) && transform.position.y > minCamHeight)
			transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
		else if(Input.GetKey(KeyCode.LeftShift) && transform.position.y < maxCamHeight)
			transform.Translate(transform.forward * (-moveSpeed) * Time.deltaTime, Space.World);
    }

    void OnGUI()
    {
        //GUI options for castle management
        ManageCastleGUI();
        if (isManagingCastle && isPlacingCastle)
            PromptForCastlePlacement();

        //GUI options for road management
        ManageRoadsGUI();
        if (isManagingRoad && !isPlacingRoad && playerManager.hasPlacedCastle && !isSelectingRoad)
        {
            PromptForRoadPlacement();
            PromptForRoadSelection();
        }
        else if (isManagingRoad && isPlacingRoad && !isSelectingRoad)
            PromptForRoadCreation();
        else if (isManagingRoad && isSelectingRoad)
            ChooseActiveRoad();

        //GUI options for unit management
        ManageUnitsGUI();
        if (isManagingUnit && !isCreatingUnit && !isMovingUnit)
        {
            PromptForUnitCreation();
            if (playerManager.roads.Count > 0)
                PromptForUnitMovement();
        }
        else if (isManagingUnit && isCreatingUnit)
            UnitCreation();
        else if (isManagingUnit && isMovingUnit)
            MoveUnit();


        //GUI options for tower management
        ManageTowersGUI();
        if (isManagingTower)
            CreateTower();
    }

    //GUI options for managing the castle
    void ManageCastleGUI()
    {
        if (GUI.Button(new Rect(castleX, topLayer, buttonWidth, buttonHeight), "Manage Castle"))
        {
            isManagingCastle = !isManagingCastle;
            isManagingRoad = false;
            isManagingUnit = false;
            isManagingTower = false;

            if (!playerManager.hasPlacedCastle)
                isPlacingCastle = true;
        }
    }

    void PromptForCastlePlacement()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(castleX, positionY, buttonWidth, buttonHeight), "Place Castle"))
        {
            castlePlacer.StartPlacingCastle();
            isPlacingCastle = false;
        }
    }

    //GUI options for managing roads
    void ManageRoadsGUI()
    {
        if (GUI.Button(new Rect(roadsX, topLayer, buttonWidth, buttonHeight), "Manage Roads"))
        {
            isManagingRoad = !isManagingRoad;
            isManagingCastle = false;
            isManagingUnit = false;
            isSelectingRoad = false;
            isManagingTower = false;

            isSelectingRoad = false;
            isPlacingRoad = false;
        }
    }

    void PromptForRoadPlacement()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(roadsX, positionY, buttonWidth, buttonHeight), "Place Road"))
        {
            isPlacingRoad = true;
            isSelectingRoad = false;
            roadPlacer.StartPlacingRoad();
        }
    }

    void PromptForRoadCreation()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(roadsX, positionY, buttonWidth, buttonHeight), "Create Road"))
        {
            isPlacingRoad = false;
            roadPlacer.CreateRoad();
        }
    }

    void PromptForRoadSelection()
    {
        int positionY = topLayer - 2 * buttonHeight;
        if (playerManager.roads.Count > 0)
        {
            if (GUI.Button(new Rect(roadsX, positionY, buttonWidth, buttonHeight), "Change Active Road"))
            {
                isSelectingRoad = true;
            }
        }
    }

    void ChooseActiveRoad()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(roadsX, positionY, buttonWidth, buttonHeight), "Cancel"))
        {
            isSelectingRoad = false;
        }

        for (int i = 0; i < playerManager.roads.Count; i++)
        {
            positionY -= buttonHeight;
            if (GUI.Button(new Rect(roadsX, positionY, buttonWidth, buttonHeight), "Make Road " + i + " Active"))
            {
                playerManager.MakeRoadActive(i);
            }
        }
    }

    //GUI options for unit management
    void ManageUnitsGUI()
    {
        if (GUI.Button(new Rect(unitX, topLayer, buttonWidth, buttonHeight), "Manage Units"))
        {
            isManagingUnit = !isManagingUnit;
            isManagingCastle = false;
            isManagingRoad = false;
            isManagingTower = false;

            isMovingUnit = false;
            isCreatingUnit = false;
        }
    }

    void PromptForUnitCreation()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Create Unit"))
        {
            isCreatingUnit = true;
        }
    }

    void UnitCreation()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Cancel"))
        {
            isCreatingUnit = false;
        }

        for (int i = 0; i < numberOfUnitTypes; i++)
        {
            positionY -= buttonHeight;
            if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Create Unit Type " + i))
            {
                unitCreator.Create(i);
            }
        }
    }

    void PromptForUnitMovement()
    {
        int positionY = topLayer - 2 * buttonHeight;
        if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Move Unit"))
        {
            isMovingUnit = true;
        }
    }

    void MoveUnit()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Cancel"))
        {
            isMovingUnit = false;
        }
        for (int i = 0; i < playerManager.units.Count; i++)
        {
            if (playerManager.units[i].GetComponent<UnitManager>().canMove)
            {
                positionY -= buttonHeight;
                if (GUI.Button(new Rect(unitX, positionY, buttonWidth, buttonHeight), "Move Unit " + i))
                {
                    playerManager.MoveUnit(i);
                }
            }
        }
    }

    //GUI options for tower management
    void ManageTowersGUI()
    {
        if (GUI.Button(new Rect(towerX, topLayer, buttonWidth, buttonHeight), "Manage Towers"))
        {
            isManagingTower = !isManagingTower;
            isManagingCastle = false;
            isManagingRoad = false;
            isManagingUnit = false;
        }
    }

    void CreateTower()
    {
        int positionY = topLayer - buttonHeight;
        if (GUI.Button(new Rect(towerX, positionY, buttonWidth, buttonHeight), "Create Towers"))
        {
            towerPlacer.TowerPlace();
        }
    }
}
