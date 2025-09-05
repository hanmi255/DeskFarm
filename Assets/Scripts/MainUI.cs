using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public int money { get; set; }
    public int chickenNum { get; set; } = 1;
    public int chickNum { get; set; } = 1;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI animalsText;
    [SerializeField] private GameObject chicken;
    [SerializeField] private GameObject chick;
    [SerializeField] private GameObject storeUI;
    [SerializeField] private GameObject exitUI;
    [SerializeField] private GameObject newSaveUI;
    [SerializeField] private Transform animals;
    [SerializeField] private GameObject mainScene;
    [SerializeField] private List<GameObject> hiddenObjects;
    public RectTransform goldIcon;

    private float saveTimer;

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        moneyText.text = money.ToString();
        animalsText.text = (chickenNum + chickNum).ToString();
    }

    private void FixedUpdate()
    {
        saveTimer += Time.fixedDeltaTime;
        if (saveTimer >= 30)
        {
            SaveData();
            saveTimer = 0;
        }
    }

    public void ClickOpenStore()
    {
        storeUI.SetActive(true);
    }

    public void ClickCloseStore()
    {
        storeUI.SetActive(false);
    }

    public void ClickExit()
    {
        exitUI.SetActive(true);
    }

    public void ClickNewSave()
    {
        newSaveUI.SetActive(true);
    }

    public void ClickHidden()
    {
        if (mainScene.activeSelf)
        {
            foreach (GameObject hiddenObject in hiddenObjects)
            {
                hiddenObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject hiddenObject in hiddenObjects)
            {
                hiddenObject.SetActive(true);
            }
        }
    }

    public void ClickMinimize()
    {
        if (mainScene.transform.localScale.x < 1)
        {
            mainScene.transform.localScale = Vector3.one;
        }
        else
        {
            mainScene.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void ClickBuyChicken()
    {
        if (money >= 100)
        {
            money -= 100;
            GameObject newChicken = Instantiate(chicken, animals);
            float spawnX = Random.Range(-6f, -3f);
            newChicken.transform.localPosition = new Vector3(spawnX, 0, 0);
            chickenNum++;
        }
    }

    public void ClickBuyChick()
    {
        if (money >= 50)
        {
            money -= 50;
            GameObject newChick = Instantiate(chick, animals);
            float spawnX = Random.Range(-6f, -3f);
            newChick.transform.localPosition = new Vector3(spawnX, 0, 0);
            chickNum++;
        }
    }

    public void ClickQuitCancel()
    {
        exitUI.SetActive(false);
    }

    public void ClickQuitConfirm()
    {
        SaveData();
        Application.Quit();
    }

    public void ClickNewSaveCancel()
    {
        newSaveUI.SetActive(false);
    }

    public void ClickNewSaveConfirm()
    {
        newSaveUI.SetActive(false);
        NewSave();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("chickenNum", chickenNum);
        PlayerPrefs.SetInt("chickNum", chickNum);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        money = PlayerPrefs.GetInt("money", 0);
        chickenNum = PlayerPrefs.GetInt("chickenNum", 1);
        chickNum = PlayerPrefs.GetInt("chickNum", 1);

        if (chickenNum > 1)
        {
            for (int i = 1; i < chickenNum; i++)
            {
                GameObject newChicken = Instantiate(chicken, animals);
                float spawnX = Random.Range(-6f, -3f);
                newChicken.transform.localPosition = new Vector3(spawnX, 0, 0);
            }
        }

        if (chickNum > 1)
        {
            for (int i = 1; i < chickNum; i++)
            {
                GameObject newChick = Instantiate(chick, animals);
                float spawnX = Random.Range(-6f, -3f);
                newChick.transform.localPosition = new Vector3(spawnX, 0, 0);
            }
        }
    }

    public void NewSave()
    {
        money = 0;
        chickenNum = 1;
        chickNum = 1;

        foreach (Transform child in animals)
        {
            Destroy(child.gameObject);
        }

        GameObject newChicken = Instantiate(chicken, animals);
        newChicken.transform.localPosition = new Vector3(-4.5f, 0, 0);
        GameObject newChick = Instantiate(chick, animals);
        newChick.transform.localPosition = new Vector3(-3.5f, 0, 0);
        SaveData();
    }
}
