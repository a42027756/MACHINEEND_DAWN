using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Serialization;

[System.Serializable]
public class DrawArea
{
    public float perlin_x;
    public float perlin_y;

    [Range(0, 1000)] public int a_min;
    [Range(0, 1000)] public int a_max;
    
    [Range(0, 1000)] public int b_min;
    [Range(0, 1000)] public int b_max;
    
    [Range(0, 1000)] public int c_min;
    [Range(0, 1000)] public int c_max;
    
    private int a;
    private int b;
    [FormerlySerializedAs("c")] public int map_amount;
    
    [FormerlySerializedAs("_zone")] public Zone zone;
    public TileBase drawTile;

    public DrawArea(Zone _z, TileBase _t)
    {
        zone = _z;
        drawTile = _t;
    }
    
    public void SpawnTile(Tilemap tilemap,int drawTime_x, int drawTime_y)
    {
        
        if (a_max > a_min && b_max > b_min && c_max > c_min)
        {
            a = Random.Range(a_min, a_max);//随机取值范围a和b值决定地图分布形式
            b = Random.Range(b_min, b_max);
            map_amount = Random.Range(c_min, c_max);
        }
        else
        {
            a = Random.Range(0, 100);
            b = Random.Range(0, 100);
        }
        for (perlin_x = 0; perlin_x < zone.size; perlin_x++)
        {
            for (perlin_y = 0; perlin_y < zone.size; perlin_y++)
            {
                float m = perlin_x / a;
                float n = perlin_y / b;
                float o = Mathf.PerlinNoise(m, n) * zone.size;
                // Debug.Log(o);
                o = Mathf.Round(o);
                Vector3Int v = new Vector3Int((int)(perlin_x) + drawTime_x * zone.size, (int)(perlin_y) + drawTime_y *zone.size, 0);
                // Debug.Log((int)(perlin_x) + " " + (int)(perlin_y) + " " + o);
                if (o < map_amount)
                {
                    tilemap.SetTile(v ,drawTile);
                    zone.enabledBools[(int)perlin_y * zone.size + (int)perlin_x] = true;
                    // _zone.enabledBools[(int)(perlin_x * _zone.size), (int)(perlin_y * _zone.size)]
                }
                
            }
        }
    }
}

[CustomPropertyDrawer(typeof(DrawArea))]
public class InspectorAreaDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        //position: 在Inspector面板的位置、大小
        //property: 待绘制的属性
        //label: 值的字段名

        //绘制一个SerializedProperty的属性字段
        EditorGUI.PropertyField(position, property, label, true);

        //获取属性信息
        SerializedProperty data = property.FindPropertyRelative("_zone");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //按照行数增加高度
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property);
    
        return EditorGUI.GetPropertyHeight(property);
    }
}

