using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Fur : GeneBase
{
    [SerializeField] Color colour;

    public override void ApplyGeneticInformation(Transform parent)
    {
        parent.GetComponent<Renderer>().material.color = colour;
    }
}

