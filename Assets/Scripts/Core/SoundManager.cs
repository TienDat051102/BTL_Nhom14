using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();


        //Giữ đối tượng này ngay cả khi chúng ta chuyển sang cảnh mới
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Phá hủy các đối tượng trò chơi trùng lặp
        else if (instance != null && instance != this)
            Destroy(gameObject);

        //theem  nhac
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {

        // Lấy giá trị ban đầu của volume và thay đổi nó
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;


        // Kiểm tra xem chúng ta đã đạt đến giá trị lớn nhất hay nhỏ nhất chưa
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Gán giá trị cuối cùng
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;


        // Lưu giá trị cuối cùng vào 
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}