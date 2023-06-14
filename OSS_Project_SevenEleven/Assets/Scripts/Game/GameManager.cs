using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bound[] bounds;
    private PlayerManager thePlayer;
    private CameraManager theCamera;

    public GameObject item_object_should_destroyed;
    public bool isFirstGameStart = true;

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
