using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Serialization;

[System.Serializable]
public class Zone : Singleton<Zone>
{
    public int size;
    // [HideInInspector] public bool[] enabledBools = new bool[1000000];
}

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