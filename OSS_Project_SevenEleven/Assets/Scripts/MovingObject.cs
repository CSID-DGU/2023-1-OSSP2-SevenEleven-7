using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    // Variables

    //Static
    static public MovingObject instance; //static���� ����� ������ ���� ����

    // Public
    public float speed; // ĳ���� �̵��ӷ� ����
    public float runSpeed; // �޸��� �ӷ�

    public int walkCount;

    public LayerMask layerMask; // ��� �Ұ� ���̾� ����

    public string currentMapName; 
    //TransferMap�� �ִ� transferMapName�� ������ ����


   
    // Private
    private Vector3 vector; // x,y,z�� ���� ���ÿ� ���� Vector ����
    private float applyRunSpeed; // ���� ���� RunSpeed

    private int currentWalkCount; //���� walkCount loop�� ���������� ���� ����

    private bool canMove = true; //�ڷ�ƾ ���� ���� ����

    private bool applyRunFlag = false;

    private  Animator animator; // �ִϸ��̼� ������ ���� ����

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null) // ó�� ������ ���
        {
            //Scene ��ȯ�� ��ü �ı� ���� �ڵ�
            DontDestroyOnLoad(this.gameObject);
            animator = GetComponent<Animator>(); // ������Ʈ�� animator ������ �ҷ���
            boxCollider = GetComponent<BoxCollider2D>();
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator MoveCoroutine() // ���ð��� ������� Coroutine
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) // ���� �ڷ�ƾ �� �̵��� ��� �����ϰ� ��
        {
            //Runs
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            //Vector Set
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); //���Ͱ� ����

            if (vector.x != 0) vector.y = 0; //�¿�� �̵��Ѵٸ� y���͸� 0���� ����� �̵��� �̻����� ������ ��

            //Animation
            animator.SetFloat("DirX", vector.x); // x���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetFloat("DirY", vector.y); // y���� ���� �����ؼ� animation�� �����Ŵ

            //RayCast : A -> B�� �������� �� �ƹ��͵� ���� �ʴ´ٸ� hit == Null; else hit = ���ع�
            RaycastHit2D hit;

            Vector2 start = transform.position; // ���� ĳ���� ��ġ ��
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //�̵��Ϸ��� �ϴ� ��ġ ��

            boxCollider.enabled = false; //hit ���� �ش� ������Ʈ�� ���� �ʵ��� ��
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null) break; //�浹�Ǵ� ��ü�� �ִٸ� break


            animator.SetBool("Walking", true); // Bool ���� �����ؼ� animation ����


            //Add Value
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); //Translate ���� ��ġ ���� ()���� ���� ������, �� speed�� ����ŭ ������
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }

                if (applyRunFlag) currentWalkCount++; // Run Flag�� ������ �� cuurentWalkCount�� �ι辿 ������Ŵ

                currentWalkCount++;

                yield return new WaitForSeconds(0.01f); // () �ȸ�ŭ ���
                                                        //speed = 2.4, walkcount = 20 => 2.4 * 20 = 48 
                                                        //����Ű �ѹ����� 48�ȼ��� �����̵��� ��

                //if currentWalkCount�� 20�� �Ǹ� �ݺ����� �������� 

            }
            currentWalkCount = 0; // 0���� �ʱ�ȭ
        }
        animator.SetBool("Walking", false);
        canMove = true; //����Ű �Է��� �����ϵ��� ��
    } // ���� ó�� ��� �Լ�


    // Update is called once per frame
    void Update()
    {

        if (canMove) //�ڷ�ƾ ���� ���� ���� �б⹮
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //����Ű�� ���� -1 �Ǵ� 1 ����
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
