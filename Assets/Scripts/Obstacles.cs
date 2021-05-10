using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 5, 0);

        StartCoroutine("Move");
    }
    IEnumerator Move()
    {
        while (!GameManager.Instance.isGameOver)
        {
            if (transform.position.y > 1.7f)
            {
                transform.position += 7f*Time.deltaTime * speed;
            }
            transform.position += 4f * Time.deltaTime * Vector3.left;
            yield return new WaitForEndOfFrame();
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (!GameManager.Instance.isBig)
            {
                Destroy(this.gameObject);
                // hp까이는거
                GameManager.Instance.hpChange(-40);
            }
            else
            {
                //거대화중 부숴지는 애니메이션
                StopCoroutine("Move");
                StartCoroutine("spin");
                Destroy(this.gameObject, 2f);
            }
        }
        else if (collision.CompareTag("endzone"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator spin()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(20f, 3f), ForceMode2D.Impulse);
        while (true)
        {
            transform.RotateAround(transform.position, Vector3.back, Time.deltaTime * 150f);
            yield return new WaitForEndOfFrame();
        }
    }
}
