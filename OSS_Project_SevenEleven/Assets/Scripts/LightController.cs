using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerManager thePlayer; //�÷��̾ �ٶ󺸰� �ִ� ����
     private Vector2 vector;

    private Quaternion rotation;//ȸ��(����)�� ����ϴ� Vector4 xyzw

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
    }
}
