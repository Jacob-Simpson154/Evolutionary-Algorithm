using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State
{
    [SerializeField] ThinkState thinkState;

    public override State RunCurrentState(AnimalManager manager)
    {
        if(manager.sleep.GetSleeping()==false)
        {
            manager.sleep.SetSleeping(true);
        }

        else 

        if(manager.sleep.GetSleeping() == true && manager.sleep.NeedsMoreSleep() == false)
        {
            manager.sleep.SetSleeping(false);
            return thinkState;
        }

        return this;
    }
}
