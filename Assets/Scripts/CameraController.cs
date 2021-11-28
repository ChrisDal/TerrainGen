using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 refposition; // keep in mind camera pos 
    float dx = 0.05f; 
    // Start is called before the first frame update
    void Start()
    {
        refposition = new Vector3(transform.position.x,transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += dx * Vector3.back; 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += dx * Vector3.forward; 
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += dx * Vector3.left; 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += dx * Vector3.right;  
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset 
            transform.position = refposition; 
        }


        
    }
}
