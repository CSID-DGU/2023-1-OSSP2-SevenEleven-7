using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    // Variables

    //Static
    static public PlayerManager instance; //static���� ����� ������ ���� ����

    // Public

    public float runSpeed; // �޸��� �ӷ�

    public bool notMove = false;

    public string currentMapName;
    public string currentSceneName; // ĳ���Ͱ� ��� ���� �ִ��� Ȯ���ϱ� ���� �뵵.

    public string walkSound_1; // �̸����� �����ؼ� ���� �̿�
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    // Private

    private float applyRunSpeed; // ���� ���� RunSpeed
    public bool canMove = true; //�ڷ�ƾ ���� ���� ����
    private bool applyRunFlag = false;

    private AudioManager theAudio;
    private SaveNLoad theSaveNLoad;

    public GameObject current_transfer;
    public bool istransfer = false;

    public bool ghostNotMove = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>(); //ť �ʱ�ȭ
        animator = GetComponent<Animator>(); // ������Ʈ�� animator ������ �ҷ���
        boxCollider = GetComponent<BoxCollider2D>();
        theAudio = FindObjectOfType<AudioManager>();
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
    }

    IEnumerator MoveCoroutine() // ���ð��� ������� Coroutine
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove) // ���� �ڷ�ƾ �� �̵��� ��� �����ϰ� ��
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

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag) break;

            animator.SetBool("Walking", true); // Bool ���� �����ؼ� animation ����

            //AUDIO
            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;

                case 2:
                    theAudio.Play(walkSound_2);
                    break;

                case 3:
                    theAudio.Play(walkSound_3);
                    break;

                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            //�����̰��� �ϴ� �������� Boxcolider �̵�
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

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
                if (currentWalkCount == 12) boxCollider.offset = Vector2.zero;

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

    public void CallSave1()
    {
        theSaveNLoad.CallSave(1);
    }

    public void CallSave2()
    {
        theSaveNLoad.CallSave(2);
    }

    public void CallSave3()
    {
        theSaveNLoad.CallSave(3);
    }

    public void LoadSave1()
    {
        theSaveNLoad.CallLoad(1);
    }

    public void LoadSave2()
    {
        theSaveNLoad.CallLoad(2);
    }

    public void LoadSave3()
    {
        theSaveNLoad.CallLoad(3);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) // F5 Ű�� ���� ����
        {
            theSaveNLoad.CallSave(1);
        }

        if (Input.GetKeyDown(KeyCode.F9)) // F9 Ű�� ���� �ҷ�����
        {
            theSaveNLoad.CallLoad(1);
        }

        if (canMove && !notMove) //�ڷ�ƾ ���� ���� ���� �б⹮
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //����Ű�� ���� -1 �Ǵ� 1 ����
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
