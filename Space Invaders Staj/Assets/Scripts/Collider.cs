using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public Vector3 position;

    public float width;
    public float height;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.y;
        height = GetComponent<SpriteRenderer>().bounds.size.x;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(position, new Vector3(height, width, 1));
    } 
}
