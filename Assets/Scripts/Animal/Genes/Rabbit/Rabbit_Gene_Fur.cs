using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Fur : GeneBase
{
    [SerializeField] Color colour;
    [SerializeField] float length; //not implemented yet
    public void Setup(float minVariation, float maxVariation)
    {
        float r = colour.r * Random.Range(minVariation, maxVariation);
        float g = colour.g * Random.Range(minVariation, maxVariation);
        float b = colour.b * Random.Range(minVariation, maxVariation);
        colour = new Color(r, g, b);
    }

    public void Creation(Color col, float len)
    {
        colour = col;
        length = len;
    }

    public override void ApplyGeneticInformation(AnimalManager manager)
    {
        manager.GetComponent<Renderer>().material.color = colour;
    }

    public override float GetGene(int i)
    {
        if (i == 0)
            return colour.r;
        if (i == 1)
            return colour.g;
        if (i == 2)
            return colour.b;
        return 0;
    }
}

