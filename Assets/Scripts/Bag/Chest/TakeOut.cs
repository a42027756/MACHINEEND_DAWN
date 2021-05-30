using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TakeOut : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    public Sprite normalSprite, pressedSprite;
    public ChestManager chestManager;
    
    private Image image;
    
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.sprite = pressedSprite;

        chestManager.TakeOut();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }
}
