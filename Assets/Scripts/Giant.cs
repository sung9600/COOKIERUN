using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Item
{
    protected override void DoJob()
    {
        Player.Instance.Giant();
    }
}
