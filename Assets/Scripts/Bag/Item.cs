using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public bool canPlace;
    public int size_raw, size_column;      //物品的占空间大小
    public Transform bag_Trans;
    private Sprite sprite;
    private Vector2 extents;

    private Vector2 initPos;        //拖拽开始前的位置
    private Vector2 destinationPos;
    private bool isInitVertical;
    [SerializeField] private bool isSelected;
    private bool isVertical;
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
        isVertical = false;
        isSelected = false;
    }

    void Update()
    {
        if(isSelected)
        {
            FollowPointer();
            ColorSlot();
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

    public void ColorSlot()
    {
        canPlace = true;
        foreach(Transform rawChild in bag_Trans)
        {
            List<GameObject> line = new List<GameObject>();
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
                    //利用最后一个遍历的格子的位置信息赋值物品目的位置
                    destinationPos = child.transform.position + new Vector3(50 * (size_raw-1), 50 * (size_column-1), 0);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    public void PlaceHere()
    {
        ColorSlot();

        if(canPlace)
        {
            //放置物体至新位置
            Debug.Log("move to new place");
            Debug.Log(destinationPos);
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
        Vector2 lower_left, upper_right;            //物品左上及右下坐标
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
