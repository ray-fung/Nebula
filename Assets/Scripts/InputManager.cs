using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The InputManager class detects all finger presses on the
/// phone screen. 
/// </summary>

public class InputManager : MonoBehaviour, IInputManager {

    [SerializeField] public bool IsInDeveloperMode;

    /// <summary>
    /// Senses when the player touches the screen
    /// </summary>
    /// <returns>Returns a bool of whether the screen has been touched</returns>
    public bool GetRocketInput()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        
        return (IsInDeveloperMode && Input.GetKeyDown(KeyCode.Space));
    }
}
