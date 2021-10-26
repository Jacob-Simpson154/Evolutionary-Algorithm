using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [Header("Navigation")]
    public Transform nodePosition;
    public bool canTraverse;

    [Header("Consumable")]
    [SerializeField] float tileFood = 0;
    [SerializeField] float tileWater = 0;
    public List<ConsumableController> trackedNourishment = new List<ConsumableController>();

    public float temperature = 1.0f;

    TimeController timeController;
    CalenderWeather weatherController;

    private void Awake()
    {
        timeController = FindObjectOfType<TimeController>();
        timeController.AddDailyListener(UpdateTileInformation);
        weatherController = FindObjectOfType<CalenderWeather>();
        temperature = weatherController.GetAmbientTemperature();
    }

    public void AddNourishment(ConsumableController cc)
    {
        trackedNourishment.Add(cc);
    }

    void UpdateTileInformation()
    {
        //Temperature
        if (weatherController.IsAffectedByWind(this.transform))
        {
            temperature = weatherController.GetTemperatureAfterWindChill();
        } else temperature = weatherController.GetAmbientTemperature();
    }

    /// <summary>
    /// Consume the tiles nourishment, prioritise plants first
    /// </summary>
    public void Consume(float water, float food)
    {
        foreach (ConsumableController item in trackedNourishment)
        {
            if (item.nourishment.nutritionalAmount >= food)
            {
                item.nourishment.nutritionalAmount -= food;
                food = 0;
            }
            else
            if (item.nourishment.nutritionalAmount <= food)
            {
                food -= item.nourishment.nutritionalAmount;
                item.nourishment.nutritionalAmount = 0;
            }
        }

        tileWater -= water;
        tileFood -= food;
    }

    /// <summary>
    /// type 0 = water. type 1 = food
    /// </summary>
    public float GetAvailableNourishment(int type)
    {
        float total = 0.0f;

        if(type == 0)
        {
            total = tileWater;

            foreach (ConsumableController item in trackedNourishment)
            {
                string name = transform.name;
                total += item.GetHydration();
            }
        }

        if(type == 1)
        {
            total = tileFood;
            foreach (ConsumableController item in trackedNourishment)
            {
                total += item.GetNutrition();
            }
        }

        return total;
    }
}
