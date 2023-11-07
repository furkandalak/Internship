using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    Vector3 leftEdge;
    Vector3 rightEdge;

    public Projectile laserPrefab;

    public int maxBullets = 1;

    List<Projectile> spawnedBullets = new List<Projectile>();
    
    void Awake()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayer(speed);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayer(-speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        foreach (Projectile curBullet in spawnedBullets)
        {
            if (curBullet == null)
            {
                maxBullets += 1;
                spawnedBullets.Remove(curBullet);
                break;
            }
        }
    }

    void MovePlayer(float speed)
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        float xPos = Mathf.Clamp(transform.position.x,leftEdge.x, rightEdge.x);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    void Shoot()
    {
        if (maxBullets > 0)
        {
            maxBullets -= 1;
            Projectile firedBullet = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            spawnedBullets.Add(firedBullet);
        }
    }
}
