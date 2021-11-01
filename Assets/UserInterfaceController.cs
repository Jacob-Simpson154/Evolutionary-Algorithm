using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] Text temperature;
    [SerializeField] Text populationSize;

    [SerializeField] PopulationManager manager;

    [Header("Rabbit")]
    [SerializeField] AnimalManager rab;
    [SerializeField] int index = 0;
    [SerializeField] Text rabbitIndex;
    [SerializeField] Text age;
    [SerializeField] Text temp;
    [SerializeField] Text sex;
    [SerializeField] Text length;
    [SerializeField] Text thickness;

    public void UpdateTemperature(float temp)
    {
        temperature.text = "" + temp + "°C";
    }

    public void UpdatePopulation(float size)
    {
        populationSize.text = "" + size;
    }

    public void NextRabbit()
    {
        index++;
        if (index >= manager.GetPopulationCount())
            index = 0;

        DisplayRabbit();
    }

    public void PreviousRabbit()
    {
        index--;
        if (index < 0)
            index = manager.GetPopulationCount()-1;

        DisplayRabbit();
    }

    public void DisplayRabbit()
    {
        if(manager == null)
            manager = FindObjectOfType<PopulationManager>();

        rabbitIndex.text = "Rabbit #" + index;

        rab = manager.GetAnimal(index);

        age.text = rab.ageCurrent.ToString("0.00") + "/" + rab.ageOfDeathInDays.ToString("0.00");
        temp.text = rab.temperature.GetTemperature().ToString("0.00") + "°C";
        sex.text = "" + rab.identity.GetSex();

        length.text = "" + rab.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().GetGene(3);
        thickness.text = "" + rab.chromosomes.GetComponentInChildren<Rabbit_Gene_Fur>().GetGene(4);
    }

    private void Update()
    {
        age.text = rab.ageCurrent.ToString("0.00") + "/" + rab.ageOfDeathInDays.ToString("0.00");
        temp.text = rab.temperature.GetTemperature().ToString("0.00") + "°C";
    }
}
