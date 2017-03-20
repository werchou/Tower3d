using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlace:GameEntry {

    public List<Tower> TowerList;

    public Tower currentTower;

    private bool isActive;

    public TowerPlace(GameObject g)
    {
        gameObject = g;
        TowerList = new List<Tower>(2);
        isActive = false;
    }

    public override void Init()
    {
        
    }
}
