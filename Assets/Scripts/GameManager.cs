using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;
    public bool nextBlank = false;
    public int RunLevel = 1;
    public float maxHp = 500;
    public float currentHp;
    public int score = 0;
    public int coin = 0;
    public int mailcount = 0;
    public bool inBonus = false;
    public bool isBig = false;
    public Slider hpbar;
    public TextMeshPro coinText;
    public TextMeshPro scoreText;
    private static GameManager instance = null;

    public List<(int jelly, int obstacle, int floor)> map = new List<(int jelly, int obstacle, int floor)>();
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
            Destroy(this);
    }
    private void Start()
    {
        //맵 리딩
        map = CSVReader.ReadList("Excel/map");
        StartCoroutine("hpdown");
        //Debug.Log(currentHp);
        Invoke("make", 2f);
        Invoke("make", 2.5f);
        Invoke("make", 3f);
        Invoke("make", 3.5f);
        Invoke("make2", 4f);
    }
    IEnumerator hpdown()
    {
        while (!this.isGameOver)
        {
            hpChange(-1);
            yield return new WaitForSeconds(0.25f);
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
        }
        hpbar.value = 1 - currentHp / maxHp;
    }
    public void coinChange(int var)
    {
        coin += var;
        coinText.text = string.Format("{0:n0}", coin);
    }
}
