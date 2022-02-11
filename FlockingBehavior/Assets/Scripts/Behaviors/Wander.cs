using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;


    float wanderOffset = .5f;
    float wanderRadius = 1f;
 
    Vector3 targetPosition;
    float maxAcceleration = 100f;
    bool NeedNewTarget = true;
    float DistToPoint = 1f;

    protected Vector3 getTargetPosition()
    {
        Vector3 targetDir = target.transform.position - character.transform.position;

        targetDir.Normalize();
        targetDir *= Random.Range(.25f, 1); ;



        float degrees = Random.Range(0, 360);

        Vector3 CirPos = new Vector3(Mathf.Sin(degrees) * wanderRadius, 0, Mathf.Cos(degrees) * wanderRadius);
        Vector3 Combined = targetDir + CirPos;
        Vector3 NewTarget = character.transform.position + Combined;
        return NewTarget;
        
    }
    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        if (NeedNewTarget)
        {
            targetPosition = getTargetPosition();
            NeedNewTarget = false;
        }

        if(targetPosition == Vector3.positiveInfinity)
        {
            return null;

        }


        else
        {
            result.linear = targetPosition - character.transform.position;
        }


        if(result.linear.magnitude < DistToPoint)
        {
            NeedNewTarget = true;
            Debug.Log("new Target");
        }


        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
    
}
