using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitClass : AnimalBaseClass
{
    [Header("Genetic Components")]
    public RabbitGenetics genes;

    private void Awake()
    {
        DetermineSex();

        genes.size.ApplyGeneticInformation(transform);
        genes.fur.ApplyGeneticInformation(transform);
        //genes.speed.ApplyGeneticInformation(transform);
        genes.eyesight.ApplyGeneticInformation(transform);
        genes.lifeExpectancy.ApplyGeneticInformation(transform);
        //genes.eyesight.ApplyGeneticInformation(transform);

        timeController = FindObjectOfType<TimeController>();
        timeController.AddDailyListener(AgeUpByDay);

        sizeIncreaseByDay = (matureSize-transform.localScale) / ageOfMaturityInDays;
    }

    private void Update()
    {
        if (isDead == false)
        {
            FindWater();
            FindFood();
        }
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


}
