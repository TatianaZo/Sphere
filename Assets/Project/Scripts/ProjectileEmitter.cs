using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{    
    Stack<Projectile> stack = new Stack<Projectile>();
    [SerializeField] private Projectile projectilePrefab;
    Projectile current;

    private void Start()
    {
        
    }

    public void AimHorizontal(float delta)
    {
        transform.eulerAngles = new Vector3(0, delta/50, 0);        
    }

    private Projectile ChooseBullet()
    {
        Projectile bullet;
        if(stack.Count < 1)
        {
            bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);            
        }
        else
        {
            bullet = stack.Pop();
        }
        return bullet;
    }

    public void Prepare()
    {
        var bullet = ChooseBullet();
        if(bullet != null)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody>().isKinematic = true;
        }
        current = bullet;
    }

    public void Fire(Vector3 force)
    {
        var bullet = current;

        if(bullet != null)
        {     
            var rigid = bullet.GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            rigid.velocity = force;
            bullet.onHit += RemoveBullet;
        }
    }

    private void RemoveBullet(Projectile bullet)
    {
        bullet.onHit -= RemoveBullet;
        bullet.gameObject.SetActive(false);
        stack.Push(bullet);
    }
}
