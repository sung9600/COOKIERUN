using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundpart : MonoBehaviour
{
    Vector3 speed = new Vector3(4f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MovePart");
    }
    IEnumerator MovePart()
    {
        while (true)
        {
            if(transform.position.x<-9.6f)
            {
                //Debug.Log($"{gameObject.name} move, x: {transform.position.x}");
                transform.position =new Vector3(11.2f,-4,0);
                if (GameManager.Instance.nextBlank)
                {
                    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
                }
                else
                {

                    gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
            transform.position -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }
}
