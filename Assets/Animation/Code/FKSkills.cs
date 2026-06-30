using UnityEngine;
using System.Collections;

public class FKSkills : MonoBehaviour
{
    [Header("Final Kamehameha")]
    public GameObject finalKamePrefab;
    public Transform firePoint;
    public float cooldown = 2f;

    private float lastTimeUsed;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueManager.Instance != null &&
        DialogueManager.Instance.IsTalking())
        {
            return;
        }

        // S + L để dùng skill
        //if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.L))
        if (Input.GetKeyDown(KeyCode.K))
        {
            TryUseSkill();
        }
    }

    void TryUseSkill()
    {
        if (Time.time < lastTimeUsed + cooldown) return;

        lastTimeUsed = Time.time;

        animator.SetTrigger("ChargeFK"); // vào Charge FK

    }

    public void Fire()
    {
        animator.SetTrigger("Final Kamehameha");

        GameObject beam = Instantiate(finalKamePrefab, firePoint.position, Quaternion.identity);

        float dir = transform.localScale.x > 0 ? 1f : -1f;

        beam.GetComponent<FinalKameBeam>().SetDirection(dir);
    }
}