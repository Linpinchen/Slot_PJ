using UnityEngine;
using System.Collections.Generic;
using baseSys.Audio.Sources;//這裡利用namespace 來使用Source的東西
using DG.Tweening;//這裡有用到 DoTween

namespace baseSys.Audio.Method
{
    public class PlayMethod
    {
        /// <summary>
        /// 播放清單
        /// </summary>
        Dictionary<string, Source> _list = new Dictionary<string, Source>();//用 Dictionary 來裝資料 ---> 利用一個Key 來找Value （這裡的Value 是一個 Source的資料 ）

        /// <summary>
        /// 現在播放中（是一個List<GameObject>）
        /// </summary>
        List<GameObject> _nowPlayer = new List<GameObject>();

        /// <summary>
        /// 音量較正值
        /// </summary>
        [Range(0, 1)]
        float FixValue = 0.5f;//Ｕnity 裡 音量最大就是 1 這裡社0.5的用意在於 我一個音量如果原始聲以調到最大 還是不夠 那我這個 音量校正值設0.5 就能讓他還有往上調整的空間 反之亦然  //這裡卡死在0.5所以 Souce Class的Volume 就算調到1 也只會是0.5

        /// <summary>
        /// 播放父物件
        /// </summary>
        GameObject playerObject;

        /// <summary>
        /// 物件池
        /// </summary>
        GameObject ObjectPool;

        bool _mute = false;//要不要靜音 （注意！！ 這裡靜音但是實際上音樂還是在播放）

        /// <summary>
        /// 初始化播放清單
        /// </summary>
        /// <param name="thisObject"></param>
        /// <param name="type">播放器類型名稱</param>
        /// <param name="playlist">播放清單</param>
        /// <param name="fixValue">初始音量較正值</param>
        public PlayMethod(GameObject thisObject, string type, Source[] playlist, float fixValue)
        {

            for (int i = 0; i < playlist.Length; ++i)//這裡為了不要跟 Source 有關連（因為要把外部得Source清空 來節省資源） 所以 將 Source 的資料 一個個放進Dictionary 來做使用
            {
                var pl = playlist[i];
                _list.Add(pl.Name, pl);
            }

            FixValue = fixValue;

            #region [產生物件]
            playerObject = new GameObject();//這裡是建構子的方法內 所以在物件被New出來後 新增一個GameObject 來等於 playerObject 
            playerObject.transform.SetParent(thisObject.transform, false);//這裡要設定新增出來的 GameObject 的父物件是誰 （這裡寫 thisObject 就是腳本掛在哪個物件就是誰）
            playerObject.name = type;//名字就用建構子需要的參數的string type

            ObjectPool = new GameObject();//這裡一樣新增一個物件 （用來當物件池的 物件的 父物件）
            ObjectPool.transform.SetParent(playerObject.transform, false);//新增在上面那個物件的底下
            ObjectPool.name = "Pool";//取名字
            #endregion

        }

        /// <summary>
        /// 重設音量
        /// </summary>
        /// <param name="newValue">新較正值</param>
        public void ResetValue(float newValue)
        {

            //---------------------------------------------------------------------------------
            //                 這裡校正的是場景上現在正在播放的

            for (int i = 0; i < _nowPlayer.Count; ++i)//把現在正在播放中的物件的音量每個都調整較正值
            {
                var playlist = _nowPlayer[i];
                float value = playlist.GetComponent<AudioSource>().volume;
                //重設音量
                playlist.GetComponent<AudioSource>().volume = (value / FixValue) * newValue;//這裡要 value / FixValue 是因為 原本有*音量校正值 所以要 除回來 不然會 越 * 越小
            }

            //---------------------------------------------------------------------------------



            //替換新音量較正值
            FixValue = newValue; //而這個 從新給值的用意在於 讓未播放的 在要播放時 抓到的是我們重新設定過的

        }

        /// <summary>
        /// 靜音
        /// </summary>
        /// <param name="setAct"></param>
        public void OnMute(bool setAct)
        {
            _mute = setAct;
            for (int i = 0; i < _nowPlayer.Count; ++i)//把所有現在正在播放的物件的mute（是否禁音）來做設定 ㄧ樣禁音是指聲音為0 但是還是有在做播放
            {
                var playlist = _nowPlayer[i];
                playlist.GetComponent<AudioSource>().mute = _mute;
            }
        }


        /// <summary>
        /// 一般播放
        /// </summary>
        /// <param name="name"></param>
        public void NextPlay(string name , int ClipCount)
        {

            //如果播放清單有
            if (_list.ContainsKey(name))//_list內有很多的Souce 然後 用一個Key去尋找_list 有沒有 這個Key 的Valu
            {
                nextPlay(name, ClipCount);//有的話就執行 ,這個播放功能是 只有一個物件 去替換播放內容
            }
            else
            {
                Debug.LogError("Not Find Audio");//沒有就跳警告
            }
        }

        /// <summary>
        /// 產生播放器
        /// </summary>
        /// <param name="name"></param>
        public void ADDPlay(string name,int ClipCount)
        {
            //如果播放清單有
            if (_list.ContainsKey(name))
            {
                play(name, ClipCount);//這裡用的就是另一種播放功能（多個物件用得,播完就回收）
            }
            else
            {
                Debug.LogError("Not Find Audio");
            }
        }

        /// <summary>
        /// 停止所有播放
        /// </summary>
        public void StopAll()
        {

            for (int i = 0; i < _nowPlayer.Count; ++i)
            {
                var stop = _nowPlayer[i];
                recover(stop);
            }

        }

        /// <summary>
        /// 停止特定聲音
        /// </summary>
        /// <param name="name"></param>
        public void Stop(string name)
        {
            for (int i = 0; i < _nowPlayer.Count; ++i)
            {
                var stop = _nowPlayer[i];
                if (stop.name == name)
                    recover(stop);
            }
        }

        /// <summary>
        /// 檢查物件池是否有可用物件，無則產生一個，並返回物件(減少創物件)。
        /// </summary>
        /// <returns></returns>
        GameObject create()
        {

            Transform tsf = ObjectPool.transform;
            GameObject obj;

            if (tsf.childCount > 0)
            {

                obj = tsf.GetChild(0).gameObject;
            }

            else
            {
                obj = new GameObject();

                obj.AddComponent<AudioSource>();

            }

            if (obj.GetComponent<AudioSource>() == null)//這裡是拉一個保險 確保物件有AudioSource
            {

                obj.AddComponent<AudioSource>();

            }


            obj.transform.SetParent(playerObject.transform, false);

            return obj;

        }

        /// <summary>
        /// 回收該物件(減少創物件)
        /// </summary>
        /// <param name="obj"></param>
        void recover(GameObject obj)
        {
            //從使用中移除
            _nowPlayer.Remove(obj);//從List移除 但是次見實際還是存在的

            //丟進物件池並關閉
            obj.transform.SetParent(ObjectPool.transform, false);
            obj.SetActive(false);

        }


        /// <summary>
        /// 播放功能
        /// </summary>
        /// <param name="name"></param>
        void play(string name,int ClipCount)
        {
            //取得物件
            GameObject obj = create();
            AudioSource aos = obj.GetComponent<AudioSource>();
            obj.name = name;

            bool retrigger = set(_list[name], ref aos, ClipCount);
            obj.SetActive(true);
            aos.Play();

            _nowPlayer.Add(obj);

            float life = aos.clip.length;

            //判斷是否循環播放，或者重複觸發
            if (!retrigger)
            {

                if (!_list[name].Loop)
                {

                    Sequence _delayCallback;
                    _delayCallback = DOTween.Sequence();
                    _delayCallback.InsertCallback(life, delegate
                    {
                        recover(obj);

                    });

                }

            }

            else
            {
                Sequence _delayCallback;
                _delayCallback = DOTween.Sequence();
                _delayCallback.InsertCallback(life, delegate
                {
                    play(name, ClipCount);
                });
            }

        }


        Sequence _delayNextPlay;//這是DoTween的東西

        /// <summary>
        /// 同個播放器，播放下一首
        /// </summary>
        /// <param name="name"></param>
        void nextPlay(string name, int ClipCount)
        {

            int playerCount = _nowPlayer.Count;//目前正在播放中的有幾個

            //取得物件
            GameObject obj;
            AudioSource aos;
            //-------------------------------------------------
            //這裡就是物件池的應用 有就取出沒有就生成
            //如果有正在播放
            if (playerCount > 0)
            {
                obj = _nowPlayer[0];
                aos = obj.GetComponent<AudioSource>();
            }

            else
            {
                obj = create();
                _nowPlayer.Add(obj);
                aos = _nowPlayer[0].GetComponent<AudioSource>();

            }
            //-------------------------------------------------


            //-------------------------------------------------
            //這是在說 DoTween的東西
            //移除Delay
            if (_delayNextPlay != null)
            {
                _delayNextPlay.Kill();
            }

            if (obj.name != name)//如果物件的名字跟播放的名字不是同一個名字 就改變物件的名字
            {
                obj.name = name;
            }
            //-------------------------------------------------

            //是否重置播放時間
            float time;
            if (!_list[name].ResetTime)//如果_list裡面的 name 的 ResetTime 是False 就是不要重制 要繼續播放
            {
                time = aos.time + 0.01f;//這裡是為了避免Bug 所以多加了0.01f 因為音樂是一直在走的 實際上已經過了aos.time的時間（實際上會過去一幀） 突然拉回去 會有點問題
            }
            else
            {
                time = 0;
            };

            //載入設定檔&播放
            bool retrigger;
            retrigger =
            set(_list[name], ref aos,ClipCount);

            aos.time = time;//這裡就重新給時間 看是要重來還是繼續播這樣（播放到哪裡的這件事）
            aos.Play();//然後播放
            obj.SetActive(true);

            if (retrigger && _list[name].Loop)
            {
                float life = aos.clip.length;//記錄歌曲得總時間

                life -= aos.time;//取得剩餘時間

                //這裡就是用 DOTween 來做接續播放
                _delayNextPlay = DOTween.Sequence();

                //當音樂播完就回來執行nextPlay(name)
                _delayNextPlay.InsertCallback(life, delegate
                {
                    nextPlay(name, ClipCount);
                });

            }

        }

        /// <summary>
        /// return reTrigger
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        bool set(Source setting, ref AudioSource player,int ClipCount)
        {
            bool reTrigger;

            if (setting.Clip.Length > 1 && setting.Loop)//如果Source的歌有很多首 而且有Loop
            {
                reTrigger = true;//循環播放
            }
            else
            {
                reTrigger = false;//不循環播放
            }

            //取得播放歌曲
            AudioClip clip = getClip(setting.Clip, ClipCount);//取得Source的AudioClip

            float volume = getVol(setting.Volume);

            float pitch = getPitch(setting.Pitch);


            //-------------------------------------------------
            //這裡就把要的內容 放進AudioSource做設定

            player.clip = clip;
            player.loop = setting.Loop;
            player.volume = volume;
            player.pitch = pitch;

            if (setting.MixerGroup)
            {

                player.outputAudioMixerGroup = setting.MixerGroup;

            }

            else
            {

                player.outputAudioMixerGroup = null;

            }

            //-------------------------------------------------

            if (_mute)
            {
                player.mute = _mute;//這裏的靜音 指的事音效還在播放 只是聲音大小為0
            }


            return reTrigger;
        }

        /// <summary>
        /// 取得播放音源
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        AudioClip getClip(AudioClip[] data,int ClipCount)
        {
            //這裡要取得 Source 的 clip 陣列內的歌

            AudioClip clip;
            int rang = data.Length;//取得長度

            //陣列範圍0
            if (rang == 1)
            {
                clip = data[0];
            }
            //若陣列大於，則亂數選取播放
            //else if (rang > 1)
            //{
            //    System.Random ptr = new System.Random(System.Guid.NewGuid().GetHashCode());//這是Ｃ＃的亂數隨機 可以換成熟悉的Unity的Radom.Range
            //    int num = ptr.Next(rang);
            //    clip = data[num];
            //}
            else if (rang>1)
            {

                clip = data[ClipCount];

            }
            else
            {
                clip = null;
                Debug.LogError("out of rang! AudioClip...");
            }

            return clip;//回傳 AudioClip
        }

        /// <summary>
        /// 取得音量
        /// </summary>
        /// <param name="vol"></param>
        /// <returns></returns>
        float getVol(Source.Vol vol)
        {
            float volume;

            if (!vol.IsRandom)//有沒有要隨機
            {
                volume = vol.Volume;
            }
            else
            {
                volume = Random.Range(vol.Max, vol.Min);//老師偷秀 ！！ Random.Range裡面 正常來說左邊放最小值 右邊放最大值最大值不會中 去隨機  但是這裡 兩個位置放相反的 ！！ 會不會出錯？ 不會！！ 因為他只是要一個範圍而以
            }

            //音量較正
            volume *= FixValue;

            return volume;//回傳音量
        }

        /// <summary>
        /// 取得pitch
        /// </summary>
        /// <param name="pit"></param>
        /// <returns></returns>
        float getPitch(Source.AudioPitch pit)
        {
            float pitch;

            if (!pit.IsRandom)
            {
                pitch = pit.Pitch;
            }
            else
            {
                pitch = Random.Range(pit.Max, pit.Min);
            }

            return pitch;
        }

    }

}
