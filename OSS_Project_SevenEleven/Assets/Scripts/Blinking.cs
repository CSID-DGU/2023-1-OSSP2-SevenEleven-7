using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public float blinkDuration = 1f; // ��¦�̴� �ֱ⸦ �����ϴ� ���� (�� ����)
    public float minTransparency = 0.3f; //�ּ� ����(0���� 1 ������ ��)
    public float maxTransparency = 1f; // �ִ� ���� (0���� 1 ������ ��)

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

// Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(BlinkingCoroutine());
    }

    IEnumerator BlinkingCoroutine()
    {
        while (true)
        {
            // ��¦�̴� �������� �ִ� �������� �̵�
            for (float t = 0; t < 1; t += Time.deltaTime / blinkDuration)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(minTransparency, maxTransparency, t);
                spriteRenderer.color = newColor;
                yield return null;
            }

            // ��¦�̴� �������� �ּ� �������� �̵�
            for (float t = 0; t < 1; t += Time.deltaTime / blinkDuration)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(maxTransparency, minTransparency, t);
                spriteRenderer.color = newColor;
                yield return null;
            }
        }
    }
}
