using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public Dialogue dialogue_1;
    //public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private bool hasEntered = false;

    //Use this for initialization
    void Start()
    {

        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hasEntered && collision.gameObject.name == "Player")
        {
            StartCoroutine(EventCoroutine());
            hasEntered = true;
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        /*theOrder.Move("Player", "UP"); //�����̵�
        theOrder.Move("Player", "UP"); 
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");*/
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        //theDM.ShowDialogue(dialogue_2);
        //yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

    }
}
