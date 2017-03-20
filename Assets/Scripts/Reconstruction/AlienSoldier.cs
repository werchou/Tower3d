using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienSoldier : Soldier {

    //巡逻半径和中心
    private float patrolRadius;
    private Vector3 patrolCenter;

    public AlienSoldier(GameObject g)
    {
        gameObject = g;
    }

    public override void Init()
    {
        
        //初始化巡逻半径和中心
        patrolCenter = gameObject.transform.parent.gameObject.transform.position;
        patrolRadius = gameObject.transform.parent.gameObject.GetComponent<UFOArmyControl>().Radius;
        //设置初试状态为巡逻
        States = new List<BaseState>();
        TSPatrolState patrols = new TSPatrolState(this.gameObject);
        States.Add(patrols);
        currentState = patrols;
        currentState.enter();
    }

    public override void PatrolAction()
    {
        Vector3 randomPos = randPos();
        iTween.MoveTo(gameObject, randomPos, 1);
    }

    private Vector3 randPos()
    {
        //生成随机位置
        float randX = gameObject.transform.position.x + Random.Range(-patrolRadius, patrolRadius);
        float randZ = gameObject.transform.position.z + Random.Range(-patrolRadius, patrolRadius);
        Vector3 randPos = new Vector3(randX, 0, randZ);
        return randPos;
    }

}
