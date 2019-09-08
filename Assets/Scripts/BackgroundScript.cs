using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour, IBackground {

    // The material that is going to be rotating
    Material material;
    Vector2 offset;
    bool update;

[SerializeField]
    public int xVelocity, yVelocity;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
        update = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Activate only if the update bool is true
        if (update) {
            material.mainTextureOffset += offset * Time.deltaTime;
        }
    }

    // Functions to start and stop the moving background
    public void UpdateBackground() {
        update = true;
    }

    public void StopBackground() {
        update = false;
    }
}
