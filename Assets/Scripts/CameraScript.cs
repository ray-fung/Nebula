using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour, ICamera {
    [SerializeField] float cameraSpeed;
    [SerializeField] float cameraYDistanceFromAsteroid;

    private Vector3? pointToStopMoving;

    void Update()
    {
        if(pointToStopMoving != null)
        {
            float cameraPosY = transform.localPosition.y;
            if (cameraPosY - pointToStopMoving.Value.y >= cameraYDistanceFromAsteroid)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void MoveCameraUntil(Vector3 pointToStop)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * cameraSpeed;
        pointToStopMoving = pointToStop;
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }
}
