using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [Header("State")]
    [SerializeField] State currentState;
    
    [Header("Requirements")]
    public Astar navigation;
    public DietController diet;
    public SleepController sleep;
    public IdentityController identity;
    public MatingController mating;
    public TimeController timeCon;

    [Header("State variables")]
    public Transform state_target;
    public List<Vector3> movement_path = new List<Vector3>();
    public bool shouldUpdatePath = false;

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;


    private void Start()
    {
        timeCon = FindObjectOfType<TimeController>();
        navigation.Init(this);  
        diet.Init(this);
        sleep.Init(this);
        identity.Init(this);
        mating.Init(this);
    }

    private void Update()
    {
        UpdatePath();
        RunStateMachine();
        PerceiveEnvironment();
    }

    //State controller
    public void RunStateMachine()
    {
        //If current state exists (not null) run current state
        State nextState = currentState?.RunCurrentState(this);

        //Returned state will either be the same as current or next stage
        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }
    void SwitchToNextState(State next)
    {
        currentState = next;
    }

    void PerceiveEnvironment()
    {
        diet.FindWater();
        diet.FindFood();
        mating.FindMates();
    }

    public float GetDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    public void UpdatePath()
    {
        if(shouldUpdatePath == true)
        {

        }
    }
}
