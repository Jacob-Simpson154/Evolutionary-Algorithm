using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBaseClass : MonoBehaviour
{
    public Sex sex;

    [Header("Age")]
    public TimeController timeController;
    public int currentAgeInDays;
    public int ageOfMaturityInDays;
    public int ageOfDeathInDays;
    public bool isDead = false;
    public string causeOfDeath = "Not applicable";

    [Header("Size")]
    public Vector3 matureSize;
    public Vector3 sizeIncreaseByDay;

    [Header("Diet")]
    public List<ConsumableController> visibleFood = new List<ConsumableController>();
    public List<ConsumableController> visibleWater = new List<ConsumableController>();
    public float food;
    public float water;
    public LayerMask consumableMask;

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;



    //Find consumables
    //Find food is virtual as some animals may have specific diets whereas all animals need food
    public virtual void FindFood()
    {

    }

    public void FindWater()
    {
        visibleWater.Clear();

        Collider[] waterInArea = Physics.OverlapSphere(transform.position, eyeSightRange, consumableMask);
        foreach (Collider item in waterInArea)
        {
            ConsumableController inspectedItem = item.GetComponentInParent<ConsumableController>();
            if (inspectedItem.nourishment.hydrationAmount>0)
            {
                if(!visibleWater.Contains(inspectedItem))
                {
                    Vector3 directionToWater = (inspectedItem.transform.position - transform.position).normalized;
                    if(Vector3.Angle(transform.forward, directionToWater)<eyeSightAngle/2)
                    {
                        RaycastHit hit;
                        if(Physics.Raycast(transform.position, directionToWater, out hit, Mathf.Infinity))
                        {
                            if(hit.transform.GetComponentInParent<ConsumableController>() == inspectedItem)
                            {
                                visibleWater.Add(item.GetComponentInParent<ConsumableController>());
                            }
                        }
                    }
                }
            }
        }
    }

    public void AgeUpByDay()
    {
        currentAgeInDays++;

        if(currentAgeInDays<= ageOfMaturityInDays)
            transform.localScale += sizeIncreaseByDay;

        if(currentAgeInDays>=ageOfDeathInDays)
        {
            Death("Old Age");
        }
    }

    public void DetermineSex()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < 0.5f)
            sex = Sex.Female;
        else sex = Sex.Male;
    }

    public void Death(string cause)
    {
        isDead = true;
        causeOfDeath = cause;
    }
}
