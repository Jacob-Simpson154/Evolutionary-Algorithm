using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Eyesight : GeneBase
{
    [SerializeField] float eyeSightRange;
    [Range(0, 360)]
    [SerializeField] float eyeSightFOV;

    public void Setup(float range,float fov)
    {
        eyeSightRange = eyeSightRange * range;
        eyeSightFOV = eyeSightFOV * fov;
        ControlCheck();
    }

    public void Creation(float range, float fov)
    {
        eyeSightRange = range;
        eyeSightFOV = fov;
        ControlCheck();
    }

    void ControlCheck()
    {
        if (eyeSightFOV > 360)
            eyeSightFOV = 360;
    }

    public override void ApplyGeneticInformation(AnimalManager manager)
    {
        manager.eyeSightRange = eyeSightRange;
        manager.eyeSightAngle = eyeSightFOV;
    }

    public override float GetGene(int i)
    {
        if (i == 0)
            return eyeSightRange;
        else

        if (i == 1)
            return eyeSightFOV;
        else

        return 0;
    }
}
