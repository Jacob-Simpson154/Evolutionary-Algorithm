using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietController : MonoBehaviour
{
    [SerializeField] AnimalManager manager;

    [Header("Diet")]
    public List<TileController> visibleFood = new List<TileController>();
    public LayerMask foodMask;
    public List<TileController> visibleWater = new List<TileController>();
    public LayerMask waterMask;

    [Header("Current Dietry Values")]
    public float food = 0;
    public float water = 0;

    [Header("Malnourishment")]
    public float daysWithoutWater = 0;
    public float daysUntilDehydrationDeath = 3;
    public float daysWithoutFood = 0;
    public float daysUntilStarvingDeath = 10;

    [Header("Daily requirements")]
    public float foodConsumedPerDay = 2000;
    public float waterConsumedPerDay = 2000;

    [Header("Percentile motivation")]
    [Tooltip("At what percentage of current food to food required per day does animal get hungry and start looking for food")]
    public float hungry = 40;
    [Tooltip("At what percentage of current water to water required per day does animal get thirsty and start looking for water")]
    public float thirsty = 40;

    

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    void Update()
    {
        float t = manager.timeCon.GetDayTimer();
        food -= foodConsumedPerDay*t;
        water -= waterConsumedPerDay*t;

        if(IsDehydrated())
        {
            daysWithoutWater += t;
            if(daysWithoutWater>=daysUntilDehydrationDeath)
            {
                manager.Death();
            }
        }

        if (IsStarving())
        {
            daysWithoutFood += t;
            if (daysWithoutFood >= daysUntilStarvingDeath)
            {
                manager.Death();
            }
        }
    }

    public bool IsThirsty()
    {
        if (((water / waterConsumedPerDay) * 100) <= thirsty)
            return true;
        return false;
    }

    bool IsDehydrated()
    {
        if (water <= 0)
            return true;
        else return false;
    }

    public bool IsHungry()
    {
        if(((food/foodConsumedPerDay)*100)<=hungry)
            return true;
        return false;
    }

    bool IsStarving()
    {
        if (food <= 0)
            return true;
        else return false;
    }

    public bool FindWater()
    {
        visibleWater.Clear();

        for (int scanRange = 1; scanRange < manager.eyeSightRange; scanRange++)
        {
            Collider[] foodInArea = Physics.OverlapSphere(manager.transform.position, scanRange, waterMask);
            if (foodInArea.Length == 0)
                continue;

            foreach (Collider item in foodInArea)
            {
                TileController inspectedItem = item.GetComponentInParent<TileController>();

                if (inspectedItem.GetAvailableNourishment(0) > 0)
                {
                    if (!visibleWater.Contains(inspectedItem))
                    {
                        Vector3 directionToFood = (inspectedItem.transform.position - manager.transform.position).normalized;
                        if (Vector3.Angle(manager.transform.forward, directionToFood) < manager.eyeSightAngle / 2)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(manager.transform.position, directionToFood, out hit, Mathf.Infinity))
                            {
                                if (hit.transform.GetComponentInParent<TileController>() == inspectedItem)
                                {
                                    visibleWater.Add(item.GetComponentInParent<TileController>());
                                }
                            }
                        }
                    }
                }
            }

            if (visibleWater.Count > 0)
                return true;
        }

        return false;
    }

    public bool FindFood()
    {
        visibleFood.Clear();

        for (int scanRange = 1; scanRange < manager.eyeSightRange; scanRange++)
        {
            Collider[] foodInArea = Physics.OverlapSphere(manager.transform.position, scanRange, foodMask);
            if (foodInArea.Length == 0)
                continue;

            foreach (Collider item in foodInArea)
            {
                TileController inspectedItem = item.GetComponentInParent<TileController>();

                if (inspectedItem.GetAvailableNourishment(1) > 0)
                {
                    if (!visibleFood.Contains(inspectedItem))
                    {
                        Vector3 directionToFood = (inspectedItem.transform.position - manager.transform.position).normalized;
                        if (Vector3.Angle(manager.transform.forward, directionToFood) < manager.eyeSightAngle / 2)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(manager.transform.position, directionToFood, out hit, Mathf.Infinity))
                            {
                                if (hit.transform.GetComponentInParent<TileController>() == inspectedItem)
                                {
                                    visibleFood.Add(item.GetComponentInParent<TileController>());
                                }
                            }
                        }
                    }
                }
            }

            if (visibleFood.Count > 0)
                return true;
        }

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

                foreach (TileController item in visibleWater)
                {
                    if (closestWater == null && item.GetAvailableNourishment(0) > 0)
                        closestWater = item.transform;

                    if (closestWater != null && (manager.GetDistance(item.transform.position) < manager.GetDistance(closestWater.position)) && (closestWater.GetComponent<TileController>().GetAvailableNourishment(0) < item.GetAvailableNourishment(0)))
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

                foreach (TileController item in visibleFood)
                {
                    if (closestWater == null && item.GetAvailableNourishment(1) > 0)
                        closestWater = item.transform;

                    if (closestWater != null && (manager.GetDistance(item.transform.position) < manager.GetDistance(closestWater.position)) && (closestWater.GetComponent<TileController>().GetAvailableNourishment(1) < item.GetAvailableNourishment(1)))
                        closestWater = item.transform;
                }

                return closestWater;
            }
        }


        return null;
    }

    public void Consume(TileController item)
    {
        float availableWater = item.GetAvailableNourishment(0);
        float availableFood = item.GetAvailableNourishment(1);

        float targetWater = waterConsumedPerDay - water;
        float targetFood = foodConsumedPerDay - food;

        float waterToConsume = 0;
        float foodToConsume = 0;

        if (availableWater >= targetWater)
        {
            waterToConsume = targetWater;
        }
        else

        if (availableWater <= targetWater)
        {
            waterToConsume = availableWater;
        }

        if (availableFood>=targetFood)
        {
            foodToConsume = targetFood;
        } else
            
        if(availableFood<=targetFood)
        {
            foodToConsume = availableFood;
        }

        water += waterToConsume;
        if (water > 0)
            daysWithoutWater = 0;
        food += foodToConsume;
        if (food > 0)
            daysWithoutFood = 0;

        item.Consume(waterToConsume, foodToConsume);
    }
}
