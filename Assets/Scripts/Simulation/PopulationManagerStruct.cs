using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PopulationManagerStruct
{
    public int populationAmount;
    public GameObject animal;
    public Species species;

    [Header("Size genetic variation")]
    [Range(0, 1)]
    public float sizeVariationMin;
    [Range(1, 10)]
    public float sizeVariationMax;

    [Header("Color genetic variation")]
    [Range(0, 1)]
    public float colourRangeMin;
    [Range(1, 10)]
    public float colourRangeMax;

    [Header("Speed genetic variation")]
    [Range(0, 1)]
    public float speedVariationMin;
    [Range(1, 10)]
    public float speedVariationMax;

    [Header("Eye sight range genetic variation")]
    [Range(0, 1)]
    public float eyeSightRangeMin;
    [Range(1, 10)]
    public float eyeSightRangeMax;

    [Header("Eye  genetic FOV variation")]
    [Range(0, 1)]
    public float eyeSightFOVMin;
    [Range(1, 10)]
    public float eyeSightFOVMax;

    [Header("Male Min Maturity variation")]
    [Range(0, 1)]
    public float maturityMinMaleVariationMin;
    [Range(1, 10)]
    public float maturityMinMaleVariationMax;

    [Header("Male Max Maturity variation")]
    [Range(0, 1)]
    public float maturityMaxMaleVariationMin;
    [Range(1, 10)]
    public float maturityMaxMaleVariationMax;

    [Header("Female Min Maturity variation")]
    [Range(0, 1)]
    public float maturityMinFemaleVariationMin;
    [Range(1, 10)]
    public float maturityMinFemaleVariationMax;

    [Header("Female Max Maturity variation")]
    [Range(0, 1)]
    public float maturityMaxFemaleVariationMin;
    [Range(1, 10)]
    public float maturityMaxFemaleVariationMax;

    [Header("Minimum lifetime variation")]
    [Range(0, 1)]
    public float expectedLifetimeMinVariationMin;
    [Range(1, 10)]
    public float expectedLifetimeMinVariationMax;

    [Header("Maximum lifetime variation")]
    [Range(0, 1)]
    public float expectedLifetimeMaxVariationMin;
    [Range(1, 10)]
    public float expectedLifetimeMaxVariationMax;
}

public enum Species
{
    rabbit, NotImplemented, 
}
