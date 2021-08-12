using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Life : GeneBase
{


    //Maturity will determine how long it'll take for full size
    //in addition at what age they can reproduce
    //Male's reach maturity sooner
    public int maturityMinMale = 90;
    public int maturityMaxMale = 120;
    //Female take longer to mature
    public int maturityMinFemale = 180;
    public int maturityMaxFemale = 210;

    public int expectedLifetimeMin = 2920;
    public int expectedLifetimeMax = 4380;

    public override void ApplyGeneticInformation(Transform parent)
    {
        AnimalBaseClass rabbit = parent.GetComponent<AnimalBaseClass>();

        if(rabbit.sex == Sex.Female)
        {
            rabbit.ageOfMaturityInDays = Random.Range(maturityMinFemale, maturityMaxFemale + 1);
        }
        else rabbit.ageOfMaturityInDays = Random.Range(maturityMinMale, maturityMaxMale + 1);

        rabbit.ageOfDeathInDays = Random.Range(expectedLifetimeMin, expectedLifetimeMax + 1);
    }
}
