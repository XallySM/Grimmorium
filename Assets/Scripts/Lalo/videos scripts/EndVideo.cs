
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer video;
    public Slider slider;

    public string sceneName;

    
    void OnEnable()
    {
        video.loopPointReached += loopPointReached;
    }

    void OnDisable()
    {
        video.loopPointReached -= loopPointReached;
    }

    void loopPointReached(VideoPlayer v)
    {
        SceneManager.LoadScene(sceneName);
    }
}
