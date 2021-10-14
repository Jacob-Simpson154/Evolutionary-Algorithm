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
            if (inspectedItem.identity.GetSpecies() == manager.identity.GetSpecies() && inspectedItem.identity.GetSex() != manager.identity.GetSex() && inspectedItem.mating.isPregnant == false && manager.mating.isPregnant ==false)
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

                //This should plug into genetic algorithm
                if (manager.identity.GetSex() == Sex.Female)
                {
                    FindObjectOfType<ReproductionManager>().Crossover(manager.transform, otherMate.transform);
                }
            }
        }
    }


    public void AlertedOfMate()
    {
        manager.currentState = manager.thinkState.mateState;
    }
}
