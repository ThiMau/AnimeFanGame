using UnityEngine;
using System.Collections.Generic;

public class DamageHitbox : MonoBehaviour
{
    public int damage = 1000;

    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();

    void OnEnable()
    {
        hitEnemies.Clear(); // reset mỗi lần bật
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null && !hitEnemies.Contains(enemy))
        {
            hitEnemies.Add(enemy);
            enemy.TakeDamage(damage);

            Debug.Log("DSS Hit: " + other.name);
        }
    }
}