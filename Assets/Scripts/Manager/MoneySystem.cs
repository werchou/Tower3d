using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ObjectType
{
    ArmyTower,
    AttackTower,
    MovingTower,
    AlienSoldier,
    AlienMonster,
    HumanSoldier,
    HumanSWAT,
    HumanTank
}

[System.Serializable]
public class MoneySystem
{
    //初始资金
    public int InitMoney;

    //当前资金
    private int currentMoney;

    public List<PricePair> GoodsList;
    //初始化
    public void Init()
    {
        currentMoney = InitMoney;
    }

    public int getCurrent
    {
        get
        {
            return currentMoney;
        }
    }
    public bool Buy(ObjectType t)
    {
        //如果价格列表里有该类型的商品
        PricePair buyGood = IsContain(t);
        if (buyGood!=null)
        {
            //判断余额
            if (currentMoney >= buyGood.Price)
            {
                currentMoney -= buyGood.Price;
                //Debug.Log("当前余额："+currentMoney);
                return true;
            }
            else
            {
                
                return false;
            }
        }
        return false;
    }

    public void Sell(ObjectType t)
    {
        PricePair sellGood = IsContain(t);
        //价格列表里有登记
        if (sellGood != null)
        {
            currentMoney += sellGood.Price/2;
            //Debug.Log("now:"+ currentMoney);
        }
    }

    PricePair IsContain(ObjectType t)
    {
        foreach(PricePair good in GoodsList)
        {
            if (good.GoodType == t)
            {
                return good;
            }
        }
        return null;
    }
}
