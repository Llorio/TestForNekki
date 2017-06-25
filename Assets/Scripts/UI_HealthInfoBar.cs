using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_HealthInfoBar : MonoBehaviour {
    public static float normalDist = 10f;
    [SerializeField]public PoolObject _poolObject;

    [SerializeField]private Image _healthBar;
    [SerializeField]private Gradient _healthColorGradient;

    [SerializeField]private ActorBase _parentActor;

    public void Init(ActorBase _parentActor, Transform _parentCanvas) {
        this._parentActor = _parentActor;
        _parentActor.onActorGetDamage += updateData;
        transform.SetParent(_parentCanvas);
        updateData(_parentActor.healthPoints);
    }   

    void Update () {
        if (Vector3.Angle(Camera.main.transform.forward, _parentActor.transform.position - Camera.main.transform.position) < 180f)
        {
            gameObject.SetActive(true);
            transform.position = Camera.main.WorldToScreenPoint(_parentActor.transform.position);
            float distanse = Vector3.Distance(Camera.main.transform.position, _parentActor.transform.position);
            transform.localScale = Vector3.one * (normalDist / distanse);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void updateData(float _currHealth) {

        _healthBar.fillAmount = (float)_parentActor.healthPoints/ _parentActor.healthPoints_max;
        _healthBar.color = _healthColorGradient.Evaluate(_parentActor.healthPoints / _parentActor.healthPoints_max);

    }
}
