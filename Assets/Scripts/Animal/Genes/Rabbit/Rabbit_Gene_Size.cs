using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Size : GeneBase
{
    [SerializeField] Vector3 birthSize;
    [SerializeField] Vector3 matureSize;

    public override void ApplyGeneticInformation(Transform parent)
    {
        parent.transform.localScale = birthSize;
        parent.GetComponent<AnimalBaseClass>().matureSize = matureSize;
    }
}
