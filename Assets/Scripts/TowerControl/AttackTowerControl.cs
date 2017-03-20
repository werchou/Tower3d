using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTowerControl : MonoBehaviour {

    public GameObject weapon;

    public float healthy;

    //状态列表
    private Dictionary<StateType, BaseState> States;

    private BaseState currentState;

    //攻击半径
    public float attackRadius;

    public string enemyTag;

    public float attackSpeed;

    private GameObject enemyObject;

    private float tick;

    private ObjectType Type = ObjectType.AttackTower;

    private float moveTick;

    private float moveOffset;
    // Use this for initialization
    void Start () {
        moveOffset = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        enemyObject = AttackLogic.AttackNearest(transform.position, attackRadius, enemyTag);
        if (enemyObject != null)
        {
            Fire(enemyObject.transform.position);
        }
        randMove();
    }

    //开火
    public void Fire(Vector3 dest)
    {
        tick -= Time.deltaTime;
        if (tick < 0)
        {
            GetComponent<AudioSource>().Play();
            Vector3 target = new Vector3(transform.position.x, 3f, transform.position.z);
            GameObject bullte = Instantiate(weapon, target, Quaternion.identity) as GameObject;
            bullte.GetComponent<WeaponControl>().setTag = enemyTag;
            Vector3 posOffset = new Vector3(dest.x, 0.5f, dest.z);
            iTween.MoveTo(bullte, iTween.Hash("speed", bullte.GetComponent<WeaponControl>().speed, "position", posOffset));
            tick = attackSpeed;
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
