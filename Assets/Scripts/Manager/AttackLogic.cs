using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//提供静态的攻击逻辑系统
public static  class AttackLogic  {

    //判断玩家在一定的范围内是否有敌人
    public static Vector3 IsSafe(Vector3 center,float radius,string enemyTag)
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            foreach(var c in hitColliders)
            {
                //
                if (c.gameObject.tag == enemyTag)
                {
                    return c.gameObject.transform.position;
                }
            }
        }
        return center;
    }

    

    //获取最近的敌人
    public static GameObject AttackNearest(Vector3 center, float radius, string enemyTag)
    {
        float dist;
        //获取最近的敌人位置
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            dist = 1000;
            GameObject target = null;
            //循环遍历攻击范围内的所有敌人，得到一个最近的距离和敌人
            foreach (var c in hitColliders)
            {
                if (c.gameObject.tag == enemyTag)
                {
                    float temp = Vector3.Distance(c.gameObject.transform.position, center);
                    if (temp<dist)
                    {
                        dist = temp;
                        target = c.gameObject;
                    }
                }
            }
            return target;
        }
        return null;
    }

    //获得攻击范围内的所有敌人，返回列表
    public static List<GameObject> AllEnemies(Vector3 center, float radius, string enemyTag)
    {
        List<GameObject> enemiesList = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            foreach (var c in hitColliders)
            {
                if (c.gameObject.tag == enemyTag)
                {
                    enemiesList.Add(c.gameObject);
                }
            }
            return enemiesList;
        }
        return null;
    }


}
