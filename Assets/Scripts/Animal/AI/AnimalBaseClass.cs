using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBaseClass : MonoBehaviour
{
    public Sex sex;
    public Species species = Species.rabbit;

    [Header("Status")]
    public bool isDead = false;
    public string causeOfDeath = "Not applicable";
    public float food = 100;
    [SerializeField] float hungry = 50;
    public float water = 100;
    [SerializeField] float thirsy = 50;

    [Header("Age")]
    public TimeController timeController;
    public int currentAgeInDays;
    public float ageOfMaturityInDays;
    public float ageOfDeathInDays;

    [Header("Size")]
    public Vector3 matureSize;
    public Vector3 sizeIncreaseByDay;

    [Header("Diet")]
    public List<ConsumableController> visibleFood = new List<ConsumableController>();
    public List<ConsumableController> visibleWater = new List<ConsumableController>();
    public LayerMask consumableMask;

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;

    [Header("Navigation")]
    public bool hasTarget = false;
    public Action currentState;
    public Transform target;
    public List<Vector3> path = new List<Vector3>();


    public virtual void Activate()
    {

    }

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

    public Transform GetClosestWater()
    {
        if (visibleWater.Count > 0)
        {
            Transform cloestestSource = visibleWater[0].transform;
            foreach (ConsumableController item in visibleWater)
            {
                if (GetDistance(item.transform.position) < GetDistance(cloestestSource.position))
                    cloestestSource = item.transform;
            }

            return cloestestSource;
        }
        else return null;
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

    public Sex DetermineSex()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < 0.5f)
            sex = Sex.Female;
        else sex = Sex.Male;

        return sex;
    }

    public void Death(string cause)
    {
        isDead = true;
        causeOfDeath = cause;
    }

    public bool IsHungry()
    {
        if (food <= hungry)
            return true;
        else return false;
    }

    public bool IsThirsty()
    {
        if (water <= thirsy)
            return true;
        else return false;
    }

    public float GetDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }









    public virtual void ChooseState()
    {

    }
    public virtual void FinaliseState()
    {

    }



    public void MoveToTarget(Vector3 target)
    {
        PathManager.RequestPath(new PathRequest(transform.position, target, PathCallback));
    }

    public void PathCallback(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            Debug.Log("Found path");
            path.Clear();
            path.AddRange(waypoints);
        }
        else
        {
            Debug.Log("cant find path");
        }
    }

    public Vector3 CreatePointInReach(float distance)
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * distance;
        RaycastHit hit;
        randomPoint.y = 500f;
        if (Physics.Raycast(randomPoint, Vector3.down, out hit, 1000f))
        {
            return hit.point;
        }
        else return randomPoint;
    }
}


public enum Action
{
    hungry, thirsty, moving
}