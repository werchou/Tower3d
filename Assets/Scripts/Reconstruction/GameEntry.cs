using UnityEngine;
using System.Collections;

public class GameEntry  {

    public string prefabPath;
    public float healthy;
    public GameObject gameObject;

    public virtual void Init() { }
    public virtual void Update() { }
}
