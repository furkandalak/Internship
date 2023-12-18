using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  This Class performs FPS changes
/// </summary>
public class FPS : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.targetFrameRate = 15;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.targetFrameRate = 30;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.targetFrameRate = 60;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.targetFrameRate = 120;
        }
    }


}

/// \section name "NAME"
/// @brief Makes some calculating.
///
/// Makes calculations for some basic *Math* stuff.
///
///
/// @note Check Examples from @ref Math "here"
/// @see @ref Extra
/// 
public class Math : MonoBehaviour
{
    /// @brief Adds two Integers
    /// @param a an Integer @param b an Integer
    /// @return int (a + b)
    public int AddTwoIntegers(int a, int b)
    {
        int add = 0;
        add = a + b;
        return add;
    }
    
    /// @brief Substracts two Integers
    /// @param a an Integer @param b an Integer
    /// @return int (a - b)
    public int SubTwoIntegers(int a, int b)
    {
        int sub = 0;
        sub = a - b;
        return sub;
    }

    /// @brief Multiplies two Integers
    /// @param a an Integer @param b an Integer
    /// @return int (a * b)
    /// @note It uses Math.AddTwoIntegers() with for loop
    /// @attention Not Tested Yet
    /// @warning Do not use \b 0 as a parameter
    public int MultiTwoIntegers(int a, int b)
    {
        int mul = 0;
        for (int i = 0; i < a; a++)
        {
            AddTwoIntegers(mul, b);
        }
        return mul;
    }

    public void Update()
    {
        ///
        int c = AddTwoIntegers(3, 5);
    }
}



