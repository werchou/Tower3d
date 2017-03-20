using UnityEngine;
using System.Collections;

public enum StateType{
    TSPatrol,
    TSAttack,
    ASPatrol,
    ASAttack
}

public class BaseState {
    public GameObject OwnObject;
    public BaseState() { }
    public virtual void execute() { }
    public virtual void enter() { }
}


public class StateFactory
{
    public static BaseState CreatState(StateType t,GameObject g)
    {
        // 根据传入的类型来生成不同的状态
        switch (t)
        {
            case StateType.TSPatrol:
                return new TSPatrolState(g);
            case StateType.TSAttack:
                return new TSAttackState(g);
            case StateType.ASPatrol:
                return new ASPatrolState(g);
            case StateType.ASAttack:
                return new ASAttackState(g);
        }

        return null;
    }
}


public class TSPatrolState : BaseState
{

    public TSPatrolState(GameObject own)
    {
        OwnObject = own;
    }


    public override void execute()
    {
        //执行巡逻状态行为
        OwnObject.GetComponent<TowerSoldierControl>().PatrolExe();
    }

    public override void enter()
    {
        //进入巡逻状态，移动带随机位置
        OwnObject.GetComponent<TowerSoldierControl>().PatrolMove();
        
    }
}

public class TSAttackState : BaseState
{
    public TSAttackState(GameObject own)
    {
        OwnObject = own;
    }

    //进入攻击状态的行为逻辑
    public override void enter()
    {
        OwnObject.GetComponent<TowerSoldierControl>().AttackMove();
    }

    //执行攻击逻辑
    public override void execute()
    {
        OwnObject.GetComponent<TowerSoldierControl>().AttackExe();
    }

}

public class ASPatrolState:BaseState
{
    public ASPatrolState(GameObject g)
    {
        OwnObject = g;
    }

    public override void enter()
    {
        
    }

    public override void execute()
    {
        OwnObject.GetComponent<AttackSoldierControl>().PatrolExe();
    }
}

public class ASAttackState:BaseState
{
    public ASAttackState(GameObject g)
    {
        OwnObject = g;
    }

    public override void enter()
    {
        OwnObject.GetComponent<AttackSoldierControl>().AttackMove();
    }

    public override void execute()
    {
        OwnObject.GetComponent<AttackSoldierControl>().AttackExe();
    }
}