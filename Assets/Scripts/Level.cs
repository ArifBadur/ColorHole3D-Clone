using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    #region Singleton class: Level

    public static Level Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] ParticleSystem winFx;

    //All objects in levels
    [HideInInspector] public int objectsInScene;

    [HideInInspector] public int totalFirstObjects;
    [HideInInspector] public int totalFirstObjectsCount;

    [HideInInspector] public int totalSecondObjects;
    [HideInInspector] public int totalSecondObjectsCount;
    
    //Level count
    [HideInInspector] public int levelCount;

    //Level start
    [HideInInspector] public bool gameStart = false;


    [Space]
    //Object definition for different levels
    public LevelBot[] levelBot;
    int lbCount;

    [Space]
    //All levels
    public GameObject[] levelGroup;


    void Start()
    {
        // PlayerPrefs.DeleteAll();

        //Current level
        levelCount = PlayerPrefs.GetInt("level");
        if (levelCount == 0)
        {
            lbCount = 0;
            levelGroup[0].SetActive(true);
        }
        if (levelCount == 1)
        {
            lbCount = 1;
            levelGroup[1].SetActive(true);
        }
        if (levelCount == 2)
        {
            lbCount = 2;
            levelGroup[2].SetActive(true);
        }

        gameStart = true;
        
        if (gameStart)
        {
            CountObjects();
        }
    }

    void CountObjects()
    {
        //Level childObject count calculation
        for (int i = 0; i < levelBot[lbCount].FOGroup.Length; i++)
        {
            totalFirstObjects += levelBot[lbCount].FOGroup[i].childCount;
        }

        for (int i = 0; i < levelBot[lbCount].SOGroup.Length; i++)
        {
            totalSecondObjects += levelBot[lbCount].SOGroup[i].childCount;
        }

        //Total object count
        totalFirstObjectsCount = totalFirstObjects;
        totalSecondObjectsCount = totalSecondObjects;
        objectsInScene = totalFirstObjects + totalSecondObjects;
    }

    public void PlayWinFx()
    {
        winFx.Play();
    }

    public void LoadNextLevel()
    {
        if (levelCount == 2)
        {
            levelCount = 0;
            PlayerPrefs.SetInt("level", levelCount);
            SceneManager.LoadScene(0);
        }
        else
        {
            levelCount++;
            PlayerPrefs.SetInt("level", levelCount);
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
