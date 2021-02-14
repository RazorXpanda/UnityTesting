using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;      // public singletone of the event system

    private void Awake()
    {
        current = this;
    }

    public Action<int, int> onDamageReceived;
    public void DamageReceived(int _id, int _damage)
    {
        if (onDamageReceived != null)
            onDamageReceived(_id, _damage);
    }
}
