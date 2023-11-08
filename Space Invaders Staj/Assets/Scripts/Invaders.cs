using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject missilePrefab;
    public Player player;
    Collider playerCollider;
    
    public int rows = 5;

    public int columns = 11;

    public float invaderSpacing = 2.0f;
    public float invaderAdvancing = 1.0f;

    public float invaderAttackSpeed = 1.0f;
    public float invaderAttackChance = 0.2f;
    
    public float speed = 10.0f;
    public Vector3 direction = Vector2.right;

    public List<GameObject> activeBullets;
    GameObject[,] _spawnedInvaders;

    void Awake()
    {
        playerCollider = player.GetComponent<Collider>();
        _spawnedInvaders = new GameObject[rows, columns];
        for (int row = rows - 1; row >= 0; row--)
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
                _spawnedInvaders[row, col] = invader;
            }
        }
        InvokeRepeating(nameof(InvaderShoot), invaderAttackSpeed, invaderAttackSpeed);
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

        for (int row = rows - 1; row >= 0; row--)
        {
            for (int col = 0; col < columns; col++)
            {
                if (_spawnedInvaders[row, col] == null)
                {
                    continue; // Osman'ın verdiği taktik ;)
                }
                if (direction == Vector3.right && _spawnedInvaders[row, col].transform.position.x >= (rightEdge.x - invaderSpacing) )
                {
                    AdvanceRow(_spawnedInvaders[row, col]);
                }
                else if (direction == Vector3.left && _spawnedInvaders[row, col].transform.position.x <= (leftEdge.x + invaderSpacing))
                {
                    AdvanceRow(_spawnedInvaders[row, col]);
                }

                // Enemy ile Player testi
                if (CheckColliderCollision(_spawnedInvaders[row, col].GetComponent<Collider>(), playerCollider))
                {
                    SceneManager.LoadScene("SpaceInvaders");
                }

                Collider _invaderHitbox = _spawnedInvaders[row, col].GetComponent<Collider>();
                // Enemy ile Bullet testi
                foreach (GameObject curBullet in activeBullets)
                {
                    if (curBullet == null)
                    {
                        continue;
                    }
                    bool hit = CheckColliderCollision(_invaderHitbox, curBullet.GetComponent<Collider>());
                    if (hit == true)
                    {
                        Destroy(curBullet);
                        Destroy(_spawnedInvaders[row, col].gameObject);
                        //activeBullets.Remove(curBullet);
                        break;
                    }
                }
            }
        }
    }

    void AdvanceRow(GameObject advancingInvader)
    {
        direction.x = -direction.x;
        Vector3 position = transform.position;
        position.y -= invaderAdvancing;
        transform.position = position;
    }

    void InvaderShoot()
    {
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                if (_spawnedInvaders[row, col] == null)
                {
                    continue;
                }
                else
                {
                    if (Random.value <= invaderAttackChance)
                    {
                        GameObject missile = Instantiate(missilePrefab, _spawnedInvaders[row, col].transform.position, Quaternion.identity);
                        player.currentEnemyMissiles.Add(missile);
                    }
                    break;
                }
            }
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
