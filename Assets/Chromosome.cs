using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour
{
    AnimalManager manager;
    public List<GeneBase> genes;

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public void Activate()
    {
        foreach (GeneBase item in genes)
        {
            item.ApplyGeneticInformation(manager);
        }
    }
}
