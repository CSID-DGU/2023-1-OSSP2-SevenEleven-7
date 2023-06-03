using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID; // �����ͺ��̽��� ItemID�� ���Ѵ�. ��ġ�ϴ� ��� �κ��丮�� �߰���
    public int _count;
    public string pickUpSound; // ������ ȹ��� ����
    private OrderManager theOrder;
 
    public bool isPick = false;
    public GameObject displayImage;

    /*private void OnTriggerStay2D(Collider2D collision)
    {   
        if (Input.GetKeyDown(KeyCode.X)) // ZŰ ������ ������ ȹ��
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� ȹ���� ������ �߰��ϴ� ����
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            isPick = true;
        }
    }*/

    private bool isPlayerOn = false;                        // �ڽ� �ݶ��̴��� �÷��̾ �����ִ��� Ȯ���ϴ� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //�÷��̾ �ڽ� �ݶ��̴� ���� ������
            isPlayerOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //�÷��̾ �ڽ� �ݶ��̴� ���� ������
            isPlayerOn = false;
    }

    private void ShowItemImage()                            //������ �󼼻��� �÷��� (�ϱ��� ���� ��)
    {
        theOrder.NotMove();
        displayImage.SetActive(true);
    }

    private void HideItemImage()                            //������ �󼼻��� ���� (�ϱ��� ���� ��)
    {
        displayImage.SetActive(false);
        theOrder.Move();
    }

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.X)) 
        {
            if (isPlayerOn)                                     //XŰ ������ playeron�̸�
            {
                if (10006 <= itemID && itemID <= 10015 && displayImage != null)   //�ϱ��� ȹ���ϸ� �ڵ����� ȭ�鿡 ��������
                {
                    ShowItemImage();
                    Invoke("HideItemImage", 8);
                }

                AudioManager.instance.Play(pickUpSound);
                Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� ȹ���� ������ �߰��ϴ� ����
                                                              //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                isPick = true;
            }
        }
    }
}
