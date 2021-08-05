using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CalenderWeather : MonoBehaviour
{
    //Weather taken from UK stats for London
    //https://weather-and-climate.com/average-monthly-Rainfall-Temperature-Sunshine,London,United-Kingdom

    public WeatherByMonth monthOfJanuary;
    public WeatherByMonth monthOfFebruary;
    public WeatherByMonth monthOfMarch;
    public WeatherByMonth monthOfApril;
    public WeatherByMonth monthOfMay;
    public WeatherByMonth monthOfJune;
    public WeatherByMonth monthOfJuly;
    public WeatherByMonth monthOfAugust;
    public WeatherByMonth monthOfSeptember;
    public WeatherByMonth monthOfOctober;
    public WeatherByMonth monthOfNovember;
    public WeatherByMonth monthOfDecember;

    int currentMonth = 0;
    public WeatherByMonth currentMonthDetails;

    //Daily variables
    public float ambientTemperature;
    public float temperatureAfterWindchill;
    public Vector3 windDirection;


    TimeController timeController;

    private void Awake()
    {
        currentMonthDetails = monthOfJanuary;
        timeController = FindObjectOfType<TimeController>();
        timeController.AddMonthlyListener(UpdateMonth);
        timeController.AddDailyListener(UpdateDailyVariables);

        UpdateDailyVariables();
    }

    public WeatherByMonth GetWeatherByMonth(float month)
    {
        WeatherByMonth tempToReturn = new WeatherByMonth();
        switch (month)
        {
            case 0: tempToReturn = monthOfJanuary; break;
            case 1: tempToReturn = monthOfFebruary; break;
            case 2: tempToReturn = monthOfMarch; break;
            case 3: tempToReturn = monthOfApril; break;
            case 4: tempToReturn = monthOfMay; break;
            case 5: tempToReturn = monthOfJune; break;
            case 6: tempToReturn = monthOfJuly; break;
            case 7: tempToReturn = monthOfAugust; break;
            case 8: tempToReturn = monthOfSeptember; break;
            case 9: tempToReturn = monthOfOctober; break;
            case 10: tempToReturn = monthOfNovember; break;
            case 11: tempToReturn = monthOfDecember; break;
        }

        return tempToReturn;
    }

    public void UpdateDailyVariables()
    {
        //Temperature
        float temp = UnityEngine.Random.Range(currentMonthDetails.minTemperature, currentMonthDetails.maxTemperature);
        ambientTemperature = CalculateAmbientTemperature(temp, currentMonthDetails.humidityPercentage);

        //Wind
        windDirection = new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));

    }


    public float GetAmbientTemperature()
    {
        return ambientTemperature;
    }

    public float GetTemperatureAfterWindChill()
    {
        return GetTemperatureAfterWindchill(ambientTemperature, currentMonthDetails.averageWindSpeed);
    }

    public void UpdateMonth()
    {
        currentMonth++;
        if(currentMonth>=12)
        {
            currentMonth = 0;
        }
        currentMonthDetails = GetWeatherByMonth(currentMonth);
    }

    public float CalculateAmbientTemperature(float temperature, float humidity)
    {
        //Calculation is based of heat index
        //https://en.wikipedia.org/wiki/Heat_index
        //https://www.ncbi.nlm.nih.gov/pmc/articles/PMC3801457/
        float farenheit = ConvertToFarenheit(temperature);

        if(farenheit < 40)
        {
            return ConvertToCelcius(farenheit);
        }

        float a = -10.3f + 1.1f * farenheit + 0.047f * humidity;

        if(a < 79)
        {
            return ConvertToCelcius(a);
        }

        const float c1 = -42.379f;
        const float c2 = -2.04901523f;
        const float c3 = -10.14333127f;
        const float c4 = -0.224755415f;
        const float c5 = -71.3783f;
        const float c6 = -56.81717f;
        const float c7 = -15.2874f;
        const float c8 = 81.282f;
        const float c9 = -25.9f;
        float tempSquared = farenheit * farenheit;
        float humSquared = humidity * humidity;

        float heatIndex = c1
            + (c2 * temperature)
            + (c3 * humidity)
            + (c4 * temperature * humidity)
            + (c5 * tempSquared)
            + (c6 * humSquared)
            + (c7 * tempSquared * humidity)
            + (c8 * temperature * humSquared)
            + (c9 * tempSquared * humSquared);

        if (humidity < 13 && (farenheit > 80 && farenheit < 112))
        {
            return ConvertToCelcius(Mathf.Pow(heatIndex - ((13 - humidity) / 4) * ((17 - (farenheit - 95) / 17)), 0.5f));
        }

        if (humidity > 85 && (farenheit > 80 && farenheit < 87))
        {
            return ConvertToCelcius(heatIndex + 0.02f * (humidity - 85) * (87 - farenheit));
        }


        return ConvertToCelcius(heatIndex);
    }

    float ConvertToFarenheit(float celcius)
    {
        return (celcius * (9.0f / 5.0f)) + 32;
    }

    float ConvertToCelcius(float farenheit)
    {
        return ((farenheit - 32) * (5.0f / 9.0f));
    }

    float GetTemperatureAfterWindchill(float temperature, float wind)
    {
        //https://www.alpinetrek.co.uk/wind-chill-calculator/

        float farenheit = ConvertToFarenheit(temperature);
        float mph = ConvertToMilesPerHour(wind);

        const float c1 = 35.74f;
        const float c2 = 0.6215f;
        const float c3 = 35.75f;
        const float c4 = 0.4275f;

        float windChill = c1
            + (c2 * farenheit)
            - (c3 * Mathf.Pow(mph, 0.16f))
            + (c4 * farenheit * Mathf.Pow(mph, 0.16f));


        return ConvertToCelcius(windChill);
    }

    float ConvertToMilesPerHour(float mps)
    {
        return mps * 2.237f;
    }

    public bool IsAffectedByWind(Transform a)
    {
        RaycastHit hit;
        //If hits object then the transform is blocked from wind
        if (Physics.Raycast(a.position, -windDirection, out hit, Mathf.Infinity))
        {
            return true;
        } 
        
        //If no hit object then transform in wind

        else

        return false;
    }
}

[Serializable]
public struct WeatherByMonth
{
    public float minTemperature;
    public float maxTemperature;
    public float changeOfPrecipitation;
    public float precipitationAmount;
    public float humidityPercentage;
    public float averageWindSpeed;
}
