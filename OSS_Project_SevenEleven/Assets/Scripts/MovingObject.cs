using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //Variablels

    public float speed; // ĳ���� �̵��ӷ� ���� 
    public int walkCount;
    protected int currentWalkCount; //���� walkCount loop�� ���������� ���� ����
    public LayerMask layerMask; // ��� �Ұ� ���̾� ����

    protected Vector3 vector; // x,y,z�� ���� ���ÿ� ���� Vector ����
    public Animator animator; // �ִϸ��̼� ������ ���� ����
    public BoxCollider2D boxCollider;
    private bool notCoroutine = false; //�ڷ�ƾ �ߺ� ���� ����

    public string characterName;
    public Queue<string> queue;

    public void Move(string _dir, int _frequency = 5)  //�����Ҷ����� ť�� ������ enqueue
    {
        queue.Enqueue(_dir);
        if(!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }

    }

    IEnumerator MoveCoroutine(string _dir, int _frequency) //�̵� �ڷ�ƾ
    {
        while(queue.Count != 0) //ť�� �������� �ݺ�
        {
            switch (_frequency) //���� ��� ������ ���� ����ġ��
            {
                case 1:
                    yield return new WaitForSeconds(4f);
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);
                    break;
                case 3:
                    yield return new WaitForSeconds(2f);
                    break;
                case 4:
                    yield return new WaitForSeconds(1f);
                    break;
                case 5:
                    break;
            }

            string direction = queue.Dequeue();     
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            //Animation
            animator.SetFloat("DirX", vector.x); // x���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetFloat("DirY", vector.y); // y���� ���� �����ؼ� animation�� �����Ŵ

            while (true) //�浹üũ
            {
                bool checkCollisionFlag = CheckCollision();
                if (checkCollisionFlag)
                {
                    animator.SetBool("Walking", false); // �̵� �ִϸ��̼� false => ��ֹ��� �ִٸ� �������� ����
                    yield return new WaitForSeconds(1f); //1�� ���
                }
                else
                {
                    break; //���� ��ֹ��� ���ٸ� break
                }
            }


            animator.SetBool("Walking", true);

            //�����̰��� �ϴ� �������� Boxcolider �̵�
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);


            while (currentWalkCount < walkCount)
            {
                //�ڷ�ƾ ������ �ʱ�ȭ ���ױ� ������ ������ ���� �ڵ带 �ۼ��Ͽ��� ����
                transform.Translate(vector.x * speed, vector.y * speed, 0); //Translate ���� ��ġ ���� ()���� ���� ������, �� speed�� ����ŭ ������ 
                currentWalkCount++;
                if (currentWalkCount == 12) boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f); // () �ȸ�ŭ ���
                                                        //if currentWalkCount�� walkCount�� �Ǹ� �ݺ����� �������� 
            }
            currentWalkCount = 0;

            //�ִϸ��̼� ���� ���� ����
            if (_frequency != 5) animator.SetBool("Walking", false);
        }
        animator.SetBool("Walking", false); //�ݺ��� ����Ǹ� �ִϸ��̼� off
        //�ִϸ��̼� ���� ���� �����
        notCoroutine = false;
    }
    
    //�浹 ���� �Լ� 
    protected bool CheckCollision()
    {
        //RayCast : A -> B�� �������� �� �ƹ��͵� ���� �ʴ´ٸ� hit == Null; else hit = ���ع�
        RaycastHit2D hit;

        Vector2 start = transform.position; // ���� ĳ���� ��ġ ��
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //�̵��Ϸ��� �ϴ� ��ġ ��

        boxCollider.enabled = false; //hit ���� �ش� ������Ʈ�� ���� �ʵ��� ��
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null) return true; //�浹�Ǵ� ��ü�� �ִٸ� break
        return false;
    }
}
