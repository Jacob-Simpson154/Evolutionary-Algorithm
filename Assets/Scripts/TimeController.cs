using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    public float realSecondsPerSimulatedDay = 60;

    public float dayTimer = -.25f;


    public float calender_day = 0;
    public float calender_month = 0;
    public float calender_year = 0;

    public Text timeTxt;
    public Text calenderTxt;
    public Text simulationSpeed;
    public Slider simulationSpeedSlider;
    public GameObject sun;

    UnityEvent dailyEvents;
    UnityEvent monthlyEvents;

    private void Update()
    {
        dayTimer += GetDayTimer();

        float hours = GetHour();
        float minutes = hours * 60;
        timeTxt.text = hours.ToString("00") + ":" + minutes.ToString("00");
        sun.transform.eulerAngles = new Vector3(((dayTimer * 360) - 90), 0, 0);

        if (dayTimer>1.0f)
        {
            CalculateCalender();    
        }
    }

    void CalculateCalender()
    {
        dayTimer = 0;

        dailyEvents.Invoke(); //increment age of all life one day

        calender_day++;
        if(calender_day>= 30.4167f)
        {
            calender_day = 0;
            calender_month++;
            monthlyEvents.Invoke();
        }

        if(calender_month>=12)
        {
            calender_month = 0;
            calender_year++;
        }

        calenderTxt.text = calender_day.ToString("00") + "/" + (calender_month+1).ToString("00") + "/" + calender_year.ToString("00");
    }


    public float GetDayTimer()
    {
        return Time.deltaTime / realSecondsPerSimulatedDay;
    }

    public void SliderValueChange()
    {
        realSecondsPerSimulatedDay = simulationSpeedSlider.value;
        simulationSpeed.text = "Real Seconds Per Simulated Day: " + realSecondsPerSimulatedDay;
    }

    public void AddDailyListener(UnityAction action)
    {
        if (dailyEvents == null)
            dailyEvents = new UnityEvent();

        dailyEvents.AddListener(action);
    }

    public void AddMonthlyListener(UnityAction action)
    {
        if (monthlyEvents == null)
            monthlyEvents = new UnityEvent();

        monthlyEvents.AddListener(action);
    }

    public bool IsDay()
    {
        if (GetHour() > 6 && GetHour() < 18)
        {
            return true;
        }
        else return false;
    }

    public float GetHour()
    {
        return dayTimer * 24;
    }
}