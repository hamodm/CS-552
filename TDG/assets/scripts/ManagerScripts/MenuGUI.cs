using UnityEngine;
using System.Collections;
using System;

public class MenuGUI : MonoBehaviour
{

    public string ipAddress = "127.0.0.1";
    public string portNumber = "22222";

    enum Scenes { MainMenu, Game };

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (Application.loadedLevel == (int)Scenes.MainMenu)
        {
            MainMenu();
        }
        if (Application.loadedLevel == (int)Scenes.Game)
        {
            GameMenu();
        }

    }

    void MainMenu()
    {
        float stdW = 100;
        float stdH = 20;
        float currY = 0;
        if (GUI.Button(new Rect(0, currY, stdW, stdH), "Host Game"))
        {
            SendMessage("StartServer");
            SendMessage("GoToNextScene");
        }
        currY += stdH;

        GUI.Label(new Rect(0, currY, stdW, stdH), "IP Address");
        currY += stdH;

        ipAddress = GUI.TextArea(new Rect(0, currY, stdW, stdH), ipAddress);
        currY += stdH;

        portNumber = GUI.TextArea(new Rect(0, currY, stdW, stdH), portNumber);
        currY += stdH;


        if (GUI.Button(new Rect(0, currY, stdW, stdH), "Connect to Server"))
        {
            return;
            /*
            ConnectionInfo info = new ConnectionInfo();
            info.ipAddress = ipAddress;
            int iPortNum = 0;
            if (Int32.TryParse(portNumber, out iPortNum))
            {
                info.port = iPortNum;
            }

            SendMessage("ConnectToServer", info);
            */
        }
    }
    void GameMenu()
    {
        if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Start"))
        {
            //SendMessage("SpawnPlayer", true);
            //GameObject.Find("TempCamera").SetActive(false);
            enabled = false;
            return;
        }
    }
}

