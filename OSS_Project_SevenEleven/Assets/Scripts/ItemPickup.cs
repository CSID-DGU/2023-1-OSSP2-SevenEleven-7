using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID; // �����ͺ��̽��� ItemID�� ���Ѵ�. ��ġ�ϴ� ��� �κ��丮�� �߰���
    public int _count;
    public string pickUpSound; // ������ ȹ��� ����

    public bool isPick = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z)) // ZŰ ������ ������ ȹ��
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� ȹ���� ������ �߰��ϴ� ����
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            isPick = true;
        }
    }
}
