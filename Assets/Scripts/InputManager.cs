using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager {

    [SerializeField] public bool IsInDeveloperMode;

    public bool GetRocketInput()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        
        return (Input.GetKeyDown(KeyCode.Space) && IsInDeveloperMode);
    }
}
