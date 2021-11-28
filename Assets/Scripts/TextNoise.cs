using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class TextNoise : MonoBehaviour
{
    
    float m_intensite = 1f; 
    Text labelText;
    string message;  
    // Start is called before the first frame update
    void Start()
    {
        labelText = GetComponent<Text>(); 
        message = "Noise intensity of first frequency"; 
        labelText.text = string.Format("{0} : {1}", message, m_intensite.ToString("F1")); 
        
    }

    // Update is called once per frame
    void Update()
    {
        labelText.text = string.Format("{0} : {1}", message, m_intensite.ToString("F1"));
    }

    public void getIntensity(float intensity)
    {
        m_intensite = intensity; 
         
    }
}
