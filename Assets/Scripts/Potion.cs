using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    protected bool isbig;
    protected override void DoJob()
    {
        GameManager.Instance.hpChange(isbig ? 50 : 15);
    }
}
