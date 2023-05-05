using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Nếu tạm dừng màn hình đã hoạt động bỏ tạm dừng và ngược lại
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }


    // Kích hoạt trò chơi trên màn hình
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //khởi động các màn trò chơi 
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Thoát game/thoát chế độ chơi nếu trong Editor
    public void Quit()
    {
        Application.Quit(); // Thoát khỏi trò chơi (chỉ hoạt động trong bản dựng)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Thoát khỏi chế độ phát (sẽ chỉ được thực thi trong trình chỉnh sửa)
#endif
    }
   

    #region Pause
    public void PauseGame(bool status)
    {
        //nếu trạng thái đúng sẽ tạm dừng , nếu sai bỏ tạm dừng
        pauseScreen.SetActive(status);

        // Khi trạng thái tạm dừng là true, thay đổi thang thời gian thành 0 (thời gian dừng)
        // khi nó sai, đổi lại thành 1 (thời gian trôi qua bình thường)

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}