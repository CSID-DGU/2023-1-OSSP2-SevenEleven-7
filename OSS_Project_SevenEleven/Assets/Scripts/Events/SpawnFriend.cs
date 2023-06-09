using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnFriend : MonoBehaviour
{
    public GameObject GhostPrefab;
    public GameObject ghostSpawner;
    public Transform SpawnPoint;
    public GameObject instance;
    public float customlifeTime;

    private PlayerManager thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        //GhostPrefab.GetComponent<GhostManager>().lifeTime = customlifeTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 spawnPoint = SpawnPoint.position;
        instance = Instantiate(GhostPrefab, spawnPoint, Quaternion.identity);
        instance.GetComponent<GhostManager>().target = thePlayer.transform;
        ghostSpawner.SetActive(false); //한번만 소환
    }
}
