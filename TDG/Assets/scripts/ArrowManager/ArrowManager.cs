using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour {

	// Use this for initialization
	float lifeTime = 5f;
	//Vector3 gravity = new Vector3(0, -9.8f, 0);
	//Vector3 velocity = new Vector3(0, 0, 0);
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//v = v0 + at
		//y = y0 + vt + .5at**2
		//velocity += gravity * Time.deltaTime;
		//gameObject.transform.position += (velocity * Time.deltaTime);
		//gameObject.transform.position += (0.5f * gravity * Time.deltaTime * Time.deltaTime);
		
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0)
		{
			Destroy(gameObject);	
		}
	}
	
	
	/*public void OnTriggerEnter(Collider col)
	{
        if(col.gameObject.tag != "Arrow" && col.gameObject.tag != "Tower")
		{
			Destroy(gameObject);	
		}
    }*/
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag != "Arrow")
		{
			Network.Destroy(gameObject);	
		}
	}
}
