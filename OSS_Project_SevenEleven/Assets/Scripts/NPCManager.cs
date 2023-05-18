using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")]
    public bool NPCmove;

    public string[] direction; //NPC�� ������ ����

    [Range(1, 5)] [Tooltip("1(�ſ� õõ��) ~ 5(�ſ� ������)")]
    public int frequency; // ������ �������� �����̴� ��
    //

}
public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if(npc.NPCmove)
        {
            SetMove();
        }

    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                //��Ÿ�� ���� ����
                yield return new WaitUntil(() => queue.Count < 2); //ť�� ��� 0�� 1���̷� ���� ��Ŵ
                //ť�� 3�� ��� ��� ������� �߻�

                base.Move(npc.direction[i], npc.frequency);

                if(i == npc.direction.Length - 1) //���� �ݺ��� ����
                {
                    i = -1;
                }
            }
        }
    }
}