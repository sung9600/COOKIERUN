using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheeze_Mail : Jelly
{
    protected override void DoJob()
    {
        GameManager.Instance.mailcount++;
        if (GameManager.Instance.mailcount == 16)
        {
            GameManager.Instance.mailcount = 0;
            StartCoroutine("coinparty");
        }
    }
}
