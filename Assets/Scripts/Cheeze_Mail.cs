using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cheeze_Mail : Item
{
    float y;
    float floating_spd;
    protected override void DoJob()
    {
        GameManager.Instance.mailcount++;
        if (GameManager.Instance.mailcount == 5)
        {
            Player.Instance.cheezeabil();
            GameManager.Instance.mailcount = 0;
        }
        GameManager.Instance.mailText.SetText(GameManager.Instance.mailcount.ToString());
    }
    new void Start()
    {
        base.Start(); 
        y = Random.Range(-2.5f, 1.5f);
        floating_spd = Mathf.Round(Random.Range(0.5f, 1f)*10f)*0.1f;
        transform.position = new Vector3(9, y, 0);
        speed = new Vector3(-1.5f, 0, 0);
        StartCoroutine("mailmove");
    }
    IEnumerator mailmove()
    {
        while (!GameManager.Instance.isGameOver)
        {
            transform.position = new Vector3(transform.position.x,Mathf.Round((y+Mathf.Sin(Time.time *floating_spd)) * 100f) *0.01f,transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
