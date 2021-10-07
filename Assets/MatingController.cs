using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingController : MonoBehaviour
{
    AnimalManager manager;

    public List<AnimalManager> visibleMates;

    bool isPregnant = false;

    [SerializeField] LayerMask mask;

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public bool FindMates()
    {
        visibleMates.Clear();

        Collider[] animalsInArea = Physics.OverlapSphere(transform.position, manager.eyeSightRange, mask);
        foreach (Collider item in animalsInArea)
        {
            AnimalManager inspectedItem = item.GetComponentInParent<AnimalManager>();
            if (inspectedItem.identity.GetSpecies() == manager.identity.GetSpecies() && inspectedItem.identity.GetSex() != manager.identity.GetSex())
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
}
