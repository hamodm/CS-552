using UnityEngine;
using System.Collections;

public enum Scenes { Loading, MainMenu, Game };
public class SceneManager : MonoBehaviour
{

    Scenes currScene;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currScene = Scenes.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GoToNextScene()
    {
        switch (currScene)
        {
            case Scenes.Loading:
                currScene = Scenes.MainMenu;
                Application.LoadLevel("Main");
                break;
            case Scenes.MainMenu:
                currScene = Scenes.Game;
                Application.LoadLevel("TestScene");
                break;
            case Scenes.Game:
                currScene = Scenes.MainMenu;
                Application.LoadLevel("Main");
                break;
            default:
                currScene = Scenes.MainMenu;
                Application.LoadLevel("Main");
                break;
        }

    }
}