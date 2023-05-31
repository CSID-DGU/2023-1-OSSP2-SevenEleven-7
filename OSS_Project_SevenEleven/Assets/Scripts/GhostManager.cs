using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostManager : MovingObject
{
    public Transform target; // �߰� ���(�÷��̾�)
    public string gameOver;  //���ӿ�����
    public float lifeTime;
    public GameObject GhostPrefab;

    private Rigidbody2D rb;

    private AudioManager audioManager;
    private PlayerManager thePlayer;

    BGMManager BGM;

    public int PlayMusicTrack;

    public bool ghostcanMove = true;



    private void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerManager>();

        Destroy(GameObject.Find(GhostPrefab.name), lifeTime);

        BGM.Play(PlayMusicTrack);//������ ��� ���
        BGM.Loop();
        Invoke("stopBGM", 25); //�Ҹ�� ��� ����
        thePlayer.istransfer = false; //�� �ڵ� �̵� ���� ����
    }

    private void Update()
    {
        if (!thePlayer.notMove&&!thePlayer.ghostNotMove&&ghostcanMove)
        {
            ghostcanMove = false; //�ߺ� �ڷ�ƾ ����
            StartCoroutine(GhostCoroutine()); //�ڷ�ƾ ����
        }
    }

    IEnumerator GhostCoroutine()
    {
        if (thePlayer.isDeathPoint)
        {
            BGM.FadeOutMusic();
            Destroy(GameObject.Find(GhostPrefab.name));
            thePlayer.isDeathPoint = false;
        }

        if (thePlayer.istransfer) Invoke("warpGhost", 1f);
        thePlayer.istransfer = false;

        // �߰� ����� ��ġ ��������
        Vector2 targetPosition = target.position;


        // �߰��ڿ� �߰� ��� ������ ���� ���
        Vector2 direction = targetPosition - rb.position;
        direction.Normalize(); // ���� ���� ����ȭ

        //����ȯ �� ���͸� ���� vector�� ���� (1,0,0) or (0,1,0)
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x�� ���Ͱ��� �� ũ�ٸ� x�� ���Ͱ��� ����ȭ
        {
            vector.x = Mathf.Sign(direction.x);
            vector.y = 0;
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            vector.x = 0;
            vector.y = Mathf.Sign(direction.y);
        }

        //Animation
        animator.SetFloat("DirX", vector.x); // x���� ���� �����ؼ� animation�� �����Ŵ
        animator.SetFloat("DirY", vector.y); // y���� ���� �����ؼ� animation�� �����Ŵ
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x���� �Ÿ��� �� ũ�ٸ�
            {
                //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                transform.Translate(vector.x * speed, 0, 0);
            }
            else
            {
                //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                transform.Translate(0, vector.y * speed, 0);

            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        animator.SetBool("Walking", false);
        ghostcanMove = true;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            stopBGM();
            thePlayer.currentMapName = gameOver; // ���� �̵� ������ �ε����ٸ� �̵��� ���� �̸��� Player������Ʈ�� �Ѱ���
            SceneManager.LoadScene(gameOver); // transferMapName���� �̵�
        }

    }

    void warpGhost()
    {
        this.gameObject.transform.position = thePlayer.current_transfer.GetComponent<TransferMap>().target.transform.position;
    }

    void stopBGM()
    {
        BGM.FadeOutMusic();
        BGM.UnLoop();
    }
}