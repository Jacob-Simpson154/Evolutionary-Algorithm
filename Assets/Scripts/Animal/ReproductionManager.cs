using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductionManager : MonoBehaviour
{
    [SerializeField] float mutationChance = 0.3f;



    public void Test(AnimalBaseClass a, AnimalBaseClass b)
    {
        Crossover(a.transform, b.transform);
    }


    public void Crossover(Transform parent1, Transform parent2)
    {

        if(parent1.GetComponent<AnimalBaseClass>().species == animalType.rabbit)
        {
            RabbitGenetics genesA = parent1.GetComponentInChildren<RabbitClass>().genes;
            RabbitGenetics genesB = parent2.GetComponentInChildren<RabbitClass>().genes;


            float offspring_Size_X = TwoPointPrecision(genesA.size.GetGene(0), genesB.size.GetGene(0));
            float offspring_Size_Y = TwoPointPrecision(genesA.size.GetGene(1), genesB.size.GetGene(1));
            float offspring_Size_Z = TwoPointPrecision(genesA.size.GetGene(2), genesB.size.GetGene(2));

            float offspring_Speed = TwoPointPrecision(genesA.speed.GetGene(), genesB.speed.GetGene());

            float offspring_EyesightRange = TwoPointPrecision(genesA.eyesight.GetGene(0), genesB.eyesight.GetGene(0));
            float offspring_EyesightFOV = TwoPointPrecision(genesA.eyesight.GetGene(1), genesB.eyesight.GetGene(1));

            float offspring_ColourRed = TwoPointPrecision(genesA.fur.GetGene(0), genesB.fur.GetGene(0));
            float offspring_ColourGreen = TwoPointPrecision(genesA.fur.GetGene(1), genesB.fur.GetGene(1));
            float offspring_ColourBlue = TwoPointPrecision(genesA.fur.GetGene(2), genesB.fur.GetGene(2));
            //int offspring_MaturityMaleMin = TwoPointPrecisionMix(0, 0);
            //int offspring_MaturityMaleMax = TwoPointPrecisionMix(0, 0);
            //int offspring_MaturityFemaleMin = TwoPointPrecisionMix(0, 0);
            //int offspring_MaturityFemaleMax = TwoPointPrecisionMix(0, 0);
            //int offspring_LifeExpectancyMin = TwoPointPrecisionMix(0, 0);
            //int offspring_LifeExpectancyMax = TwoPointPrecisionMix(0, 0);

            GameObject animal = Instantiate(parent1.gameObject);
            animal.transform.position = parent1.transform.position + transform.forward * 5;

            RabbitClass rabbitClass = animal.GetComponent<RabbitClass>();
            RabbitGenetics genesC = rabbitClass.genes;

            genesC.size.GetComponent<Rabbit_Gene_Size>().Creation(new Vector3(offspring_Size_X, offspring_Size_Y, offspring_Size_Z));
            genesC.speed.Creation(offspring_Speed);
            genesC.eyesight.GetComponent<Rabbit_Gene_Eyesight>().Creation(offspring_EyesightRange, offspring_EyesightFOV);
            genesC.fur.GetComponent<Rabbit_Gene_Fur>().Creation(new Color(offspring_ColourRed, offspring_ColourGreen, offspring_ColourBlue), 0);

            rabbitClass.Activate();
        }
    }

    float TwoPointPrecision(float parent1, float parent2)
    {
        float offspring = 0;


        char[] dataA = parent1.ToString().ToCharArray();
        char[] dataB = parent2.ToString().ToCharArray();
        char[] dataC = dataA;
      
        int crossoverPoint1 = Random.Range(0, dataA.Length-1);
        int crossoverPoint2 = Random.Range(crossoverPoint1, dataB.Length-1);

        for (int i = crossoverPoint1; i < crossoverPoint2; i++)
        {
            dataC[i] = dataB[i];
        }

        string temp = new string(dataC);
        offspring = float.Parse(temp);

        return offspring;
    }

    public string DecimalToByte(float value)
    {
        return null;
    }
}
