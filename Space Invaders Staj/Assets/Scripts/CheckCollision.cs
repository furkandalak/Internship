using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public Collider enemyPrefab;
    public Collider laserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool test = CheckColliderCollision(enemyPrefab, laserPrefab);
        if (test == true)
        {
            Debug.Log(test);
        }
    }
    
    public bool CheckColliderCollision(Collider collider1, Collider collider2)
    {
        return (collider1.position.x > collider2.position.x);
    }
    
}
