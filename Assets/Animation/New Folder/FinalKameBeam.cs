using UnityEngine;
using System.Collections;

public class FinalKameBeam : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1500;
    private float direction;

    public void SetDirection(float dir)
    {
        direction = dir;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
        StartCoroutine(GrowBeam());
    }


    IEnumerator GrowBeam()
    {
        float time = 0;
        Vector3 originalScale = transform.localScale;

        while (time < 0.3f)
        {
            time += Time.deltaTime;

            float scaleX = Mathf.Lerp(0.5f, originalScale.x, time / 0.3f);
            transform.localScale = new Vector3(scaleX * Mathf.Sign(originalScale.x), originalScale.y, 1);

            yield return null;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Beam hit: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected!");

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                Debug.Log("Damage dealt!");

                enemy.TakeDamage(damage);
            }
        }
    }
}