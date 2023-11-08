using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    Vector3 leftEdge;
    Vector3 rightEdge;

    public GameObject laserPrefab;
    public Invaders invaders;

    Collider myHitbox;
    
    public int maxBullets = 1;

    List<GameObject> spawnedBullets = new List<GameObject>();
    public List<GameObject> currentEnemyMissiles = new List<GameObject>();
    
    void Awake()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        myHitbox = gameObject.GetComponent<Collider>();
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
        
        foreach (GameObject curBullet in spawnedBullets)
        {
            if (curBullet == null)
            {
                maxBullets += 1;
                spawnedBullets.Remove(curBullet);
                break;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        foreach (GameObject missile in currentEnemyMissiles)
        {
            if (missile == null)
            {
                continue;
            }

            if (CheckColliderCollision(myHitbox, missile.GetComponent<Collider>()))
            {
                Destroy(missile);
                SceneManager.LoadScene("SpaceInvaders");
                break;
            }
        }


    }

    void MovePlayer(float speed)
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        float xPos = Mathf.Clamp(transform.position.x,leftEdge.x + invaders.invaderSpacing, rightEdge.x - invaders.invaderSpacing);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    void Shoot()
    {
        if (maxBullets > 0)
        {
            maxBullets -= 1;
            GameObject firedBullet = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            spawnedBullets.Add(firedBullet);
            invaders.activeBullets.Add(firedBullet);
        }
    }
    
    public bool CheckColliderCollision(Collider collider1, Collider collider2)
    {
        return (collider1.position.x - collider1.width/2 < collider2.position.x + collider2.width/2 &&
                collider1.position.x + collider1.width/2 > collider2.position.x - collider2.width/2 &&
                collider1.position.y - collider1.height/2 < collider2.position.y + collider2.height/2 &&
                collider1.position.y + collider1.height/2 > collider2.position.y - collider2.height/2);
    }
}
