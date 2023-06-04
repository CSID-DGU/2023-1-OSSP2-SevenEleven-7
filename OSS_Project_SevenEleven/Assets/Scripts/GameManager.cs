using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bound[] bounds;
    private PlayerManager thePlayer;
    private CameraManager theCamera;

    private SaveNLoad theSave;
    public GameObject item_object_should_destroyed;
    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());
    }
    public void BoundStart()
    {
        StartCoroutine(BoundCoroutine());
    }

    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        thePlayer = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<CameraManager>();

        theCamera.target = GameObject.Find("Player");


        theSave = FindObjectOfType<SaveNLoad>();
        for (int i = 0; i < GameObject.Find("Items").transform.childCount; i++)                 //Load �� �������� ������Ʈ Ŵ
        {
            GameObject.Find("Items").transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < theSave.item_count; i++)
        {
            item_object_should_destroyed = GameObject.Find(theSave.item_id__should_destroy[i]);
            item_object_should_destroyed.SetActive(false);
        }

        /*
         ĳ���Ͱ� �ִ� ���� BoxCollider�� ī�޶� �ٿ��� �����ϱ� ���ؼ�, currentMapName�� BoundName�� ���� ���ѵ�
         ī�޶� �ٿ��� ��������.
        */
        for (int i = 0; i < bounds.Length; i++)
        {
            if (bounds[i].boundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
    }


    IEnumerator BoundCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        thePlayer = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<CameraManager>();

        theCamera.target = GameObject.Find("Player");
        

        /*
         ĳ���Ͱ� �ִ� ���� BoxCollider�� ī�޶� �ٿ��� �����ϱ� ���ؼ�, currentMapName�� BoundName�� ���� ���ѵ�
         ī�޶� �ٿ��� ��������.
        */
        for (int i = 0; i < bounds.Length; i++)
        {
            if (bounds[i].boundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
    }
}
