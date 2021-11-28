using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOctave : MonoBehaviour
{
    
    int m_octave = 1; 
    Text labelText;
    string message;  
    // Start is called before the first frame update
    void Start()
    {
        labelText = this.GetComponent<Text>(); 
        message = "Frequencies Number : "; 
        labelText.text = message + "1"; 
        
    }

    // Update is called once per frame
    void Update()
    {
        labelText.text = message + m_octave.ToString(); 
    }

    public void getOctave(float octave)
    {
        m_octave = (int)octave; 
        
    }
}
