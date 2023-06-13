using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Vector2 _startPosition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
                BackToMenuScene();
        }

        else if (Application.platform == RuntimePlatform.IPhonePlayer) //Свайп вправо на IOS
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    _startPosition = touch.position;
                else if (touch.phase == TouchPhase.Moved && touch.position.x > _startPosition.x)
                {
                    if (Mathf.Abs(touch.position.x - _startPosition.x) > Screen.width / 2)
                        BackToMenuScene();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                BackToMenuScene();
        }
    }
    public void BackToMenuScene()
    {
        Load(1);
    }
    public void Load(int index)
    {
        PlayerPrefs.SetInt("SceneToLoad", index);
        SceneManager.LoadScene(0);
    }
}
