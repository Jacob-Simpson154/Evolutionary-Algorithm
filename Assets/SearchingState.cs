using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : State
{
    [SerializeField] State moveState;

    public override State RunCurrentState(AnimalManager manager)
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * manager.eyeSightRange;
        RaycastHit hit;
        randomPoint.y = 500f;
        if (Physics.Raycast(randomPoint, Vector3.down, out hit, 1000f))
        {
            Transform destination = hit.transform;
            if (destination != null)
            {
                manager.navigation.CreatePathToTarget(manager.transform.position, destination.position);
                return moveState;
            }
        }

        return this;
    }
}
