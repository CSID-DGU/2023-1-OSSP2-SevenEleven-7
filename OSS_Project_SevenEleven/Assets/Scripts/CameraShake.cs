using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private Camera theCamera;

    public AudioManager theAudio;
    private FadeManager theFade;
    public string crash_sound;


    [SerializeField]  float shake_strength = 20f;      
    [SerializeField]  float shake_duration = 2f;        //ȭ����鸲����,���ӽð� ����Ƽ������ ���������ϰ� serialfield
    [SerializeField]  float fall_second = 2f;
    private void OnTriggerEnter2D(Collider2D collision)                                
    {
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        theCamera.GetComponent<CameraManager>().enabled = false;            //ī�޶�Ŵ����� ��� ī�޶� ��ġ�����ؼ� off
        
        onShakeCamera(shake_strength, shake_duration);
                                                                             
        //theCamera.GetComponent<CameraManager>().enabled = true;             //�׽��ÿ� ī�޶� �ٽõ��ƿ���
    }

    public void onShakeCamera(float shake_strength, float shake_duraiton)
    {
        this.shake_duration = shake_duraiton;
        this.shake_strength= shake_strength;
        StartCoroutine("delay");                        
    }

    private IEnumerator delay()
    {
        yield return StartCoroutine("ShakeByPosition");
        yield return StartCoroutine("Elevator_Down");               //�ڷ�ƾ ����ȣ��

    }

    private IEnumerator ShakeByPosition()                                   //ī�޶� ��鸲
    {
        
        Vector3 startPosition= theCamera.transform.position;

       
        while(shake_duration>0.0f)
        {

            theCamera.transform.position= startPosition + Random.insideUnitSphere*shake_strength;   //�������� 1�α��� ����xyz * ��鸲������ŭ ��ġ ��ȭ

            shake_duration -= Time.deltaTime;     

            yield return null;

        }
        theCamera.transform.position= startPosition;
    }


    private IEnumerator Elevator_Down()                                 //ī�޶������̵�(�����������߶�)
    {
        Vector3 startPosition = theCamera.transform.position;
        Vector3 endPosition = theCamera.transform.position+ Vector3.up * 450;           //ī�޶� ��ǥ��ġ y��+300     


        while (fall_second > 0.0f)
        {

            theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, endPosition, 5f);      //5f�ӵ���ŭ ��ǥ��ġ�� ī�޶��̵�

            fall_second -= Time.deltaTime;

            yield return null;

        }
        if (fall_second < 0)
        {
            theFade = FindObjectOfType<FadeManager>();
            theFade.Fadeout();
        }
    }
}
