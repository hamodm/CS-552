using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
    public float attackRadius = 10f;
    public GameObject arrowPrefab;
	public Transform spawnPoint;
    private GameObject[] players;
    private PlayerManager[] playerManagers;
    private bool isAttacking;
    private int targetCount;
	private float shotSpeed = 100f;
	private float atackTimer = 0f;
    public bool isPlaced;
	// Use this for initialization
	void Start () 
    {
        isPlaced = false;
        isAttacking = false;
        targetCount = 0;
        int i = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerManagers = new PlayerManager[players.Length];
        foreach (GameObject player in players)
        {
            print(i);
            playerManagers[i] = player.GetComponent<PlayerManager>();
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        foreach (PlayerManager manager in playerManagers)
        {
            print(manager.movingUnits.Count);
            //change to !manager.towers.Contains(gameObject)
            if (manager.movingUnits.Count > 0 && manager.towers.Contains(gameObject))
            {
                targetCount = 0;
                for (int i = 0; i < manager.movingUnits.Count; i++)
                {
					try
					{
						if (Vector3.Distance(gameObject.transform.position, manager.movingUnits[i].transform.position) < attackRadius && isPlaced)
                            Attack(manager.movingUnits[i]);
					}
					catch(System.Exception e)
					{
					}
                    
                }
            }
            else if (isAttacking)
                NotAttacking();

            if (isAttacking && targetCount == 0)
                NotAttacking();
        }
	}

    void Attack(GameObject target)
    {
		if(atackTimer > 0f)
		{
			atackTimer -= Time.deltaTime;
		}
		else{
			atackTimer = 1f;
			fireArrow(target);
	        isAttacking = true;
	        targetCount++;
	        gameObject.renderer.material.color = Color.red;
	        target.GetComponent<UnitManager>().DecrHealth();	
		}
    }

    void NotAttacking()
    {
        isAttacking = false;
        gameObject.renderer.material.color = Color.white;
    }
	
	void fireArrow(GameObject target)
	{
		GameObject arrow = (GameObject) Network.Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation, 0);
		float flightTime = (target.rigidbody.position - spawnPoint.position).magnitude / shotSpeed;
		Vector3 aimPos = target.rigidbody.position - target.rigidbody.velocity * flightTime;
		Vector3 aimDir = aimPos - spawnPoint.position;
		arrow.rigidbody.AddForce(aimDir * shotSpeed);
	}
}
