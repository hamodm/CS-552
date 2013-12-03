using UnityEngine;
using System.Collections;

public class Wood : MonoBehaviour {

    Color woodColor = Color.black;
    int resourceType = 1;
    GameObject newResource;
    // Use this for initialization
    void Start()
    {
        gameObject.renderer.material.color = woodColor;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
