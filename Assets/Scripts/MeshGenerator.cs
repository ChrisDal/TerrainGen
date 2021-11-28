using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    
    Mesh mesh; 
    Vector3[] vertices; 
    Color[] vertexColors; 
    int[] triangles; 

    private int xSize = 80; 
    private int zSize = 80; 
    public float noiseScale = 2.0f;
    public float intensity = 1.0f; 

    public int octave = 1; 

    private int verticesCount = 0; 

    public Gradient gradient; 
    float minTerrainHeight; 
    float maxTerrainHeight; 

    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh(); 
        GetComponent<MeshFilter>().mesh = mesh; 

        minTerrainHeight = float.MaxValue; 
        maxTerrainHeight = float.MinValue; 

        CreateShape(); 

    }

    void Update()
    {
        UpdateMesh(); 
    }

    void CreateShape()
    {
        verticesCount = (xSize + 1) * (zSize + 1); 
        vertices = new Vector3[verticesCount]; 
        

        for (int i=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {
                float value = Mathf.PerlinNoise( noiseScale*((float)x /(float)xSize) ,   noiseScale*((float)z/(float)zSize)); 
                vertices[i] = new Vector3(x, value, z); 

                if (value > maxTerrainHeight){
                    maxTerrainHeight = value; 
                }

                if (value < minTerrainHeight){
                    minTerrainHeight = value; 
                }


                i++; 
            }
        }

        vertexColors = new Color[vertices.Length]; 
        for (int i=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {
                float zNormalizedHeight = (vertices[i].y - minTerrainHeight) /  maxTerrainHeight; 
                vertexColors[i]  = gradient.Evaluate(zNormalizedHeight);
                i++; 
            }
        }

        triangles = new int[xSize*zSize*6]; 
        int vert = 0; 
        int tris = 0; 
        for (int z=0; z < zSize; z++)
        {
            for (int x=0; x < xSize; x++)
            {
                triangles[tris+0]   = 0 + vert; 
                triangles[tris+1] = xSize + 1 + vert ; 
                triangles[tris+2] = 1 + vert; 
                triangles[tris+3] = 1 + vert; 
                triangles[tris+4] = xSize + 1 + vert; 
                triangles[tris+5] = xSize + 2 + vert; 
                vert++; 
                tris +=6; 

            }
            vert++; 

        }    
    }

    void UpdateVertices()
    {
        
        if (octave < 1)
        {
            octave = 1; 
        }

        maxTerrainHeight = float.MinValue; 
        minTerrainHeight = float.MaxValue; 
        
        for (int i=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {
                float value = 0.0f; 
                float roughness = intensity; 

                for (int oct = 0; oct < octave; oct++)
                {
                    float freqy = noiseScale *  Mathf.Pow(2.0f, oct); 
                    float octavium = Mathf.PerlinNoise( freqy * ((float)x /(float)xSize),  
                                                        freqy * ((float)z/(float)zSize));
                    // lower intensity according to freq 
                    octavium *= roughness;
                    roughness /= 2.0f;

                    value += octavium; 
                }
                
                vertices[i] = new Vector3(x, value, z); 

                if (value > maxTerrainHeight){
                    maxTerrainHeight = value; 
                }

                if (value < minTerrainHeight){
                    minTerrainHeight = value; 
                }

                i++; 
                 
            }
        }
    }

    void UpdateVerticesColors()
    {
        vertexColors = new Color[vertices.Length]; 
        for (int i=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {
                float zNormalizedHeight = (vertices[i].y - minTerrainHeight) /  maxTerrainHeight; 
                vertexColors[i]  = gradient.Evaluate(zNormalizedHeight);
                i++; 
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear(); 
        UpdateVertices(); 
        UpdateVerticesColors(); 
        
        mesh.vertices = vertices; 
        mesh.triangles = triangles;
        mesh.colors = vertexColors; 
        mesh.RecalculateNormals(); 
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return; 
        }
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f); 
        }
    }

    public void adjustOctave(float newoctave){
        octave = (int)newoctave; 
    }

    public void adjustNoise(float noiseintensity){
        intensity = noiseintensity; 
    }

}
