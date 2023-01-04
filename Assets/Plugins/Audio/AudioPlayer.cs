using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using baseSys.Audio.Sources;
using baseSys.Audio.Method;

public class AudioPlayer : MonoBehaviour {



    //這裡是我要怎麼播 （音樂的播放器）




    //這個播放器 會需要用到 播放方法 也就是 “ PlayMethod ”

    PlayMethod _player; 

    /// <summary>
    /// 初始化播放清單
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="type">播放器類型名稱</param>
    /// <param name="playlist">播放清單</param>
    /// <param name="fixValue">初始音量較正值</param>
    public AudioPlayer(GameObject thisObject, string typeName, Source[] bgmList, float fixValue)
    {
        _player = new PlayMethod(thisObject, typeName, bgmList, fixValue);
    }

    /// <summary>
    /// 播放聲音檔
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        _player.NextPlay(name);
    }



    /// <summary>
    /// 播放聲音檔（多個物件循環利用版）
    /// </summary>
    /// <param name="name"></param>
    public void AddPlay(string name)
    {
        _player.ADDPlay(name);
    }


    /// <summary>
    /// 停止生音檔
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        _player.Stop(name);
    }

    /// <summary>
    /// 停止所有聲音
    /// </summary>
    public void StopAll()
    {
        _player.StopAll();
    }

    /// <summary>
    /// 重設音量較正值
    /// </summary>
    public void ResetValue(float value)
    {
        _player.ResetValue(value);
    }

    /// <summary>
    /// 靜音
    /// </summary>
    /// <param name="mute"></param>
    public void Mute(bool mute)
    {
        _player.OnMute(mute);
    }
}
