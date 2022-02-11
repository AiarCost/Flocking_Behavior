using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleAvoidanceKin : Kinematic
{
    ObsticleAvoidance myMoveType;
    LookWhereGoing myRotateType;

    private void Start()
    {
        myMoveType = new ObsticleAvoidance();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;

    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
