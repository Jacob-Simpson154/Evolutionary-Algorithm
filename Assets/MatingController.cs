using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingController : MonoBehaviour
{
    [SerializeField] AnimalManager manager;

    public List<AnimalManager> visibleMates;
    [SerializeField] LayerMask mask;

    [Header("Gestation")]
    public bool isPregnant = false;
    [SerializeField] float gestestationProgress = 0.0f;
    [SerializeField] float gestationPeriodInDays = 0.0f;
    AnimalManager otherMate;


    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public bool FindMates()
    {
        visibleMates.Clear();

        Collider[] animalsInArea = Physics.OverlapSphere(transform.position, manager.eyeSightRange, mask);
        if (animalsInArea.Length == 0)
            return false;
        foreach (Collider item in animalsInArea)
        {
            AnimalManager inspectedItem = item.GetComponentInParent<AnimalManager>();
            if (inspectedItem.identity.GetSpecies() == manager.identity.GetSpecies() && inspectedItem.identity.GetSex() != manager.identity.GetSex() && inspectedItem.mating.isPregnant == false && manager.mating.isPregnant == false && inspectedItem.IsAdult() && !manager.TooMature() && !inspectedItem.TooMature())
            {
                if (!visibleMates.Contains(inspectedItem))
                {
                    Vector3 direction = (inspectedItem.transform.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, direction) < manager.eyeSightAngle / 2)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
                        {
                            if (hit.transform.GetComponentInParent<AnimalManager>() == inspectedItem)
                            {
                                visibleMates.Add(item.GetComponentInParent<AnimalManager>());
                            }
                        }
                    }
                }
            }
        }

        if (visibleMates.Count > 0)
            return true;

        return false;
    }

    public Transform GetClosestMate()
    {
        if(visibleMates.Count>0)
        {
            Transform closestMate = null;

            foreach (AnimalManager item in visibleMates)
            {
                if (closestMate == null)
                    closestMate = item.transform;

                if (closestMate != null && manager.GetDistance(item.transform.position) < manager.GetDistance(closestMate.position))
                    closestMate = item.transform;
            }

            return closestMate;
        }

        return null;
    }

    public Transform GetBestMate()
    {
        if(visibleMates.Count>0)
        {
            Transform bestMate = null;
            int bestMateFitnesss = 0;

            foreach (AnimalManager item in visibleMates)
            {
                if (bestMate == null)
                {
                    bestMate = item.transform;
                    bestMateFitnesss = bestMate.GetComponent<AnimalManager>().mating.CalculateFitness(manager);
                }

                else

                if (bestMate != null && item.mating.CalculateFitness(manager) > bestMateFitnesss)
                {
                    bestMate = item.transform;
                    bestMateFitnesss = bestMate.GetComponent<AnimalManager>().mating.CalculateFitness(manager);
                }
            }

            return bestMate;
        }
        return null;
    }

    public bool HasMates()
    {
        if (visibleMates.Count > 0)
            return true;
        else return false;
    }

    public void Mate(AnimalManager other)
    {
        if(manager.identity.GetSex() == Sex.Female)
        {
            isPregnant = true;
            otherMate = other;
        }

        manager.state_target = null;
        manager.currentState = manager.thinkState;
        manager.thinkState.mateState.GetComponent<MatingState>().StopWaiting();

        Debug.Log(manager.gameObject.name + " just mated");
    }

    private void Update()
    {
        if(isPregnant == true)
        {
            gestestationProgress += manager.timeCon.GetDayTimer();
            if(gestestationProgress>=gestationPeriodInDays)
            {
                gestestationProgress = 0.0f;
                isPregnant = false;
                if (otherMate != null)
                {
                    //This should plug into genetic algorithm
                    if (manager.identity.GetSex() == Sex.Female)
                    {
                        FindObjectOfType<ReproductionManager>().Crossover(manager.transform, otherMate.transform);
                    }
                }
            }
        }
    }


    public void AlertedOfMate()
    {
        manager.currentState = manager.thinkState.mateState;
    }

    public int CalculateFitness(AnimalManager comparison)
    {
        AnimalManager baseRabbit = FindObjectOfType<ReproductionManager>().rabbit.GetComponent<AnimalManager>();

        int score = 0;

        Vector3 thisSize = new Vector3(manager.chromosomes.genes[0].GetGene(0), manager.chromosomes.genes[0].GetGene(1), manager.chromosomes.genes[0].GetGene(2));
        Vector3 compSize = new Vector3(comparison.chromosomes.genes[0].GetGene(0), comparison.chromosomes.genes[0].GetGene(1), comparison.chromosomes.genes[0].GetGene(2));
        Vector3 baseSize = new Vector3(baseRabbit.chromosomes.genes[0].GetGene(0), comparison.chromosomes.genes[0].GetGene(1), comparison.chromosomes.genes[0].GetGene(2));

        float thisFurLength = manager.chromosomes.genes[1].GetGene(3);
        float compFurLength = comparison.chromosomes.genes[1].GetGene(3);
        float baseFurLength = baseRabbit.chromosomes.genes[1].GetGene(3);

        float thisFurThickness = manager.chromosomes.genes[1].GetGene(4);
        float compFurThickness = comparison.chromosomes.genes[1].GetGene(4);
        float baseFurThickness = baseRabbit.chromosomes.genes[1].GetGene(4);

        float thisEyeRange = manager.chromosomes.genes[3].GetGene(0);
        float compEyeRange = comparison.chromosomes.genes[3].GetGene(0);
        float baseEyeRange = baseRabbit.chromosomes.genes[3].GetGene(0);

        float thisEyeFOV = manager.chromosomes.genes[3].GetGene(1);
        float compEyeFOV = comparison.chromosomes.genes[3].GetGene(1);
        float baseEyeFOV = baseRabbit.chromosomes.genes[3].GetGene(1);

        float thisLife = manager.ageOfDeathInDays;
        float compLife = comparison.ageOfDeathInDays;
        float baseLife = baseRabbit.ageOfDeathInDays;

        if (thisSize.magnitude >= baseSize.magnitude)
            score++;
        if (thisSize.magnitude >= compSize.magnitude)
            score++;

        if (thisEyeRange >= baseEyeRange)
            score++;
        if (thisEyeRange >= compEyeRange)
            score++;

        if (thisEyeFOV >= baseEyeFOV)
            score++;
        if (thisEyeFOV >= compEyeFOV)
            score++;

        if (thisLife >= baseLife)
            score++;
        if (thisLife >= compLife)
            score++;

        const float weatherIdealMin = 10.0f;
        const float weatherIdealMax = 20.0f;
        float overallTemp = FindObjectOfType<CalenderWeather>().GetAmbientTemperature() + (compFurLength * compFurThickness);

        if (overallTemp < weatherIdealMin)
            score--;
        if (overallTemp > weatherIdealMin && overallTemp < weatherIdealMax)
            score++;
        if (overallTemp > weatherIdealMax)
            score--;

        return score;
    }
}
