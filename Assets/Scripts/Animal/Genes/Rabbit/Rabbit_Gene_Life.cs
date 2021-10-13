using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Gene_Life : GeneBase
{


    //Maturity will determine how long it'll take for full size
    //in addition at what age they can reproduce
    //Male's reach maturity sooner
    public float maturityMinMale = 90;
    public float maturityMaxMale = 120;
    //Female take longer to mature
    public float maturityMinFemale = 180;
    public float maturityMaxFemale = 210;

    public float expectedLifetimeMin = 2920;
    public float expectedLifetimeMax = 4380;

    public void Setup(Sex sex, float maturityMin, float maturityMax, float expectedMin, float expectedMax)
    {
        if(sex == Sex.Female)
        {
            maturityMinFemale *= maturityMin;
            maturityMaxFemale *= maturityMax;
        }
        else
        {
            maturityMinMale *= maturityMin;
            maturityMaxMale *= maturityMax;
        }

        expectedLifetimeMin *= expectedMin;
        expectedLifetimeMax *= expectedMax;
    }

    public void Creation(Sex sex, float matureMaleMin, float matureMaleMax, float matureFemaleMin, float matureFemaleMax, float expectedMin, float expectedMax)
    {
        maturityMinMale = matureMaleMin;
        maturityMaxMale = matureMaleMax;
        maturityMinFemale = matureFemaleMin;
        maturityMaxFemale = matureFemaleMax;
        expectedLifetimeMin = expectedMin;
        expectedLifetimeMax = expectedMax;
    }

    public override float GetGene(int i)
    {
        if(i == 0)
        {
            return maturityMinMale;
        }   else

        if(i== 1)
        {
            return maturityMaxMale;
        }   else

        if(i == 2)
        {
            return maturityMinFemale;
        }   else

        if (i == 3)
        {
            return maturityMaxFemale;
        }   else

        if(i == 4)
        {
            return expectedLifetimeMin;
        }   else

        if(i == 5)
        {
            return expectedLifetimeMax;
        }

        else return 0;
    }

    public override void ApplyGeneticInformation(AnimalManager manager)
    {
        if(manager.identity.GetSex() == Sex.Female)
        {
            manager.ageOfMaturityInDays = Random.Range(maturityMinFemale, maturityMaxFemale);
        }
        else manager.ageOfMaturityInDays = Random.Range(maturityMinMale, maturityMaxMale);

        manager.ageOfDeathInDays = Random.Range(expectedLifetimeMin, expectedLifetimeMax);
    }
}
