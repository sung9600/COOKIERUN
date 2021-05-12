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
        while (!GameManager.Instance.isGameOver)
        {
            if (transform.position.x < -9.6f)
            {
                //Debug.Log($"{gameObject.name} move, x: {transform.position.x}");
                transform.position = new Vector3(11.2f, -4, 0);
                //floor();
                //JellyGen();
                //ObstacleGen();
                //Debug.Log(Time.time);
                //GameManager.Instance.currentPos++;
            }
            transform.position -= Time.deltaTime * speed;
            yield return new WaitForFixedUpdate();
        }
    }
    #region gens
    // map.jelly_h: 젤리높이, map.jelly: 젤리 종류 , map.obstacle: obstacle 종류, map.floor: 발판 높이
    void floor()
    {
        if (GameManager.Instance.currentPos <= GameManager.Instance.map.Count)
        {
            if (GameManager.Instance.map[GameManager.Instance.currentPos].floor == 1)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }
    void JellyGen()
    {
        if (GameManager.Instance.currentPos <= GameManager.Instance.map.Count)
        {
            if (GameManager.Instance.map[GameManager.Instance.currentPos].jelly != 0)
            {
                int h = GameManager.Instance.map[GameManager.Instance.currentPos].jelly_h; // 높이
                int j = GameManager.Instance.map[GameManager.Instance.currentPos].jelly; // 종류

                //Instantiate();
            }
            else
                return;
        }
    }

    void ObstacleGen()
    {
        if (GameManager.Instance.map[GameManager.Instance.currentPos].obstacle != 0)
        {

        }
        else
            return;
    }

    #endregion
}
