using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> audioClips; // 効果音リストを公開フィールドとして定義
    private Dictionary<string, AudioSource> audioSources; // 効果音名とAudioSourceの辞書


    private float defaultVolume = 0.1f;

    public static SoundManager Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // インスタンスを設定
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを保持
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は新しいオブジェクトを破棄
        }
    }

    void Start()
    {
        audioSources = new Dictionary<string, AudioSource>();

        foreach (AudioClip clip in audioClips)
        {
            GameObject audioObject = new GameObject(clip.name);
            audioObject.transform.SetParent(this.transform);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSources[clip.name] = audioSource;
            audioSource.volume = defaultVolume; // デフォルトの音量を設定

        }
    }

    public void PlaySound(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
        }
    }

    public void SetLoop(string clipName, bool loop)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].loop = loop;
        }
        else
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
        }
    }

    public void StopSound(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].Stop();
        }
        else
        {
            Debug.LogWarning($"Sound: {clipName} not found!");
        }
    }

    public void SetVolume(string clipName, float volume)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].volume = Mathf.Clamp(volume, 0f, 1f); // 0.0から1.0の範囲で音量を設定
        }
        else
        {
            Debug.LogWarning($"Sound: {clipName} not found!");
        }
    }
}
