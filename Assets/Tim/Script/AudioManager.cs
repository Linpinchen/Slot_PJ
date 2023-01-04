using UnityEngine;
using System;
using baseSys.Audio;
using baseSys.Audio.Sources;

public class AudioManager : MonoBehaviour
{

    public static AudioManager inst; //單例

    //！！基本上 這裡就是開放 方法給其他人做使用的  更底層的 Souce PlayerMethod AudioPlayer腳本 其他人不會認識！！

    //音樂資源設定 有需要可以自己再新增 像環境音什麼的

    /// <summary>
    /// 音樂資源設定
    /// </summary>
    [SerializeField]
    Source[] BGMSetting;

    /// <summary>
    /// 音效資源設定
    /// </summary>
    [SerializeField]
    Source[] SFXSetting;


    /// <summary>
    /// 音樂資源設定
    /// </summary>
    [SerializeField]
    Source[] SlotSetting;

    /// <summary>
    /// 音樂播放器
    /// </summary>
    AudioPlayer BGM;

    /// <summary>
    /// 音效播放器
    /// </summary>
    AudioPlayer SFX;

    /// <summary>
    /// 音效播放器
    /// </summary>
    AudioPlayer Slot;


    /// <summary>
    /// 音樂音量較正值
    /// </summary>
    [Range(0, 1)]
    float BGMValue = 0.5f;


    /// <summary>
    /// 音樂音量較正值
    /// </summary>
    [Range(0, 1)]
    float SlotValue = 0.5f;

    /// <summary>
    /// 音效音量較正值
    /// </summary>
    [Range(0, 1)]
    float SFXValue = 0.5f;

    void Awake()//腳本掛在哪裡 inst 就是哪一個
    {
        inst = this;

        BGM = new AudioPlayer(gameObject, "BGMPlayer", BGMSetting, BGMValue);//gameObject 就是自己當前得這個物件
        SFX = new AudioPlayer(gameObject, "SFXPlayer", SFXSetting, SFXValue);
        Slot = new AudioPlayer(gameObject,"SlotPlayer",SlotSetting,SlotValue);

    }

    void Start()
    {
        //清空省記憶體
        BGMSetting = null;
        SFXSetting = null;
        SlotSetting = null;
    }

    #region [BGM播放]
    /// <summary>
    /// 播放設定BGM
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGM(string name,int ClipCount)
    {
        BGM.Play(name, ClipCount);
    }

    /// <summary>
    /// StopAll
    /// </summary>
    public void BGMStop()
    {
        BGM.StopAll();
    }

    /// <summary>
    /// Stop Name
    /// </summary>
    /// <param name="name"></param>
    public void BGMStop(string name)
    {
        BGM.Stop(name);
    }

    /// <summary>
    /// 重設音量
    /// </summary>
    /// <param name="value"></param>
    public void BGMReset(float value)
    {
        BGM.ResetValue(value);
    }
    #endregion

    #region [SFX播放]
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name,int ClipCount)
    {
        SFX.Play(name, ClipCount);
    }



    /// <summary>
    /// 播放音效(多個物件循環利用版)
    /// </summary>
    /// <param name="name"></param>
    public void PlayAddSFX(string name,int ClipCount)
    {

        SFX.AddPlay(name,ClipCount);

    }




    /// <summary>
    /// Stop All
    /// </summary>
    public void SFXStop()
    {
        SFX.StopAll();
    }

    /// <summary>
    /// Stop Name
    /// </summary>
    /// <param name="name"></param>
    public void SFXStop(string name)
    {
        SFX.Stop(name);
    }

    /// <summary>
    /// 重設音量
    /// </summary>
    /// <param name="value"></param>
    public void SFXReset(float value)
    {
        SFX.ResetValue(value);
    }
    #endregion

    /// <summary>
    /// 靜音選項
    /// </summary>
    /// <param name="setAct"></param>
    public void Mute(bool setAct)
    {
        BGM.Mute(setAct);
        SFX.Mute(setAct);
    }



    /// <summary>
    /// 播放音效(多個物件循環利用版)
    /// </summary>
    /// <param name="name"></param>
    public void PlayAddSlot(string name, int ClipCount)
    {

        Slot.AddPlay(name, ClipCount);

    }

    /// <summary>
    /// StopAll
    /// </summary>
    public void SlotStop()
    {
        Slot.StopAll();
    }

    /// <summary>
    /// Stop Name
    /// </summary>
    /// <param name="name"></param>
    public void SlotStop(string name)
    {
        Slot.Stop(name);
    }

    /// <summary>
    /// 重設音量
    /// </summary>
    /// <param name="value"></param>
    public void SlotReset(float value)
    {
        Slot.ResetValue(value);
    }

}
