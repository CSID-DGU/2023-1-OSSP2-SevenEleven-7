using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Variables

    //Static
    static public CameraManager instance; // ī�޶� ������Ʈ �ν��Ͻ� 

    //Public
    public GameObject target; // ī�޶� ���� ��� 
    public float moveSpeed; // ī�޶� �̵� �ӵ�

    public BoxCollider2D bound; // �� ũ�� ��ŭ�� Bound

    //Private
    private Vector3 targetPosition; // ����� ���� ��ġ ��

    private Vector3 minBound;
    private Vector3 maxBound; // BoxColider ������ �ּ� �ִ� ��ǩ���� ���� ����

    private float halfWidth;
    private float halfHeight; // ī�޶��� �ݳʺ�, �ݳ��� ���� ���� ����

    private Camera theCamera; // ī�޶��� ��ǥ���� ���� ����


    private void Awake() //Start���� ���� ����
    {
        //Scene ��ȯ�� ��ü �ı� ���� �ڵ�
        //��Ȱ�� ���� ī�޶� ������Ʈ�� ���� �� ��!

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize; // ī�޶��� �ݳ���
        halfWidth = halfHeight * Screen.width / Screen.height; // �ݳʺ� ���ϴ� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null) // ����� �־�� ī�޶� �̵�
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);  // 1�ʿ� moveSpeed ��ŭ ������   
            //Lerp �߰��� ���� , deltaTime : 1�ʿ� ����Ǵ� �������� ���� 

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth); 
            // value�� min �� max ���̿� �ִٸ� ����, max �ʰ��� max, min �̸� �� min ����
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);

        }
    }

    public void SetBound(BoxCollider2D newBound) 
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
