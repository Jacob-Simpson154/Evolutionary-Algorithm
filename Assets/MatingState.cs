using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingState : State
{
    [SerializeField] ThinkState thinkState;

    public override State RunCurrentState(AnimalManager manager)
    {
        AnimalManager partnersManager = manager.state_target.GetComponent<AnimalManager>();

        if (partnersManager != null)
        {
            //Complete this sides part
            manager.mating.Mate(partnersManager);
            //Complete partners side
            partnersManager.mating.Mate(manager);
        }

        return thinkState;
    }
}