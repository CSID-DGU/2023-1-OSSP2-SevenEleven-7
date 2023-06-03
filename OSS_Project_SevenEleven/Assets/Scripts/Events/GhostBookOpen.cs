using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBookOpen : MonoBehaviour
{



    public OrderManager theOrder;
    public GameObject displayImage;
    private Animator theAnimator;
    public string ghostscream;
    private AudioManager theAudio;


    void Start()
    {
        
        theAudio = FindObjectOfType<AudioManager>();
        theAnimator = GetComponent<Animator>();

    }
    /*public GameObject read_or_exit_UI;
    public bool activated=false;               //UI Ȱ��ȭ��Ȱ��ȭ ����

    public void Resume_UI()
    {
        theOrder.Move();
        read_or_exit_UI.SetActive(false);
        activated = false;
    }

    public void Read_Book()
    {
        theOrder.Move();
        read_or_exit_UI.SetActive(false);
        activated = false;
    }*/                                         //������ ������

    private void ShowItemImage()                            //������ �󼼻��� �÷��� 
    {
        theOrder.NotMove();
        displayImage.SetActive(true);
    }

    private void HideItemImage()                            //������ �󼼻��� ���� 
    {
        displayImage.SetActive(false);
        this.gameObject.SetActive(false);
        theOrder.Move();
    }
    private void play_scream()                            //������ �󼼻��� ���� 
    {
        theAudio.Play("ghostscream");

    }
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isPlayerOn)         //XŰ ������ playeron�� true activated�� false��
            {
                theOrder.NotMove();
                ShowItemImage();
                BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                boxCollider.enabled = false;                             
                theAnimator.SetBool("Read", true);
                this.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                this.GetComponent<SpriteRenderer>().sortingOrder = 101;
                Invoke("play_scream", 1.4f);
                Invoke("HideItemImage", 3.1f);
            }
        }

    }
}
