using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBaseClass : MonoBehaviour
{
    public Sex sex;
    public float food;
    public float water;

    public List<ConsumableController> visibleFood = new List<ConsumableController>();
    public List<ConsumableController> visibleWater = new List<ConsumableController>();

    [Header("Genetics - Eyesight")]
    public float eyeSightRange = 10.0f;
    public float eyeSightAngle = 10.0f;

    public LayerMask consumableMask;


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
}
