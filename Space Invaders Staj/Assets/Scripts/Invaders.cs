using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    
    public int rows = 5;

    public int columns = 11;

    public float invaderSpacing = 2.0f;
    public float invaderAdvancing = 1.0f;

    public float speed = 10.0f;
    public Vector3 direction = Vector2.right;
    
    void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = invaderSpacing * (this.columns - 1);
            float height = invaderSpacing * (this.rows - 1);
            Vector3 centering = new Vector3(- width / 2, - height / 2, 0);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * invaderSpacing, 0.0f);
            for (int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
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
        this.transform.position += direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); // Sonunda
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        
        foreach (Transform invader in this.transform)
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
        }
    }

    void AdvanceRow()
    {
        direction.x = -direction.x;
        Vector3 position = this.transform.position;
        position.y -= invaderAdvancing;
        this.transform.position = position;
    }
    
}
