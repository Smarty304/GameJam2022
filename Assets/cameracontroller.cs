using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public Transform Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            Cam.position += new Vector3(0.02f, 0,0);
        }
        if (Input.GetKey("d"))
        {
            Cam.position += new Vector3(-0.02f, 0,0);
        }
    }
}
