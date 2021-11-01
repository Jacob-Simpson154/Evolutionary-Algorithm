using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    [SerializeField] AnimalManager manager;
    [SerializeField] float currentTemperature = 10.0f;

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public void TemperatureCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2.0f))
        {
            if (hit.transform.GetComponent<TileController>())
            {
                TileController tc = hit.transform.GetComponent<TileController>();
                float tcTemperature = tc.temperature;

                if (tcTemperature + manager.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().length * manager.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().thickness > currentTemperature)
                    currentTemperature += manager.timeCon.GetDayTimer();
                else if (tcTemperature + manager.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().length * manager.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().thickness < currentTemperature)
                    currentTemperature -= manager.timeCon.GetDayTimer();

                if (currentTemperature < 0)
                {
                    manager.Death();
                }
            }
        }
    }

    public float GetTemperature()
    {
        return currentTemperature;
    }
}
