using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public bool isgold;
    protected override void DoJob()
    {
        //Debug.Log("Coin override");
        GameManager.Instance.coin += isgold ? 500 : 100;
    }
}
