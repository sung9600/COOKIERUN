using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Vector3 speed = new Vector3(.5f, 0f, 0f);
    void Awake()
    {
        //spr=Resources.Load<Sprite>("Materials/Images/MyBackground/Land1/Land1_background.png");
        //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Materials/Images/MyBackground/Land1/Land1_background.png");
        //Debug.Log((Sprite)Resources.Load<Sprite>("Materials/Images/MyBackground/Land1/Land1_background"));
        StartCoroutine("MoveBackground");
    }
    

   public IEnumerator MoveBackground()
    {
        yield return null;
        while (!GameManager.Instance.isGameOver)
        {
            if (transform.position.x > -19.04f)
                transform.position -= Time.deltaTime * speed;
            else
            {
                transform.position = new Vector3(19.04f, 0, 0);
            }
            yield return new WaitForEndOfFrame();
        }

        
    }

}
