using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Transform nodePosition;
    public bool canTraverse;

    public List<ConsumableController> trackedNourishment = new List<ConsumableController>();
    public ConsumableDetails tileNourishment;

    [SerializeField]float temperature = 1.0f;

    TimeController timeController;
    CalenderWeather weatherController;

    private void Awake()
    {
        tileNourishment = new ConsumableDetails();
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
        //Nourishment
        tileNourishment.hydrationAmount = 0;
        tileNourishment.nutritionalAmount = 0;

        foreach (ConsumableController item in trackedNourishment)
        {
            tileNourishment.hydrationAmount += item.nourishment.hydrationAmount;
            tileNourishment.nutritionalAmount += item.nourishment.nutritionalAmount;
        }

        //Temperature
        if (weatherController.IsAffectedByWind(this.transform))
        {
            temperature = weatherController.GetTemperatureAfterWindChill();
        } else temperature = weatherController.GetAmbientTemperature();
    }

    public ConsumableDetails GetNourishment()
    {
        return tileNourishment;
    }
}
