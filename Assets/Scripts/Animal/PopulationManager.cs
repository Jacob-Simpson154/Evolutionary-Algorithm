﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is used to create population with varying genetics
//Does not effect evolution, just base diversity

public class PopulationManager : MonoBehaviour
{
    [Header("Use to create diversity throughout created population, leave settings at 1 for default")]
    public List<PopulationManagerStruct> population;


    public List<AnimalManager> trackedAnimals;

    UserInterfaceController uiController;

    public void Awake()
    {
        foreach (PopulationManagerStruct item in population)
        {
            Vector3 pos = new Vector3(0, 0.1f, 0);

            for (int i = 0; i < item.populationAmount; i++)
            {
                GameObject animal = Instantiate(item.animal, transform);
                animal.transform.position = pos;
                pos.x++;

                float sizeMod = Random.Range(item.sizeVariationMin, item.sizeVariationMax);
                float furLengthMod = Random.Range(item.lengthRangeMin, item.lengthRangeMax);
                float furThicknessMod = Random.Range(item.thicknessRangeMin, item.thicknessRangeMax);
                float speedMod = Random.Range(item.speedVariationMin, item.speedVariationMax);
                float eyesightRangeMod = Random.Range(item.eyeSightRangeMin, item.eyeSightRangeMax);
                float eyesightFOVMod = Random.Range(item.eyeSightFOVMin, item.eyeSightFOVMax);
                float maturityMaleModMin = Random.Range(item.maturityMinMaleVariationMin, item.maturityMinMaleVariationMax);
                float maturityMaleModMax = Random.Range(item.maturityMinMaleVariationMin, item.maturityMinMaleVariationMax);
                float maturityFemaleModMin = Random.Range(item.maturityMinFemaleVariationMin, item.maturityMinFemaleVariationMax);
                float maturityFemaleModMax = Random.Range(item.maturityMaxFemaleVariationMin, item.maturityMaxFemaleVariationMax);
                float lifeExpectancyMin = Random.Range(item.expectedLifetimeMinVariationMin, item.expectedLifetimeMinVariationMax);
                float lifeExpectancyMax = Random.Range(item.expectedLifetimeMaxVariationMin, item.expectedLifetimeMaxVariationMax);

                if (item.species == Species.rabbit)
                {
                    AnimalManager rabbitClass = animal.GetComponent<AnimalManager>();
                    Chromosome genetics = rabbitClass.chromosomes;

                    genetics.genes[0].Setup(sizeMod);
                    genetics.genes[1].GetComponent<Rabbit_Gene_Fur>().Setup(item.colourRangeMin, item.colourRangeMax, furLengthMod, furThicknessMod);
                    genetics.genes[2].Setup(speedMod);
                    genetics.genes[3].GetComponent<Rabbit_Gene_Eyesight>().Setup(eyesightRangeMod, eyesightFOVMod);

                    if (rabbitClass.identity.DetermineSex() == Sex.Female)
                        genetics.genes[4].GetComponent<Rabbit_Gene_Life>().Setup(Sex.Female, maturityFemaleModMin, maturityFemaleModMax, lifeExpectancyMin, lifeExpectancyMax);
                    else genetics.genes[4].GetComponent<Rabbit_Gene_Life>().Setup(Sex.Male, maturityMaleModMin, maturityMaleModMax, lifeExpectancyMin, lifeExpectancyMax);

                    rabbitClass.ApplyChromosome();
                }

                AddToPopulation(animal.GetComponent<AnimalManager>());
            }
        }

        uiController.DisplayRabbit();
    }

    public void AddToPopulation(AnimalManager manager)
    {
        if (uiController == null)
            uiController = FindObjectOfType<UserInterfaceController>();

        trackedAnimals.Add(manager);
        uiController.UpdatePopulation(trackedAnimals.Count);
    }

    public void RemoveFromPopulation(AnimalManager manager)
    {
        if (uiController == null)
            uiController = FindObjectOfType<UserInterfaceController>();

        trackedAnimals.Remove(manager);
        uiController.UpdatePopulation(trackedAnimals.Count);
    }

    public int GetPopulationCount()
    {
        return trackedAnimals.Count;
    }

    public AnimalManager GetAnimal(int index)
    {
        return trackedAnimals[index];
    }
}