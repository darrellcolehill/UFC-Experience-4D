using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PathFollow : MonoBehaviour
{
    public Path path;
    public float speed = 10.0f;
    [Range(1.0f, 1000.0f)]
    public float steeringInertia = 100.0f;
    public bool isLooping = false;
    public float waypointRadius = 1.0f;
    private float curSpeed;
    private int curPathIndex = 0;
    private int pathlength;
    private Vector3 targetPoint;
    Vector3 velocity;

    void Start()
    {
        pathlength = path.Length;
        velocity = transform.forward;
    }

    void Update()
    {
        curSpeed = speed * Time.deltaTime;
        targetPoint = path.getPoint(curPathIndex);

        if (Vector3.Distance(transform.position, targetPoint) < waypointRadius)
        {
            if (curPathIndex < pathlength - 1)
            {
                curPathIndex++;
            }
            else if (isLooping)
            {
                curPathIndex = 0;
            }
            else
            {
                return;
            }
        }

        if (curPathIndex >= pathlength)
        {
            return;
        }
        else if (curPathIndex < pathlength - 1 && !isLooping)
        {
            velocity += Steer(targetPoint, true);
        }
        else
        {
            velocity += Steer(targetPoint);
        }

        // Apply the velocity to move the object
        transform.position += velocity * Time.deltaTime;
    }


    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;
        desiredVelocity.Normalize();

        if(bFinalPoint && dist < waypointRadius)
        {
            desiredVelocity *= curSpeed *  (dist / waypointRadius);
        }
        else
        {
            desiredVelocity *= curSpeed;
        }

        Vector3 steeringForce = desiredVelocity - velocity;
        return steeringForce / steeringInertia;
    }
}
