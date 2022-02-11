using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic[] targets;
    public GameObject target;
    public Kinematic character;

    float radius = .5f;
    float maxAcceleration = 10f;
    public override SteeringOutput getSteering()
    {

        float ShortestTime = Mathf.Infinity;

        Kinematic firstTarget = null;
        float FirstMinSeparation = Mathf.Infinity;
        float FirstDistance = Mathf.Infinity;
        Vector3 FirstRelativePos = Vector3.positiveInfinity;
        Vector3 FirstRelativeVel = Vector3.zero;

        Vector3 RelativePos = Vector3.positiveInfinity;
        foreach (Kinematic go in targets)
        {
            RelativePos = go.gameObject.transform.position - character.transform.position;
            Vector3 RelativeVel = character.linearVelocity - go.gameObject.transform.position;

            float relativeSpeed = RelativeVel.magnitude;

            float TimeToCollision = Vector3.Dot(RelativePos, RelativeVel) / relativeSpeed * relativeSpeed;

            float distance = RelativePos.magnitude;

            float minSeparation = distance - relativeSpeed * TimeToCollision;

            if (minSeparation > 2 * radius)
            {
                continue;
            }

            if (TimeToCollision > 0 && TimeToCollision < ShortestTime)
            {
                ShortestTime = TimeToCollision;
                firstTarget = go;
                FirstMinSeparation = minSeparation;
                FirstDistance = distance;
                FirstRelativePos = RelativePos;
                FirstRelativeVel = RelativeVel;
            }
        }

        if (firstTarget == null)
        {
            return null;
        }

        //Vector3 RelativePos2;
        //if (FirstMinSeparation <= 0 || FirstDistance < 2 * radius)
        //{
        //    RelativePos2 = firstTarget.gameObject.transform.position - character.transform.position;

        //}

        //else
        //{
        //    RelativePos2 = FirstRelativePos + FirstRelativeVel * ShortestTime;

        //}

        //RelativePos2 = RelativePos2.normalized;

        SteeringOutput result = new SteeringOutput();

        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);

        if(dotResult < -.9f)
        {
            result.linear = new Vector3(character.linearVelocity.z, 0, character.linearVelocity.z);
        }
        else
        {
            result.linear = -firstTarget.linearVelocity;
        }

        
        result.linear.Normalize();
        result.linear *=maxAcceleration;
        result.angular = 0;
        return result;
    }
}
