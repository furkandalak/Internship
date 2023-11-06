using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = new Vector3(0, 0, 0);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z - target.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.DrawLine(Camera.main.transform.position, worldPos, Color.green, 5f);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 target = new Vector3(0, 0, 0);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z - target.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        //Vector3 direction = worldPos - rayOrigin;
    
        //Gizmos.DrawWireSphere(new Vector3 (worldPos.x, worldPos.y, worldPos.z), 0.5f);
    }
}
