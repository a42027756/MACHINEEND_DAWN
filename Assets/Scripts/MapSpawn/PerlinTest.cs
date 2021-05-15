using UnityEngine;

public class PerlinTest : MonoBehaviour
{
    public GameObject Par;//父物体

    public GameObject gam1;//生成物体

    public float x = 0;
    public float y = 0;

    public int a = 10;
    public int b = 10;//取值范围a和b值决定地图分布形式
    public int c = 50;//取值范围决定生成地图数量
    private void Start()
    {
        Map();
    }
    private void Map()
    {
        c = Random.Range(40, 50);//随机取值范围决定生成地图数量
        a = Random.Range(15, 30);//随机取值范围a和b值决定地图分布形式
        b = Random.Range(15, 30);
        for (x = 0; x < 1000; x = x + 1)
        {
            for (y = 0; y < 1000; y = y + 1)
            {
                float m = x / a;
                float n = y / b;
                float o = Mathf.PerlinNoise(m, n) * 100;
                o = Mathf.Round(o);//取整数
                Vector3 v3 = new Vector3(x, y, 9);
                if (o < c)
                {
                    GameObject gam = Instantiate(gam1, v3, Quaternion.identity);//实例化（名字+位置+旋转）
                    gam.transform.parent = Par.transform;
                }
            }
        }
    }
}

