using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject[] lockdoor; //��� �� ���� ������Ʈ

    private AudioManager theAudio;

    private FadeManager theFade;

    public string lockingsound; // ������ �Ҹ�
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0; i< lockdoor.Length; i++)
            {
                lockdoor[i].SetActive(true);
            }
            theFade.FadeRedIn();
            theAudio.Play(lockingsound);
            this.gameObject.SetActive(false);
        }
    }
}
