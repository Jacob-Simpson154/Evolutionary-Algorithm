using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitClass : AnimalBaseClass
{
    [Header("Genetic Components")]
    public RabbitGenetics genes;

    [Header("Predators")]
    public List<Transform> spottedPredators = new List<Transform>();
    public LayerMask predatorMask;
    public override void Activate()
    {
        //genes.size.ApplyGeneticInformation(transform);
        //genes.fur.ApplyGeneticInformation(transform);
        //genes.speed.ApplyGeneticInformation(transform);
        //genes.eyesight.ApplyGeneticInformation(transform);
        //genes.lifeExpectancy.ApplyGeneticInformation(transform);

        timeController = FindObjectOfType<TimeController>();
        timeController.AddDailyListener(AgeUpByDay);

        sizeIncreaseByDay = (matureSize-transform.localScale) / ageOfMaturityInDays;
    }

    private void Update()
    {
        if (isDead == false)
        {
            if(hasTarget == true)
            {
                if (path.Count > 0)
                {
                    transform.rotation = Quaternion.LookRotation(path[0] - transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, path[0], Time.deltaTime);
                    if (transform.position == path[0])
                    {
                        path.RemoveAt(0);
                    }
                } else
                {
                    FinaliseState();
                }
            }

            else

            if (hasTarget == false)
            {
                FindWater();
                FindFood();

                ChooseState();
            }    
        }
    }

    public override void ChooseState()
    {
        if (IsThirsty())
        {
            Transform destination = GetClosestWater();
            if (destination != null)
            {
                MoveToTarget(destination.position);
                hasTarget = true;
                target = destination;
                currentState = Action.thirsty;
            }
            return;
        }

        if(IsHungry())
        {
            //MoveToTarget food
            return;
        }

        MoveToTarget(CreatePointInReach(10));
    }

    public override void FinaliseState()
    {
        switch (currentState)
        {
            case Action.hungry:
                break;
            case Action.thirsty:
                Debug.Log("Drinking");

                //ConsumableDetails nourishment = target.GetComponent<TileController>().GetNourishment();
                //water += nourishment.hydrationAmount;
                //food += nourishment.nutritionalAmount;

                break;
            case Action.moving:
                break;
            default:
                break;
        }
        
        
        ChooseState();
    }


    public override void FindFood()
    {
        visibleFood.Clear();

        Collider[] foodInArea = Physics.OverlapSphere(transform.position, eyeSightRange, consumableMask);
        foreach (Collider item in foodInArea)
        {
            ConsumableController inspectedItem = item.GetComponentInParent<ConsumableController>();
            if (inspectedItem.nourishment.nutritionalAmount > 0)
            {
                if (!visibleFood.Contains(inspectedItem))
                {
                    Vector3 directionToFood = (inspectedItem.transform.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, directionToFood) < eyeSightAngle / 2)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, directionToFood, out hit, Mathf.Infinity))
                        {
                            if (hit.transform.GetComponentInParent<ConsumableController>() == inspectedItem)
                            {
                                visibleFood.Add(item.GetComponentInParent<ConsumableController>());
                            }
                        }
                    }
                }
            }
        }
    }

    void FindPredators()
    {
        spottedPredators.Clear();

        Collider[] predatorsInArea = Physics.OverlapSphere(transform.position, eyeSightRange, predatorMask);
        foreach (Collider item in predatorsInArea)
        {
            Transform inspectedItem = item.GetComponentInParent<Transform>();
            
            if (!spottedPredators.Contains(inspectedItem))
            {
                Vector3 directionToPredator = (inspectedItem.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToPredator) < eyeSightAngle / 2)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, directionToPredator, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.GetComponentInParent<Transform>() == inspectedItem)
                        {
                            spottedPredators.Add(item.GetComponentInParent<Transform>());
                        }
                    }
                }

            }
        }
    }

    void GetClosestPredator()
    {
        Transform closest = null;
        float distanceToCurrentClosest = 0.0f;
        foreach (Transform item in spottedPredators)
        {
            if (closest == null)
            {
                closest = item;
                distanceToCurrentClosest = Vector3.Distance(transform.position, closest.position);
            }
            else
            {
                float distanceToItem = Vector3.Distance(transform.position, item.position);
                if(distanceToItem< distanceToCurrentClosest)
                {
                    closest = item;
                    distanceToCurrentClosest = distanceToItem;
                }
            }
        }
    }


}
