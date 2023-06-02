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
    private ParticleSystem theParticle;   //��ƼŬ ������Ʈ ����
    BGMManager BGM;

    public int PlayMusicTrack;

    public bool ghostcanMove = true;

    public bool ghostdeath = false;             //�ͽ� ���� ���� true�� �ٲٸ� �״� �ִϸ��̼� ����
    public bool ghost_stop_corutine = false;     //�ͽ� ������ �ڷ�ƾ ����� ����

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

        if (Input.GetKeyDown(KeyCode.K))              // KŰ������ �ͽ����� �׽�Ʈ��!!!
                    ghostdeath = true;                //�׽��ÿ� �浹�� ���� ������Ʈ 
    }

    IEnumerator GhostCoroutine()
    {
        if(ghostdeath)                               //�ͽ��� ���� �ÿ� ghostdeath�� true�� �ٲ�� �����ϸ� �״¸�ǽ���
        {
            if(!ghost_stop_corutine)
                Invoke("playDeath",3f);                         // Invoke�Լ��� ���� ���ִϸ��̼ǳ����� �ͽ��״¾ִϸ��̼�
            ghost_stop_corutine = true;
            yield break;
        }


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

    void playDeath()
    {
            stopBGM();
            BoxCollider2D boxCollider =GetComponent<BoxCollider2D>();
            boxCollider.enabled= false;                             //�ͽ������� ��� ���� boxcollider off (�浹����)
            theParticle =FindObjectOfType<ParticleSystem>();
            Vector2 particle_postion = transform.position;
            theParticle.transform.position = particle_postion;      //��ƼŬ �������� �ͽ� ���������� �ű�� ���
            theParticle.Play();
            animator.SetBool("Death", true);                        //�״� �ִϸ��̼� ���
            
    }
}