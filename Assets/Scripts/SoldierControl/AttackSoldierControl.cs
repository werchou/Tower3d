using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackSoldierControl:MonoBehaviour{

    #region public
    //生命
    public float healthy;

    public float speed;

    //攻击半径
    public float attackRadius;

    public string enemyTag;

    public GameObject weapon;

    public float attackSpeed;

    public ObjectType type;

    public bool IsAttack;
    
    //到达玩家老家造成的伤害
    public int harm;

    public List<Vector3> setPath
    {
        set
        {
            points = new List<Vector3>(value);
        }
    }
    #endregion

    #region private
    //状态列表
    private Dictionary<StateType, BaseState> States;

    private BaseState currentState;

    private UnityEngine.AI.NavMeshAgent agent;

    private  List<Vector3> points;

    private int destPoint=0;

    private GameObject enemyObject;

    private bool isAttack = false;

    private float tick;

    #endregion

    // Use this for initialization
    void Start () {

        agent =GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
        agent.autoBraking = false;
        
        GotoNextPoint();

        tick = attackSpeed;
        
        //设置初试状态为巡逻
        States = new Dictionary<StateType, BaseState>();
        ASPatrolState patrols = new ASPatrolState(gameObject);
        States.Add(StateType.ASPatrol, patrols);
        currentState = patrols;
        currentState.enter();

    }

    void GotoNextPoint()
    {
        if (points.Count == 0)
            return;
        //如果到达目的地
        if (destPoint == points.Count)
        {
            GameKernel.ChangeLife(-harm);
            Destroy(gameObject);
            return;
        }
        agent.destination = points[destPoint];
        destPoint++;
    }

    // Update is called once per frame
    void Update () {

        currentState.execute();

        if (healthy <= 0)
        {
            GameKernel.MoneySys.Sell(type);
            Destroy(gameObject);
        }
    }

    //进入攻击状态
    public void AttackMove()
    {
        
    }

    public void AttackExe()
    {
        //若当前敌人不为空
        if (enemyObject != null)
        {
            float dist = Vector3.Distance(enemyObject.transform.position, transform.position);
            //如果敌人在攻击范围内
            if (dist < attackRadius)
            {
                Fire(enemyObject.transform.position);
            }
            //如果敌人出了攻击半径，重置敌人对象，寻找下一个最近的敌人（如果有的话）
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
                ChangeState(StateType.ASPatrol);
            }

        }
    }

    //巡逻状态下的行为逻辑：如果接近路径点，更新
    //搜寻攻击半径内的敌人，若发现有敌人，停止前进，攻击。
    public void PatrolExe()
    {
        if (agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        
        //检查攻击范围内的敌人
        enemyObject = AttackLogic.AttackNearest(transform.position, attackRadius, enemyTag);

        //如果有敌人，更新状态
        if (enemyObject != null)
        {
            if (IsAttack)
            {
                ChangeState(StateType.ASAttack);
            }
           
        }
    }

    //开火
    void Fire(Vector3 dest)
    {
        tick -= Time.deltaTime;
        if (tick < 0)
        {
            GameObject bullte = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
            bullte.GetComponent<WeaponControl>().setTag = enemyTag;
            iTween.MoveTo(bullte, dest,0.5f);
            tick = attackSpeed;
        }
    }
    
    //修改状态
    void ChangeState(StateType t)
    {
        if (!States.ContainsKey(t))
        {
            //创建新的状态并加载到状态列表中
            BaseState newState = StateFactory.CreatState(t, gameObject);
            States.Add(t, newState);
        }
        //修改当前状态
        currentState = States[t];
        currentState.enter();
    }

    //反转路径，具体是：destPoint-2是刚刚路过的上一个路径点，所以需要将path的[0,destPoint-2]部分反转
    public void rePath()
    {
        int prePoint = destPoint - 2;
        List<Vector3> reversePath = new List<Vector3>();
        //反转
        for(int i = prePoint; i >-1; i--)
        {
            reversePath.Add(points[i]);
        }

        //伤害反转
        harm = -harm;

        points.Clear();
        destPoint = 0;
        points = reversePath;
        GotoNextPoint();
        
    }
}
