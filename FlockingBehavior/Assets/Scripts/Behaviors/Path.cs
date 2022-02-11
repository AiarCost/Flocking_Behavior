using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : Seek
{
    int CurrentIndex = 0;
    public GameObject[] PathCheckPoints;
    float maxAcceleration = 100f;
    float DistToPath = .5f;
    bool NewTarget = true;

    Vector3 targetPosition;

    protected override Vector3 getTargetPosition()
    {
        CurrentIndex++;
        target = PathCheckPoints[CurrentIndex % PathCheckPoints.Length];
        return PathCheckPoints[CurrentIndex % PathCheckPoints.Length].transform.position;
    }

    public override SteeringOutput getSteering()
    {

        SteeringOutput result = new SteeringOutput();
        if (NewTarget)
        {
            targetPosition = getTargetPosition();
            NewTarget = false;
        }
            
        if (targetPosition == Vector3.positiveInfinity)
        {
            return null;
        }

        // Get the direction to the target

        else
        {
            //result.linear = target.transform.position - character.transform.position;
            result.linear = targetPosition - character.transform.position;
        }


        if(result.linear.magnitude < DistToPath)
        {
            NewTarget = true;
        }

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }

    public int CurrentGOSection(Vector3 charPos, int curPos)
    {
        int ClosestGOIndex = 0;
        int GoingAround = 0;
        float SmallestDistance = 1000f;
        foreach (GameObject go in PathCheckPoints)
        {
            Vector3 directionToGO = go.transform.position - character.transform.position;
            float distanceToGO = directionToGO.magnitude;
            if (distanceToGO < SmallestDistance)
            {
                SmallestDistance = distanceToGO;
                ClosestGOIndex = GoingAround;
            }
            GoingAround++;
        }

        return ClosestGOIndex;
    }
}
