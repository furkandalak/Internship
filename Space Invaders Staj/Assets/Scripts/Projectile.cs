using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 direction;

    public float speed;

    private Vector3 topEdge, bottomEdge;

    void Awake()
    {
        topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.down);
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.y >= topEdge.y)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y <= bottomEdge.y)
        {
            Destroy(gameObject);
        }
    }
}
