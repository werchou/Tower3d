using UnityEngine;
using System.Collections;

public class InputSystem  {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public  void Update () {
        MouseClickListen();
    }


    //响应鼠标点击事件
    void MouseClickListen()
    {

        //如果按了左键
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null )
                {
                    if (hit.collider.tag == "TowerFactory")
                    {
                        TowerFactory tf= hit.collider.gameObject.GetComponent<TowerFactory>();
                        if (!tf.editIsActive)
                        {
                            if (GameKernel.MoneySys.Buy(ObjectType.ArmyTower))
                            {
                                tf.CreateTower(0, ObjectType.ArmyTower);
                            }
                        }
                    }

                }
            }
        }

        //右键
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if(hit.collider.tag == "TowerFactory")
                    {
                        TowerFactory tf = hit.collider.gameObject.GetComponent<TowerFactory>();
                        //如果基地没激活，开启建造模式
                        if (!tf.editIsActive)
                        {
                            if (GameKernel.MoneySys.Buy(ObjectType.AttackTower))
                            {
                                tf.CreateTower(1, ObjectType.AttackTower);
                            }
                        }
                    }


                    //拆塔
                    if (hit.collider.tag == "AlienTower")
                    {
                        TowerFactory tf = new TowerFactory();
                        tf= hit.collider.transform.parent.GetComponent<TowerFactory>();
                        tf.editIsActive = false;
                        GameKernel.MoneySys.Sell(tf.getcurrentType);
                        Object.Destroy(hit.collider.gameObject);
                    }

                }
            }
        }
    }

}
