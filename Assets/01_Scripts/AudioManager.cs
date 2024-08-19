using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx
    {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Win
    }

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = this.transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = this.bgmVolume;
        bgmPlayer.clip = this.bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = this.transform;
        sfxPlayers = new AudioSource[this.channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = this.sfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + this.channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }
            sfxPlayers[loopIndex].clip = this.sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
        ;
    }
}