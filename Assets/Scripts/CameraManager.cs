using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectRatio : MonoBehaviour
{
    public float targetAspect = 9.0f / 16.0f;  // 目標のアスペクト比（縦長の場合）

    void Start()
    {
        SetAspectRatio();
    }

    void SetAspectRatio()
    {
        // 現在の画面のアスペクト比を取得
        float windowAspect = (float)Screen.width / (float)Screen.height;
        // スケール値を計算
        float scaleHeight = windowAspect / targetAspect;

        // カメラコンポーネントを取得
        Camera camera = GetComponent<Camera>();

        // スクリーンがアスペクト比と一致しない場合にレターボックスやピラーボックスを適用
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
