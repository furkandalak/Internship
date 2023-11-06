using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    public int cubeColumn, cubeRow;
    public int state = 0;
    
    public Material MaterialEmpty;
    public Material MaterialHL;
    public Material MaterialFull;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            GetComponent<Renderer>().material = MaterialEmpty;
        }
        else if (state == 1)
        {
            GetComponent<Renderer>().material = MaterialFull;
        }
        else
        {
            GetComponent<Renderer>().material = MaterialHL;
        }
    }

    public void SetIndex(int c, int r)
    {
        cubeColumn = c;
        cubeRow = r;
    }

    public void TestScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
