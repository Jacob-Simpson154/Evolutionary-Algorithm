using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityController : MonoBehaviour
{
    AnimalManager manager;

    [SerializeField] Species species;
    [SerializeField] Sex sex;

    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public void Setup(Species sp, Sex sx)
    {

    }

    public Species GetSpecies()
    {
        return species;
    }

    public Sex GetSex()
    {
        return sex;
    }
}
