using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//军队成员的控制脚本
public class TowerSoldierControl : MonoBehaviour {
    
    public float healthy;
    
    public float speed;

    public float attack;
    //状态列表
    private Dictionary<StateType,BaseState> States;

    private BaseState currentState;

    //攻击半径
    public float attackRadius;

    //攻速
    public float attackSpeed;

    //巡逻半径和中心
    private float patrolRadius;
    private Vector3 patrolCenter;

    public  string enemyTag;

    private UnityEngine.AI.NavMeshAgent agent;

    private GameObject enemyObject;

    private float tick;

    //激光生成器
    private LineRenderer laser;

	// Use this for initialization
	void Start () {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
        agent.autoBraking = false;

        laser = GetComponent<LineRenderer>();
        laser.SetWidth(0.1f,0.1f);
        laser.enabled = false;
        laser.SetColors(Color.blue, Color.blue);

        //初始化巡逻半径和中心
        patrolCenter = this.transform.parent.gameObject.transform.position;
        patrolRadius = this.transform.parent.gameObject.GetComponent<UFOArmyControl>().Radius;

        //设置初试状态为巡逻
        States = new Dictionary<StateType, BaseState>();
        TSPatrolState patrols = new TSPatrolState(gameObject);
        States.Add(StateType.TSPatrol,patrols);
        currentState = patrols;
        currentState.enter();

        tick = attackSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        currentState.execute();
        if (healthy == 0)
        {
            Destroy(gameObject);
        }

        
	}


    //巡逻状态下，检查巡逻范围内是否有敌人
    public void PatrolExe()
    {
        Vector3 pos = AttackLogic.IsSafe(patrolCenter, patrolRadius,enemyTag);
        //如果有敌人
        if (pos!= patrolCenter)
        {
            ChangeState(StateType.TSAttack);
        }
    }


    //巡逻状态下的逻辑行为
    public void PatrolMove()
    {
        RandomTranslate();
    }
    
    private void RandomTranslate()
    {
        //生成随机位置
        float randX = patrolCenter.x + Random.Range(-patrolRadius/2, patrolRadius/2);
        float randZ = patrolCenter.z + Random.Range(-patrolRadius/2, patrolRadius/2);
        Vector3 randPos = new Vector3(randX, transform.position.y, randZ);
        agent.destination = randPos;
    }

    //进入攻击状态的逻辑
    public void AttackMove()
    {
        //agent.destination = enemyObject.transform.position;
    }

    //攻击状态下的执行逻辑
    public void AttackExe()
    {
        //若当前敌人不为空
        if (enemyObject != null)
        {
            float dist = Vector3.Distance(enemyObject.transform.position, patrolCenter);
            //如果敌人在巡逻半径内
            if (dist < patrolRadius)
            {
                //更新当前位置，跟踪敌人
                //后期修改：添加了偏移，不会进入到敌人身体中
                agent.destination = enemyObject.transform.position+new Vector3(1,0,1);
                
                Fire(enemyObject.transform.position);
            }
            //如果敌人出了巡逻半径，重置敌人对象，寻找下一个最近的敌人（如果有的话）
            else
            {
                enemyObject = null;
            }

        }
        else
        {
            enemyObject = AttackLogic.AttackNearest(transform.position, attackRadius, enemyTag);

            //攻击半径内找不到敌人了，转为巡逻状态
            if (enemyObject == null)
            {
                ChangeState(StateType.TSPatrol);
            }
        }
    }


    //开火
    public void Fire( Vector3 dest)
    {

        tick -= Time.deltaTime;
        if (tick < 0)
        {
            GetComponent<AudioSource>().Play();
            laser.enabled = true;
            laser.SetVertexCount(2);
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, dest);
            enemyObject.GetComponent<AttackSoldierControl>().healthy -= attack;
            tick = attackSpeed;
        }
        else
        {
            laser.enabled = false;
        }

    }

    //修改状态
    void ChangeState(StateType t)
    {
        if (!States.ContainsKey(t))
        {
            //创建新的状态并加载到状态列表中
            BaseState newState = StateFactory.CreatState(t,gameObject);
            States.Add(t, newState);
        }
        //修改当前状态
        currentState = States[t];
        currentState.enter();
    }
}
