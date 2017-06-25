/*
 Тут можно прописывать управление для различных платформ
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : ManagerBase<InputManager> {
    public bool inputAllowed = true;


    public float moveAxis
    {
        get
        {
            return inputAllowed?Input.GetAxis("Vertical"):0f;
        }
    }

    public float rotatonAxis
    {
        get
        {
            return inputAllowed?Input.GetAxis("Horizontal"):0f;
        }
    }
    public bool shooting
    {
        get
        {
            return inputAllowed&&Input.GetAxis("Fire1")==1f;
        }
    }

    public int switchWeapon
    {
        get
        {
            if (inputAllowed)
            {
#if UNITY_STANDALONE
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    return -1;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    return 1;
                }
#endif
            }
            return 0;
        }
    }
}
