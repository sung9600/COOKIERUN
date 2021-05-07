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
            transform.position -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
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
