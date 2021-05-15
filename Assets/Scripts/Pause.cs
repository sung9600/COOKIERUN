using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool ispaused = false;
    public void pause()
    {
        Debug.Log(ispaused);
        if (!ispaused)
        {
            ispaused = true;
            Time.timeScale = 0f;
            GameManager.Instance.canvas.transform.GetChild(8).gameObject.SetActive(true);
            GameManager.Instance.StopCoroutine("hpdown");
            if (Player.Instance.cheeze)
                Player.Instance.StopCoroutine("genMails");
        }
        else
        {
            GameManager.Instance.canvas.transform.GetChild(8).gameObject.SetActive(false);
            ispaused = false;
            GameManager.Instance.StartCoroutine("startuichange");
            if (Player.Instance.cheeze)
                Player.Instance.StopCoroutine("genMails");
        }
    }
}
