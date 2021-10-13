using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Size : GeneBase
{
    [SerializeField] Vector3 birthSize;
    [SerializeField] Vector3 matureSize;

    public override void Setup(float mod)
    {
        matureSize = matureSize * mod;
    }

    public void Creation(Vector3 size)
    {
        matureSize = size;
    }

    

    public override void ApplyGeneticInformation(AnimalManager manager)
    {
        manager.transform.localScale = birthSize;
        manager.sizeMature = matureSize;
    }

    /// <summary>
    /// Returns x y and z component of gene relevant to i.
    /// </summary>
    public override float GetGene(int i)
    {
        if(i== 0)
            return matureSize.x;        
        if(i== 1)
            return matureSize.y;
        if (i == 2)
            return matureSize.z;
        else return 0;
    }

    
}
