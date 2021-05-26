using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public bool canPlace;                                   //当前位置是否可放置物品
    public Transform bag_Trans;                             //背包单元格父物体的transform值，用于遍历所有单元格
    private Vector2 extents;                                //物品的四种相对于中心的偏移量

    public int size;                                        //物品的占空间大小
    private Vector2Int[] startCoordinate, destCoordinate;      //记录物品初始位置以及目的位置占据背包单元格坐标信息
    private int scanIndex;                                  //扫描背包单元格中物品即将放入的格子的顺序
    private Vector2 firstPos, lastPos, destinationPos;      //扫描第一个格子位置和最后一个格子位置取均值的物品目标放置位置
    
    private Vector2 initPos;                                //拖拽开始前的位置
    private bool isInitVertical;                            //记录鼠标点击之前物品方向信息
    private bool isSelected;                                //物品是否处于选中状态
    private bool isVertical;                                //记录物体被选中后物品方向信息
    private Vector2 mousePos;

    bool test = true;

    void Awake()
    {  
        Vector3[] corners = new Vector3[4];
        gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
        extents = corners[2] - transform.position;

        startCoordinate = new Vector2Int[size];
        destCoordinate = new Vector2Int[size];

        isVertical = false;
        isSelected = false;
    }

    void Update()
    {
        // InitOnlyOnce();
        
        Vector2 pos = transform.position;
        Debug.DrawLine(pos, pos + extents, Color.red);
        Debug.DrawLine(pos, pos - extents, Color.red);
        
        if(isSelected)
        {
            transform.position = Input.mousePosition;
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
            //第一次点击开始移动物品并记录初始信息
            RecordInitialData();
        }
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

    //扫描当前鼠标选中物体所在位置在背包单元格上的投影是否可放置物品
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
                        childImage.color = Color.red;           //测试用
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
        RecordCoordinate(destCoordinate);

        if(canPlace)
        {
            SetCoordinateTag(destCoordinate, false);
            destinationPos = (firstPos + lastPos) / 2;
            transform.position = destinationPos;
        }
        else
        {
            transform.position = initPos;
            isVertical = isInitVertical;
            DecideDirection(isInitVertical);
        }
        isSelected = false;
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

    //鼠标左键点击选中物品并记录初始位置及方向信息
    public void RecordInitialData()
    {
        initPos = transform.position;
        isInitVertical = isVertical;

        //记录初始时物品坐标信息并将坐标设为空
        RecordCoordinate(startCoordinate);
        SetCoordinateTag(startCoordinate, true);

        transform.position = Input.mousePosition;
        isSelected = true;
    }

    //处理坐标上的标记值
    public void SetCoordinateTag(Vector2Int[] _coordinate, bool _vacant)
    {
        BagSlots bagSlots = bag_Trans.GetComponent<BagSlots>();
        for(int index = 0;index < size;index++)
        {
            bagSlots.itemList2D[_coordinate[index].x-1][_coordinate[index].y-1].GetComponent<ItemSlot>().vacant = _vacant;
        }
    }

    public void RecordCoordinate(Vector2Int[] _coordinate)
    {
        int index = 0;
        foreach(Transform rawChild in bag_Trans)
        {
            foreach(Transform child in rawChild.transform)
            {
                Vector2 pos = child.transform.position;
                if(ContainsPos(pos))
                {
                    Debug.Log("Enter");
                    ItemSlot slot = child.GetComponent<ItemSlot>();
                    _coordinate[index].x = slot.index_Raw;
                    _coordinate[index].y = slot.index_Column;
                    index++;
                }
                child.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void InitOnlyOnce()
    {
        if(test)
        {
            scanIndex = 1;
            foreach(Transform rawChild in bag_Trans)
            {
                foreach(Transform child in rawChild.transform)
                {
                    Vector2 pos = child.transform.position;
                    if(ContainsPos(pos))
                    {
                        child.GetComponent<ItemSlot>().vacant = false;
                        if(scanIndex == 1)
                        {
                            firstPos = pos;
                        }else if(scanIndex == size)
                        {
                            lastPos = pos;
                        }
                        scanIndex++;
                    }
                }
            }
            destinationPos = (firstPos + lastPos) / 2;
            transform.position = destinationPos;
            test = false;
        }
    }
}
