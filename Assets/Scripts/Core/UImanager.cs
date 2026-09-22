using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Input de enemigos")]
    [SerializeField] private TMP_InputField enemyCountInput;

    [Header("Textos de victoria y derrota")]
    [SerializeField] private TMP_Text resultText;

    private const int MinEnemies = 1;
    private const int MaxEnemies = 7;
    private const int DefaultEnemies = 1;


    private void Start()
    {
        ShowMainMenu();
        enemyCountInput.text = DefaultEnemies.ToString();
    }


    public void OnPlayButtonPressed()
    {
        int enemyCount = GetEnemyCount();
        HideMainMenu();
        GameManager.Instance.StartGame(enemyCount);

    }

    private int GetEnemyCount()
    {
        if (!int.TryParse(enemyCountInput.text, out int enemyCount))
        {
            enemyCount = DefaultEnemies;
        }

        enemyCount = Mathf.Clamp(
            enemyCount,
            MinEnemies,
            MaxEnemies
        );

        enemyCountInput.text = enemyCount.ToString();

        return enemyCount;
    }


   
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


  
    public void ShowVictory()
    {
        gameOverPanel.SetActive(true);
        resultText.text = "¡Ganaste!";
    }

    public void ShowDefeat()
    {
        gameOverPanel.SetActive(true);
        resultText.text = "Perdiste";
    }
}