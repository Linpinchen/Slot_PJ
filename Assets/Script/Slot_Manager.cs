using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class Slot_Manager : MonoBehaviour
{

    public IDate _SlotDate;//玩家金錢及盤面資料腳本
    //public IDateEvent _SlotDateEvent;
    public IShow _ShowScript;
    public IUIControlMethod _UIControlMethod;
    public ResourceManager _ResourceManager;
    public Reel_Move[] _Reel_Moves;
    public PlayerControl _PlayerControl;
    public Button_EventTrigger _ButtonPlus_EventTrigger;
    public Button_EventTrigger _ButtonReduce_EventTrigger;
    public ShowScript showScript;

    public Texture2D[] MIcon;
    public float RollSpeed;//輪條移動速度
    public int Loopcount;//循環次數
    public int Slot_mantissa;//輪條陣列內最後的數
    public int FreeGameCount;//小遊戲要玩的次數
    public int NowFreeCount;//當前遊玩次數
    public bool Start_Slot;
    public bool CurrentReel_B;
    bool BonusStateInfo_B;
    public bool B_Slot_timeOut;//讓_Slot_timeOut只會執行一次
    bool WinShowOk;//表演是否完成
    bool St_Roll;//是否開始轉輪
    bool StCoShow;//確保CoShow只有一個
    bool UseBonus; //執行Bonus的開關
    bool StEndShow; //是否開始動畫
    IEnumerator _CoAll_Slot_Roll;//全輪條轉動用的協程
    IEnumerator _Show;//表演用的協程
    IEnumerator _Slot_timeOut;//判斷是否需要繼續滾動用的協程
    AnimatorStateInfo BonusStateInfo;//動畫當前狀態
    Animator TempAnimator;
    string Animator_Name;
    public SlotGrid CommonGrid;
    public SlotGrid BonusGrid;
    string LoadPath;
    

    public bool isLog;


    public bool LoadOK;

    public Button btn;

    void Start()
    {




        // PlayerPrefs.DeleteAll();

        Debuger.Enable = isLog;

        platformdetection();//設備檢測以載入正確Bundle



        _ResourceManager.init(LoadPath);

        StartCoroutine(Slot_Initialization());



        AudioManager.inst.PlayBGM("Lobby_Main", 0);








        //Slot_Initialization();
        //SlotGridCreat();
        //Debuger.Log("_ShowScript.BonusPrepareDrawline.Count" + _ShowScript.BonusPrepareDrawline.Count);








    }


    void Update()
    {

        if (LoadOK)
        {
            UpdateMethod(CommonGrid, BonusGrid);
        }
        

    }

    #region 資料初始化
    /// <summary>
    /// 資料初始化
    /// </summary>

    public IEnumerator Slot_Initialization()
    {

        LoadOK = false;




        _SlotDate = new Slot_data(_Reel_Moves, _ResourceManager);

        //_SlotDateEvent = slot_Data;
        //_SlotDateEvent= new Slot_data(_Reel_Moves, _ResourceManager);

        showScript.Init(_SlotDate, _ResourceManager, _Reel_Moves);
        _ShowScript = showScript;

        //_ShowScript = new ShowScript(_SlotDate, _ResourceManager, _Reel_Moves);
        _UIControlMethod = new UIControlMethod(_SlotDate, _ButtonPlus_EventTrigger, _ButtonReduce_EventTrigger, _ResourceManager);

        _PlayerControl = new PlayerControl();

        yield return StartCoroutine(CheckBundleLoad());



        Operational();


        FreeGameCount = 3;
        B_Slot_timeOut = true;
        WinShowOk = true;
        StEndShow = false;
        UseBonus = false;
        StCoShow = true;
        St_Roll = false;
        Start_Slot = false;
        CurrentReel_B = false;

       
        _PlayerControl.PlayerControl_Init(_UIControlMethod, this, _ResourceManager);//PlayerControl 給變數的 Interface 指定是誰

      
        for (int i = 0; i < _Reel_Moves.Length; i++)// 各個_Reel_Moves 的變數設定 
        {

            _Reel_Moves[i].Init(Loopcount, _ResourceManager.Sprite_Pool, RollSpeed);

        }

        for (int i = 0; i < _Reel_Moves.Length; i++)
        {
            _SlotDate.Date.Add(new intCount());
            _SlotDate.Slot_SeverDate.Data.Add(new intCount());

            Debuger.Log("Manager Init - _SlotDate.Date的數量 ： "+ _SlotDate.Date.Count);
            Debuger.Log("Manager Init -  _SlotDate.Slot_SeverDate..Date的數量 ： " + _SlotDate.Slot_SeverDate.Data.Count);

            for (int j = 0; j < _Reel_Moves[i].transform.childCount; j++)
            {

                _SlotDate.Date[i]._IntCount.Add(0);
               _SlotDate.Slot_SeverDate.Data[i]._IntCount.Add(0);
            }

            _Reel_Moves[i].Sprites = _ResourceManager.Sprite_Pool;



        }

        for (int i = 0; i < FreeGameCount; i++)
        {

            _SlotDate.BonusWinCoin.Add(0);
            _ShowScript.BonusUiShow.Add(new List<Transform>());
            _ShowScript.BonusPrepareDrawline.Add(new List<GameObject>());
            _ShowScript.BonusDrawLineOk.Add(new List<bool>());

        }

        _ShowScript.ObjectPoolInitialization();//物件池 預置物生成

        _SlotDate.CycleCount = 0;
        _SlotDate.PlayerCoin = 1099;
        _SlotDate.AutoCount = 1;
        _SlotDate.LeastBetCount = 100;
        Debuger.Log("Player_coin" + _SlotDate.PlayerCoin);
        _ResourceManager._PlayerCoin_Text.text = "Money :" + _SlotDate.PlayerCoin.ToString();
        Slot_mantissa = _Reel_Moves.Length - 1;//紀錄陣列的最大編號


        for (int i = 0; i < _ShowScript.BonusPrepareDrawline.Count; i++)
        {

            if (_ShowScript.BonusPrepareDrawline[i] != null)
            {

                Debuger.Log(string.Format("_ShowScript.BonusPrepareDrawline[{0}], 不是空得,當前數量：{1}", i, _ShowScript.BonusPrepareDrawline[i].Count));

            }
            else
            {

                Debuger.Log(string.Format("_ShowScript.BonusPrepareDrawline[{0}], !是空得!", i));

            }

        }

        if (!PlayerPrefs.HasKey("遊戲資料"))
        {

            _SlotDate.Initialization_Slot_Sprite();

        }
        else
        {

            _UIControlMethod.GetDateSave();

        }




        Debuger.Log("是否開始滾動：" + Start_Slot);




        //AudioManager.inst.PlayBGM("Lobby_Main", 0);


        SlotGridCreat();
        Debuger.Log("_ShowScript.BonusPrepareDrawline.Count" + _ShowScript.BonusPrepareDrawline.Count);



        LoadOK = true;


    }
    #endregion

    #region 盤面物件創造
    /// <summary>
    /// 盤面物件創造
    /// </summary>
    public void SlotGridCreat()
    {

        int Reelcount;
        Reelcount = _Reel_Moves.Length;
        List<int> ImageQuantity = new List<int>();

        for (int i = 0; i < Reelcount; i++)
        {
            int Chcount = _Reel_Moves[i].Self.transform.childCount;
            ImageQuantity.Add(Chcount);
            Debuger.Log(string.Format("第幾輪 : {0} , 圖片數 ：{1}",i, ImageQuantity[i]));
        }

        CommonGrid = new SlotGrid(1, Reelcount, ImageQuantity, _SlotDate.Generate_Date_Sprite);
        BonusGrid = new SlotGrid(FreeGameCount, Reelcount, ImageQuantity, _SlotDate.GenerateBonusDate);


    }
    #endregion

    #region 讓各輪條開始轉動的開關
    /// <summary>
    /// 讓各輪條開始轉動的開關
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoAll_Slot_Roll(GridIntS _Grints)
    {
        if (St_Roll)
        {

            Debuger.Log("CoAll_Slot_Roll() :St_Roll 開始轉動");

            for (int i = 0; i < _Reel_Moves.Length; i++)
            {

                for (int j = 0; j < _Reel_Moves[i].Self.transform.childCount; j++)//記錄每輪中獎要換什麼圖片
                {

                    _Reel_Moves[i].ChangeSprite[j] = (int)_Grints._Grids[i]._GridInt[j];

                }

                // Debug.Log("CoAll_Slot_Roll() :迴圈");
                AudioManager.inst.PlayAddSlot("Rool", 0);
                _Reel_Moves[i].strool = true;
                Debuger.Log(string.Format("輪條編號：{0},是否開啟{1}", i, _Reel_Moves[i].strool));
                yield return new WaitForSeconds(0.5f);

            }

            St_Roll = false;
        }

    }
    #endregion

   
    #region 開始按鈕要執行得內容
    public void StartGame()
    {
        Debuger.Log("執行StartGame");

        if (Start_Slot == false && _SlotDate.Bet_Coin != 0 && _SlotDate.PlayerCoin >= _SlotDate.LeastBetCount)//如果Start_Slot == false（代表按鈕有作用 現在並無作動） 而且 下注金額不等於0 而且  玩家金額要大於最低押注金額
        {

            //int Autoi;
            //Autoi = int.Parse(_ResourceManager._Auto_text.text);
            //_SlotDate.AutoSurplus = Autoi;
            _SlotDate.PlayerCoin -= _SlotDate.Bet_Coin;
            Debuger.Log("_SlotDate.Bet_Coin :"+ _SlotDate.Bet_Coin);

            _ResourceManager._PlayerCoin_Text.text = "Money:" + _SlotDate.PlayerCoin;

            //if (_SlotDate.AutoSurplus > 1)
            //{

            //    _SlotDate.AutoCount = _SlotDate.AutoSurplus;

            //}

            GridCreat_Event( CommonGrid, BonusGrid);
            _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0]);
            StartCoroutine(_CoAll_Slot_Roll);
            Start_Slot = true;
            Debuger.Log("是否開始滾動：" + Start_Slot);

        }

    }
    #endregion

    #region （新） 判斷滾輪次數是否達成條件  沒達成條件就繼續滾
    /// <summary>
    /// 當Start_Slot為true時而且轉動次數到達上限 初始化開始按鈕跟輪條開始的bool
    /// </summary>

    public IEnumerator Co_Slot_timeOut(SlotGrid CommonGrid, SlotGrid BonusGrid)
    {

        if (_Reel_Moves[Slot_mantissa].tempi == _Reel_Moves[Slot_mantissa].Roolcount)//如果最後一個輪條達到滾動次數
        {

            int CurrentCount = _SlotDate.CurrentReel + 1;
            for (int i = CurrentCount; i < _Reel_Moves.Length; i++)
            {
                _Reel_Moves[i].transform.parent.GetChild(1).gameObject.SetActive(false);
                _Reel_Moves[i].Roolcount = Loopcount;
            }

            if (_SlotDate.BonusCount != FreeGameCount)
            {
                AudioManager.inst.BGMReset(0.5f);
                AudioManager.inst.SFXStop();
            }


            for (int j = 0; j < _Reel_Moves.Length; j++)//輪條轉動次數初始化
            {
                _Reel_Moves[j].tempi = 0;

                _Reel_Moves[j].Date_Temp = 0;

                Debuger.Log(string.Format("輪調{0}內滾動次數{1}", j, _Reel_Moves[j].tempi));

            }

            _SlotDate.CurrentReel = 0;

            Debuger.Log("啟動Co_Slot_timeOut");

            if (StCoShow)
            {
                StCoShow = false;
                _Show = WinShow();
                StartCoroutine(_Show);
                Debuger.Log("StART Win SHow");

            }

            yield return new WaitUntil(() => WinShowOk == true);
            Debuger.Log("WinShowOk :" + WinShowOk);

            if (_SlotDate.BonusCount == FreeGameCount && NowFreeCount < FreeGameCount)//如果_IDate.BonusCount 等於設定的 免費遊戲數
            {

                for (int i = 0; i < _Reel_Moves.Length; i++)
                {
                    _Reel_Moves[i].transform.parent.GetChild(1).gameObject.SetActive(true);

                }

                Debuger.Log("開啟Bonus開場表演");
                TempAnimator = _ResourceManager._BonusAnimator;
                Animator_Name = "BonusShow2";
                _ResourceManager._BonusAnimator.SetBool("ShowBool", true);
                BonusStateInfo_B = true;
                Debuger.Log("BonusStateInfo.normalizedTime :" + BonusStateInfo.normalizedTime);
                yield return new WaitUntil(() => _ResourceManager._BonusAnimator.GetBool("ShowBool") == false);
                Debuger.Log("-------------- : Bonus 動畫是否播放完畢 WaitUntil: -----------------" + _ResourceManager._BonusAnimator.GetBool("ShowBool"));

                for (int i = 0; i < FreeGameCount; i++)
                {
                    StCoShow = true;
                    UseBonus = true;
                    St_Roll = true;

                    _CoAll_Slot_Roll = CoAll_Slot_Roll(BonusGrid._grids[NowFreeCount]);
                    StartCoroutine(_CoAll_Slot_Roll);
                    yield return new WaitUntil(() => _Reel_Moves[Slot_mantissa].tempi == Loopcount);


                    for (int j = 0; j < _Reel_Moves.Length; j++)//輪條轉動次數初始化
                    {
                        _Reel_Moves[j].tempi = 0;

                        _Reel_Moves[j].Date_Temp = 0;

                        Debuger.Log(string.Format("輪調{0}內滾動次數{1}", j, _Reel_Moves[j].tempi));

                    }

                    if (StCoShow)
                    {
                        StCoShow = false;
                        _Show = WinShow(NowFreeCount);
                        StartCoroutine(_Show);
                        Debuger.Log("StART Win SHow");
                        yield return new WaitUntil(() => WinShowOk == true);
                        NowFreeCount++;

                    }

                }

                UseBonus = false;
                StCoShow = true;

                if (StCoShow)
                {

                    StCoShow = false;
                    _Show = Coin_EndShow();
                    StartCoroutine(_Show);
                    Debuger.Log("StART Win SHow");

                }

                yield return new WaitUntil(() => WinShowOk == true);//等待金錢動畫結束
                AudioManager.inst.BGMReset(0.01f);
                AudioManager.inst.SFXStop("SFX");

                TempAnimator = _ResourceManager._BonusEndShow;
                Animator_Name = "BonusEndShow";
                BonusStateInfo_B = true;
                _ResourceManager._BonusEndShow.SetBool("ShowBool", true);//開啟揮拳動畫

                AudioManager.inst.PlayAddSFX("SFX", 3);

                yield return new WaitUntil(() => _ResourceManager._BonusEndShow.GetBool("ShowBool") == false);//等待揮拳動畫結束

                for (int i = 0; i < _Reel_Moves.Length; i++)
                {
                    _Reel_Moves[i].transform.parent.GetChild(1).gameObject.SetActive(false);

                }

                //_Ishow.VideoImage.gameObject.SetActive(true);

                _ResourceManager._VideoImage.GetComponent<VideoPlayer>().Play();
                yield return new WaitForSeconds(0.5f);
                _ResourceManager._VideoImage.GetComponent<RawImage>().enabled = true;
                StEndShow = true;//開啟最後得表演
                StCoShow = true;

            }

            
            yield return new WaitUntil(() => _ResourceManager._VideoImage.GetComponent<RawImage>().enabled == false);
            _ResourceManager._EndShowPlayer.time = 0;

            AudioManager.inst.BGMReset(0.5f);

            if (_SlotDate.CycleCount < _SlotDate.AutoCount && _SlotDate.PlayerCoin > _SlotDate.Bet_Coin)//循環次數未到 而且 玩家金額不小於下注金額
            {

                NowFreeCount = 0;
                Debuger.Log("-----------------------------開始轉動-------------------------");

                if (_Reel_Moves[Slot_mantissa].tempi == 0) //這裡重新生成盤面資料 兌獎 設定預制物 
                {
                    
                    GridCreat_Event(CommonGrid, BonusGrid);
                    B_Slot_timeOut = true;
                    yield return new WaitForSeconds(1f);
                    Debuger.Log("Wait");
                    _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0]);
                    StartCoroutine(_CoAll_Slot_Roll);
                    //_SlotDate.CycleCount += 1;
                    //_SlotDate.AutoSurplus -= 1;
                    //_ResourceManager._Auto_text.text = _SlotDate.AutoSurplus.ToString();
                    _SlotDate.PlayerCoin -= _SlotDate.Bet_Coin;
                    _ResourceManager._PlayerCoin_Text.text = "Money:" + _SlotDate.PlayerCoin;
                    Debuger.Log(_SlotDate.CycleCount + "已循環次數");
                    //Debug.Log("Win_Mpney_Temp" + Win_Money_Temp);
                    StCoShow = true;


                    Debug.Log($"<Date>玩家要循環次數_AutoCount:{_SlotDate.AutoCount}");
                    Debug.Log($"<Date>未循環次數:{_SlotDate.AutoSurplus}");
                    Debug.Log($"<Date>已循環次數:{_SlotDate.CycleCount}");

                }

            }

            else
            {

                Debuger.Log("各輪盤滾動完畢");
                Debuger.Log("是否開始滾動：" + Start_Slot);
                _SlotDate.AutoCount = 1;
                Start_Slot = false;
                St_Roll = true;
                StCoShow = true;
                NowFreeCount = 0;
                _SlotDate.CycleCount = 0;
                _SlotDate.AutoSurplus = 1;
                B_Slot_timeOut = true;

            }

        }

    }
    #endregion

    #region 普盤表演
    /// <summary>
    /// 普盤表演
    /// </summary>
    /// <param name="_Ishow"></param>
    /// <param name="_IDate"></param>
    /// <returns></returns>
    public IEnumerator WinShow()
    {
        if (_SlotDate.Win_Coin != 0)
        {

            Debuger.Log("進入普盤表演");
            WinShowOk = false;
            Debuger.Log("表演中 ：WinShowOk" + WinShowOk);

            IEnumerator Show;
            Show = _ShowScript.ShinyShow(_ShowScript.UiShow);

            Debuger.Log("執行普盤表演的： _Ishow.ShinyShow");
            yield return StartCoroutine(Show);

            Show = _ShowScript.StartDrawLine(_ShowScript.PrepareDrawLine, _ShowScript.DrawLineOK);
            StartCoroutine(Show);

            Debuger.Log("執行普盤表演的： _Ishow.StartDrawLine");
            yield return new WaitUntil(() => _ShowScript.DrawLineOK.Contains(true) == false);
            Debuger.Log(" WinShow :" + !_ShowScript.DrawLineOK.Contains(true));

            Show = _ShowScript.CoinShow(_SlotDate.Win_Coin);

            Debuger.Log("執行普盤表演的： _Ishow.CoinShow");
            _ResourceManager._Amr_WinShow.SetBool("ShowBool", true);//開啟動畫
            _ShowScript.AddCoin = true;//要做加錢動作

            StartCoroutine(Show);
            AudioManager.inst.PlayAddSFX("SFX", 2);
            _ShowScript.UiShow.Clear();//清除UiShow資料
            yield return new WaitUntil(() => _ShowScript.CoinShow_Bool == true);
            _ResourceManager._Amr_WinShow.SetBool("ShowBool", false);//關閉動畫
            _SlotDate.Win_Coin = 0;
            Debuger.Log("普盤表演結束");
            WinShowOk = true;
            Debuger.Log(" 表演中 ： WinShowOk :" + WinShowOk);

        }

    }
    #endregion

    #region Bonus盤表演
    /// <summary>
    /// 普盤表演
    /// </summary>
    /// <param name="_Ishow"></param>
    /// <param name="_IDate"></param>
    /// <returns></returns>
    public IEnumerator WinShow(int NowFreeCount)
    {
        if (_SlotDate.BonusWinCoin[NowFreeCount] != 0)
        {
            Debuger.Log("進入普盤表演");
            WinShowOk = false;
            Debuger.Log("表演中 ：WinShowOk" + WinShowOk);

            IEnumerator Show;
            Show = _ShowScript.ShinyShow(_ShowScript.BonusUiShow[NowFreeCount]);

            Debuger.Log("執行普盤表演的： _Ishow.ShinyShow");
            yield return StartCoroutine(Show);

            Show = _ShowScript.StartDrawLine(_ShowScript.BonusPrepareDrawline[NowFreeCount], _ShowScript.BonusDrawLineOk[NowFreeCount]);
            StartCoroutine(Show);

            Debuger.Log("執行普盤表演的： _Ishow.StartDrawLine");
            yield return new WaitUntil(() => _ShowScript.BonusDrawLineOk[NowFreeCount].Contains(true) == false);
            Debuger.Log(" WinShow :" + !_ShowScript.BonusDrawLineOk[NowFreeCount].Contains(true));

            Show = _ShowScript.CoinShow(_SlotDate.BonusWinCoin[NowFreeCount]);

            _ShowScript.AddCoin = true;//要做加錢動作
            Debuger.Log("執行普盤表演的： _Ishow.CoinShow");
            _ShowScript.BonusUiShow[NowFreeCount].Clear();//清除閃爍 資料
            _ResourceManager._Amr_WinShow.SetBool("ShowBool", true);//開啟動畫
            AudioManager.inst.PlayAddSFX("SFX", 2);
            StartCoroutine(Show);

            yield return new WaitUntil(() => _ShowScript.CoinShow_Bool == true);
            _ResourceManager._Amr_WinShow.SetBool("ShowBool", false);//關閉動畫
            _SlotDate.BonusWinCoin[NowFreeCount] = 0;
            Debuger.Log("普盤表演結束");
            WinShowOk = true;
            Debuger.Log(" 表演中 ： WinShowOk :" + WinShowOk);

        }

    }
    #endregion

    #region 最後的表演
    /// <summary>
    /// 最後得表演
    /// </summary>
    /// <param name="_Ishow"></param>
    /// <param name="_IDate"></param>
    /// <returns></returns>
    public IEnumerator Coin_EndShow()
    {
        if (_SlotDate.Total_BonusWinCoin != 0)
        {
            WinShowOk = false;
            Debuger.Log(" 開始Bonus最後的金錢表演 ： WinShowOk :" + WinShowOk);

            IEnumerator Show;
            Show = _ShowScript.CoinShow(_SlotDate.Total_BonusWinCoin);

            _ShowScript.AddCoin = false;//不要做加錢動作 單純展示
            AudioManager.inst.PlayAddSFX("SFX", 2);
            StartCoroutine(Show);
            _ResourceManager._BonusWinBackSprite.gameObject.SetActive(true);

            yield return new WaitUntil(() => _ShowScript.CoinShow_Bool == true);

            _ResourceManager._BonusWinBackSprite.gameObject.SetActive(false);
            _SlotDate.Total_BonusWinCoin = 0;
            WinShowOk = true;
            Debuger.Log(" 結束Bonus最後的金錢表演 ： WinShowOk :" + WinShowOk);
        }


    }

    #endregion




    /*
     * 盤面表演生成需要切開
     * 
     * 這裡調用SeverScript 將最終盤面生成
     * 生成後的資料會丟給Date
     * Date只有一個盤面（SlotGrid）
     * 
     * 透過 將 Date中的 Json陣列 轉換回來放近 Date的 SlotGrid 來更改盤面內容
     */



    #region 盤面 表演物件生成
    /// <summary>
    /// 盤面,表演物件 生成 
    /// </summary>
    /// <param name="_IDate"></param>
    /// <param name="_IUIMethod"></param>
    /// <param name="_IDateEvent"></param>
    /// <param name="_Ishow"></param>
    public void GridCreat_Event(SlotGrid CommonGrid, SlotGrid BonusGrid)
    {
        _SlotDate.CurrentReel = 0;
        _SlotDate.BonusCount = 0;//預設大獎中獎圖數為0

        if (_UIControlMethod.StAddBonusDate)//如果有按下 直接中大獎按鈕
        {

            _SlotDate.Add_BonusDate(_UIControlMethod.StAddBonusDate, CommonGrid);//將普盤的盤面資料 灌成 有中Bonus的盤面

        }
        else
        {
            CommonGrid.CreatGrid();//普盤 生成盤面
           
        }

        _SlotDate.Win_Coin = _SlotDate.WInChack(CommonGrid._grids[CommonGrid._girdcount - 1]);//普盤地0個盤面對獎
        Debuger.Log("_SlotDate.Win_Coin :" + _SlotDate.Win_Coin);
        Debuger.Log("中了多少條線 ： " + _SlotDate.Temp.Count);

        for (int i = 0; i < _SlotDate.Temp.Count; i++)//取得_SlotDate.Temp.Count（將中了哪幾條線拿來使用 ）
        {

           _ShowScript.ObjectPool(_SlotDate.Temp[i], _ResourceManager._LineRenderOutPool, _ShowScript.PrepareDrawLine);
            _ShowScript.ListShiny(_SlotDate.Temp[i], _ShowScript.UiShow);

        }

        _SlotDate.Temp.Clear();
        Debuger.Log(string.Format("普盤預制物生成完畢： _Ishow.LineRenderOutPool名字：{0} , _Ishow.PrepareDrawLine 內容數量：{1}", _ResourceManager._LineRenderOutPool.gameObject.name, _ShowScript.PrepareDrawLine.Count));
        Debuger.Log(string.Format("單圖閃爍已放進陣列等待表演：_Ishow.UiShow : 當前數量 {0} 張圖", _ShowScript.UiShow.Count));
        Debuger.Log(string.Format("Bonus圖在盤面的數量：{0}",_SlotDate.BonusCount));
        if (_SlotDate.BonusCount == FreeGameCount)//如果有Bonus獎
        {

           
            BonusGrid.CreatGrid();//Bonus盤 生成盤面

            for (int i = 0; i < BonusGrid._girdcount; i++) //Bonus 各個盤面的兌獎
            {

                _SlotDate.BonusWinCoin[i] = _SlotDate.WInChack(BonusGrid._grids[i]);
                Debuger.Log(string.Format(" _IDate.BonusWinCoin[{0}] : {1}", i, _SlotDate.Win_Coin));

                for (int j = 0; j < _SlotDate.Temp.Count; j++)
                {

                    _ShowScript.ObjectPool(_SlotDate.Temp[j], _ResourceManager._BonusLineRenderOutPool.transform.GetChild(i), _ShowScript.BonusPrepareDrawline[i]);
                    _ShowScript.ListShiny(_SlotDate.Temp[j], _ShowScript.BonusUiShow[i]);

                }

                _SlotDate.Temp.Clear();
                Debuger.Log(string.Format("Bonus盤預制物生成完畢： _Ishow.BonusLineRenderOutPool.transform.GetChild(i)：{0} , _Ishow.BonusPrepareDrawline[{1}] 內容數量：{2}", _ResourceManager._BonusLineRenderOutPool.transform.GetChild(i).name, i, _ShowScript.BonusPrepareDrawline[i].Count));
                Debuger.Log(string.Format("Bonus單圖閃爍已放進陣列等待表演：_Ishow.BonusUiShow[{0}] : 當前數量 {1} 張圖", i, _ShowScript.BonusUiShow[i].Count));

            }

            for (int i = 0; i < _SlotDate.BonusWinCoin.Count; i++)//將各個盤面的Bonus獎的中獎金額總和起來
            {

                _SlotDate.Total_BonusWinCoin += _SlotDate.BonusWinCoin[i];

            }

        }


        _SlotDate.CycleCount += 1;
        _SlotDate.AutoSurplus -= 1;
        if (_SlotDate.AutoSurplus<1)
        {
            _SlotDate.AutoSurplus = 1;
        }
        _ResourceManager._Auto_text.text = _SlotDate.AutoSurplus.ToString();


        _SlotDate.DateSave(CommonGrid, BonusGrid, FreeGameCount);

        St_Roll = true;

        if (_SlotDate.BonusCount >= 2)
        {
            CurrentReel_B = true;
        }

        Debuger.Log("是否開始滾動：" + Start_Slot);

    }
    #endregion

    #region UPdate執行內容
    public void UpdateMethod(SlotGrid CommonGrid, SlotGrid BonusGrid)
    {

        if (Input.GetMouseButtonDown(0))
        {

            Cursor.SetCursor(MIcon[1], new Vector2(0, 5), CursorMode.Auto);

            //Debug.Log("Mouse : Down");

        }

        if (Input.GetMouseButtonUp(0))
        {

            Cursor.SetCursor(MIcon[0], new Vector2(0, 5), CursorMode.Auto);

            //Debug.Log("Mouse : Up");

        }



        if (CurrentReel_B && _Reel_Moves[_SlotDate.CurrentReel].tempi == Loopcount && _SlotDate.CurrentReel != Slot_mantissa && _SlotDate.CurrentReel != 0)
        {
            CurrentReel_B = false;
            int CurrentCount = _SlotDate.CurrentReel + 1;

            AudioManager.inst.BGMReset(0.05f);
            AudioManager.inst.PlayAddSFX("SFX", 0);

            for (int i = CurrentCount; i < _Reel_Moves.Length; i++)
            {
                _Reel_Moves[i].transform.parent.GetChild(1).gameObject.SetActive(true);
                _Reel_Moves[i].Roolcount = Loopcount * 4;
            }

        }


        GetAnimatiorStayInfo();



        if (B_Slot_timeOut == true && _Reel_Moves[Slot_mantissa].tempi == _Reel_Moves[Slot_mantissa].Roolcount)
        {

            Debuger.Log("執行 Co_Slot_timeOut ");
            B_Slot_timeOut = false;
            _Slot_timeOut = Co_Slot_timeOut(CommonGrid, BonusGrid);
            StartCoroutine(_Slot_timeOut);

        }

        if (!Start_Slot)
        {

            _UIControlMethod.Button_PressAndHold();
            _UIControlMethod.Button_Reduce_Press();

        }

        if (UseBonus)
        {

            _ShowScript.RecoverComply(_ResourceManager._BonusLineRenderOutPool.GetChild(NowFreeCount), _ShowScript.BonusPrepareDrawline[NowFreeCount], _ShowScript.BonusDrawLineOk[NowFreeCount]);//BONUS的LineRender回收

        }

        _ShowScript.RecoverComply(_ResourceManager._LineRenderOutPool, _ShowScript.PrepareDrawLine, _ShowScript.DrawLineOK);//普通盤的LineRender回收

        if (StEndShow)
        {
            //if (_IShow.VideoImage.gameObject.activeInHierarchy)//判斷播影片是否開啟
            //{
            if (_ResourceManager._VideoImage.GetComponent<RawImage>().enabled==true)//判斷播影片是否開啟
            {
                Debuger.Log("VideoImage.gameObject.active :" + _ResourceManager._VideoImage.gameObject.activeInHierarchy);

                if (_ResourceManager._EndShowPlayer.frame != 0 && _ResourceManager._EndShowPlayer.frameCount != 0)
                {

                    Debuger.Log("_IShow.EndShowPlayer.frame :" + _ResourceManager._EndShowPlayer.frame);
                    Debuger.Log("_IShow.EndShowPlayer.frameCount :" + _ResourceManager._EndShowPlayer.frameCount);

                   

                    if (_ResourceManager._EndShowPlayer.frame >= (long)_ResourceManager._EndShowPlayer.frameCount - 2)
                    {


                        _ResourceManager._EndShowPlayer.Pause();//暫停影片
                        Debuger.Log("暫停影片");

                        _ResourceManager._VideoImage.GetComponent<RawImage>().enabled = false;


                       // _IShow.VideoImage.gameObject.SetActive(false);//關掉RawImage

                        StEndShow = false;
                        _SlotDate.Total_BonusWinCoin = 0;


                    }

                }

            }
           // }

        }

    }
    #endregion

    #region 取得動畫的StateInfo
    /// <summary>
    /// 取得動畫的StateInfo
    /// </summary>
    public void GetAnimatiorStayInfo()
    {

        if (BonusStateInfo_B)
        {

            Debuger.Log("BonusStateInfo_B :" + BonusStateInfo_B);
            BonusStateInfo = TempAnimator.GetCurrentAnimatorStateInfo(0);//階層為0
            Debuger.Log("BonusStateInfo.normalizedTime " + BonusStateInfo.normalizedTime);

            if (BonusStateInfo.normalizedTime >= 1.0f && BonusStateInfo.IsName(Animator_Name))
            {
                BonusStateInfo_B = false;

                TempAnimator.SetBool("ShowBool", false);
                Debuger.Log("影片播放完畢");
            }

        }

    }
    #endregion

    #region  Options  按鈕事件
    public void Options_Yes()
    {


        Debuger.Log("有無遊戲資料 ：" + PlayerPrefs.HasKey("遊戲資料"));


        if (PlayerPrefs.HasKey("遊戲資料"))
        {

            string GetDate;
            GetDate = PlayerPrefs.GetString("遊戲資料");
            Debuger.Log("儲存的資料內容 ： " + GetDate);
            _SlotDate.Slot_SeverDate = JsonUtility.FromJson<Slot_SeveDate>(GetDate);

            int All_Coin;
            All_Coin = _SlotDate.Slot_SeverDate.Player_Coin + _SlotDate.Slot_SeverDate.BonusCoin + _SlotDate.Slot_SeverDate.Win_Coin;
            _SlotDate.Bet_Coin = _SlotDate.Slot_SeverDate.BetCoin;

            if (_SlotDate.Slot_SeverDate.Auto_PlayerSet > _SlotDate.Slot_SeverDate.Auto_NotYet)
            {

                //_SlotDate.AutoSurplus = _SlotDate.Slot_SeverDate.Auto_HasRollcount - 1;

                //_SlotDate.CycleCount = _SlotDate.Slot_SeverDate.Auto_NotYet + 1;

                _SlotDate.AutoSurplus = _SlotDate.Slot_SeverDate.Auto_NotYet;
                _SlotDate.CycleCount = _SlotDate.Slot_SeverDate.Auto_HasRollcount;
                _SlotDate.AutoCount = _SlotDate.Slot_SeverDate.Auto_PlayerSet;

                Debug.Log($"讀取紀錄：未循環次數:{_SlotDate.AutoSurplus},以循環次數：{_SlotDate.CycleCount},玩家設定次數:{_SlotDate.AutoCount}");

            }
            else
            {
                Debug.Log($"else:  __ SlotDate Auto:{_SlotDate.AutoCount}, playerSte:{_SlotDate.Slot_SeverDate.Auto_PlayerSet} , AutoSurplus:{_SlotDate.AutoSurplus}, AutoNotyet :{_SlotDate.Slot_SeverDate.Auto_NotYet}, CycleCount : {_SlotDate.CycleCount} , AutoHasRpll:{_SlotDate.Slot_SeverDate.Auto_HasRollcount}");
                _SlotDate.AutoSurplus = 1;
                _SlotDate.CycleCount = 0;

            }
            _SlotDate.AutoCount = _SlotDate.Slot_SeverDate.Auto_PlayerSet;
            _SlotDate.PlayerCoin = All_Coin;
            _ResourceManager._PlayerCoin_Text.text = "Money:" + _SlotDate.PlayerCoin;
            _ResourceManager._BetMenu_Text.text = _SlotDate.Bet_Coin.ToString();
            _ResourceManager._Bet_Text.text = _SlotDate.Bet_Coin.ToString();
            _ResourceManager._Auto_text.text = _SlotDate.AutoSurplus.ToString();
            for (int i = 0; i < _SlotDate.Slot_SeverDate.Data.Count; i++)
            {


                for (int j = 0; j < _SlotDate.Slot_SeverDate.Data[i]._IntCount.Count; j++)
                {
                    _Reel_Moves[i].Self.transform.GetChild(j).GetComponent<Image>().sprite = _ResourceManager.Sprite_Pool[_SlotDate.Slot_SeverDate.Data[i]._IntCount[j]];

                    Debuger.Log("_SlotDate._Slot_SeverDate.Data[i]._IntCount[j] :" + _SlotDate.Slot_SeverDate.Data[i]._IntCount[j]);

                }

            }

            _ResourceManager._Options.SetActive(false);

        }


    }

    public void Options_No()
    {

        PlayerPrefs.DeleteKey("遊戲資料");
        Debuger.Log("是否有儲存的資料 ：" + PlayerPrefs.HasKey("遊戲資料"));
        _SlotDate.Initialization_Slot_Sprite();
        _ResourceManager._Options.SetActive(false);


    }
    #endregion

    #region 執行本地與雲端板號確認並載入Bundle近場景
    /// <summary>
    /// 執行本地與雲端板號確認並載入Bundle近場景
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckBundleLoad()
    {

        yield return StartCoroutine(_ResourceManager._CSAComper.CheckAssetBundle());

        _ResourceManager.LoadClientAssetBundel(_Reel_Moves);


    }
    #endregion

    #region 平台偵測
    /// <summary>
    /// 平台偵測
    /// </summary>
    /// <param name="_IuiMethod"></param>
    public void Operational()
    {
        #if UNITY_STANDALONE_OSX
                GetDeviceInformation();
                Debuger.Log("UNITY_STANDALONE_OSX");
        #endif

        #if UNITY_STANDALONE_WIN
                    GetDeviceInformation();
                    Debuger.Log("Stand Alone Windows");
        #endif


        #if UNITY_ANDROID
                    GetDeviceInformation();
                    Debuger.Log("Android");
        #endif

        #if UNITY_IOS
                    GetDeviceInformation();
                    Debuger.Log("Iphone");
        #endif

    }

    #region 詳細設備資訊
    /// <summary>
    /// 詳細設備資訊
    /// </summary>
    public void GetDeviceInformation()
    {

        _ResourceManager._Img_Operational.transform.GetChild(0).GetComponent<Text>().text = "設備型號 ：" + SystemInfo.deviceModel;
        _ResourceManager._Img_Operational.transform.GetChild(1).GetComponent<Text>().text = "設備名稱 ：" + SystemInfo.deviceName;
        _ResourceManager._Img_Operational.transform.GetChild(2).GetComponent<Text>().text = "操作系統 ：" + SystemInfo.operatingSystem;
        _ResourceManager._Img_Operational.transform.GetChild(3).GetComponent<Text>().text = "設備類型 ：" + SystemInfo.deviceType;
        _ResourceManager._Img_Operational.transform.GetChild(4).GetComponent<Text>().text = "顯存大小（單位：ＭＢ） ：" + SystemInfo.graphicsMemorySize;
        _ResourceManager._Img_Operational.transform.GetChild(5).GetComponent<Text>().text = "系統內存大小(單位：ＭＢ) ：" + SystemInfo.systemMemorySize;

    }
    #endregion

    #endregion

    #region 運行平台偵測
    /// <summary>
    /// Bundle載入平台檢測
    /// </summary>
    public void platformdetection()
    {

        #if UNITY_STANDALONE_OSX

                LoadPath = "https://drive.google.com/uc?export=download&id=1U7fVXWpwPfuujRrtuZQeRj6eLrz2hjEX";
                Debug.Log("UNITY_STANDALONE_OSX");

        #endif



        #if UNITY_STANDALONE_WIN

                LoadPath = "https://drive.google.com/uc?export=download&id=1z0IA0P4I-I7O8qMAV-f15J9Unw7FtXlA";
                Debug.Log("Stand Alone Windows");

        #endif


        #if UNITY_ANDROID

                 LoadPath = "https://drive.google.com/uc?export=download&id=1wKcVbcYCafNovYdaVA_--d4N0GP_u2pE";
                Debug.Log("Android");

        #endif


        #if UNITY_IOS


                 LoadPath = "https://drive.google.com/uc?export=download&id=17zOwWE_6m9WkRVRGlvoFVkfKAXaLZQHa";
                Debug.Log("Iphone");


        #endif

    }
    #endregion




}
