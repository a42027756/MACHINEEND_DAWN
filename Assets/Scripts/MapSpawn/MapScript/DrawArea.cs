using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class DrawArea 
{
    private float perlin_x;
    private float perlin_y;

    [SerializeField][Range(0,1000)]private int a_min;
    [SerializeField][Range(0,1000)]private int a_max;

    [SerializeField][Range(0,1000)]private int b_min;
    [SerializeField][Range(0,1000)]private int b_max;
    
    [Range(0, 1000)] public int c_min;
    [Range(0, 1000)] public int c_max;
    
    private int a;
    private int b;
    [FormerlySerializedAs("c")] public int map_amount;
    public TileBase drawTile;

    //初始化噪音参数
    public void InitBrush(int base_a,int base_b,int dec)
    {
        a_min = base_a + Random.Range(0,3);
        a_max = base_a + Random.Range(4, 6);

        b_min = base_b  + Random.Range(0,3);
        b_max = b_min + Random.Range(4, 6);

        c_min = c_max - Random.Range(20,dec);
    }
    
    public void SpawnTile(Tilemap tilemap)
    {
        if (a_max > a_min && b_max > b_min && c_max > c_min)
        {
            a = Random.Range(a_min, a_max);//随机取值范围a和b值决定地图分布形式
            b = Random.Range(b_min, b_max);
            map_amount = Random.Range(c_min, c_max);
        }
        else
        {
            Debug.Log("RandomAB");
            a = Random.Range(0, 100);
            b = Random.Range(0, 100);
        }

        int x_start = TileExpand.Instance.GetSeed()%2000;
        int y_start = (x_start + 520)%1000;
        Debug.Log(x_start + " " + y_start);

        for (perlin_x = x_start; perlin_x < Zone.Instance.size + x_start ; perlin_x++)
        {
            for (perlin_y = y_start; perlin_y < Zone.Instance.size + y_start; perlin_y++)
            {
                Vector3Int v = new Vector3Int((int)(perlin_x - x_start), (int)(perlin_y - y_start), 0);
                if (!tilemap.GetTile(v))
                {
                    float m = (perlin_x)/ a;
                    float n = (perlin_y)/ b;
                    float o = Mathf.PerlinNoise(m, n) * 1000;
                    // Debug.Log(o);
                    o = Mathf.Round(o);
                    // Debug.Log(Zone.Instance.enabledBools[v.x * Zone.Instance.size + v.y]);
                    if (o < map_amount)
                    {
                        tilemap.SetTile(v ,drawTile); 
                    }
                }
                
            }
        }
        
    }

    public void Blank_Fill(Tilemap _tilemap)
    {
        for (int i = 0; i < Zone.Instance.size; i++)
        {
            for (int j = 0; j < Zone.Instance.size; j++)
            {
                Vector3Int v = new Vector3Int(i, j, 0);
                if (!_tilemap.GetTile(v))
                {
                    _tilemap.SetTile(v,drawTile);
                }
            }
        }
    }
}

#if UNITY_EDITOR
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
#endif
