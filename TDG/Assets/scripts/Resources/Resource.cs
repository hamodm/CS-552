using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour
{
    public int resourceType = 0;

    enum Resources { Wood, Stone, Food };

    Color woodColor = Color.black;
    Color stoneColor = Color.gray;
    Color foodColor = Color.green;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeType(int resType)
    {
        resourceType = resType;
        SetColor(resType);
    }

    void SetColor(int resType)
    {
        if (resType == (int)Resources.Wood)
            gameObject.renderer.material.color = woodColor;
        else if (resType == (int)Resources.Stone)
            gameObject.renderer.material.color = stoneColor;
        else if (resType == (int)Resources.Food)
            gameObject.renderer.material.color = foodColor;
    }
}
