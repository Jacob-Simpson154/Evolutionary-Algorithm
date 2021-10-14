using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingState : State
{
    [SerializeField] ThinkState thinkState;
    [SerializeField] bool waitingForMate = false;

    public override State RunCurrentState(AnimalManager manager)
    {
        if (waitingForMate == false)
        {
            if (manager.state_target != null)
            {
                AnimalManager partnersManager = manager.state_target.GetComponent<AnimalManager>();

                if (partnersManager != null)
                {
                    //Complete this sides part
                    manager.mating.Mate(partnersManager);
                    //Complete partners side
                    partnersManager.mating.Mate(manager);
                }
            }

            return thinkState;
        }
        else return this;
    }

    public void WaitForMate()
    {
        waitingForMate = true;
    }

    public void StopWaiting()
    {
        waitingForMate = false;
    }
}