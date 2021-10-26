using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    public ConsumableDetails nourishment;

    public float GetHydration()
    {
        return nourishment.hydrationAmount;
    }
    public float GetNutrition()
    {
        return nourishment.nutritionalAmount;
    }

    public void Consume()
    {

    }
}
