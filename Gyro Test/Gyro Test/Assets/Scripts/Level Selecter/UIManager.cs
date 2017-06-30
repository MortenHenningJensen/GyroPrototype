using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instanceRef;

    public GameObject containerPrefab;
    public GameObject worldButtonPrefab;
    public GameObject levelButtonPrefab;
    public GameObject HUDPrefab;
    public GameObject resultsPrefab;

    private CanvasGroup[] displays;
    private GameObject[] worldButtons;
    private GameObject[] levelButtons;
    private GameObject hud;
    private GameObject results;
    private int currentDisplay;

    private GameControl control;
    private int worldIndex;
    private int levelIndex;

    public int WorldIndex
    {
        set
        {
            worldIndex = value;
            control.CurrWorld = control.allWorlds[worldIndex];
        }
        get
        {
            return worldIndex;
        }
    }
    public GameControl Control { get { return control; } }
    //private void Awake()
    //{
    //    if (instanceRef == null)
    //    {
    //        instanceRef = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        DestroyImmediate(gameObject);
    //    }
    //}

    public int LevelIndex
    {
        set
        {
            levelIndex = value;
            control.CurrLevel = control.CurrWorld.Levels[levelIndex];
        }
        get
        {
            return levelIndex;
        }
    }

    // Use this for initialization
    void Start()
    {
        control = FindObjectOfType<GameControl>();
        currentDisplay = 0;
        InitDisplays();

    }

    void InitDisplays()
    {
        displays = new CanvasGroup[4];

        worldButtons = InitializeItems(worldButtonPrefab, "WORLDS", GameControl.NumWorlds, 180, 300, 10, GameControl.NumWorlds);

        levelButtons = InitializeItems(levelButtonPrefab, "LEVELS", GameControl.NumLevels, 75, 75, 10, 5);

        //hud = InitUIElement(HUDPrefab, transform);
        //displays[2] = hud.GetComponent<CanvasGroup>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateCurrent();
        UpdateDisplayed();
    }

    void UpdateCurrent()
    {
        for (int i = 0; i < displays.Length; i++)
        {
            if (displays[i] != null)
            {
                if (i == currentDisplay)
                {
                    displays[i].alpha = 1;
                    displays[i].blocksRaycasts = true;
                }
                else
                {
                    displays[i].alpha = 0;
                    displays[i].blocksRaycasts = false;
                }
            }
        }
    }

    void UpdateDisplayed()
    {
        switch (currentDisplay)
        {
            case 0: //Worlds being displayed
                UpdateWorldButtons();
                break;
            case 1:
                UpdateLevelButtons();
                break;
            case 2:              
                if (SceneManager.GetActiveScene().name != (worldIndex + 1 + "-" + levelIndex + 1).ToString())
                {
                    control.StartGame((worldIndex + 1 + "-" + levelIndex + 1).ToString());

                }
                // }
                break;
            case 3:
                results.GetComponent<Results>().UpdateResults(GetComponent<UIManager>());
                break;

        }
    }

    void UpdateWorldButtons()
    {
        for (int i = 0; i < worldButtons.Length; i++)
        {
            Text titleText = worldButtons[i].transform.FindChild("TitleText").GetComponent<Text>();
            titleText.text = "WORLD " + (i + 1).ToString();

            Text infoText = worldButtons[i].transform.FindChild("InfoPanel").FindChild("InfoText").GetComponent<Text>();
            infoText.text = "Levels: " + control.allWorlds[i].TotalDefeated + " / " + GameControl.NumLevels;
            infoText.text += "\nStars: " + control.allWorlds[i].TotalStars + " / 60";
            infoText.text += "\nScore: " + control.allWorlds[i].TotalScore;
        }
    }

    void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Text levelText = levelButtons[i].transform.FindChild("LevelText").GetComponent<Text>();
            levelText.text = (i + 1).ToString();
            GameObject stars = levelButtons[i].transform.FindChild("Stars").gameObject;
            if (i == 0 || control.allWorlds[WorldIndex].Levels[i].Unlocked)
            {
                stars.SetActive(true);
                levelButtons[i].GetComponent<Button>().interactable = true;

                for (int j = 0; j < stars.transform.childCount; j++)
                {
                    stars.transform.GetChild(j).gameObject.SetActive(false);

                    if (control.allWorlds[WorldIndex].Levels[i].HighStarScore > j)
                    {
                        stars.transform.GetChild(j).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                stars.SetActive(false);
                levelButtons[i].GetComponent<Button>().interactable = false;

            }
        }
    }

    public void Clicked(Button b)
    {
        if (currentDisplay == 0)
        {
            currentDisplay += 1;
            WorldIndex = int.Parse(b.name);
        }
        else if (currentDisplay == 1)
        {
            currentDisplay += 1;
            levelIndex = int.Parse(b.name);
        }
    }

    GameObject[] InitializeItems(GameObject prefab, string title, int numItems, int itemWidth, int itemHeight, int padding, int cols)
    {
        GameObject[] temp = new GameObject[numItems];
        int rows = numItems / cols;
        int leftinset = padding;
        int titleSpace = 50;

        int topinset = titleSpace;

        //Set up container
        GameObject container = InitUIElement(containerPrefab, transform);
        container.name = title;
        container.GetComponentInChildren<Text>().text = title;
        RectTransform contRect = container.GetComponent<RectTransform>();
        contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (itemWidth + padding) * cols + titleSpace);
        contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (itemHeight + padding) * rows + titleSpace);

        //Attatch canvas group to container & add the container to displays[]
        container.AddComponent<CanvasGroup>();
        for (int i = 0; i < displays.Length; i++)
        {
            if (displays[i] == null)
            {
                displays[i] = container.GetComponent<CanvasGroup>();
                break;
            }
        }

        //Set up items
        int colCount = 0;
        for (int i = 0; i < numItems; i++)
        {
            GameObject item = InitUIElement(prefab, container.transform);
            item.name = i.ToString();
            RectTransform itemRect = item.GetComponent<RectTransform>();
            itemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftinset, itemWidth);
            itemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, topinset, itemHeight);

            leftinset += (itemWidth + padding);
            colCount++;
            if (colCount == cols)
            {
                colCount = 0;
                topinset += (padding + itemHeight);
                leftinset = padding;
            }

            temp[i] = item;
        }

        return temp;
    }

    GameObject InitUIElement(GameObject prefab, Transform parent)
    {
        GameObject temp = Instantiate(prefab) as GameObject;
        temp.transform.SetParent(parent);

        RectTransform tempRect = temp.GetComponent<RectTransform>();
        RectTransform prefabRect = prefab.GetComponent<RectTransform>();
        tempRect.localPosition = prefabRect.localPosition;
        tempRect.localScale = prefabRect.localScale;

        if (temp.GetComponent<Button>())
        {
            Button b = temp.GetComponent<Button>();
            b.onClick.AddListener(() => Clicked(b));
        }

        return temp;
    }
}
