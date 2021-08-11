using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct RabbitGenetics
{
    //Each specicies of rabbit will have different genetics
    public GeneBase size;
    public GeneBase fur;
    public GeneBase speed;
    public GeneBase eyesight;
    public GeneBase lifeExpectancy;
    public GeneBase reproduction;

}