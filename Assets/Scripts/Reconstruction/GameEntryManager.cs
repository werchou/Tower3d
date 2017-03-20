using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEntryManager  :Manager {
    //基地
    public List<TowerPlace> towerBases;

    public GameEntryManager()
    {

    }

    public override void Init()
    {
        GameObject[] towerPlaces = GameObject.FindGameObjectsWithTag("TowerFactory");
        if (towerPlaces.Length > 0)
        {
            foreach(var tp in towerPlaces)
            {
                towerBases.Add(new TowerPlace(tp));
            }
        }
    }

    //生成一个游戏实体
    public GameEntry CreateEntry(string prefabPath)
    {
        GameEntry temp = new Soldier();
        temp.gameObject = Prefab2Object(prefabPath);
        return temp;
    }

    //预制件->游戏对象
    public GameObject Prefab2Object(string prefabPath)
    {
        GameObject prefab = Resources.Load(prefabPath) as GameObject;
        GameObject GameObj = Object.Instantiate(prefab) as GameObject;
        return GameObj;
    }
}
