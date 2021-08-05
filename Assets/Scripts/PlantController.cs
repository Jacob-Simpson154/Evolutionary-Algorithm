using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlantController : ConsumableController
{
    [SerializeField] int currentStage = -1;
    [SerializeField]float growthTimer = 0, growthInterval = 0;
    [SerializeField] List<GrowthStage> stages;

    TimeController timeController;
    TileController tileController;

    private void Start()
    {
        timeController = FindObjectOfType<TimeController>();
        timeController.AddDailyListener(AgeUp);

        tileController = GetComponentInParent<TileController>();
        tileController.AddNourishment(this);

        NextStage();
    }

    void AgeUp()
    {
        growthTimer++;
        if(growthTimer>=growthInterval && currentStage<stages.Count-1)
        {
            NextStage();
        }
    }

    void NextStage()
    {
        currentStage++;
        foreach (GameObject item in stages[currentStage].GO)
        {
            item.SetActive(true);
        }
        nourishment.nutritionalAmount = stages[currentStage].nutritionAmount;
        nourishment.hydrationAmount = stages[currentStage].hydrationAmount;
        growthInterval = stages[currentStage].daysToMature;
    }
}

[Serializable]
public struct GrowthStage
{
    [Tooltip("Asthetic desgin")]
    public List<GameObject> GO;
    [Tooltip("The amount of food in grams provided per stage")]
    public float hydrationAmount;
    public float nutritionAmount;
    public float daysToMature;
}
