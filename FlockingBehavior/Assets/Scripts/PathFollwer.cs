using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollwer : Kinematic
{
    Path myMoveType;
    Face myRotateType;

    public GameObject[] CheckPoints;


    private void Awake()
    {
        
    }
    private void Start()
    {
        myMoveType = new Path();
        myMoveType.character = this;
        myMoveType.PathCheckPoints = CheckPoints;

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;



    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }

}
