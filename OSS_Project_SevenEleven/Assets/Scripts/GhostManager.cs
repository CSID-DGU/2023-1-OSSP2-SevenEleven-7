using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostManager : MovingObject
{
    public Transform target; // 추격 대상(플레이어)
    public string Ghost_walkSound;
    public string gameOver;  //게임오버씬

    private Rigidbody2D rb;

    private AudioManager audioManager;
    private PlayerManager thePlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerManager>();
        StartCoroutine(GhostCoroutine());
    }

    private void Update()
    {
    }

    IEnumerator GhostCoroutine()
    {
        while (true)
        {
            // 추격 대상의 위치 가져오기
            Vector2 targetPosition = target.position;

            // 추격자와 추격 대상 사이의 방향 계산
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize(); // 방향 벡터 정규화

            //정규환 된 벡터를 통해 vector값 결정 (1,0,0) or (0,1,0)
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x의 벡터값이 더 크다면 x의 벡터값만 단위화
            {
                vector.x = Mathf.Sign(direction.x);
                vector.y = 0;
            }
            else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                vector.x = 0;
                vector.y = Mathf.Sign(direction.y);
            }

            //Animation
            animator.SetFloat("DirX", vector.x); // x벡터 값을 전달해서 animation을 실행시킴
            animator.SetFloat("DirY", vector.y); // y벡터 값을 전달해서 animation을 실행시킴
            animator.SetBool("Walking", true);



            while (currentWalkCount < walkCount)
            {

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x와의 거리가 더 크다면
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = gameOver; // 만약 이동 영역과 부딪힌다면 이동할 맵의 이름을 Player오브젝트로 넘겨줌
            SceneManager.LoadScene(gameOver); // transferMapName으로 이동
        }
    }
}