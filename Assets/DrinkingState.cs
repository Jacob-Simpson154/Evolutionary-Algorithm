using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingState : State
{
    [SerializeField] State thinkState;

    public override State RunCurrentState(AnimalManager manager)
    {
        if(manager.state_target.GetComponent<TileController>())
        {
           manager.diet.Consume(manager.state_target.GetComponent<TileController>().GetNourishment());
        }

        manager.state_target = null;

        return thinkState;
    }
}
