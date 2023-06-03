using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private DatabaseManager theDatabase;
    private OrderManager theOrder;
    private AudioManager theAudio;
    private OkOrCancel theOOC;
    private DialogueManager theDialogueManager;
    private Menu theMenu;

    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound; // �κ��丮 ų�� �鸮�� ����
    public string beep_sound; // ���� ���� �ൿ�� ������ �����Ҹ� ����ֵ��� (ex. ����Ʈ ������ ����)

    private InventorySlot[] slots; // �κ��丮 ���Ե�

    public List<Item> inventoryItemList; // �÷��̾ ������ ������ ����Ʈ.
    private List<Item> inventoryTabList; // ������ �ǿ� ���� �ٸ��� ������ ������ ����Ʈ.

    public Text Description_Text; // �ο� ����.
    public string[] tabDescription; // �� �ο� ����.

    public Transform tf; // slot �θ�ü.

    public GameObject go; // �κ��丮 Ȱ��ȭ ��Ȱ��ȭ.
    public GameObject[] selectedTabImages;
    public GameObject go_OOC; // ������ Ȱ��ȭ ��Ȱ��ȭ
    public GameObject prefab_Floating_Text; // �÷��� �ؽ�Ʈ

    public GameObject menu_obj; // �޴������ ������Ʈ

    private int selectedItem; // ���õ� ������.
    private int selectedTab; // ���õ� ��

    private int page; 
    private int slotCount; // Ȱ��ȭ�� ���԰���
    private const int MAX_SLOTS_COUNT = 10; // �ִ뽽�԰���


    public bool activated; // �κ��丮 Ȱ��ȭ�� true.
    private bool activated_Menu; // �κ��丮&�޴�UI ����ȭ�뺯��.

    private bool tabActivated; // �� Ȱ��ȭ�� true.
    private bool itemActivated; // ������ Ȱ��ȭ�� true.
    private bool stopKeyInput; // Ű�Է� ���� (�Һ��� �� ���ǰ� ���� �ٵ�, �� �� Ű�Է� ����)
    private bool preventExec; // �ߺ����� ����.

    private GameObject diaryNote; //�ϱ��� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        theOOC = FindObjectOfType<OkOrCancel>();
        theDialogueManager = FindObjectOfType<DialogueManager>();
        theMenu = FindObjectOfType<Menu>();

        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public List<Item> SaveItem()
    {
        return inventoryItemList;
    }
    public void LoadItem(List<Item> _itemList)
    {
        inventoryItemList = _itemList;
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < theDatabase.itemList.Count; i++) // �����ͺ��̽� ������ �˻�
        {
            if (_itemID == theDatabase.itemList[i].itemID) // �����ͺ��̽��� ������ �߰�
            {
                //var clone = Instantiate(prefab_Floating_Text, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
                //clone.GetComponent<FloatingText>().text.text = theDatabase.itemList[i].itemName + " " + _count + "�� ȹ�� +";
                //clone.transform.SetParent(this.transform);
                
                inventoryItemList.Add(theDatabase.itemList[i]); // ����ǰ�� �ش� �������� ���� ��� ����ǰ�� �ش� ������ �߰�
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("�����ͺ��̽��� �ش� ID���� ���� �������� �������� �ʽ��ϴ�."); // �����ͺ��̽��� ItemID ����.
    }
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // �κ��丮 ���� �ʱ�ȭ

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } // �� Ȱ��ȭ
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    } // ���õ� ���� �����ϰ� �ٸ� ��� ���� �÷� ���İ� 0���� ����.
    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }// ���õ� �� ��¦�� ȿ��

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;
        page = 0;

        if (selectedTab == 0) // ����Ʈ �������� ������ ���
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                    inventoryTabList.Add(inventoryItemList[i]);
            }
        }   //�װ��� �κ��丮 �� ����Ʈ�� �߰�



        ShowPage();

        SelectedItem();
    }// ������ Ȱ��ȭ (inventoryTabList�� ���ǿ� �´� �����۵鸸 �־��ְ�, �κ��丮 ���Կ� ���)
    
    public void ShowPage()
    {
        slotCount = -1;
        for (int i = page*MAX_SLOTS_COUNT; i < inventoryTabList.Count; i++)
        {
            slotCount = i - (page * MAX_SLOTS_COUNT);
            slots[slotCount].gameObject.SetActive(true);
            slots[slotCount].Additem(inventoryTabList[i]);

            if (slotCount == MAX_SLOTS_COUNT-1)
                break;

        } // �κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        if (slotCount> -1)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i <= slotCount; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "Nothing in my pocket...";
    }// ���õ� �������� �����ϰ�, �ٸ� ��� ���� �÷� ���İ��� 0���� ����.
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }// ���õ� ������ ��¦�� ȿ��.

    // Update is called once per frame
    void Update()
    {
        if (theDialogueManager.talking == false)
        {
            if (!stopKeyInput)
            {
                if ((Input.GetKeyDown(KeyCode.I) && theMenu.activated == false) || activated_Menu) //  IŰ�� �����ų� �޴�UI�����κ��丮��ư�� ������ �κ��丮 â Ȱ��ȭ
                {
                    activated = !activated;
                    activated_Menu = false;
                    theMenu.activated = false;
                    if (activated)
                    {
                        theAudio.Play(open_sound);
                        theOrder.NotMove();
                        go.SetActive(true);
                        selectedTab = 0;
                        tabActivated = true;
                        itemActivated = false;
                        ShowTab();
                    }
                    else
                    {
                        theAudio.Play(cancel_sound);
                        StopAllCoroutines();
                        go.SetActive(false);
                        tabActivated = false;
                        itemActivated = false;
                        theOrder.Move();
                    }
                }

                if (activated)
                {
                    if (tabActivated)
                    {
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedTab < selectedTabImages.Length - 1)
                                selectedTab++;
                            else
                                selectedTab = 0;
                            theAudio.Play(key_sound);
                            SelectedTab();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedTab > 0)
                                selectedTab--;
                            else
                                selectedTab = selectedTabImages.Length - 1;
                            theAudio.Play(key_sound);
                            SelectedTab();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z))
                        {
                            theAudio.Play(enter_sound);
                            Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                            color.a = 0.25f;
                            selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                            itemActivated = true;
                            tabActivated = false;
                            preventExec = true;
                            ShowItem();
                        }
                    }// �� Ȱ��ȭ�� Ű�Է� ó��.

                    else if (itemActivated)
                    {
                        if (inventoryTabList.Count > 0)
                        {
                            if (Input.GetKeyDown(KeyCode.DownArrow))
                            {
                                if (selectedItem + 2 > slotCount)
                                {
                                    if (page < (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT)
                                        page++;
                                    else
                                        page = 0;
                                    RemoveSlot();
                                    ShowPage();
                                    selectedItem = -2;
                                }

                                if (selectedItem < slotCount - 1)
                                    selectedItem += 2;
                                else
                                    selectedItem %= 2;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                            {
                                if (selectedItem - 2 < 0)
                                {
                                    if (page != 0)
                                        page--;
                                    else
                                        page = (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT;
                                    RemoveSlot();
                                    ShowPage();
                                }

                                if (selectedItem > 1)
                                    selectedItem -= 2;
                                else
                                    selectedItem = slotCount - selectedItem;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                            {
                                if (selectedItem + 1 > slotCount)
                                {
                                    if (page < (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT)
                                        page++;
                                    else
                                        page = 0;
                                    RemoveSlot();
                                    ShowPage();
                                    selectedItem = -1;
                                }

                                if (selectedItem < slotCount)
                                    selectedItem++;
                                else
                                    selectedItem = 0;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                            {
                                if (selectedItem - 1 < 0)
                                {
                                    if (page != 0)
                                        page--;
                                    else
                                        page = (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT;
                                    RemoveSlot();
                                    ShowPage();
                                }

                                if (selectedItem > 0)
                                    selectedItem--;
                                else
                                    selectedItem = slotCount;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                            {
                                if (selectedTab == 0) // �Ҹ�ǰ
                                {
                                    theAudio.Play(enter_sound);
                                    stopKeyInput = true;
                                    StartCoroutine(OOCCoroutine());
                                }
                                else if (selectedTab == 1)
                                {
                                    // ��� ����
                                }
                                else // ����Ʈ, ��Ÿ�� ��� ������ ���
                                {
                                    theAudio.Play(beep_sound);
                                }
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            theAudio.Play(cancel_sound);
                            StopAllCoroutines();
                            itemActivated = false;
                            tabActivated = true;
                            ShowTab();
                        }
                    }// ������ Ȱ��ȭ�� Ű�Է� ó��.

                    if (Input.GetKeyUp(KeyCode.Z)) // �ߺ� ���� ����.
                        preventExec = false;
                }
            }
        }
        if (CheckItemsInRange()) //����1~4 ���� �������, ���������� ȹ��
        {
            inventoryItemList.RemoveAll(item => item.itemID >= 10001 && item.itemID <= 10004);
            GetAnItem(10005, 1);
        }
    }


    public void OnclickFromMenu()  //�޴� UI���� �κ��丮UI ���ٿ� ����(�κ��丮��ư��Ŭ���̶�����)
    {
        if(go.activeSelf==false)
            activated_Menu= true;
    }

    public void Close_Menu()   // �κ��丮 UI ���� �޴� UI �ݱ�(�κ��丮��ư��Ŭ���̶�����)
    {
        menu_obj.SetActive(false);
    }


    IEnumerator OOCCoroutine()
    {
        go_OOC.SetActive(true);
        theOOC.ShowTwoChoice("Use", "Cancel");
        yield return new WaitUntil(() => !theOOC.activated);
        if (theOOC.GetResult())
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    //theDatabase.UseItem(inventoryItemList[i].itemID); -> �Ҹ�ǰ�� ���� ��쿡�� �־��� (������ ���ర���� �����Ƿ� �н�)

                    if (inventoryItemList[i].itemID == 10029) // ������ ����ϸ� Ű�� ȹ���ϰ�, ������ �����
                    {
                        GetAnItem(10027, 1);
                        inventoryItemList.RemoveAt(i);
                    }
                    else if (10016 <= inventoryItemList[i].itemID && inventoryItemList[i].itemID <= 10025)
                    {
                        string diaryname = (inventoryItemList[i].itemID-10).ToString();
                        diaryNote = GameObject.Find(diaryname);
                        diaryNote.SetActive(true);
                        theOrder.NotMove();


                        if (Input.GetKey(KeyCode.Z))
                        {
                            diaryNote.SetActive(false);
                            theOrder.Move();
                        }

                    }

                    else // �׿��� ���� ����ص� �ƹ��� ��ȭ x
                    {
                        theAudio.Play(cancel_sound);
                    }

                    //�������� ���� �Ҹ�ǰ�� ��� use�ϸ� ī��Ʈ�� ���ҽ�Ű���� ���� (������ ����)
                    /*else if (inventoryItemList[i].itemCount > 1) 
                        inventoryItemList[i].itemCount--;   */ 

                    ShowItem();
                    break;
                }
            }
        }

        stopKeyInput = false;
        go_OOC.SetActive(false);
    }

    public bool CheckItemsInRange() //�ʿ��� �������� �� ��Ҵ��� üũ�ϴ� �Լ� (���⼱ ������ ����)
    {
        int[] itemCodes = new int[] { 10001, 10002, 10003, 10004 };
        bool[] foundItems = new bool[4];

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            for (int j = 0; j < itemCodes.Length; j++)
            {
                if (inventoryItemList[i].itemID == itemCodes[j])
                {
                    foundItems[j] = true;
                    break;
                }
            }
        }

        foreach (bool found in foundItems)
        {
            if (!found) return false;
        }

        return true;
    }

}
