using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast : Item
{
    protected override void DoJob()
    {
        GameManager.Instance.fast();
    }
}
