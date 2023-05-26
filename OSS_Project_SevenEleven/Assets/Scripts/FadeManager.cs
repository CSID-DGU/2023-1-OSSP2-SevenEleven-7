using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{

    public SpriteRenderer white;
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void Fadeout(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeoutCorutine(_speed));
    }
    IEnumerator FadeoutCorutine(float _speed)    //���̵� �� �ڷ�ƾ  speed��ŭ ���İ�(����)����
    {
        color = black.color;

        while(color.a<1f)
        {
            color.a += _speed;
            black.color= color;
            yield return waitTime;
        }

    }

    public void FadeIn(float _speed = 0.02f)     
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCorutine(_speed));
    }
    IEnumerator FadeInCorutine(float _speed)    //���̵� �ƿ� �ڷ�ƾ  speed��ŭ ���İ�(����)����
    {
        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }

    }


    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashoutCorutine(_speed));
    }
    IEnumerator FlashoutCorutine(float _speed)    //���̵� �� �ڷ�ƾ  speed��ŭ ���İ�(����)����
    {
        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;
            yield return waitTime;
        }

    }

    public void FlashIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCorutine(_speed));
    }
    IEnumerator FlashCorutine(float _speed)    //���̵� �ƿ� �ڷ�ƾ  speed��ŭ ���İ�(����)����
    {
        color = white.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;
            yield return waitTime;
        }

    }

    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_speed));
    }
    IEnumerator FlashCoroutine(float _speed)
    {
        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;
            yield return waitTime;
        }

        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;
            yield return waitTime;
        }
    }
}
