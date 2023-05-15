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

    protected bool npcCanMove = true; //��Ÿ�� ���� ����
    protected Vector3 vector; // x,y,z�� ���� ���ÿ� ���� Vector ����
    public Animator animator; // �ִϸ��̼� ������ ���� ����
    public BoxCollider2D boxCollider;

    public string characterName;

    public void Move(string _dir, int _frequency = 5) 
    {
         StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        npcCanMove = false;
        vector.Set(0, 0, vector.z);

        switch(_dir)
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
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {
            //�ڷ�ƾ ������ �ʱ�ȭ ���ױ� ������ ������ ���� �ڵ带 �ۼ��Ͽ��� ����
            transform.Translate(vector.x * speed, vector.y * speed, 0); //Translate ���� ��ġ ���� ()���� ���� ������, �� speed�� ����ŭ ������ 
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f); // () �ȸ�ŭ ���
            //if currentWalkCount�� walkCount�� �Ǹ� �ݺ����� �������� 
        }
        currentWalkCount = 0;

        //�ִϸ��̼� ���� ���� ����
        if (_frequency != 5) animator.SetBool("Walking", false);
        npcCanMove = true;
    }

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
