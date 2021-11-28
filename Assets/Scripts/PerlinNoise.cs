
using UnityEngine;
using System.Collections; 

public class PerlinNoise : MonoBehaviour
{
    public int m_width = 256; // in pixels
    public int m_height = 256;  // in pixels
    public float m_noiseScale = 20.0f; 

    public float xOffset = 0f; 
    public float yOffset = 0f;

    Renderer m_renderer; 
    
    // Start is called before the first frame update
    void Start()
    {
        xOffset = Random.Range(0f, 99999f); 
        yOffset = Random.Range(0f, 99999f); 
        
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.mainTexture = GenerateTexture();  
        
        
    }

    void Update()
    {
       m_renderer.material.mainTexture = UpdateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(m_width, m_height); 

        float[,] noise2D = GenerateNoiseMap(m_width, m_height, m_noiseScale); 
        Debug.Log("Generate Noise Map"); 
        
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Color pixColor = new Color(noise2D[x, y], noise2D[x, y], noise2D[x, y]); 
                texture.SetPixel(x, y, pixColor); 
            }
        }
        
        texture.Apply(); 

        return texture; 
    }

    Texture2D UpdateTexture()
    {
        Texture2D texture = new Texture2D(m_width, m_height); 

        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                float value = GeneratePerlinValue(x ,y,m_width, m_height,  m_noiseScale,  xOffset, yOffset); 
                Color pixColor = new Color(value,value,value); 
                texture.SetPixel(x, y, pixColor); 
            }
        }

        texture.Apply(); 
        return texture;
    }


    public float[,] GenerateNoiseMap(int width, int height, float scale)
    {
        float[,] noiseMap = new float[width, height]; 

        if (scale <= 0.0f){
            scale = 0.0001f; 
        }

        for (int y = 0; y < width; y++)
        {
            for (int x = 0 ; x < height; x++)
            {
                
                noiseMap[x,y] = GeneratePerlinValue(x , y, width, height, scale, xOffset, yOffset);; 
            }
        }

        return noiseMap; 
    }

    float GeneratePerlinValue(int x , int y, int width, int height, float scale, float offsetx, float offsety)
    {
        float sampleX = ((float)x / (float)width)  * scale  + xOffset; 
        float sampleY = ((float)y / (float)height) * scale  + yOffset; 

        float perlinVal = Mathf.PerlinNoise(sampleX, sampleY); 

        return perlinVal; 
    }
}
