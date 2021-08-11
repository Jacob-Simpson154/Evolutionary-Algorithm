using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureEmitter : MonoBehaviour
{
    public TileController[] tiles;
    TimeController timeController;
    CalenderWeather weatherController;
    public float temperature;
    private void Start()
    {
        tiles = FindObjectsOfType<TileController>();
        timeController = FindObjectOfType<TimeController>();
        weatherController = FindObjectOfType<CalenderWeather>();
    }

    void Update()
    {
        if(timeController.IsDay())
        {
            //temperature = weatherController.GetDailyTemperature();

            //foreach (TileController item in tiles)
            //{
            //    Vector3 dir = item.transform.position - transform.position;
            //    RaycastHit hit;
            //    if(Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            //    {
            //        //If true then tile is in direct sunlight
            //        if(hit.transform == item.transform)
            //        {
            //            float sampleTemp = item.GetTemperature();
            //            if (sampleTemp < temperature)
            //                item.ChangeTemperature(timeController.GetDayTimer());
            //            else if(sampleTemp > temperature)
            //                item.ChangeTemperature(-timeController.GetDayTimer());
            //        }
            //    }
            //}
        }
    }
}
