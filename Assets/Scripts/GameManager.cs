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
    public bool isFast = false;
    public Slider hpbar;
    public Canvas canvas;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mailText;
    private static GameManager instance = null;
    public GameObject[] items=new GameObject[10];
    public GameObject[] obstacles = new GameObject[7];
    public GameObject[] cookies = new GameObject[2];


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
        if (Transfer.Instance.cheeze)
        {
            Instantiate(cookies[0]);
        }
        else
        {
            Instantiate(cookies[1]);
        }
        Destroy(Transfer.Instance.gameObject);
        canvas = GameObject.Find("UI").transform.GetChild(0).GetComponent<Canvas>();
         coinText = canvas.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
         scoreText = canvas.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        mailText = canvas.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        map = CSVReader.ReadList("Excel/map.csv");
        Time.timeScale = 0f;
    }
    private void Start()
    {
        //맵 리딩
        StartCoroutine("startuichange");
    }
    IEnumerator startuichange()
    {
        canvas.transform.GetChild(10).gameObject.SetActive(true);
        TextMeshProUGUI a =canvas.transform.Find("Image").GetChild(0).GetComponent<TextMeshProUGUI>();
        a.SetText("3");
        yield return new WaitForSecondsRealtime(1f);
        a.SetText("2");
        yield return new WaitForSecondsRealtime(1f);
        a.SetText("1");
        yield return new WaitForSecondsRealtime(1f);
        canvas.transform.Find("Image").gameObject.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine("hpdown");
        if (Player.Instance.cheeze)
            Player.Instance.StartCoroutine("genMails");
        yield break;
    }
    IEnumerator hpdown()
    {
        while (!isGameOver)
        {
            hpChange(-1);
            yield return new WaitForSecondsRealtime(0.25f);
        }
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

    #region statchanges
    public void hpChange(int var)
    {
        currentHp += var;
        if (currentHp > maxHp)
            currentHp = maxHp;
        if (currentHp <= 0)
        {
            isGameOver = true;
            currentHp = 0;
            gameOver(true);
        }
        hpbar.value = 1 - currentHp / maxHp;
    }
    public void coinChange()
    {
        coinText.text = string.Format("{0:n0}", coin);
    }
    public void scoreChange()
    {
        scoreText.text = string.Format("{0:n0}", score);
    }
    public void gameOver(bool dead)
    {
        if(dead)
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
    #endregion

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
        Time.timeScale = 4f;
        isFast = true;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        isFast = false;
        yield break;
    }
}
