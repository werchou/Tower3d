using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

    public float attack;

    public float speed;

    private string targetTag;
    public string setTag
    {
        set
        {
            targetTag = value;
        }
    }
	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == targetTag)
        {
            switch (other.gameObject.tag)
            {
                case "Human": other.gameObject.GetComponent<AttackSoldierControl>().healthy -= attack;break;
                case "Alien": other.gameObject.GetComponent<TowerSoldierControl>().healthy -= attack; break;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}
