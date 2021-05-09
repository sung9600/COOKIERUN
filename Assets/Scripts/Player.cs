using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3 BonusEntrypos = new Vector3(-6, 2.8f, 0);
    Vector3 Bonuspos = new Vector3(-6, -0.2f, 0);
    public enum State { run, jump, slide, dead, bonus };
    public State _state;
    private void Awake()
    {
        isMobile = Application.isMobilePlatform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        animNames.Add("Jump1");
        animNames.Add("Jump2");
        _collideroffsetandsize.Add(new Vector2(0, -1.34f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 1.23f));
        _collideroffsetandsize.Add(new Vector2(0, -1.52f));
        _collideroffsetandsize.Add(new Vector2(1.03f, 0.8f));
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
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
            yield return new WaitForEndOfFrame();
        }
        while (Vector3.Distance(transform.position, Bonuspos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Bonuspos, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
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
                                rb.velocity = Vector2.zero;
                            rb.AddForce(Vector2.up * 22.5f, ForceMode2D.Impulse);
                        }

                    }
                    else if (Input.GetKey(KeyCode.LeftControl) && _state == State.run)
                    {
                        //run ->slide
                        _state = State.slide;
                        anim.Play("Slide");
                        _collider.offset = _collideroffsetandsize[2];
                        _collider.size = _collideroffsetandsize[3];
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftControl))
                    {
                        //slide -> run
                        _state = State.run;
                        anim.Play($"Run{GameManager.Instance.RunLevel}");
                        _collider.offset = _collideroffsetandsize[0];
                        _collider.size = _collideroffsetandsize[1];
                    }
                }
                else
                {
                    // ????? -> ?????? ?????? ???
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
    }

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
            // 여기를 스케일이 한번에 바뀌는게 아니라 코루틴으로 점진적으로 바뀌게
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            yield return new WaitForSeconds(3f);
            Debug.Log("stopgiant");
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.position -= new Vector3(0, 4, 0);
            GameManager.Instance.isBig = false;
            yield break;
        }
    }
}
