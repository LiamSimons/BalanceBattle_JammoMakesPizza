using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverJammo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject Jammo;
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
        Jammo.transform.GetComponent<Animator>().SetBool("hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Jammo.transform.GetComponent<Animator>().SetBool("hover", false);
    }
}
