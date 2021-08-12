using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Fur : GeneBase
{
    [SerializeField] Color colour;
    [SerializeField] float length;
    public void Setup(float minVariation, float maxVariation)
    {
        float r = colour.r * Random.Range(minVariation, maxVariation);
        float g = colour.g * Random.Range(minVariation, maxVariation);
        float b = colour.b * Random.Range(minVariation, maxVariation);
        colour = new Color(r, g, b);
    }
    public override void ApplyGeneticInformation(Transform parent)
    {
        parent.GetComponent<Renderer>().material.color = colour;
    }
}

