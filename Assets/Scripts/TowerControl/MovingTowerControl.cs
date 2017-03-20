using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MovingTowerControl : MonoBehaviour {

    //移动速度
    public float speed;
    //攻击半径
    public float attackRadius;
    //敌人标签
    public string enemyTag;
    //攻击速度
    public float attackSpeed;
    //飞船持续时间
    public float duringTime;

    private GameObject enemyObject;

    private ObjectType Type = ObjectType.MovingTower;

    private float moveTick;

    private float moveOffset;

    private float tick;

    // Use this for initialization
    void Start () {
        tick = attackSpeed;
        moveOffset =0.2f;
        randMove();
    }
	
	// Update is called once per frame
	void Update () {
        //检查是否需要自我销毁
        CheckDestory();
        //改造敌人
        TransformEnemy();
        randMove();
    }

    //吸收敌人，改造
    void TransformEnemy()
    {
        tick -= Time.deltaTime;
        if (tick < 0)
        {
            //一定时间间隔，探测最近的敌人
            enemyObject = AttackLogic.AttackNearest(transform.position, attackRadius, enemyTag);
            //如果探测到敌人
            if (enemyObject != null)
            {
                //吸收敌人
                iTween.MoveTo(enemyObject, transform.position, 0.5f);
                //改变敌人的路径
                ChangeEnemy();
            }
            tick = attackSpeed;
        }
    }

    void ChangeEnemy()
    {
        enemyObject.GetComponent<AttackSoldierControl>().rePath();
    }


    void CheckDestory()
    {
        duringTime -= Time.deltaTime;
        if (duringTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void randMove()
    {
        Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + moveOffset, transform.position.z);
        moveTick -= Time.deltaTime;
        if (moveTick < 0)
        {
            iTween.MoveTo(gameObject, offsetPos, 3f);
            moveTick = 0.5f;
            moveOffset *= -1;
        }
    }

    //生成UFO，拖拽图标到指定位置生成UFO
    public void CreateUFO()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Instantiate(gameObject, new Vector3(hit.point.x, 3, hit.point.z), Quaternion.identity);
        }
    }
}
