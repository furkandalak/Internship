using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.targetFrameRate = 15;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.targetFrameRate = 30;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.targetFrameRate = 60;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.targetFrameRate = 120;
        }
    }


}
