using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITitleUi {
    /// <summary>
    /// Instructs the title ui elements to fade into
    /// view
    /// </summary>
    void FadeIn();

    /// <summary>
    /// Instructs the title ui elements to fade out
    /// of view
    /// </summary>
    void FadeOut();
}
