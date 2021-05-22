using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Cluster : MonoBehaviour
{
    float y;
    private void Awake()
    {
        transform.position = new Vector3(Random.Range(0, 5f), -4.75f, 0);
        y = Random.Range(-3f, 2.5f);
        StartCoroutine("moveUp");
    }
    IEnumerator moveUp()
    {
        while (transform.position.y < y)
        {
            transform.position += Vector3.up * Time.deltaTime*(y+4.75f);
            //Vector3.Lerp(transform.position, target, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
    private void Start()
    {
        GetComponent<Animator>().Play($"Cherry_Coin_EFFECT {Random.Range(0, 3)}");
        Destroy(gameObject, 5f);
    }
    void genchild()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
