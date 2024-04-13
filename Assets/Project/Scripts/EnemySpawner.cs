using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private Stack<Enemy> enemies = new Stack<Enemy>();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < 3; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemies.Push(enemy);
            enemy.gameObject.SetActive(false);
        }
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            if (enemies.Count > 0)
            {
                var enemy = enemies.Pop();
                enemy.gameObject.SetActive(true);
                RespawnEnemy(enemy);
                enemy.beenKilled += Grave;
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }        
    }

    private void Grave(Enemy enemy)
    {
        enemy.beenKilled -= Grave;
        enemies.Push(enemy);
        gameManager.AddCounter();
    }

    private void RespawnEnemy(Enemy enemy)
    {

        enemy.transform.position = new Vector3(Random.Range(-1.25f, 1.25f), 0.5f, Random.Range(-4, 11));

    }
    
}
