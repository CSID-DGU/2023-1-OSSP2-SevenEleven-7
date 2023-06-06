using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public List<int> playerItemInventory;
        public List<int> playerItemInventoryCount;

        public string mapName;
        public string sceneName;

        //public GameObject[] checkVisits;

        //public List<bool> swList;
        //public List<string> swNameList;
        //public List<string> varNameList;
        //public List<float> varNumberList;


        //check visit
        public List<bool> isCheckVisit;
        public List<int> confirmCheckVisit;
    }

    private DatabaseManager theDatabase;
    private PlayerManager thePlayer;
    private Inventory theInven;
    public GameObject load_canvas_obj;
    public Data data;

    //check visit
    public GameObject[] theCheckVisit;


    private Vector3 vector;

    public List<string> item_id__should_destroy;
    public int item_count;
  
    public void CallSave(int k)
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theInven = FindObjectOfType<Inventory>();

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        //check visit
        theCheckVisit = GameObject.FindGameObjectsWithTag("checkVisit");

        for (int i = 0; i < data.confirmCheckVisit.Count; i++)
        {
            data.confirmCheckVisit[i] = theCheckVisit[i].GetComponent<checkVisit>().confirmvisitnum;
        }

        foreach (GameObject cv in theCheckVisit)
        {
            if (cv.activeSelf)
            {
                data.isCheckVisit.Add(true);
            }
            else
            {
                data.isCheckVisit.Add(false); 
            }
        }



        data.mapName = thePlayer.currentMapName;
        data.sceneName = thePlayer.currentSceneName;

        Debug.Log("기초 데이터 성공");

        data.playerItemInventory.Clear();
        data.playerItemInventoryCount.Clear();

        /*
        for (int i = 0; i < theDatabase.var_name.Length; i++)
        {
            data.varNameList.Add(theDatabase.var_name[i]);

            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switch_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switch_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }
        */
        List<Item> itemList = theInven.SaveItem();

        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log("인벤토리의 아이템 저장 완료 : " + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile" + k + ".dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallLoad(int k)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile" + k + ".dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DatabaseManager>();
            thePlayer = FindObjectOfType<PlayerManager>();
            theInven = FindObjectOfType<Inventory>();

            thePlayer.currentMapName = data.mapName;
            thePlayer.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            //check visit
            theCheckVisit = GameObject.FindGameObjectsWithTag("checkVisit");

            for (int i = 0; i < theCheckVisit.Length; i++)
            {
                theCheckVisit[i].GetComponent<checkVisit>().confirmvisitnum = data.confirmCheckVisit[i];
                theCheckVisit[i].SetActive(data.isCheckVisit[i]);
            }



            //theDatabase.var = data.varNumberList.ToArray();
            //theDatabase.var_name = data.varNameList.ToArray();
            //theDatabase.switches = data.swList.ToArray();
            //theDatabase.switch_name = data.swNameList.ToArray();

            List<Item> itemList = new List<Item>();

            for (int i = 0; i < data.playerItemInventory.Count; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        Debug.Log("인벤토리 아이템을 로드했습니다 : " + theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for (int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);

            item_count = 0;
            for (int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                item_id__should_destroy.Add((data.playerItemInventory[i]).ToString());
                item_count++;
            }

            // 카메라 바운드로 설정해야할 BoxCollider가 다른씬에 있다면, 불러올 수 가 없다.
            // 그래서 씬이동이 이루어지고, 그 씬에 붙어있는 맵의 바운드를 참조해야한다!.
            GameManager theGM = FindObjectOfType<GameManager>();
            theGM.LoadStart();

            SceneManager.LoadScene(data.sceneName);
            load_canvas_obj = GameObject.Find("Load_UI");
            if(load_canvas_obj != null )
                load_canvas_obj.SetActive(false);           //death씬에서 넘어올때 객체못찾는거 방지용
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        file.Close();
    }
}
