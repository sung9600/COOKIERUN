using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Panel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    Color a = new Color(0, 0, 0, 0);
    Color b = new Color(0, 0, 0, .5f);
    Color c = new Color(255, 255, 255, .5f);
    public GameObject other;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.name == "Cheeze_Panel")
        {
            Transfer.Instance.cheeze = true;
        }
        else if (gameObject.name == "Cherry_Panel")
        {
            Transfer.Instance.cheeze = false;
        }

        gameObject.GetComponent<Image>().color = c;
        other.GetComponent<Image>().color = b;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"{gameObject.name} enter");
        gameObject.GetComponent<Image>().color = a;
        other.GetComponent<Image>().color = b;
    }
}
