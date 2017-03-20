using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ArmyControl {

    //private GameObject gameobject;

    //军队的数据
    public GameObject soldierObject;

    public int soldierCount;

    //路径
    public List<Vector3> paths;

    public float radius;

    public float startTime;

	// Use this for initialization
	public void Init () {

        GameObject EnemyControl = GameObject.FindGameObjectWithTag("EnemyControl");
	    for(int i = 0; i < soldierCount; i++)
        {
            
            float randX = EnemyControl.transform.position.x + Random.Range(-radius, radius);
            float randZ = EnemyControl.transform.position.z + Random.Range(-radius, radius);
            Vector3 randPos = new Vector3(randX, 0, randZ);
            GameObject temp = Object.Instantiate(soldierObject, randPos, Quaternion.identity) as GameObject;
            temp.name = "soldier" + i;
            temp.transform.parent = EnemyControl.transform;
            temp.GetComponent<AttackSoldierControl>().setPath = paths;
        }
        //iTween.MoveTo(gameobject, iTween.Hash("path", iTweenPath.GetPath(armyPath), "speed", soldierObject.GetComponent<AttackSoldierControl>().speed));
	}

}
