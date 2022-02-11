using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : Kinematic
{
    public GameObject UltimateTarget;
    BlendedSteering mySteering;
    public Kinematic[] birds;
    // Start is called before the first frame update
    void Start()
    {
        Separation seperate = new Separation();
        seperate.character = this;
        GameObject[] birdsGO = GameObject.FindGameObjectsWithTag("bird");
        birds = new Kinematic[birdsGO.Length];
        int j = 0;
        for (int x = 0; x < birdsGO.Length; x++)
        {
            if (birdsGO[x] == this)
            {
                continue;
            }
            else
            {
                Debug.Log(birdsGO[x].name);

                birds[j++] = birdsGO[x].GetComponent<Kinematic>();
            }
 
        }

        seperate.targets = birds;

        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = UltimateTarget;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        mySteering = new BlendedSteering();
        mySteering.Behaviors = new BehaviorAndWeight[3];
        mySteering.Behaviors[0] = new BehaviorAndWeight();
        mySteering.Behaviors[0].behavior = seperate;
        mySteering.Behaviors[0].weight = 10f;

        mySteering.Behaviors[1] = new BehaviorAndWeight();
        mySteering.Behaviors[1].behavior = cohere;
        mySteering.Behaviors[1].weight = 1f;

        mySteering.Behaviors[2] = new BehaviorAndWeight();
        mySteering.Behaviors[2].behavior = myRotateType;
        mySteering.Behaviors[2].weight = 1f;

    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate = mySteering.getSteering();

        base.Update();
    }
}
