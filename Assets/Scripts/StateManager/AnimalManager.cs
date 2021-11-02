using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [Header("State")]
    public State currentState;
    public ThinkState thinkState;

    [Header("Requirements")]
    public bool hasInitialised = false;
    public Astar navigation;
    public DietController diet;
    public SleepController sleep;
    public IdentityController identity;
    public MatingController mating;
    public TemperatureController temperature;
    public Chromosome chromosomes;
    public TimeController timeCon;

    [Header("State variables")]
    public Transform state_target;
    public List<Vector3> movement_path = new List<Vector3>();
    public bool shouldUpdatePath = false;
    [SerializeField] float pathRefreshTimer = 0;
    [SerializeField] float pathRefreshInterval = 0.5f;

    [Header("Genetics - Size")]
    public Vector3 sizeMature;
    public Vector3 sizeIncreaseByDay;

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;

    [Header("Genetics - Life")]
    public float ageCurrent = 0.0f;
    public float ageOfMaturityInDays;
    public float ageOfDeathInDays;


    private void Start()
    {
        if (hasInitialised == false)
            Init();
    }

    void Init()
    {
        hasInitialised = true;
        timeCon = FindObjectOfType<TimeController>();
        navigation.Init(this);  
        diet.Init(this);
        sleep.Init(this);
        identity.Init(this);
        mating.Init(this);
        temperature.Init(this);
        chromosomes.Init(this);
    }

    public void ApplyChromosome()
    {
        if(hasInitialised==false)
        {
            Init();
        }
        chromosomes.Activate();

        sizeIncreaseByDay = (sizeMature - transform.localScale) / ageOfMaturityInDays;
    }

    private void Update()
    {
        UpdatePath();
        RunStateMachine();
        PerceiveEnvironment();
        AgeUp();
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
        temperature.TemperatureCheck();
    }

    void AgeUp()
    {
        ageCurrent += timeCon.GetDayTimer();

        if(ageCurrent <= ageOfMaturityInDays)
            transform.localScale += sizeIncreaseByDay*timeCon.GetDayTimer();

        if (ageCurrent>=ageOfDeathInDays)
        {
            Death();
        }
    }



    public bool IsAdult()
    {
        if (ageCurrent >= ageOfMaturityInDays)
            return true;
        else return false;
    }

    public bool TooMature()
    {
        if (ageCurrent >= (ageOfDeathInDays / 3))
            return true;
        else return false;
    }

    public float GetDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    public void UpdatePath()
    {
        if(shouldUpdatePath == true && state_target!=null)
        {
            pathRefreshTimer += Time.deltaTime;
            if (pathRefreshTimer >= pathRefreshInterval)
            {
                pathRefreshTimer = 0;
                navigation.CreatePathToTarget(transform.position, state_target.position);
            }
        }
    }

    public void Death()
    {
        FindObjectOfType<PopulationManager>().RemoveFromPopulation(this);
        Destroy(this.gameObject);
    }



}
