using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneBase : MonoBehaviour
{
    /// <summary>
    /// Used for the population stage.
    /// </summary>
    public virtual void Setup(float modifer)
    {

    }

    public virtual void Creation(float value)
    {

    }

    /// <summary>
    /// Used after animal is created.
    /// </summary>
    public virtual void ApplyGeneticInformation(Transform parent)
    {

    }

    /// <summary>
    /// Used for Crossover stage.
    /// </summary>
    public virtual float GetGene()
    {
        return 0;
    }

    /// <summary>
    /// Used for Crossover stage.
    /// </summary>
    public virtual float GetGene(int i)
    {
        return 0;
    }
}
