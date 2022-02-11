using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleAvoidance : Seek
{
    public float DistFromObsticles = 5f;
    public float AvoidDist = 4f;
    protected override Vector3 getTargetPosition()
    {
        RaycastHit hit;
        //Vector3 direction = target.transform.position - character.transform.position;

        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, 5f))
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.yellow, 1f);
            //Vector3 Normal = hit.normal * DistFromObsticles;

            return hit.point + (hit.normal * AvoidDist);

        }
        else
        {
            return base.getTargetPosition();
        }
        
    }
}
