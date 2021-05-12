using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;
    public int RunLevel = 1;
    public float maxHp = 500;
    public float currentHp;
    public int score = 0;
    public int coin = 0;
    public int mailcount = 0;
    public bool inBonus = false;
    public bool isBig = false;
    public Slider hpbar;
    public Canvas canvas;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mailText;
    private static GameManager instance = null;

    public List<(int jelly_h, int jelly, int obstacle, int floor)> map = new List<(int, int, int, int)>();
    public int currentPos = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            currentHp = maxHp;
        }
        else
            Destroy(this.gameObject);
        canvas = GameObject.Find("UI").transform.GetChild(0).GetComponent<Canvas>();
        coinText = canvas.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText = canvas.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        mailText = canvas.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        //맵 리딩
        map = CSVReader.ReadList("Excel/map");
        StartCoroutine("hpdown");
        //Debug.Log(currentHp);
        //Invoke("make", 2f);
        //Invoke("make", 2.5f);
        //Invoke("make", 3f);
        //Invoke("make", 3.5f);
        //Invoke("make2", 4f);
    }
    IEnumerator hpdown()
    {
        while (!isGameOver)
        {
            hpChange(-1);
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
    public GameObject target;
    public GameObject target2;
    void make()
    {
        Instantiate(target, new Vector3(3, 0, 0), transform.rotation, null);
    }
    void make2()
    {
        Instantiate(target2, new Vector3(6, 0, 0), transform.rotation, null);
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void hpChange(int var)
    {
        currentHp += var;
        if (currentHp > maxHp)
            currentHp = maxHp;
        if (currentHp <= 0)
        {
            isGameOver = true;
            currentHp = 0;
            gameOver();
        }
        hpbar.value = 1 - currentHp / maxHp;
    }
    public void coinChange()
    {
        coinText.text = string.Format("{0:n0}", coin);
    }
    public void scoreChange()
    {
        scoreText.text = string.Format("{0:n0}", scoreText);
    }
    public void gameOver()
    {
        Player.Instance.anim.Play("Dead"); 

        GameObject panel = GameObject.Find("UI").transform.Find("Canvas").Find("Panel").gameObject;
        panel.SetActive(true);
        StartCoroutine(showresult_c(panel.transform.Find("Inner").Find("Score"), GameManager.Instance.score));
        StartCoroutine(showresult_c(panel.transform.Find("Inner").Find("Coin"), GameManager.Instance.coin));
        Invoke("scalechange", 1f);
    }
    void scalechange()
    {
        Time.timeScale = 0f;
    }


    IEnumerator showresult_c(Transform t, float f)
    {
        float duration = 0.5f;
        float offset = f / duration;
        float current = 0;
        while (current < f)
        {
            current += offset * Time.deltaTime;
            t.GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:n0}", current));
            yield return null;
        }
        //current = f;
        t.GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:n0}", f));
        
        yield break;
    }

    public void fast()
    {
        StartCoroutine("fastfunc");
    }
    IEnumerator fastfunc()
    {
        Time.timeScale = 5f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        yield break;
    }
}
