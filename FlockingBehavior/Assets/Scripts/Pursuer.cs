using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    Face myRotateType;
    LookWhereGoing myFleeingType;


    public bool flee = false;
    private void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = flee;

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;

        myFleeingType = new LookWhereGoing();
        myFleeingType.character = this;
        myFleeingType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;

        if (flee)
            steeringUpdate.angular = myFleeingType.getSteering().angular;
        else
            steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
