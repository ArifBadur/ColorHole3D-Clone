using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	#region Singleton class: UIManager

	public static UIManager Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	[Header ("Level Progress UI")]
	[SerializeField] Text nextLevelText;
	[SerializeField] Text currentLevelText;
	[SerializeField] Image firstProgressFillImage;
	[SerializeField] Image secondProgressFillImage;

	[Space]
	[Header("Half Level")]
	[SerializeField] GameObject holeObject;
	[SerializeField] Transform board;
	[HideInInspector]
	public bool halfLevel = false;

	[Space]
	[SerializeField] public Animator levelanim;

	[Space]
	[Header("Win")]
	[SerializeField] public GameObject completed;

	[Space]
	[Header("Money UI")]
	public Text totalMoney;
	[HideInInspector] public int money;

	void Start ()
	{
		Level.Instance.levelCount = PlayerPrefs.GetInt("level");

		//Money
		money = PlayerPrefs.GetInt("money");
		totalMoney.text = money.ToString();

		//fill reset
		firstProgressFillImage.fillAmount = 0f;
		secondProgressFillImage.fillAmount = 0f;

		SetLevelProgressText ();
	}

    private void Update()
    {
		if (Level.Instance.totalFirstObjectsCount == 0 && !halfLevel && Level.Instance.gameStart)
        {
			//Half level animation played
			levelanim.enabled = true;
			//Hole transfer
			holeObject.transform.SetParent(board);
			//Half Level complated
			halfLevel = true;
		}
    }

	void SetLevelProgressText ()
	{
        currentLevelText.text = (Level.Instance.levelCount + 1).ToString ();
		nextLevelText.text = (Level.Instance.levelCount + 2).ToString ();
	}

	public void UpdateLevelProgress ()
	{
		if(!halfLevel)
        {
			float val = 1f - ((float)Level.Instance.totalFirstObjectsCount / Level.Instance.totalFirstObjects);
			firstProgressFillImage.DOFillAmount(val, .4f);
		}
        else
        {
			float val = 1f - ((float)Level.Instance.totalSecondObjectsCount / Level.Instance.totalSecondObjects);
			secondProgressFillImage.DOFillAmount(val, .4f);
		}
	}
}
