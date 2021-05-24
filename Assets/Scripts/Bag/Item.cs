using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public bool canPlace;
    public int size_x, size_y;      //物品的占空间大小

    private Vector2 initPos;        //拖拽开始前的位置
    private bool isInitVertical;
    [SerializeField] private bool isSelected;
    private bool isVertical;
    private Vector2 mousePos;

    void Start()
    {
        isVertical = false;
        isSelected = false;
    }

    void Update()
    {
        if(isSelected)
        {
            FollowPointer();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected)
        {
            //第二次点击判断当前位置是否可放置物品或是切换物品方向
            if(eventData.pointerId == -2)
            {
                //旋转物体
                isVertical = !isVertical;
                DecideDirection(isVertical);
            }
            else if(eventData.pointerId == -1)
            {
                if(canPlace)
                {
                    //放置物体至新位置
                    Debug.Log("move to new place");
                }
                else
                {
                    //回归初始位置即初始方向
                    transform.position = initPos;
                    DecideDirection(isInitVertical);
                    isVertical = isInitVertical;
                    isSelected = false;
                }
            }
        }
        else if(eventData.pointerId == -1)
        {
            //鼠标左键点击选中物品并记录初始方向
            initPos = transform.position;
            isInitVertical = isVertical;
            FollowPointer();
            isSelected = true;
        }
        
    }

    public void FollowPointer()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
    }

    public void DecideDirection(bool vertical)
    {
        if(vertical)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
