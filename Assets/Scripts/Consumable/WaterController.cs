using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : ConsumableController
{
    private void Start()
    {
        GetComponent<TileController>().canTraverse = false;
    }
}
