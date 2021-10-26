using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Fur : GeneBase
{
    [SerializeField] Color colour;
    public float length;
    public float thickness; //will act as multiplier for temperature
    public void Setup(float minVariation, float maxVariation, float len, float thick)
    {
        float r = colour.r * Random.Range(minVariation, maxVariation);
        float g = colour.g * Random.Range(minVariation, maxVariation);
        float b = colour.b * Random.Range(minVariation, maxVariation);
        colour = new Color(r, g, b);
        length *= len;
        thickness *= thick;
    }

    public void Creation(Color col, float len, float thick)
    {
        colour = col;
        length = len;
        thickness = thick;
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
        if (i == 3)
            return length;
        if (i == 4)
            return thickness;
        return 0;
    }
}

