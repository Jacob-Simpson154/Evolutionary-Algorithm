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
    public TimeController timeCon;

    [Header("State variables")]
    public Transform state_target;
    public List<Vector3> movement_path = new List<Vector3>();

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;


    private void Start()
    {
        timeCon = FindObjectOfType<TimeController>();
        navigation.Init(this);  
        diet.Init(this);
    }

    private void Update()
    {
        RunStateMachine();
        PerceiveEnvironment();
    }

    //State controller
    public void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState(this);

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
    }

    public float GetDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }
}
