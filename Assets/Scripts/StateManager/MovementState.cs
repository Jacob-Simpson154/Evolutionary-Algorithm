using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField] State thinkState;
    [SerializeField] Vector3 buffer = Vector3.up;

    public override State RunCurrentState(AnimalManager manager)
    {
        if (manager.movement_path.Count > 0)
        {
            manager.transform.rotation = Quaternion.LookRotation(manager.movement_path[0] + buffer - manager.transform.position);
            manager.transform.position = Vector3.MoveTowards(manager.transform.position, manager.movement_path[0] + buffer, Time.deltaTime);
            if (manager.transform.position == manager.movement_path[0] + buffer)
            {
                manager.movement_path.RemoveAt(0);
            }


            return this;
        }

        return thinkState;
    }
}
