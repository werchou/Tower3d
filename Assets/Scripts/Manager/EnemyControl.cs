using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyControl : MonoBehaviour
{

    public ArmyControl[] ArmyList;

    public bool loopArmy;

    private List<float> tick=new List<float>();

    private int tickIndex = 0;

    

    void Start () {

        
        if (ArmyList.Length > 0)
        {
            
            for(int i =0;i< ArmyList.Length; i++)
            {
                tick.Add(ArmyList[i].startTime);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        UpdateArmy();
    }

    //更新军队
    void UpdateArmy()
    {
        float delta = Time.deltaTime;

        //这一波军队还没有全部生成
        if (tickIndex < tick.Count)
        {
            for (int i = tickIndex; i < tick.Count; i++)
            {
                tick[i] -= delta;
                if (tick[i] < 0)
                {
                    ArmyList[i].Init();
                    tickIndex++;
                }
            }
        }

        //如果这一波军队全部生成，检查是否应该循环
        else
        {
            if (loopArmy)
            {
                //修改军队的生成时间
                LoopArmys();

                //重置当前更新的下标 
                tickIndex = 0;

            }
        }

    }
    
    //循环生成军队
    void LoopArmys()
    {
        if (ArmyList.Length > 0)
        {

            for (int i = 0; i < ArmyList.Length; i++)
            {
                //下一波循环，每个军队的出发时间都要累加上当前最后一波军队的出发时间  
                //相当于重置了军队的出发时间
                ArmyList[i].startTime += tick[i];

                tick[i] = ArmyList[i].startTime;
            }
        }
    }

}
