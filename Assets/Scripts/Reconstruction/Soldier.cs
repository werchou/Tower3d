using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier :GameEntry {

    public float attack;
    public float speed;
    public float attackRadius;

    protected List<BaseState> States;
    protected BaseState currentState;
    //忽略的攻击图层索引
    private int IgnoreLayer;

    public virtual void PatrolAction() { }
}
