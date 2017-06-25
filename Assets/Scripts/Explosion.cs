using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField]private PoolObject _poolObject;
    [SerializeField]private float _lifetime;

    public void Init(Vector3 _position)
    {
        transform.position = _position;
        StartCoroutine(explotionClear());
    }
    IEnumerator explotionClear()
    {
        yield return new WaitForSeconds(_lifetime);
        PoolManager.Current.setPoolObjectBack(_poolObject);
    }
}
