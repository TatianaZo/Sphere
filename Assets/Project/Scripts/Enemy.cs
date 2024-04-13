using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public Action<Enemy> beenKilled;

    public void Kill()
    {
        gameObject.SetActive(false);
        beenKilled?.Invoke(this);
    }    
}
