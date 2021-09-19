using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietController : MonoBehaviour
{
    AnimalManager manager;

    [Header("Diet")]
    public List<ConsumableController> visibleFood = new List<ConsumableController>();
    public List<ConsumableController> visibleWater = new List<ConsumableController>();
    public LayerMask consumableMask;

    public float food = 0;
    public float water = 0;
    float hungry = 40;
    float thirsty = 40;


    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public bool IsThirsty()
    {
        if(water <= thirsty)
            return true;
        return false;
    }

    public bool IsHungry()
    {
        if(food<=hungry)
            return true;
        return false;
    }

    public bool FindWater()
    {
        visibleWater.Clear();

        Collider[] waterInArea = Physics.OverlapSphere(transform.position, manager.eyeSightRange, consumableMask);
        foreach (Collider item in waterInArea)
        {
            ConsumableController inspectedItem = item.GetComponentInParent<ConsumableController>();
            if (inspectedItem.nourishment.hydrationAmount > 0)
            {
                if (!visibleWater.Contains(inspectedItem))
                {
                    Vector3 directionToWater = (inspectedItem.transform.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, directionToWater) < manager.eyeSightAngle / 2)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, directionToWater, out hit, Mathf.Infinity))
                        {
                            if (hit.transform.GetComponentInParent<ConsumableController>() == inspectedItem)
                            {
                                visibleWater.Add(item.GetComponentInParent<ConsumableController>());
                            }
                        }
                    }
                }
            }
        }

        if (visibleWater.Count > 0)
            return true;

        return false;
    }

    public bool FindFood()
    {
        visibleFood.Clear();

        Collider[] foodInArea = Physics.OverlapSphere(transform.position, manager.eyeSightRange, consumableMask);
        foreach (Collider item in foodInArea)
        {
            ConsumableController inspectedItem = item.GetComponentInParent<ConsumableController>();
            if (inspectedItem.nourishment.nutritionalAmount > 0)
            {
                if (!visibleFood.Contains(inspectedItem))
                {
                    Vector3 directionToFood = (inspectedItem.transform.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, directionToFood) < manager.eyeSightAngle / 2)
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

        if (visibleFood.Count > 0)
            return true;

        return false;
    }

    public bool CanSeeConsumable(int type)
    {
        if (type == 0 && visibleWater.Count>0)
        {
            return true;
        }

        if (type == 1 && visibleFood.Count>0)
        {
            return true;
        }


        return false;
    }

    public Transform GetClosestConsumable(int type)
    {
        if (type == 0)
        {
            if (visibleWater.Count > 0)
            {
                Transform closestWater = null;

                foreach (ConsumableController item in visibleWater)
                {
                    if (closestWater == null && item.GetHydration() > 0)
                        closestWater = item.transform;

                    if (closestWater != null && manager.GetDistance(item.transform.position) < manager.GetDistance(closestWater.position) && closestWater.GetComponent<ConsumableController>().GetHydration() < item.GetHydration())
                        closestWater = item.transform;
                }

                return closestWater;
            }
        }

        if (type == 1)
        {
            if (visibleFood.Count > 0)
            {
                Transform closestWater = null;

                foreach (ConsumableController item in visibleFood)
                {
                    if (closestWater == null && item.GetNutrition() > 0)
                        closestWater = item.transform;

                    if (closestWater != null && manager.GetDistance(item.transform.position) < manager.GetDistance(closestWater.position) && closestWater.GetComponent<ConsumableController>().GetNutrition() < item.GetNutrition())
                        closestWater = item.transform;
                }

                return closestWater;
            }
        }


        return null;
    }

    public void Consume(ConsumableDetails item)
    {
        float temp = water;
        water += item.hydrationAmount;
        float difference = water - temp;
        item.hydrationAmount -= difference;

        temp = food;
        food += item.nutritionalAmount;
        difference = food - temp;
        item.nutritionalAmount -= difference;
    }
}
