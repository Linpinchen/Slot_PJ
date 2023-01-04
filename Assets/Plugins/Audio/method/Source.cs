using UnityEngine;
using UnityEngine.Audio;//因為用到了Unity的Audio 所以要Using近來
using System;

namespace baseSys.Audio.Sources
{


    //Source的Class Source是什麼? 就是每一首歌的資源（每一首歌 會有哪些控制項（依照 Unity 裡的 AudioSouce 原有的控制項 老師依照需求 也設定了自己需要的 ））

    /// <summary>
    /// 音樂播放資料型態
    /// </summary>
    [Serializable]
    public class Source
    {

        public string Name;
        public AudioClip[] Clip;//AudioClip 代表unity裡的一首歌的意思
        public bool Loop;//播完後是否循環播放
        public bool ResetTime;//關了在開啟後 是否要重置音樂播放的進度時間
        public Vol Volume;
        public AudioPitch Pitch;//音調 播放的速度越快 音調就會越高越尖
        public AudioMixerGroup MixerGroup;//AudioMixerGroup 官方的（混音器）  不放其實沒差   這樣建立--->按右鍵 creat --> Audio mixer


        //建構子
        public Source()
        {
            Name = "";//這裡的Name指的不是單一首歌的名字 而是集合體的名字 像是 Name 會叫" 大廳音樂 "等等 ,而大廳音樂裡面可能會有很多歌來選擇播放
            Clip = new AudioClip[1];// 音樂的陣列
            Loop = false;
            ResetTime = true;
            Volume = new Vol();
            Pitch = new AudioPitch();
        }

        /// <summary>
        /// 音量控制設定件
        /// </summary>
        [Serializable]
        public class Vol// 這是一開始設定的音量  // ！！注意 ！！ 音量的大小是根據 “原本的 AudioClip 音量” ＊  “Volume（如果IsRandom是True就是隨機0到1）” ＊ “PlayMethod腳本的音量校正值”
        {
            public Vol()//建構子
            {
                Volume = 1;
                IsRandom = false;
                Max = 1;
                Min = 1;
            }

            [Range(0, 1)]
            public float Volume = 1;

            public bool IsRandom;//要不要隨機（如果要隨機 就不是看上面的值了 而是看下面的 Max Min 來隨機一個值）

            [Range(0, 1)]
            public float Max = 1;

            [Range(0, 1)]
            public float Min = 1;
        }

        /// <summary>
        /// Pitch控制設定件（音調）
        /// </summary>
        [Serializable]
        public class AudioPitch
        {

            public AudioPitch()
            {
                Pitch = 1;
                IsRandom = false;
                Max = 1;
                Min = 1;
            }

            [Range(-3, 3)]
            public float Pitch = 1;

            public bool IsRandom;

            [Range(1, 3)]
            public float Max = 1;

            [Range(-3, 1)]
            public float Min = 1;
        }


    }

}