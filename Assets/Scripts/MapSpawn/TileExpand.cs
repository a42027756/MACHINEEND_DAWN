using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileExpand : Singleton<TileExpand>
{
    public int randSeed;
    //在Tilemap中的特定Tile种类上获取一个随机点
    public Vector3Int GetRandomPointInTilemap(Tilemap tilemap, TileBase basetile)
    {
        Vector3Int randomPos = new Vector3Int(Random.Range(0, Zone.Instance.size),Random.Range(0, Zone.Instance.size),0);
        while (tilemap.GetTile(randomPos) != basetile)
        {
            randomPos.x = Random.Range(0, Zone.Instance.size);
            randomPos.y = Random.Range(0,Zone.Instance.size);    
        }
        return randomPos;
    }
    
    
    //在basemap上判断tileBase片区上是否可以容纳长length，高height的矩形,
    public bool CanFit(Vector3Int originPos,int length, bool isHorizontal,Tilemap baseMap, TileBase tileBase)
    {
        Vector3Int up = originPos;
        Vector3Int right = originPos;
        if (isHorizontal)
        {
            up = new Vector3Int(x: originPos.x, y: originPos.y + length, z: 0);
        }
        else
        {
            right = new Vector3Int(x: originPos.x + length, y: originPos.y, z: 0);
        }
        if (baseMap.GetTile(position: up) != tileBase || baseMap.GetTile(position: right) != tileBase)
        {
            return false;
        }
        return true;
    }
    
    //特定tile片区上是否可以容纳size大小的正方形
    public bool CanFit(Vector3Int originPos,int size,Tilemap baseMap, TileBase tileBase)
    {
        Vector3Int leftUp = new Vector3Int(originPos.x, originPos.y + size, 0);
        Vector3Int rightUp = new Vector3Int(originPos.x + size, originPos.y + size, 0);
        Vector3Int rightDown = new Vector3Int(originPos.x + size, originPos.y, 0);
        if (baseMap.GetTile(rightUp) != tileBase || baseMap.GetTile(rightDown) != tileBase
                                                  || baseMap.GetTile(leftUp) != tileBase)
        {
            return false;
        }
        return true;
    }
    
    //在baseMap生成spawnSize * spawnSize大小的baseTile，起点为originPosition
    public void  SpawnBlockInTilemap(Tilemap baseMap, TileBase baseTile , Vector3Int originPosition,int spawnSize)
    {
        for (int i = 0; i < spawnSize; i++)
        {
            for (int j = 0; j < spawnSize; j++)
            {
                baseMap.SetTile(new Vector3Int(originPosition.x + i,originPosition.y + j,0),baseTile);
            }
        }
    }

    //在basemap上延x轴向右绘制tile，绘制笔刷为basetile，起点为originPosition，线段长度为spawnLength
    public void SpawnLineInTilemap_x_toRight(Tilemap baseMap, TileBase baseTile, Vector3Int originPosition, int spawnLength)
    {
        for (int i = 0; i < spawnLength; i++)
        {
            baseMap.SetTile(new Vector3Int(originPosition.x + i, originPosition.y, 0), baseTile);
        }
    }
    
    //在basemap上延x轴向左绘制tile，绘制笔刷为basetile，起点为originPosition，线段长度为spawnLength
    public void SpawnLineInTilemap_x_toLeft(Tilemap baseMap, TileBase baseTile, Vector3Int originPosition, int spawnLength)
    {
        for (int i = 0; i < spawnLength; i++)
        {
            baseMap.SetTile(new Vector3Int(originPosition.x - i, originPosition.y, 0), baseTile);
        }
    }

    //在basemap上向上延y轴绘制tile，绘制笔刷为basetile，起点为originPosition，线段长度为spawnLength
    public void SpawnLineInTilemap_y_ToUp(Tilemap baseMap, TileBase baseTile, Vector3Int originPosition, int spawnLength)
    {
        for (int i = 0; i < spawnLength; i++)
        {
            baseMap.SetTile(new Vector3Int(originPosition.x, originPosition.y + i, 0), baseTile);
        }
    }
    
    //在basemap上向下延y轴绘制tile，绘制笔刷为basetile，起点为originPosition，线段长度为spawnLength
    public void SpawnLineInTilemap_y_ToDown(Tilemap baseMap, TileBase baseTile, Vector3Int originPosition, int spawnLength)
    {
        for (int i = 0; i < spawnLength; i++)
        {
            baseMap.SetTile(new Vector3Int(originPosition.x, originPosition.y - i, 0), baseTile);
        }
    }

    //在baseMap清空spawnSize * spawnSize大小的区块，起点为originPosition
    public void ClearBlockInTilemap(Tilemap baseMap, Vector3Int originPosition, int spawnSize)
    {
        for (int i = 0; i < spawnSize; i++)
        {
            for (int j = 0; j < spawnSize; j++)
            {
                baseMap.SetTile(new Vector3Int(originPosition.x + i,originPosition.y + j,0),null);
            }
        }
    }

    public int GetSeed()
    {
        if (randSeed == 0)
        {
            randSeed = Random.Range(10000000, 92233720);
        }

        return randSeed;
    }
    
    
}
