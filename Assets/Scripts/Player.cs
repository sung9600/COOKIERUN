using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private static Player instance = null;
    public static Player Instance
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
    public bool isMobile;
    public int jumpcount = 0;
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D _collider;
    public List<Vector2> _collideroffsetandsize;
    public List<string> animNames = new List<string>();
    public bool cheeze;
    public GameObject cheeze_abil_coin;
    public GameObject mail;
    public GameObject mailUI;
    public Transform mailparent;
    public GameObject[] effects;
    Vector3 BonusEntrypos = new Vector3(-6, 2.8f, 0);
    Vector3 Bonuspos = new Vector3(-6, -0.2f, 0);
    public enum State { run, jump, slide, dead, bonus };
    public State _state;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        mailparent = GameObject.Find("items").transform.Find("mails");
        isMobile = Application.isMobilePlatform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        animNames.Add("Jump1");
        animNames.Add("Jump2");
        // cherry run
        _collideroffsetandsize.Add(new Vector2(0, -1.34f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 1.23f));
        //cherry slide
        _collideroffsetandsize.Add(new Vector2(0, -1.52f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 0.8f));
        //cheeze run
        _collideroffsetandsize.Add(new Vector2(0, -1.2f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 1.23f));
        //cheeze slide
        _collideroffsetandsize.Add(new Vector2(0, -1.39f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 0.8f));
        mailUI = GameObject.Find("UI").transform.Find("Canvas").transform.Find("Mail").gameObject;
        mailUI.SetActive(cheeze);

    }
    //private void Start()
    //{
    //    if (cheeze)
    //        StartCoroutine("genMails");
    //}
    #region cheeze_ability

    IEnumerator genMails()
    {
        float dt;
        while (!GameManager.Instance.isGameOver)
        {
            Instantiate(mail,mailparent);
            dt = Mathf.Round(Random.Range(1f, 2.5f) * 10f) * 0.1f;
            yield return new WaitForSecondsRealtime(dt);
        }
    }
    public void cheezeabil()
    {
        StartCoroutine("abil");
    }
    IEnumerator abil()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Coroutine a = StartCoroutine("coin_party");
        yield return new WaitForSecondsRealtime(4f);
        StopCoroutine(a);
        yield break;
    }
    IEnumerator coin_party()
    {
        while (true)
        {
            //Instantiate(cheeze_abil_coin,new Vector3(Random.Range(0,5f), Random.Range(-3f,2.5f),0),Quaternion.identity,GameObject.Find("items").transform);
            Instantiate(cheeze_abil_coin, GameObject.Find("items").transform);
            yield return new WaitForSecondsRealtime(.5f);
        }
    }
    #endregion
    #region bonus
    public void Bonus()
    {
        StartCoroutine("BonusEnter");
    }
    IEnumerator BonusEnter()
    {
        _state = State.bonus;
        rb.gravityScale = 0f;
        anim.Play("BonusIn");
        while (Vector3.Distance(transform.position, BonusEntrypos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, BonusEntrypos, 3f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        while (Vector3.Distance(transform.position, Bonuspos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Bonuspos, 3f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion bonus
    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            if (isMobile)
            {
                // touch ????? ????
            }
            else
            {
                // ????? ?????
                if (!GameManager.Instance.inBonus)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && (_state == State.run || _state == State.jump))
                    {

                        //jump
                        if (jumpcount < 2)
                        {
                            _state = State.jump;
                            anim.Play(animNames[jumpcount++]);
                            if (jumpcount == 1)
                                rb.velocity = Vector3.zero;
                            rb.AddForce(Vector2.up * 22.5f, ForceMode2D.Impulse);
                        }

                    }
                    else if (Input.GetKey(KeyCode.LeftControl) && _state == State.run)
                    {
                        //run ->slide
                        _state = State.slide;
                        anim.Play("Slide");
                        if (!cheeze)
                        {
                            _collider.offset = _collideroffsetandsize[2];
                            _collider.size = _collideroffsetandsize[3];
                        }
                        else
                        { 
                            _collider.offset = _collideroffsetandsize[6];
                            _collider.size = _collideroffsetandsize[7];
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftControl))
                    {
                        //slide -> run
                        _state = State.run;
                        anim.Play($"Run{GameManager.Instance.RunLevel}");
                        if (!cheeze)
                        {
                            _collider.offset = _collideroffsetandsize[0];
                            _collider.size = _collideroffsetandsize[1];
                        }
                        else
                        {
                            _collider.offset = _collideroffsetandsize[4];
                            _collider.size = _collideroffsetandsize[5];
                        }
                    }
                }
                else
                {
                    // in Bonus Stage
                    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
                    {

                    }

                }
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("endzone"))
        {
            _state = State.dead;
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.gameOver(true);
        }
        else if (collision.collider.CompareTag("ground"))
        {
            if (_state != State.slide)
            {
                _state = State.run;
                jumpcount = 0;
                anim.Play($"Run{GameManager.Instance.RunLevel}");
            }
        }
        else if(collision.collider.CompareTag("complete"))
        {
            GameManager.Instance.gameOver(false);
        }
    }


    #region giant
    public void Giant()
    {
        gameObject.transform.position += new Vector3(0, 4, 0);
        GameManager.Instance.isBig = true;
        StartCoroutine("giant");
    }
    IEnumerator giant()
    {
        if (!GameManager.Instance.isGameOver)
        {
            // ���⸦ �������� �ѹ��� �ٲ�°� �ƴ϶� �ڷ�ƾ���� ���������� �ٲ��
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            yield return new WaitForSecondsRealtime(3f);
            Debug.Log($"stopgiant {Time.time}");
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.position -= new Vector3(0, 4, 0);
            GameManager.Instance.isBig = false;
            yield break;
        }
    }
    #endregion
    

    public void playermove()
    {
        Debug.Log("playermove");
        StartCoroutine("playerMove");
    }
    IEnumerator playerMove()
    {
        Vector3 a = new Vector3(12f, -1, 0);
        Debug.Log("playermovec");
        while (transform.position.x < 11f)
        {
            transform.position = Vector3.MoveTowards(transform.position, a, 5f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

}
