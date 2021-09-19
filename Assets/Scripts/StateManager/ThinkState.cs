using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkState : State
{
    public State moveState;
    public State eatState;
    public State drinkState;
    public State searchState;

    public override State RunCurrentState(AnimalManager manager)
    {
        if (manager.diet.IsThirsty() && manager.diet.CanSeeConsumable(0))
        {
            if (manager.state_target == null)
            {
                Transform destination = manager.diet.GetClosestConsumable(0);
                manager.state_target = destination.GetComponentInParent<TileController>().transform;
                if (destination != null)
                {
                    manager.navigation.CreatePathToTarget(manager.transform.position, destination.position);
                    return moveState;
                }
            }

            else

            if (manager.movement_path.Count == 0)
            {
                return drinkState;
            }
        }

        else

        if (manager.diet.IsThirsty() && !manager.diet.CanSeeConsumable(0))
        {
            return searchState;
        }

        else

        if (manager.diet.IsHungry() && manager.diet.CanSeeConsumable(1))
        {
            if (manager.state_target == null)
            {
                Transform destination = manager.diet.GetClosestConsumable(1);
                manager.state_target = destination.GetComponentInParent<TileController>().transform;
                if (destination != null)
                {
                    manager.navigation.CreatePathToTarget(manager.transform.position, destination.position);
                    return moveState;
                }
            }

            else

            if (manager.movement_path.Count == 0)
            {
                return eatState;
            }
        }

        else

        if (manager.diet.IsHungry() && !manager.diet.CanSeeConsumable(1))
        {
            return searchState;
        }

        

        return searchState;
    }
}
