using UnityEngine;
using System.Collections;

//this class will be applied to the camera itself
public class CameraController : MonoBehaviour {
    public float moveSpeed = 50;
	public float maxCamHeight = 100;
	public float minCamHeight = 20;
    // Use this for initialization
    void Start()
    {

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
}
