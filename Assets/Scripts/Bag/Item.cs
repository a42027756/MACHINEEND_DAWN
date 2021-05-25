using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public bool canPlace;
    public int size;      //物品的占空间大小
    public Transform bag_Trans;
    private Sprite sprite;
    private Vector2 extents;

    private int scanIndex;                                  //扫描背包单元格中物品即将放入的格子的顺序
    private Vector2 firstPos, lastPos, destinationPos;      //扫描第一个格子位置和最后一个格子位置取均值的物品目标放置位置
    
    private Vector2 initPos;                                //拖拽开始前的位置
    private bool isInitVertical;                            //记录鼠标点击之前物品方向信息
    private bool isSelected;                                //物品是否处于选中状态
    private bool isVertical;                                //记录物体被选中后物品方向信息
    private Vector2 mousePos;

    void Awake()
    {
        sprite = GetComponent<Image>().sprite;

        Vector3[] corners = new Vector3[4];
        gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
        extents = corners[2] - transform.position;
    }
    
    void Start()
    {
        scanIndex = 1;

        isVertical = false;
        isSelected = false;
    }

    void Update()
    {
        if(isSelected)
        {
            FollowPointer();
            ScanSlot();
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
                PlaceHere();
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

    public void ScanSlot()
    {
        canPlace = true;
        scanIndex = 1;
        foreach(Transform rawChild in bag_Trans)
        {
            foreach(Transform child in rawChild.transform)
            {
                Vector2 pos = child.transform.position;
                if(ContainsPos(pos))
                {
                    Image childImage = child.GetComponent<Image>();
                    childImage.color = Color.green;
                    if(!child.GetComponent<ItemSlot>().vacant)
                    {
                        canPlace = false;
                        childImage.color = Color.red;
                    }
                    if(scanIndex == 1)
                    {
                        firstPos = pos;
                    }else if(scanIndex == size)
                    {
                        lastPos = pos;
                    }
                    scanIndex++;
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }
            }
        }
        if(scanIndex < size + 1)
        {
            canPlace = false;
        }
    }

    public void PlaceHere()
    {
        ScanSlot();

        if(canPlace)
        {
            //放置物体至新位置并标记占位值vacant为false
            destinationPos = (firstPos + lastPos) / 2;
            transform.position = destinationPos;
            isSelected = false;
        }
        else
        {
            //回归初始位置并回归初始方向
            transform.position = initPos;
            DecideDirection(isInitVertical);
            isVertical = isInitVertical;
            isSelected = false;
        }
    }

    public bool ContainsPos(Vector2 pos)
    {
        Vector2 lower_left, upper_right;                    //物品左上及右下坐标
        Vector2 thisPosition = transform.position;
        if(isVertical)
        {
            lower_left = new Vector2(thisPosition.x - extents.y, thisPosition.y - extents.x);
            upper_right = new Vector2(thisPosition.x + extents.y, thisPosition.y + extents.x);
        }
        else
        {
            lower_left = thisPosition - extents;
            upper_right = thisPosition + extents;
        }
        if(pos.x > lower_left.x && pos.y > lower_left.y && pos.x < upper_right.x &&　pos.y < upper_right.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
