using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingState : State
{
    [SerializeField] ThinkState thinkState;

    public override State RunCurrentState(AnimalManager manager)
    {
        manager.state_target.GetComponent<AnimalManager>().mating.Mate(manager);
        manager.mating.Mate(manager.state_target.GetComponent<AnimalManager>());
        return thinkState;
    }
}