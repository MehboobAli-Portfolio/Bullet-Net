using UnityEngine;
using UnityEngine.EventSystems;
public class Descriptions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject dropdownMenu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropdownMenu.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropdownMenu.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropdownMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
