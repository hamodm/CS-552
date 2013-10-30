using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void SpawnPlayer(bool isMine)
    {
        GameObject newPlayer = Network.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity, 0) as GameObject;
        if (isMine)
            EnablePlayerController(newPlayer);
    }

    void EnablePlayerController(GameObject enabledPlayer)
    {
        enabledPlayer.transform.Find("Main Camera").gameObject.SetActive(true);
        enabledPlayer.GetComponent<PlaceCastle>().enabled = true;
        enabledPlayer.GetComponent<PlaceRoad>().enabled = true;
        enabledPlayer.GetComponent<PlayerManager>().enabled = true;
    }
}
