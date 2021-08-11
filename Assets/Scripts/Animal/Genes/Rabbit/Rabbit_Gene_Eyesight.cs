using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Eyesight : GeneBase
{
    [SerializeField] float eyeSightRange;
    [Range(0, 360)]
    [SerializeField] float eyeSightFOV;

    public override void ApplyGeneticInformation(Transform parent)
    {
        RabbitClass rc = parent.GetComponent<RabbitClass>();
        rc.eyeSightRange = eyeSightRange;
        rc.eyeSightAngle = eyeSightFOV;
    }
}
