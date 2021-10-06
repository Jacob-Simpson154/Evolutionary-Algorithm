using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepController : MonoBehaviour
{
    AnimalManager manager;

    [SerializeField] float currentSleep = 0.0f;
    [SerializeField] float averageSleepPerDayInHours = 8.0f;
    [SerializeField] float timeWithoutSleep = 0.0f;
    [SerializeField] bool isSleeping = false;

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    private void Update()
    {
        if(isSleeping)
        {
            currentSleep += manager.timeCon.GetDayTimer()*24;
        }
        else
        {
            timeWithoutSleep += manager.timeCon.GetDayTimer() * 24;
        }
    }

    public void SetSleeping(bool state)
    {
        isSleeping = state;

        if (isSleeping == false)
        {
            currentSleep = 0.0f;
        }
        else
        {
            timeWithoutSleep = 0;
        }
    }

    public bool GetSleeping()
    {
        return isSleeping;
    }

    public bool NeedSleep()
    {
        if (timeWithoutSleep >= (24 - averageSleepPerDayInHours))
            return true;
        else return false;
    }

    public bool NeedsMoreSleep()
    {
        if (currentSleep < averageSleepPerDayInHours)
            return true;
        else return false;
    }
}
