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
        boxCollider = GetComponent<BoxCollider2D>();
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

            //����ȯ �� ���͸� ���� vector�� ���� (1,0,0) or (0,1,0)
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                vector.x = Mathf.Sign(direction.x);
                vector.y = 0;
            }
            else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                vector.x = 0;
                vector.y = Mathf.Sign(direction.y);
            }

            //Animation
            animator.SetFloat("DirX", vector.x); // x���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetFloat("DirY", vector.y); // y���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetBool("Walking", true);
            while (currentWalkCount < walkCount)
            {

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x���� �Ÿ��� �� ũ�ٸ�
                {
                    //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                    transform.Translate(vector.x * speed, 0, 0);
                }
                else
                {
                    //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                    transform.Translate(0, vector.y * speed, 0);

                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            animator.SetBool("Walking", false);
        }
    }
}