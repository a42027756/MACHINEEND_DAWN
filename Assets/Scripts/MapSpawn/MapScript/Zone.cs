 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Serialization;

 [System.Serializable]
 public class Zone
 {
     public int size;

     //很懒，启动时不创建单例对象。
     private static Zone _instance;

     //私有化构造方法，不能new对象，只能通过Singleton.instance的方法得到对象
     private Zone()
     {
     }

     //得到单例对象
     public static Zone Instance
     {
         get
         {
             //判断_instance是否为空，为空时是第一次调动该方法。创建Singleton对象返回，不为空 
             //说明不是第一次进入。返回上一次创建的对象。当两个线程同时第一次进入这里，会都判断到
             //_instance为空，而创建两个Singleton对象返回。所以时线程不安全的。
             if (_instance == null)
             {
                 _instance = new Zone();
             }

             return _instance;
         }
     }
 }
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Zone))]
public class InspectorGridDrawer : PropertyDrawer
{
    int rows;
    int columns;

    //自定义面板显示
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //position: 在Inspector面板的位置、大小
        //property: 待绘制的属性
        //label: 值的字段名

        //绘制一个SerializedProperty的属性字段
        EditorGUI.PropertyField(position, property, label, true);

        //获取属性信息
        SerializedProperty data = property.FindPropertyRelative("enabledBools");
        rows = property.FindPropertyRelative("size").intValue;
        columns = rows;

        if (rows < 0)
            rows = 0;

        if (columns < 0)
            columns = 0;

        //指定数组大小
        data.arraySize = rows * columns;

        //自定义显示区域
        // if (property.isExpanded)
        // {
        //     int count = 0;
        //     float targetX;
        //     float targetY;
        //
        //     //遍历
        //     for (int r = 0; r < rows; r++)
        //     {
        //         for (int c = 0; c < columns; c++)
        //         {
        //             //计算位置
        //             targetX = position.xMin + ((gridWidth + gridSpace) * (c + 1));
        //             targetY = 60 + position.yMin + (gridHeight + gridSpace) * (r + 2);
        //             //位置、大小
        //             Rect rect = new Rect(targetX, targetY, 15f * (EditorGUI.indentLevel + 1), gridHeight);
        //             //绘制属性值
        //             EditorGUI.PropertyField(rect, data.GetArrayElementAtIndex(count), GUIContent.none);
        //
        //             count++;
        //         }
        //     }
        // }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //按照行数增加高度
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property);
    
        return EditorGUI.GetPropertyHeight(property);
    }

}
#endif