using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingState : State
{
    [SerializeField] State thinkState;

    [SerializeField] float timer = 0;
    [SerializeField] float timeToConsume = 30.0f;


    public override State RunCurrentState(AnimalManager manager)
    {
        if(manager.state_target.GetComponent<TileController>())
        {
            timer += Time.deltaTime;
            if(timer>=timeToConsume)
            {
                timer = 0;
                manager.diet.Consume(manager.state_target.GetComponent<TileController>().GetNourishment());
                manager.state_target = null;
                return thinkState;
            }
        }


        return this;
    }
}
