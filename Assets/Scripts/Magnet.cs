using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{

    protected override void DoJob()
    {
        Pet.Instance.Magnet();
    }

}
