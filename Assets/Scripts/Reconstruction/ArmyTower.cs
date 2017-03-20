using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmyTower : Tower {

    public string towerPrefab = "PrefabsDone/UFOArmy";

    private bool isActive;

    //管理士兵数量
    public int soldierCount;

    //士兵列表
    public List<Soldier> soldierList;

    public ArmyTower(GameObject g,int i)
    {
        gameObject = g;
        soldierCount = i;
        isActive = false;
    }

    public override void Init()
    {
        soldierList = new List<Soldier>(soldierCount);
    }


    //生成列表内的一个士兵
    public void CreateSoldier(string prefabPath)
    {
        Soldier temp = new Soldier();
        temp.gameObject = Prefab2Object(prefabPath);
        soldierList.Add(temp);
    }

    //预制件->游戏对象
    public GameObject Prefab2Object(string prefabPath)
    {
        GameObject prefab = Resources.Load(prefabPath) as GameObject;
        GameObject GameObj = Object.Instantiate(prefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        return GameObj;
    }


}
