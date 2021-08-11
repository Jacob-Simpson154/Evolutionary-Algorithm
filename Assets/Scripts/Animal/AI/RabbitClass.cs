using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitClass : AnimalBaseClass
{
    public RabbitGenetics genes;

    private void Awake()
    {
        genes.size.ApplyGeneticInformation(transform);
        genes.fur.ApplyGeneticInformation(transform);
    }
}
