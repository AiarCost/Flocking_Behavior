using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorAndWeight 
{
    public SteeringBehavior behavior;
    public float weight = 0;
}


public class BlendedSteering
{
    public BehaviorAndWeight[] Behaviors;

    float maxAcceleration = 10f;
    float maxRotation = 45f;

    
    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        foreach (BehaviorAndWeight banana in Behaviors)
        {
            result.linear += banana.weight * banana.behavior.getSteering().linear;

        }

        result.linear.Normalize();
        result.linear *= maxAcceleration;

        return result;
    }
}