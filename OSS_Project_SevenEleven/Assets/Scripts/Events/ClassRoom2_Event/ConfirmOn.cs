using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmOn : MonoBehaviour
{

    public checkVisit checkV;
    public string gameOver;
    private AudioManager theAudio;
    BGMManager BGM;
    public string beep;
    public bool correct = false;

    private bool isPlayerOn = false;                        // �ڽ� �ݶ��̴��� �÷��̾ �����ִ��� Ȯ���ϴ� ����


    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        BGM = FindObjectOfType<BGMManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (isPlayerOn)
            {
                theAudio.Play(beep);
                if (correct)
                {
                    checkV.confirmvisitnum++;
                    this.gameObject.SetActive(false);
                }
                else
                {
                    BGM.Stop();
                    SceneManager.LoadScene(gameOver); // transferMapName���� �̵�
                }
            }

        }
    }

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
}
