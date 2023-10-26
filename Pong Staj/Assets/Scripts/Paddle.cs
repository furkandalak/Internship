using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5.0f;  // Paddle'ın hızı

    void Update()
    {
        // Eğer yukarı hareket tuşuna basılırsa, paddle'ı yukarı hareket ettir.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        // Eğer aşağı hareket tuşuna basılırsa, paddle'ı aşağı hareket ettir.
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}