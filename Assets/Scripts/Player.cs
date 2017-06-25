public class Player : ActorBase {


	void FixedUpdate ()
    {
        _actorRigidbody.AddForce(transform.forward * InputManager.Current.moveAxis * _speed);
        _actorRigidbody.AddTorque(transform.up * InputManager.Current.rotatonAxis * _speed);

        _weapons[_currentWeapon].shooting = InputManager.Current.shooting;

        if (InputManager.Current.switchWeapon != 0) switchWeapon(InputManager.Current.switchWeapon);
    }


    public void switchWeapon(int side)
    {
        
        _currentWeapon += side;
       
        if (_currentWeapon == _weapons.Length)
        {
            _currentWeapon = 0;
        }
        else if (_currentWeapon == -1)
        {
            _currentWeapon = _weapons.Length - 1;
        }
        //Debug.Log(_currentWeapon);
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].shooting = _weapons[i].shooting&&(i == _currentWeapon);
            _weapons[i]._weaponTransform.gameObject.SetActive(i == _currentWeapon);
        }

    }
}
