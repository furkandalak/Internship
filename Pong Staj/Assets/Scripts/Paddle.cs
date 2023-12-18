using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  This Class performs Paddle Movement
/// </summary>
public class Paddle : MonoBehaviour
{
    public float speed = 5.0f;  // Paddle'ın hızı
    private void Start()
    {
        #region example1

        Debug.Log("DEBUG");

        #endregion
    }

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
/// @example Math 
/// With Math class you can use various integer calculations \n
/// You can use Math functions for calculating different things. But beware using Math.MultiTwoIntegers().
/// 
/// @note this section is in **WIP**
/// 
/// You can find examples for functions \n
/// \n
/// Math.AddTwoIntegers()
/// <example> Example:
/// <code>
/// int _f = 3;
/// int _s = 4;
/// int total = AddTwoIntegers(_f, _s);
/// </code>
/// @return 7
/// </example>
/// 
/// Math.MultiTwoIntegers()
/// <example> Example:
/// <code>
/// ExampleClass example = new AddTwoIntegers();
/// example.ExampleMethod();
/// </code>
/// <returns> 12 </returns>
/// </example>
///
/// 

/// @example Extra
/// Some test examples for other functions. **WIP**

