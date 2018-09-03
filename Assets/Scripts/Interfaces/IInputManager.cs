using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager {
    /// <summary>
    /// Returns true if input to launch the rocket
    /// has been given, false otherwise
    /// </summary
    bool GetRocketInput();
}
