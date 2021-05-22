using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundpart : MonoBehaviour
{
    Vector3 speed = new Vector3(4f, 0f, 0f);
    public static Transform parent;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("items").transform;
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
                GameManager.Instance.currentPos++;
                if (GameManager.Instance.currentPos < 100)
                {
                    floor();
                    JellyGen();
                    ObstacleGen();
                    //Debug.Log(Time.time);
                }
                else if(GameManager.Instance.currentPos == 100)
                {
                    Player.Instance.playermove();
                }
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
            if (GameManager.Instance.map[GameManager.Instance.currentPos].floor != 0)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                transform.position = new Vector3(transform.position.x, GameManager.Instance.map[GameManager.Instance.currentPos].floor - 5, 0);
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
                int j = GameManager.Instance.map[GameManager.Instance.currentPos].jelly-1; // 종류
                Instantiate(GameManager.Instance.items[j], new Vector3(transform.position.x, -2.5f + 0.6f * h, 0), Quaternion.identity, parent);
            }
            else
                return;
        }
    }

    void ObstacleGen()
    {
        if (GameManager.Instance.map[GameManager.Instance.currentPos].obstacle != 0)
        {
            int j = GameManager.Instance.map[GameManager.Instance.currentPos].obstacle-1; // 종류
            Instantiate(GameManager.Instance.obstacles[j],parent);
        }
        else
            return;
    }

    #endregion
}
