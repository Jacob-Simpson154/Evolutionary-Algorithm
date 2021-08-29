using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Speed : GeneBase
{
    //Length of hind legs determine speed (with greater advantage uphill)
    public float speed = 2.0f;

    public override void Setup(float mod)
    {
        speed = speed * mod;
    }

    public override void Creation(float value)
    {
        speed = value;
    }


    public override void ApplyGeneticInformation(Transform parent)
    {
        base.ApplyGeneticInformation(parent);
    }

    public override float GetGene()
    {
        return speed;
    }
}
