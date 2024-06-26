using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private static WindowManager _instance;

    public int targetWidth = 1080;  // 目標の幅
    public int targetHeight = 1920; // 目標の高さ
    public float targetAspect = 9.0f / 16.0f;  // 目標のアスペクト比

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SetWindowSizeAndAspect();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetWindowSizeAndAspect()
    {
        // ディスプレイの解像度を取得
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        // 目標解像度のアスペクト比を計算
        float windowAspect = (float)screenWidth / screenHeight;
        float scaleHeight = windowAspect / targetAspect;

        // スケールを計算
        if (scaleHeight < 1.0f)
        {
            targetWidth = Mathf.RoundToInt(targetHeight * windowAspect);
        }
        else
        {
            targetHeight = Mathf.RoundToInt(targetWidth / windowAspect);
        }

        // 新しい解像度を設定
        Screen.SetResolution(targetWidth, targetHeight, false);
    }
}
