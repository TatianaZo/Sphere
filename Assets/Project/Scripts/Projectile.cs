using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Action<Projectile> onHit;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IEnemy>(out var enemy))
        {
            enemy.Kill();
        }
        onHit?.Invoke(this);
    }
}
