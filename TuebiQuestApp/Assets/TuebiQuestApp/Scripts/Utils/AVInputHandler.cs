using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVInputHandler : MonoBehaviour {
    
    /// <summary>
    /// Checks wheater Screen is pressed this update call
    /// </summary>
    /// <returns>True if screen is pressed only this update call</returns>
    public static bool PointerDown()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0);
    }

    public static bool PointerUp()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            || Input.GetMouseButtonDown(0);
    }

    public static bool PointerHeld()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            || Input.GetMouseButtonDown(0);
    }

    public static bool KeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public static bool KeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }

    public static bool KeyHeld(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public static Vector2 PointerPosition()
    {
        return Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
    }



}
