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
		[SerializeField] private Image imageLoading;
		[SerializeField] private GameObject textLoading;
		
		[SerializeField] private TextMeshProUGUI textHighScore;
		[SerializeField] private GameObject buttonPressPlay;
		[SerializeField] private GameObject buttonPressRestart;
		[SerializeField] private GameObject showGameUI;
		[SerializeField] private GameObject showMenuUI;

		[SerializeField] private Slider musicVolume;

		private bool _vibration = true;
		private float _highScore;

		AsyncOperation asyncSceneLoad;
		private void Start()
		{
			if (SceneManager.GetActiveScene().name == "Menu") 
				StartCoroutine("AsyncLoadScene", PlayerPrefs.GetString("current_scene"));

			if (PlayerPrefs.GetFloat("_highTime") <= _highScore)
				PlayerPrefs.SetFloat("_highTime", _highScore);

			_highScore = PlayerPrefs.GetFloat("_highTime");

			textHighScore.text = $"HighTime : " + _highScore.ToString();
			EventManager.EventGameOver += OnGameOver;
			EventManager.EventWinGame += OnGameOver;
			EventManager.EventDestroyPlatform += OnDestroyPlatform;
		}

		private void OnDestroyPlatform()
		{
			if(_vibration)
			{
				Handheld.Vibrate();
			}
		}

		private void OnGameOver(float score)
		{
			if (score < _highScore)
				PlayerPrefs.SetFloat("_highTime", score);
			buttonPressRestart.SetActive(true);
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

			textLoading.SetActive(false);
			buttonPressPlay.SetActive(true);
		}
		public void TriggerVibration()
		{
			_vibration = !_vibration;
		}
		public void PressButtonRestart()
		{
			SceneManager.LoadScene(0);
		}
		public void PressButtonStart()
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(1);
		}
		public void PressButtonMenu() {

			Time.timeScale = 0f;
			showGameUI.SetActive(false);
			showMenuUI.SetActive(true);
		}
		public void PressButtonResume()
		{
			showGameUI.SetActive(true);
			showMenuUI.SetActive(false);
			Time.timeScale = 1f;
		}
		public void PressButtonQuit()
		{
			Application.Quit();
		}
		private void OnDestroy()
		{
			EventManager.EventGameOver -= OnGameOver;
			EventManager.EventWinGame -= OnGameOver;
		}

	}
}
