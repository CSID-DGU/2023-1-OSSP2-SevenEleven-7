using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MovingObject
{
    public Transform target; // �߰� ���(�÷��̾�)
    public float chaseSpeed; // �߰� �ӵ�

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(GhostCoroutine());
    }

    private void Update()
    {
    }

    IEnumerator GhostCoroutine()
    {
        while (true)
        {
            // �߰� ����� ��ġ ��������
            Vector2 targetPosition = target.position;

            // �߰��ڿ� �߰� ��� ������ ���� ���
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize(); // ���� ���� ����ȭ

            yield return null;

            while (currentWalkCount < walkCount)
            {
                animator.SetBool("Walking", true);
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x���� �Ÿ��� �� ũ�ٸ�
                {
                    //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                    transform.Translate(Mathf.Sign(direction.x) * speed, 0, 0);
                    //Animation
                    animator.SetFloat("DirX", Mathf.Sign(direction.x)); // x���� ���� �����ؼ� animation�� �����Ŵ
                    animator.SetFloat("DirY", 0); // y���� ���� �����ؼ� animation�� �����Ŵ
                }
                else
                {
                    //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                    transform.Translate(0, Mathf.Sign(direction.y) * speed, 0);
                    //Animation
                    animator.SetFloat("DirX", 0); // x���� ���� �����ؼ� animation�� �����Ŵ
                    animator.SetFloat("DirY", Mathf.Sign(direction.y)); // y���� ���� �����ؼ� animation�� �����Ŵ
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            animator.SetBool("Walking", false);
        }
    }
}