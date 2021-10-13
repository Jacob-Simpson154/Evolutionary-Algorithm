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

    public Sex DetermineSex()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < 0.5f)
            sex = Sex.Female;
        else sex = Sex.Male;

        return sex;
    }
}
