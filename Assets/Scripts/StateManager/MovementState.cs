using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField] State thinkState;
    [SerializeField] Vector3 positionOffset = Vector3.up;
    [SerializeField] float speed = 2.0f;


    public override State RunCurrentState(AnimalManager manager)
    {
        if (manager.movement_path.Count > 0)
        {
            manager.transform.rotation = Quaternion.LookRotation(manager.movement_path[0] + positionOffset - manager.transform.position);
            manager.transform.position = Vector3.MoveTowards(manager.transform.position, manager.movement_path[0] + positionOffset, speed*manager.timeCon.GetDayTimer());
            if (manager.transform.position == manager.movement_path[0] + positionOffset)
            {
                manager.movement_path.RemoveAt(0);
            }


            return this;
        }

        manager.shouldUpdatePath = false;
        return thinkState;
    }
}
