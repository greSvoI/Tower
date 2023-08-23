using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDestroy
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private GameObject backGround;
	    [SerializeField] private GameObject progressBar;

		[Header("Loading On Start UP")]
		[SerializeField] private GameObject Loading;
		[SerializeField] private Image imageLoading;

		[Space(2)]
		[Header("Win and Over Game UI")]
		[SerializeField] private GameObject Game_WO;
		[SerializeField] private TextMeshProUGUI bestResult_WO;
		[SerializeField] private TextMeshProUGUI currentResult_WO;

		[Space(2)]
		[Header("Select Game UI")]
		[SerializeField] private GameObject SelectGame;
		[SerializeField] private TextMeshProUGUI bestScore_SG;
		[SerializeField] private TextMeshProUGUI bestTime_SG;

		[Space(2)]		
		[SerializeField] private GameObject buttonPressPlay;
		[SerializeField] private GameObject buttonPressRestart;


		[Space(2)]
		[Header("Game controller")]
		[SerializeField] private GameObject GameUI;
		[SerializeField] private GameObject MenuUI;
		[SerializeField] private BallController ballController;
		[SerializeField] private TowerController towerController;
		[SerializeField] private TextMeshProUGUI currentScore;
		[SerializeField] private TextMeshProUGUI currentTime;
		[SerializeField] private int _partPoints  = 10;
		[SerializeField] private int _partBlockPoints = 100;
		[SerializeField] private bool _updateX2Ball = false;
		[SerializeField] private int _factorBall = 1;
		public int FactorBall { get => _factorBall; }

		//Vibration trigger
		private bool _vibration = true;

		//Select Game
		private bool _isUnlim = true;

		//Best Result Tower
		private float _bestTime;
		private int _bestScore;

		//Best Result Unlim Tower
		private int _bestUnlimScore;
		private float _bestUnlimTime;

		//Current result
		private int _currentScore;
		private float _currentTime = 0;

		AsyncOperation asyncSceneLoad;
		private void Start()
		{
			Time.timeScale = 1.0f;
			if (SceneManager.GetActiveScene().name == "Menu") 
				StartCoroutine("AsyncLoadScene", PlayerPrefs.GetString("current_scene"));

			LoadScore();

			EventManager.EventUpdateX2Ball += OnUpdateX2;
			EventManager.EventGameOver += OnGameOver;
			EventManager.EventWinGame += OnWinGame;
			EventManager.EventPartDestroyed += OnPartDestroyed;
			EventManager.EventBlockPartDestroyed += OnBlockPartDestroyed;

		}
		private void Update()
		{
			_currentTime += Time.deltaTime;
			currentScore.text = "Score: " + _currentScore;
			currentTime.text = "Time: " + TimeSpan.FromMinutes(_currentTime).ToString().Substring(0,5);
		}
		#region EventManager 

		private void OnUpdateX2(bool obj)
		{
			if (!_updateX2Ball && obj)
			{
				_updateX2Ball = true;
				_factorBall = 2;
			}
			else if (_updateX2Ball && obj)
			{
				_factorBall = 4;
			}
			else if (!obj)
			{
				_updateX2Ball = false;
				_factorBall = 1;
			}
		}
		private void OnBlockPartDestroyed()
		{
			_currentScore += _partBlockPoints * _factorBall;
			Vibration();
		}
		private void OnPartDestroyed()
		{
			_currentScore += _partPoints * _factorBall;
			Vibration();
		}
		private void OnWinGame()
		{
			Time.timeScale = 0f;
			GameUI.SetActive(false);
			Game_WO.SetActive(true);

			float time = _currentTime;

			float currentResult = _currentScore / time;
			float bestResult;
			if (_bestScore != 0 && _bestTime != 0)
			{
				bestResult = _bestScore / _bestTime;
			}
			else bestResult = 0;

			bestResult_WO.text = _bestScore.ToString() + " in " + TimeSpan.FromMinutes(_bestTime).ToString().Substring(0, 5) + " sec";
			currentResult_WO.text = _currentScore.ToString() + " in " + TimeSpan.FromMinutes(time).ToString().Substring(0, 5) + " sec";

			if (currentResult > bestResult)
			{
				PlayerPrefs.SetFloat("_bestTime", time);
				PlayerPrefs.SetInt("_bestScore", _currentScore);
			}
		}
		private void OnGameOver()
		{
			Time.timeScale = 0f;
			GameUI.SetActive(false);
			Game_WO.SetActive(true);

			float time = (float)Math.Round(_currentTime, 2);

			if(_isUnlim)
			{

				float currentResult = _currentScore / time;
				float bestResult;

				if (_bestUnlimScore != 0 && _bestUnlimTime != 0)
				{
					bestResult = _bestUnlimScore / _bestUnlimTime;
				}
				else bestResult = 0;

				bestResult_WO.text = _bestUnlimScore.ToString() + " in " + TimeSpan.FromMinutes(_bestUnlimTime).ToString().Substring(0, 5) + " sec";
				currentResult_WO.text = _currentScore.ToString() + " in " + TimeSpan.FromMinutes(time).ToString().Substring(0, 5) + " sec";

				if (currentResult > bestResult)
				{
					PlayerPrefs.SetFloat("_bestUnlimTime", time);
					PlayerPrefs.SetInt("_bestUnlimScore", _currentScore);
				}
			}
			else
			{
				bestResult_WO.text = _bestScore.ToString() + " in " +  _bestTime.ToString() + " sec";
				currentResult_WO.text =_currentScore.ToString() + " in " + time.ToString() + " sec";
			}

		}
		#endregion
		#region PressButton UI 

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
			_isUnlim = isUnlim;
			Time.timeScale = 1f;
			int isTower = _isUnlim == true ? 1 : 0;

			if (PlayerPrefs.GetInt("_isTower") <= isTower)
				PlayerPrefs.SetInt("_isTower", isTower);
			else
				PlayerPrefs.SetInt("_isTower", isTower);

			SceneManager.LoadScene(1);
		}
		public void PressButtonMenu()
		{

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

		#endregion

		private void Vibration()
		{
			if (_vibration)
			{
				Handheld.Vibrate();
			}
		}
		private void LoadScore()
		{
			//Unlim Tower
			if (PlayerPrefs.GetInt("_bestUnlimScore") <= _bestUnlimScore)
				PlayerPrefs.GetInt("_bestUnlimScore", _bestUnlimScore);

			_bestUnlimScore = PlayerPrefs.GetInt("_bestUnlimScore");

			if (PlayerPrefs.GetFloat("_bestUnlimTime") <= _bestUnlimTime)
				PlayerPrefs.SetFloat("_bestUnlimTime", _bestUnlimTime);

			_bestUnlimTime = PlayerPrefs.GetFloat("_bestUnlimTime");



			//Tower
			if (PlayerPrefs.GetFloat("_bestTime") <= _bestTime)
				PlayerPrefs.SetFloat("_bestTime", _bestTime);

			_bestTime = PlayerPrefs.GetFloat("_bestTime");

			if (PlayerPrefs.GetInt("_bestScore") <= _bestScore)
				PlayerPrefs.SetFloat("_bestScore", _bestScore);

			_bestScore = PlayerPrefs.GetInt("_bestScore");



			bestScore_SG.text = _bestUnlimScore.ToString() +" in "+_bestUnlimTime.ToString() + "sec";
			bestTime_SG.text = _bestTime.ToString() + "sec";
			
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

		private void OnDestroy()
		{
			EventManager.EventGameOver -= OnGameOver;
			EventManager.EventWinGame -= OnWinGame;
			EventManager.EventPartDestroyed -= OnPartDestroyed;
			EventManager.EventBlockPartDestroyed -= OnBlockPartDestroyed;
			EventManager.EventUpdateX2Ball -= OnUpdateX2;
		}

	}
}
