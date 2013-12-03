using UnityEngine;
using System.Collections;

public class DrawGrid2 : MonoBehaviour 
{

	public GameObject plane;
 
    public bool showMain = true;
    public bool showSub = false;
 
    private float gridSizeX;
    private float gridSizeZ;
 
    private float smallStep = 5f;
    private float largeStep = 10f;
 
    private float startX = 0f;
    private float startZ = 0f;
 
    private float scrollRate = 0.1f;
    private float lastScroll = 0f;
 
    private Material lineMaterial;
 
    private Color mainColor = new Color(0f,1f,0f,1f);
    private Color subColor = new Color(0f,0.5f,0f,1f);
 
    void Start () 
    {
 		gridSizeX = Terrain.activeTerrain.terrainData.size.x;
		gridSizeZ = Terrain.activeTerrain.terrainData.size.z;
    }
 
    void Update () 
    {
       if(lastScroll + scrollRate < Time.time)
       {
         if(Input.GetKey(KeyCode.KeypadPlus)) 
         {
          plane.transform.position = new Vector3(plane.transform.position.x, plane.transform.position.y + smallStep, plane.transform.position.z);
          lastScroll = Time.time;
         }
         if(Input.GetKey(KeyCode.KeypadMinus))
         {
          plane.transform.position = new Vector3(plane.transform.position.x, plane.transform.position.y - smallStep, plane.transform.position.z);
          lastScroll = Time.time;
         }
       }
    }
 
    void CreateLineMaterial() 
    {
	    if( !lineMaterial ) 
		{
	        lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
	            "SubShader { Pass { " +
	            "    Blend SrcAlpha OneMinusSrcAlpha " +
	            "    ZWrite Off Cull Off Fog { Mode Off } " +
	            "    BindChannels {" +
	            "      Bind \"vertex\", vertex Bind \"color\", color }" +
	            "} } }" );
	        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
	        lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
    }
 
    void OnPostRender() 
    {   
        CreateLineMaterial();
        // set the current material
        lineMaterial.SetPass( 0 );
 
        GL.Begin( GL.LINES );
 
       if(showSub)
       {
           GL.Color(subColor);
 
         //Layers
          //X axis lines
          for(float i = 0; i <= gridSizeZ; i += smallStep)
          {
              GL.Vertex3( startX, 0, startZ + i);
              GL.Vertex3( gridSizeX, 0, startZ + i);
          }
 
          //Z axis lines
          for(float i = 0; i <= gridSizeX; i += smallStep)
          {
              GL.Vertex3( startX + i, 0, startZ);
              GL.Vertex3( startX + i, 0, gridSizeZ);
          }
       }
 
       if(showMain)
       {
         GL.Color(mainColor);
 
         //Layers
          //X axis lines
          for(float i = 0; i <= gridSizeZ; i += largeStep)
          {
              GL.Vertex3( startX, 0, startZ + i);
              GL.Vertex3( gridSizeX, 0, startZ + i);
          }
 
          //Z axis lines
          for(float i = 0; i <= gridSizeX; i += largeStep)
          {
              GL.Vertex3( startX + i, 0, startZ);
              GL.Vertex3( startX + i, 0, gridSizeZ);
          }
       }
 
 
        GL.End();
    }
}