 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public RectTransform Button;
    // Start is called before the first frame update
    void Start()
    {
        //Button.GetComponent<Animator>().Play("HoverOn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Button.localScale *= 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Button.localScale /= 1.1f;
    }
}
