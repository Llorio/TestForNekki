using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType { Bullet, Enemy, Explosion, Player, UI }
public class PoolObject : MonoBehaviour {

    public string poolObjectName;
    public int poolObjectIndex;  
    public PoolObjectType poolObjectType;

    public MonoBehaviour pooledBehaviour;
    //public MonoBehaviour prefab;
   // public int reqiredCount;

}
