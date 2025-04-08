using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHits = 10;
    public GameObject gameOverScreen;
    public Image[] heartIcons; 

    private int hitCount = 0;
    private PlayerAcceleration playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerAcceleration>();

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        UpdateHeartsUI();
    }

    public void TakeHit()
    {
        hitCount++;
        Debug.Log("Player hit! Total hits: " + hitCount);
        UpdateHeartsUI();

        if (hitCount >= maxHits)
        {
            GameOver();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < maxHits - hitCount)
            {
                heartIcons[i].enabled = true;
            }
            else
            {
                heartIcons[i].enabled = false;
            }
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }
}
