using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Vector3 speed = new Vector3(4f, 0, 0);
    // Start is called before the first frame update
    protected void Start()
    {
        StartCoroutine("MoveItem");   
    }

    protected IEnumerator MoveItem()
    {
        while (!GameManager.Instance.isGameOver)
        {
            if (Pet.Instance.magneton&&Vector3.Distance(transform.position,Pet.Instance.gameObject.transform.position)<3f)
            {
                transform.position = Vector3.Lerp(transform.position, Pet.Instance.gameObject.transform.position, Time.deltaTime);
            }
            else
                transform.position -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")||(Pet.Instance.magneton&& collision.CompareTag("pet")))
        {
            DoJob();
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("endzone"))
        {
            Destroy(this.gameObject);
        }
    }
    
    protected virtual void DoJob()
    {
    }
}
