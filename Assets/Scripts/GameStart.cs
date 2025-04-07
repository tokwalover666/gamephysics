using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;
    [SerializeField] GameObject titleScreen;

    public bool gameStarted = false;
    public string fadeInStateName = "fadeIn";   // Name of the fade-in animation state
    public string fadeOutTriggerName = "isFadingOut";

    private bool waitingToFadeOut = false;

    void Update()
    {
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X)))
        {
            StartGame();
        }

        // Wait for fade-in animation to finish, then trigger fade-out
        if (waitingToFadeOut)
        {
            AnimatorStateInfo stateInfo = fadeAnim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(fadeInStateName) && stateInfo.normalizedTime >= 1f)
            {
                titleScreen.SetActive(false);
                fadeAnim.SetTrigger(fadeOutTriggerName);
                waitingToFadeOut = false;
                Debug.Log("Fade-out triggered");
                gameStarted = true;
            }
        }
    }

    public void StartGame()
    {
        fadeAnim.SetTrigger("isFadingIn");

        waitingToFadeOut = true;

        Debug.Log("Game Started!");
    }
}
