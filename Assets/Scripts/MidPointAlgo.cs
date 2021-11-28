using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MidPointAlgo : MonoBehaviour
{

    public int subdivisions = 2;  
    int xSize; 
    int ySize; 
    private float maxX = 20.0f; 
    private float maxY = 20.0f;

    private Vector2 startPoint; 
    private Vector2 endPoint; 
    Mesh mesh; 

    Vector3[] vertices; 
    int verticesCount; 

  
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector2(0f, 0f);
        endPoint   = new Vector2(maxX, maxY); 

        float nbCarre = Mathf.Pow(4f, (float)subdivisions); 
        xSize = (int)Mathf.Pow(2f, (float)subdivisions); 
        ySize = (int)Mathf.Pow(2f, (float)subdivisions); 
        


        mesh = new Mesh(); 
        GetComponent<MeshFilter>().mesh = mesh; 


        CreateShape(); 
        UpdateMesh();
    }

    void CreateShape()
    {
        verticesCount = (xSize + 1) * (ySize + 1); 
        float dx = endPoint.x - startPoint.x; 
        
        initMesh(verticesCount); 
        
    }


    void initMesh(int gridsize)
    {
        vertices = new Vector3[verticesCount];



        

        

    }

    Vector3[] subdivision(Vector3[] grid, int nbpoint)
    {

        Vector3[] subgrid = new Vector3[getSideSize(nbpoint)]; 


        return subgrid; 
    }


    // return number of subdivisions 
    int getSideSize(int nbsub)
    {
        return (int)Mathf.Pow(2f, (float)nbsub); 
    }

    void UpdateMesh()
    {
        mesh.Clear(); 
        mesh.vertices = vertices; 
        //mesh.triangles = triangles;
        //mesh.RecalculateNormals(); 
    }
}
