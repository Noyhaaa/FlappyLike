using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class UIController : MonoBehaviour
{

    static public UIController Instance;

    public CanvasGroup MainMenu;
    public CanvasGroup Gameplay;
    public CanvasGroup GameOverMenu;

    public Text ScoreText;

    public GameObject StartGameUI;

    public GameObject RestartGame;
    public GameObject GameOverPanel;
    public Text GameOverScoreText;
    public Text GameOverHighScoreText;

    void Awake()
    {
        Instance = this;

        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameEnded += OnGameEnded;
    }

    void Start()
    {
        MainMenu.alpha = 1;
        Gameplay.alpha = 0;
        GameOverMenu.alpha = 0;

        GameOverMenu.gameObject.SetActive(false);
        Gameplay.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameEnded -= OnGameEnded;
    }

    void OnGameStarted()
    {
        MainMenu.DOFade(0, 0.2f).OnComplete(() => MainMenu.gameObject.SetActive(false));
        Gameplay.gameObject.SetActive(true);
        Gameplay.DOFade(1, 0.2f);
    }

    void OnGameEnded()
    {
        Gameplay.DOFade(0, 0.2f).OnComplete(() => Gameplay.gameObject.SetActive(false));
        GameOverMenu.gameObject.SetActive(true);
        GameOverMenu.DOFade(1, 0.2f);

        GameOverScoreText.text = ScoreManager.Instance.score.ToString();
        GameOverHighScoreText.text = ScoreManager.Instance.highscore.ToString();

        GameOverPanel.transform.localScale = Vector3.zero;
        RestartGame.transform.localScale = Vector3.zero;

        GameOverMenu.DOFade(1, 0.4f).SetDelay(0.5f)
        .OnComplete(() => GameOverPanel.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)
        .OnComplete(() => RestartGame.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)));
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
        ScoreText.transform.DOPunchScale(Vector3.one * 0.15f, 0.2f);
    }

    public void TriggerStartGame()
    {
        StartGameUI.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
