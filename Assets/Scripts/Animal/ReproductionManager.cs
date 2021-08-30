using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReproductionManager : MonoBehaviour
{
    [SerializeField] float mutationChance = 0.3f;



    public void Test(AnimalBaseClass a, AnimalBaseClass b)
    {
        Crossover(a.transform, b.transform);

        a.transform.position = a.transform.position + transform.forward * 4;
        b.transform.position = b.transform.position + transform.forward * 4;
    }


    public void Crossover(Transform parent1, Transform parent2)
    {

        if(parent1.GetComponent<AnimalBaseClass>().species == animalType.rabbit)
        {
            RabbitGenetics genesA = parent1.GetComponentInChildren<RabbitClass>().genes;
            RabbitGenetics genesB = parent2.GetComponentInChildren<RabbitClass>().genes;

            //Mix genetics////////////////////////////////////////////////////////////////////////////////////////////////////////////

            float offspring_Size_X = TwoPointPrecision(genesA.size.GetGene(0), genesB.size.GetGene(0));
            float offspring_Size_Y = TwoPointPrecision(genesA.size.GetGene(1), genesB.size.GetGene(1));
            float offspring_Size_Z = TwoPointPrecision(genesA.size.GetGene(2), genesB.size.GetGene(2));

            float offspring_ColourRed = TwoPointPrecision(genesA.fur.GetGene(0), genesB.fur.GetGene(0));
            float offspring_ColourGreen = TwoPointPrecision(genesA.fur.GetGene(1), genesB.fur.GetGene(1));
            float offspring_ColourBlue = TwoPointPrecision(genesA.fur.GetGene(2), genesB.fur.GetGene(2));

            float offspring_Speed = TwoPointPrecision(genesA.speed.GetGene(), genesB.speed.GetGene());

            float offspring_EyesightRange = TwoPointPrecision(genesA.eyesight.GetGene(0), genesB.eyesight.GetGene(0));
            float offspring_EyesightFOV = TwoPointPrecision(genesA.eyesight.GetGene(1), genesB.eyesight.GetGene(1));


            float offspring_MaturityMaleMin = TwoPointPrecision(genesA.lifeExpectancy.GetGene(0), genesB.lifeExpectancy.GetGene(0));
            float offspring_MaturityMaleMax = TwoPointPrecision(genesA.lifeExpectancy.GetGene(1), genesB.lifeExpectancy.GetGene(1));
            
            float offspring_MaturityFemaleMin = TwoPointPrecision(genesA.lifeExpectancy.GetGene(2), genesB.lifeExpectancy.GetGene(2));
            float offspring_MaturityFemaleMax = TwoPointPrecision(genesA.lifeExpectancy.GetGene(3), genesB.lifeExpectancy.GetGene(3));
            
            float offspring_LifetimeMin = TwoPointPrecision(genesA.lifeExpectancy.GetGene(4), genesB.lifeExpectancy.GetGene(4));
            float offspring_LifetimeMax = TwoPointPrecision(genesA.lifeExpectancy.GetGene(5), genesB.lifeExpectancy.GetGene(5));


            //Create animal and apply genes////////////////////////////////////////////////////////////////////////////////////////////////////////////

            GameObject animal = Instantiate(parent1.gameObject);
            animal.transform.position = parent1.transform.position + transform.forward * 5;

            RabbitClass rabbitClass = animal.GetComponent<RabbitClass>();
            RabbitGenetics genesC = rabbitClass.genes;

            genesC.size.GetComponent<Rabbit_Gene_Size>().Creation(new Vector3(offspring_Size_X, offspring_Size_Y, offspring_Size_Z));
            genesC.speed.Creation(offspring_Speed);
            genesC.eyesight.GetComponent<Rabbit_Gene_Eyesight>().Creation(offspring_EyesightRange, offspring_EyesightFOV);
            genesC.fur.GetComponent<Rabbit_Gene_Fur>().Creation(new Color(offspring_ColourRed, offspring_ColourGreen, offspring_ColourBlue), 0);
            genesC.lifeExpectancy.GetComponent<Rabbit_Gene_Life>().Creation(rabbitClass.DetermineSex(), offspring_MaturityMaleMin, offspring_MaturityMaleMax, offspring_MaturityFemaleMin, offspring_MaturityFemaleMax, offspring_LifetimeMin, offspring_LifetimeMax);

            rabbitClass.Activate();
        }
    }

    float TwoPointPrecision(float parent1, float parent2)
    {
        float offspring = 0;

        char[] dataA = parent1.ToString().ToCharArray();
        char[] dataB = parent2.ToString().ToCharArray();
        char[] dataC = dataA;

        AllignArray(dataA, dataB);

        int crossoverPoint1 = UnityEngine.Random.Range(0, dataA.Length-1);
        int crossoverPoint2 = UnityEngine.Random.Range(crossoverPoint1, dataB.Length-1);

        for (int i = crossoverPoint1; i < crossoverPoint2; i++)
        {
            dataC[i] = dataB[i];
        }

        string temp = new string(dataC);
        offspring = float.Parse(temp);

        return offspring;
    }
    /// <summary>
    /// Crossover relies on arrays having the decimal at same position,
    /// and being the same overall length. Function alligns arrays.
    /// </summary>
    public void AllignArray(char[] dataA, char[] dataB)
    {
        bool dataHasDecimalA = false;
        bool dataHasDecimalB = false;
        int decimalPointA = -1;
        int decimalPointB = -1;

        for (int i = 0; i < dataA.Length; i++)
        {
            if (dataA[i] == '.')
            {
                dataHasDecimalA = true;
                decimalPointA = i;
            }
        }

        for (int i = 0; i < dataB.Length; i++)
        {
            if (dataB[i] == '.')
            {
                dataHasDecimalB = true;
                decimalPointB = i;
            }
        }

        if (dataHasDecimalA == true || dataHasDecimalB == true)
        {
            List<char> dataAWholeNumber = new List<char>(dataA);
            List<char> dataADecimal = new List<char>(dataA);
            List<char> dataBWholeNumber = new List<char>(dataB);
            List<char> dataBDecimal = new List<char>(dataB);

            if (dataHasDecimalA == true)
            {
                dataAWholeNumber.RemoveRange(decimalPointA, dataAWholeNumber.Count - decimalPointA);
                dataADecimal.RemoveRange(0, decimalPointA + 1);
            }

            if (dataHasDecimalB == true)
            {
                dataBWholeNumber.RemoveRange(decimalPointB, dataBWholeNumber.Count - decimalPointB);
                dataBDecimal.RemoveRange(0, decimalPointB + 1);
            }

            if(dataAWholeNumber.Count < dataBWholeNumber.Count)
            {
                int difference = dataBWholeNumber.Count - dataAWholeNumber.Count;
                for (int i = 0; i < difference; i++)
                {
                    dataAWholeNumber.Insert(0, '0');
                }
            }

            if (dataAWholeNumber.Count > dataBWholeNumber.Count)
            {
                int difference = dataAWholeNumber.Count - dataBWholeNumber.Count;
                for (int i = 0; i < difference; i++)
                {
                    dataBWholeNumber.Insert(0, '0');
                }
            }

            if (dataADecimal.Count < dataBDecimal.Count)
            {
                int difference = dataBDecimal.Count - dataADecimal.Count;
                for (int i = 0; i < difference; i++)
                {
                    dataADecimal.Insert(dataADecimal.Count, '0');
                }
            }

            if (dataADecimal.Count > dataBDecimal.Count)
            {
                int difference = dataADecimal.Count - dataBDecimal.Count;
                for (int i = 0; i < difference; i++)
                {
                    dataBDecimal.Insert(dataBDecimal.Count, '0');
                }
            }

            dataAWholeNumber.Add('.');
            dataAWholeNumber.AddRange(dataADecimal);

            dataBWholeNumber.Add('.');
            dataBWholeNumber.AddRange(dataBDecimal);

            //Array.Clear(dataA, 0, dataA.Length);
            //Array.Clear(dataB, 0, dataB.Length);

            dataA = dataAWholeNumber.ToArray();
            dataB = dataBWholeNumber.ToArray();
        }
    }
}
