using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReproductionManager : MonoBehaviour
{
    [SerializeField] float mutationChance = 0.1f;
    public GameObject rabbit;
    PopulationManager popManager;

    private void Start()
    {
        popManager = GetComponent<PopulationManager>();
    }


    public void Test(AnimalBaseClass a, AnimalBaseClass b)
    {
        Crossover(a.transform, b.transform);

        a.transform.position = a.transform.position + transform.forward * 4;
        b.transform.position = b.transform.position + transform.forward * 4;
    }

    /// <summary>
    /// Mix the genetics of both parents
    /// </summary>
    public void Crossover(Transform parent1, Transform parent2)
    {

        if(parent1.GetComponent<AnimalManager>().identity.GetSpecies() == Species.rabbit)
        {
            Chromosome genesA = parent1.GetComponentInChildren<AnimalManager>().chromosomes;
            Chromosome genesB = parent2.GetComponentInChildren<AnimalManager>().chromosomes;

            //Mix genetics////////////////////////////////////////////////////////////////////////////////////////////////////////////

            float offspring_Size_X = TwoPointPrecision(genesA.genes[0].GetGene(0), genesB.genes[0].GetGene(0));
            offspring_Size_X = ShouldMutate() ? Mutate(offspring_Size_X): offspring_Size_X;
            float offspring_Size_Y = TwoPointPrecision(genesA.genes[0].GetGene(1), genesB.genes[0].GetGene(1));
            offspring_Size_Y = ShouldMutate() ? Mutate(offspring_Size_Y) : offspring_Size_Y;
            float offspring_Size_Z = TwoPointPrecision(genesA.genes[0].GetGene(2), genesB.genes[0].GetGene(2));
            offspring_Size_Z = ShouldMutate() ? Mutate(offspring_Size_Z) : offspring_Size_Z;

            float offspring_ColourRed = TwoPointPrecision(genesA.genes[1].GetGene(0), genesB.genes[1].GetGene(0));
            offspring_ColourRed = ShouldMutate() ? Mutate(offspring_ColourRed) : offspring_ColourRed;
            float offspring_ColourGreen = TwoPointPrecision(genesA.genes[1].GetGene(1), genesB.genes[1].GetGene(1));
            offspring_ColourGreen = ShouldMutate() ? Mutate(offspring_ColourGreen) : offspring_ColourGreen;
            float offspring_ColourBlue = TwoPointPrecision(genesA.genes[1].GetGene(2), genesB.genes[1].GetGene(2));
            offspring_ColourBlue = ShouldMutate() ? Mutate(offspring_ColourBlue) : offspring_ColourBlue;

            float offspring_FurLength = TwoPointPrecision(genesA.genes[1].GetGene(3), genesB.genes[1].GetGene(3));
            offspring_FurLength = ShouldMutate() ? Mutate(offspring_FurLength) : offspring_FurLength;
            float offspring_FurThickness = TwoPointPrecision(genesA.genes[1].GetGene(4), genesB.genes[1].GetGene(4));
            offspring_FurThickness = ShouldMutate() ? Mutate(offspring_FurThickness) : offspring_FurThickness;

            float offspring_Speed = TwoPointPrecision(genesA.genes[2].GetGene(), genesB.genes[2].GetGene());
            offspring_Speed = ShouldMutate() ? Mutate(offspring_Speed) : offspring_Speed;

            float offspring_EyesightRange = TwoPointPrecision(genesA.genes[3].GetGene(0), genesB.genes[3].GetGene(0));
            offspring_EyesightRange = ShouldMutate() ? Mutate(offspring_EyesightRange) : offspring_EyesightRange;
            float offspring_EyesightFOV = TwoPointPrecision(genesA.genes[3].GetGene(1), genesB.genes[3].GetGene(1));
            offspring_EyesightFOV = ShouldMutate() ? Mutate(offspring_EyesightFOV) : offspring_EyesightFOV;


            float offspring_MaturityMaleMin = TwoPointPrecision(genesA.genes[4].GetGene(0), genesB.genes[4].GetGene(0));
            offspring_MaturityMaleMin = ShouldMutate() ? Mutate(offspring_MaturityMaleMin) : offspring_MaturityMaleMin;
            float offspring_MaturityMaleMax = TwoPointPrecision(genesA.genes[4].GetGene(1), genesB.genes[4].GetGene(1));
            offspring_MaturityMaleMax = ShouldMutate() ? Mutate(offspring_MaturityMaleMax) : offspring_MaturityMaleMax;
            
            float offspring_MaturityFemaleMin = TwoPointPrecision(genesA.genes[4].GetGene(2), genesB.genes[4].GetGene(2));
            offspring_MaturityFemaleMin = ShouldMutate() ? Mutate(offspring_MaturityFemaleMin) : offspring_MaturityFemaleMin;
            float offspring_MaturityFemaleMax = TwoPointPrecision(genesA.genes[4].GetGene(3), genesB.genes[4].GetGene(3));
            offspring_MaturityFemaleMax = ShouldMutate() ? Mutate(offspring_MaturityFemaleMax) : offspring_MaturityFemaleMax;
            
            float offspring_LifetimeMin = TwoPointPrecision(genesA.genes[4].GetGene(4), genesB.genes[4].GetGene(4));
            offspring_LifetimeMin = ShouldMutate() ? Mutate(offspring_LifetimeMin) : offspring_LifetimeMin;
            float offspring_LifetimeMax = TwoPointPrecision(genesA.genes[4].GetGene(5), genesB.genes[4].GetGene(5));
            offspring_LifetimeMax = ShouldMutate() ? Mutate(offspring_LifetimeMax) : offspring_LifetimeMax;


            //Create animal and apply genes////////////////////////////////////////////////////////////////////////////////////////////////////////////

            GameObject animal = Instantiate(rabbit, transform);
            animal.transform.position = parent1.position;

            AnimalManager rabbitClass = animal.GetComponent<AnimalManager>();
            rabbitClass.currentState = rabbitClass.thinkState;
            rabbitClass.movement_path.Clear();
            rabbitClass.state_target = null;

            Chromosome genesC = rabbitClass.chromosomes;

            genesC.genes[0].GetComponent<Rabbit_Gene_Size>().Creation(new Vector3(offspring_Size_X, offspring_Size_Y, offspring_Size_Z));
            genesC.genes[1].GetComponent<Rabbit_Gene_Fur>().Creation(new Color(offspring_ColourRed, offspring_ColourGreen, offspring_ColourBlue), offspring_FurLength, offspring_FurThickness);
            genesC.genes[2].Creation(offspring_Speed);
            genesC.genes[3].GetComponent<Rabbit_Gene_Eyesight>().Creation(offspring_EyesightRange, offspring_EyesightFOV);
            genesC.genes[4].GetComponent<Rabbit_Gene_Life>().Creation(rabbitClass.GetComponentInChildren<IdentityController>().DetermineSex(), offspring_MaturityMaleMin, offspring_MaturityMaleMax, offspring_MaturityFemaleMin, offspring_MaturityFemaleMax, offspring_LifetimeMin, offspring_LifetimeMax);

            rabbitClass.ApplyChromosome();

            popManager.AddToPopulation(rabbitClass);
        }
    }

    /// <summary>
    /// Used for crossover.
    /// </summary>
    float TwoPointPrecision(float parent1, float parent2)
    {
        float offspring = 0;

        char[] dataA = parent1.ToString().ToCharArray();
        char[] dataB = parent2.ToString().ToCharArray();

        AllignArray(ref dataA, ref dataB);
        char[] dataC = dataA;

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
    void AllignArray(ref char[] dataA, ref char[] dataB)
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

            dataA = dataAWholeNumber.ToArray();
            dataB = dataBWholeNumber.ToArray();
        }
    }

    bool ShouldMutate()
    {
        float possibility = UnityEngine.Random.Range(0.0f, 100.0f);
        if (possibility <= mutationChance)
            return true;
        else return false;
    }

    float Mutate(float value)
    {
        char[] data = value.ToString().ToCharArray();

        for (int i = 0; i < data.Length; i++)
        {
            if(data[i] != '.')
            {
                int plusOrMinus = UnityEngine.Random.Range(0, 2);
                if (plusOrMinus == 0)
                {
                    int test = int.Parse(data[i].ToString());
                    if((test-1) >= 0)
                    {
                        data[i]--;
                    }
                } 
                
                else 

                if(plusOrMinus == 1)
                {
                    int test = int.Parse(data[i].ToString());
                    if ((test + 1) <= 9)
                    {
                        data[i]++;
                    }
                }
            }
        }



        return value;
    }
}
