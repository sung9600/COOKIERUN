using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public enum State { Idle, BonusEntry, Abil, Magnet };
    public State _state = State.Idle;
    public static Pet Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private static Pet instance = null;
    #region consts
    Vector3 frontpos = new Vector3(2, 0, 0);
    Vector3 initpos = new Vector3(-7, -1, 0);
    Vector3 frontscale = new Vector3(2, 2, 1);
    Vector3 initscale = new Vector3(.5f, .5f, 1);
    Vector3 BonusEntrypos = new Vector3(-6, 3, 0);
    Vector3 Bonuspos = new Vector3(-6, 0, 0);
    #endregion

    Animator animator;
    public bool magneton;
    public GameObject abiljelly;
    public Transform jellyparent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine("Abil");
        //Invoke("Magnet", 3f);
        //Magnet();
        //Bonus();
    }
    #region bonus
    public void Bonus()
    {
        StartCoroutine("BonusEntry");
    }

    IEnumerator BonusEntry()
    {
        yield return null;
        _state = State.BonusEntry;
        transform.position = new Vector3(-6, -1, 0);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        animator.SetBool("Bonus", true);
        //Debug.Log($"start:{Time.time}");
        while (Vector3.Distance(transform.position, BonusEntrypos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, BonusEntrypos, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log($"end : {Time.time}");
        while (Vector3.Distance(transform.position, Bonuspos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Bonuspos, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        animator.SetBool("entryDone", true);
        animator.SetBool("entryDone", false);

    }
    #endregion

    #region abil

    IEnumerator Abil()
    {
        yield return null;
        while (!GameManager.Instance.isGameOver)
        {
            if (Vector3.Distance(transform.position,frontpos)>0.1f)
                StartCoroutine(_Magnet_Abil_PetMove($"{_state}", "Abil"));
            Coroutine a = StartCoroutine(AbilJellyGen());
            yield return new WaitForSeconds(6f);
            StopCoroutine(a);
            yield return new WaitForSeconds(12f);
        }
        yield break;
    }

    IEnumerator AbilJellyGen()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            // ¿©±â ¾îºô Á©¸® Á¨
            for (int i = 0; i < 10; i++)
            {
                GameObject a = Instantiate(abiljelly, transform.position, transform.rotation, jellyparent);
                a.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-7f, -3f), Random.Range(6f, 10f)), ForceMode2D.Impulse);
                Destroy(a, 2f);
            }
            yield return new WaitForSeconds(1f);
        }
        //yield break;
    }
    #endregion
    #region magnet
    public void Magnet()
    {
        if (Vector3.Distance(transform.position, frontpos) > 0.1f)
            StartCoroutine(_Magnet_Abil_PetMove($"{_state}", "Magnet"));
        StartCoroutine(_Magnet());
    }
    IEnumerator _Magnet()
    {
        yield return null;
        magneton = true;
        yield return new WaitForSeconds(5f);
        magneton = false;
        yield break;
    }
    IEnumerator _Magnet_Abil_PetMove(string before, string after)
    {
        yield return null;
        _state = (State)System.Enum.Parse(typeof(State), after);
        animator.SetBool(after, true);

        while (transform.position.x < 1.7f)
        {
            transform.position = Vector3.MoveTowards(transform.position, frontpos, 5f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, frontscale, 1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3f);
        if (_state == (State)System.Enum.Parse(typeof(State), after))
        {
            while (transform.position.x > -6.95f)
            {
                transform.position = Vector3.MoveTowards(transform.position, initpos, 5f * Time.deltaTime);
                transform.localScale = Vector3.Lerp(transform.localScale, initscale, 1f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            transform.position = initpos;
            transform.localScale = initscale;
            _state = State.Idle;
        }
        animator.SetBool(after, false);
        yield break;
    }

    #endregion
}
