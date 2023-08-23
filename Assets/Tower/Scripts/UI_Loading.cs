using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDestroy
{
	public class UI_Loading : MonoBehaviour
	{
		[SerializeField] private GameObject backGround;
	    [SerializeField] private GameObject progressBar;

		[Header("Loading On Start UP")]
		[SerializeField] private GameObject Loading;
		[SerializeField] private Image imageLoading;

		[Space(2)]
		[Header("Win Game UI")]
		[SerializeField] private GameObject WinGame_WG;
		[SerializeField] private TextMeshProUGUI bestResult_WG;
		[SerializeField] private TextMeshProUGUI currentResult_WG;

		[Space(2)]
		[Header("Select Game UI")]
		[SerializeField] private GameObject SelectGame;
		[SerializeField] private TextMeshProUGUI bestScore_SG;
		[SerializeField] private TextMeshProUGUI bestTime_SG;

		[Space(2)]
		
		[SerializeField] private GameObject buttonPressPlay;
		[SerializeField] private GameObject buttonPressRestart;



		[SerializeField] private GameObject GameUI;
		[SerializeField] private GameObject MenuUI;

		[SerializeField] private TowerController towerController;

		[SerializeField] private Slider musicVolume;

		private bool _vibration = true;
		private float _bestScore;
		private float _bestTime;

		AsyncOperation asyncSceneLoad;
		private void Start()
		{

			if (SceneManager.GetActiveScene().name == "Menu") 
				StartCoroutine("AsyncLoadScene", PlayerPrefs.GetString("current_scene"));

			LoadScore();

			EventManager.EventGameOver += OnGameOver;
			EventManager.EventWinGame += OnWinGame;
			EventManager.EventPartDestroyed += OnDestroyPlatform;
		}
		private void LoadScore()
		{

			if (PlayerPrefs.GetFloat("_bestScore") <= _bestScore)
				PlayerPrefs.SetFloat("_bestScore", _bestScore);

			_bestScore = PlayerPrefs.GetFloat("_bestScore");

			if (PlayerPrefs.GetFloat("_bestTime") <= _bestTime)
				PlayerPrefs.SetFloat("_bestTime", _bestTime);

			_bestTime = PlayerPrefs.GetFloat("_bestTime");

			bestScore_SG.text = _bestScore.ToString();
			bestTime_SG.text = _bestTime.ToString() + " sec";
			
		}
		private void OnWinGame()
		{
			GameUI.SetActive(false);
			WinGame_WG.SetActive(true);
			float time = towerController.TimeGame;
			if (time < _bestTime)
			{
				PlayerPrefs.SetFloat("_bestTime", time);
			}
			bestResult_WG.text = _bestTime.ToString() + " sec";
			currentResult_WG.text = time.ToString() + " sec";
		}

		//Android Vibration Trigger
		private void OnDestroyPlatform()
		{
			if(_vibration)
			{
				//Handheld.Vibrate();
			}
		}


		//
		private void OnGameOver(float score)
		{
			WinGame_WG.SetActive(true);
		}

		private IEnumerator AsyncLoadScene()
		{
			float progress;
			asyncSceneLoad = SceneManager.LoadSceneAsync(1);
			asyncSceneLoad.allowSceneActivation = false;
			while (asyncSceneLoad.progress < 0.9f)
			{
				progress = Mathf.Clamp01(asyncSceneLoad.progress / 0.9f);

				imageLoading.fillAmount = progress;
				yield return null;
			}
			imageLoading.fillAmount = 1f;

			Loading.SetActive(false);
			SelectGame.SetActive(true);		
		}
		public void PressButtonMainMenu()
		{
			SceneManager.LoadScene(0);
		}
		public void TriggerVibration()
		{
			_vibration = !_vibration;
		}
		public void PressButtonRestart()
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(1);
		}
		public void PressButtonStart(bool isUnlim)
		{
			Time.timeScale = 1f;
			int isTower = isUnlim == true ? 1 : 0;

			if (PlayerPrefs.GetInt("_isTower") <= isTower)
				PlayerPrefs.SetInt("_isTower", isTower);
			else
				PlayerPrefs.SetInt("_isTower", isTower);

			SceneManager.LoadScene(1);
		}
		public void PressButtonMenu() {

			Time.timeScale = 0f;
			GameUI.SetActive(false);
			MenuUI.SetActive(true);
		}
		public void PressButtonResume()
		{
			GameUI.SetActive(true);
			MenuUI.SetActive(false);
			Time.timeScale = 1f;
		}
		public void PressButtonQuit()
		{
			Application.Quit();
		}
		private void OnDestroy()
		{
			EventManager.EventGameOver -= OnGameOver;
			EventManager.EventWinGame -= OnWinGame;
			EventManager.EventPartDestroyed -= OnDestroyPlatform;
		}

	}
}
