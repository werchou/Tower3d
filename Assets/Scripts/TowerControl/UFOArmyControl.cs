using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UFOArmyControl : MonoBehaviour {

    public float healthy;
    //外星人出生间隔时间
    public float waitTime;
    //外星人对象
    public GameObject alien;

    //生成的外星人数量
    public int alienCount;

    //防御半径
    public float Radius;

    //存放外星人列表
    private List<Transform> alienArmy;

    private float moveTick;

    private float moveOffset;
    //防御塔的类型
    private ObjectType Type = ObjectType.ArmyTower;

	void Start () {
        //初始生产外星人
        StartCoroutine(CreatAlien());
        moveOffset = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        randMove();

    }

    //生成外星人
    IEnumerator CreatAlien()
    {
        
        for (int i = 0; i < alienCount; i++)
        {
            Vector3 pos = transform.position - new Vector3(0, 2, -2);
            GameObject alientemp = Instantiate(alien, pos, Quaternion.identity) as GameObject;
            alientemp.name = "alien" +i;
            //alientemp.transform.position = pos;
            alientemp.transform.parent = transform;
            yield return new WaitForSeconds(waitTime);
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

}
