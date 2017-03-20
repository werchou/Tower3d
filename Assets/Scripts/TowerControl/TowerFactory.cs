using UnityEngine;
using System.Collections;

public class TowerFactory : MonoBehaviour {

    public GameObject[] tower;

    private bool isActive;

    public bool editIsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
        }
    }

    private GameObject currentTower;

    private ObjectType currentType;
    public ObjectType getcurrentType
    {
        get
        {
            return currentType;
        }
    }
	// Use this for initialization
	void Start () {
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //造塔
    public  void CreateTower(int i,ObjectType t)
    {
        Vector3 pos = transform.position + new Vector3(0, 2, 0);
        Quaternion r = Quaternion.identity;
        currentTower = Instantiate(tower[i], pos, r) as GameObject;
        currentTower.transform.parent = transform;
        currentType = t;
        isActive = true;
    }
}
