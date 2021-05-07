using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Item
{
    public int score=1000;

    protected override void DoJob()
    {
        GameManager.Instance.score += score;
    }

}
