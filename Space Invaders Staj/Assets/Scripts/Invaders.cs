using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public GameObject[] prefabs;
    
    public int rows = 5;

    public int columns = 11;

    public float invaderSpacing = 2.0f;
    public float invaderAdvancing = 1.0f;
    
    public float speed = 10.0f;
    public Vector3 direction = Vector2.right;

    public List<GameObject> activeBullets;
    
    void Awake()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = invaderSpacing * (this.columns - 1);
            float height = invaderSpacing * (this.rows - 1);
            Vector3 centering = new Vector3(- width / 2, - height / 2, 0);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * invaderSpacing, 0.0f);
            for (int col = 0; col < columns; col++)
            {
                GameObject invader = Instantiate(prefabs[row], transform);
                Vector3 position = rowPosition;
                position.x += col * invaderSpacing;
                invader.transform.localPosition = position;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); // Sonunda boundary bulmayı çözdük
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue; // Osman'ın verdiği taktik ;)
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - invaderSpacing) )
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + invaderSpacing) )
            {
                AdvanceRow();
            }
            
            foreach (GameObject curBullet in activeBullets)
            {
                if (curBullet == null)
                {
                    continue;
                }
                bool test = CheckColliderCollision(invader.GetComponent<Collider>(), curBullet.GetComponent<Collider>());
                if (test == true)
                {
                    Destroy(invader.gameObject);
                    Destroy(curBullet);
                    activeBullets.Remove(curBullet);
                    break;
                }
            }
        }
    }

    void AdvanceRow()
    {
        direction.x = -direction.x;
        Vector3 position = transform.position;
        position.y -= invaderAdvancing;
        transform.position = position;
    }
    
    public bool CheckColliderCollision(Collider collider1, Collider collider2)
    {
        return (collider1.position.x - collider1.width/2 < collider2.position.x + collider2.width/2 &&
                collider1.position.x + collider1.width/2 > collider2.position.x - collider2.width/2 &&
                collider1.position.y - collider1.height/2 < collider2.position.y + collider2.height/2 &&
                collider1.position.y + collider1.height/2 > collider2.position.y - collider2.height/2);
    }
    
}
