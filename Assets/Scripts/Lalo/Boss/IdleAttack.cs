using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAttack : State
{
    // Start is called before the first frame update
    public override State RunCurrentState()
    {
        return this;
    }
}
