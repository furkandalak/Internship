using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float xBound = 10;
    public float yBound = 5;


    
    public float radius = 0.2f;
    // Start is called before the first frame update
    public void Start()
    {
        // Yemi rastgele bir konumda oluştur
        transform.position = RandomPosition();
    }

    
    Vector3 RandomPosition()
    {
        float x = Random.Range(-xBound, xBound);
        float y = Random.Range(-yBound, yBound);
        return new Vector3(x, y, 0);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
