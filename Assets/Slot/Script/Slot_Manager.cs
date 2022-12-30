using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class Slot_Manager : MonoBehaviour
{
    
    public Slot_data _SlotDate;//玩家金錢及盤面資料腳本
    public Reel_Move[] _Reel_Moves;
    public ShowScript _ShowScript;
    public UIControlMethod _UIControlMethod;
    public PlayerControl _PlayerControl;
   


    public Button_EventTrigger _ButtonPlus_EventTrigger;
    public Button_EventTrigger _ButtonReduce_EventTrigger;
    public Texture2D[] MIcon;
    public float RollSpeed;//輪條移動速度
    public int Loopcount;//循環次數
    public int Slot_mantissa;//輪條陣列內最後的數
    public int FreeGameCount;//小遊戲要玩的次數
    public int NowFreeCount;//當前遊玩次數
    public bool Start_Slot;
    bool BonusStateInfo_B;
    bool B_Slot_timeOut;//讓_Slot_timeOut只會執行一次
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


    void Start()
    {

        Slot_Initialization(_SlotDate,_SlotDate,_ShowScript,_UIControlMethod);
        SlotGridCreat(_Reel_Moves);
        Debug.Log("_ShowScript.BonusPrepareDrawline.Count" + _ShowScript.BonusPrepareDrawline.Count);
        for (int i=0;i< _ShowScript.BonusPrepareDrawline.Count;i++)
        {

            if (_ShowScript.BonusPrepareDrawline[i] != null)
            {

                Debug.Log(string.Format("_ShowScript.BonusPrepareDrawline[{0}], 不是空得,當前數量：{1}", i, _ShowScript.BonusPrepareDrawline[i].Count));

            }
            else
            {

                Debug.Log(string.Format("_ShowScript.BonusPrepareDrawline[{0}], !是空得!",i));

            }

        }

    }

    [System.Obsolete]
    void Update()
    {



        UpdateMethod(_ShowScript, _SlotDate, _SlotDate,_UIControlMethod,_Reel_Moves,CommonGrid,BonusGrid);



    }

    #region 資料初始化
    /// <summary>
    /// 資料初始化
    /// </summary>

    public void Slot_Initialization(IDate _IDate,IDateEvent _IDateEvent,IShow _Ishow,IUIControlMethod _IuiMethod)
    {
        FreeGameCount = 3;
        B_Slot_timeOut = true;
        WinShowOk = true;
        StEndShow = false;
        UseBonus = false;
        StCoShow = true;
        St_Roll = false;
        Start_Slot = false;

        _SlotDate.Init( _Reel_Moves);//SlotDate 給變數的 Interface 指定是誰 
        _UIControlMethod.UIControlInit(_IDate,_ButtonPlus_EventTrigger,_ButtonReduce_EventTrigger);//UIControlMethod 給變數的 Interface 指定是誰 
        _ShowScript.Init(_IDate,_IuiMethod,_Reel_Moves);//ShowScript 給變數的 Interface 指定是誰 
        _PlayerControl.PlayerControl_Init(_IuiMethod,this);//PlayerControl 給變數的 Interface 指定是誰

        //指定PlayerControl按鈕要執行的事件
        _PlayerControl.startGameMethod = Del_startGame;
        _PlayerControl.Option_Yes = Del_Options_yes;
        _PlayerControl.Option_No = Del_Options_No;

        for (int i = 0; i < _Reel_Moves.Length; i++)// 各個_Reel_Moves 的變數設定 
        {

            _Reel_Moves[i].Init(Loopcount, _IDate.SpritePool, RollSpeed);

        }

        for (int i=0;i<_Reel_Moves.Length;i++)
        {
            _SlotDate._Date.Add(new intCount());

            for (int j=0;j<_Reel_Moves[i].transform.childCount;j++)
            {

                _SlotDate._Date[i]._IntCount.Add(0);

            }

            _Reel_Moves[i].Sprites = _IDate.SpritePool;

        }
       
        _IDate.BonusWinCoin = new List<int>();
        _Ishow.BonusUiShow = new List<List<Transform>>();
        _Ishow.BonusPrepareDrawline = new List<List<GameObject>>();
        _Ishow.BonusDrawLineOk = new List<List<bool>>();
        _Ishow.UiShow = new List<Transform>();
        _Ishow.DrawLineOK = new List<bool>();
        _Ishow.PrepareDrawLine = new List<GameObject>();

        for (int i = 0; i < FreeGameCount; i++)
        {

            _IDate.BonusWinCoin.Add(0);
            _Ishow.BonusUiShow.Add(new List<Transform>());
            _Ishow.BonusPrepareDrawline.Add(new List<GameObject>());
            _Ishow.BonusDrawLineOk.Add(new List<bool>());

        }

        _Ishow.ObjectPoolInitialization();//物件池 預置物生成
       
        _IDate.CycleCount = 1;
        _IDate.PlayerCoin = 1099;
        _IDate.AutoCount = 1;
        Debug.Log("Player_coin"+_IDate.PlayerCoin);
        _IuiMethod.PlayerCoin_Text.text = "Money :" + _IDate.PlayerCoin.ToString();
        Slot_mantissa = _Reel_Moves.Length - 1;//紀錄陣列的最大編號

        if (!PlayerPrefs.HasKey("遊戲資料"))
        {

            _IDateEvent.Initialization_Slot_Sprite();

        }
        else
        {

            _IuiMethod.GetDateSave();

        }


        Debug.Log("是否開始滾動：" + Start_Slot);

    }
    #endregion

    #region 盤面物件創造
    /// <summary>
    /// 盤面物件創造
    /// </summary>
    public void SlotGridCreat(IMove[] _ReelMoves)
    {

        int Reelcount;
        Reelcount = _ReelMoves.Length;
        List<int> ii = new List<int>();

        for (int i = 0; i < _ReelMoves.Length; i++)
        {
            int Chcount = _ReelMoves[i].Self.transform.childCount;
            ii.Add(Chcount);
            //Debug.Log(ii);
        }

        CommonGrid = new SlotGrid(1, Reelcount, ii, _SlotDate.Generate_Date_Sprite);
        BonusGrid = new SlotGrid(FreeGameCount, Reelcount, ii, _SlotDate.GenerateBonusDate);

    }
    #endregion

    #region 讓各輪條開始轉動的開關
    /// <summary>
    /// 讓各輪條開始轉動的開關
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoAll_Slot_Roll(GridIntS _Grints, IMove[] _ReelMoves)
    {
        if (St_Roll)
        {

            Debug.Log("CoAll_Slot_Roll() :St_Roll 開始轉動");

            for (int i = 0; i < _ReelMoves.Length; i++)
            {

                for (int j=0;j< _ReelMoves[i].Self.transform.childCount;j++)
                {

                    _ReelMoves[i].ChangeSprite[j] = (int)_Grints._Grids[i]._GridInt[j];

                }

                // Debug.Log("CoAll_Slot_Roll() :迴圈");
                _ReelMoves[i].strool = true;
                Debug.Log(string.Format("輪條編號：{0},是否開啟{1}", i, _ReelMoves[i].strool));
                yield return new WaitForSeconds(0.5f);

            }

            St_Roll = false;
        }

    }
    #endregion

    #region 按鈕長按(+)
    /// <summary>
    ///按鈕長按 
    /// </summary>
    public void Button_PressAndHold(IDate _date, IUIControlMethod _IUIMH)
    {

        if (_ButtonPlus_EventTrigger.b)
        {
            int tx;
            int coin_temp;
            tx =  _date.PlayerCoin % 100;//總金額除100的餘數
            coin_temp = _date.PlayerCoin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
            if (_date.Bet_Coin < coin_temp)
            {

                _date.Bet_Coin = _date.Bet_Coin + (_ButtonPlus_EventTrigger.Return_Valu * 100);
                _ButtonPlus_EventTrigger.b = false;
                Debug.Log(_date.Bet_Coin);
                _IUIMH.Bet_Text.text = _date.Bet_Coin.ToString();
                _IUIMH.BetMenu_Text.text = _IUIMH.Bet_Text.text;

            }

        }

    }
    #endregion

    #region 按鈕長按（-）
    /// <summary>
    /// 按鈕長按（-）
    /// </summary>
    public void Button_Reduce_Press(IDate _date,IUIControlMethod _IUIMH)
    {

        if (_ButtonReduce_EventTrigger.b)
        {

            if (_date.Bet_Coin >= 100)
            {

                _date.Bet_Coin = _date.Bet_Coin - (_ButtonReduce_EventTrigger.Return_Valu * 100);
                _ButtonReduce_EventTrigger.b = false;
                Debug.Log(_date.Bet_Coin);
                _IUIMH.Bet_Text.text = _date.Bet_Coin.ToString();
                _IUIMH.BetMenu_Text.text = _IUIMH.Bet_Text.text;

            }

        }

    }
    #endregion

    #region 開始按鈕要執行得內容
    public void StartGame(IDate _IDate,IUIControlMethod _IUIMethod, IDateEvent _IDateEvent,IShow _Ishow,IMove[] _ReelMoves, SlotGrid CommonGrid,SlotGrid BonusGrid)
    {
        Debug.Log("執行StartGame");

        if (Start_Slot == false && _IDate.Bet_Coin != 0 && _IDate.PlayerCoin >= _IDate.LeastBetCount)//如果Start_Slot == false（代表按鈕有作用 現在並無作動） 而且 下注金額不等於0 而且  玩家金額要大於最低押注金額
        {

            int Autoi;
            Autoi = int.Parse(_IUIMethod.Auto_text.text);
            _IDate.AutoSurplus = Autoi;
            _IDate.PlayerCoin -= _IDate.Bet_Coin;
            _IUIMethod.PlayerCoin_Text.text = "Money:" + _IDate.PlayerCoin;

            if (_IDate.AutoSurplus > 1)
            {

                _IDate.AutoCount = _IDate.AutoSurplus;

            }

            GridCreat_Event(_IDate,_IUIMethod,_IDateEvent,_Ishow,CommonGrid,BonusGrid);
            _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0], _ReelMoves);
            StartCoroutine(_CoAll_Slot_Roll);
            Start_Slot = true;
            Debug.Log("是否開始滾動：" + Start_Slot);

        }

    }
    #endregion

    #region （新） 判斷滾輪次數是否達成條件  沒達成條件就繼續滾
    /// <summary>
    /// 當Start_Slot為true時而且轉動次數到達上限 初始化開始按鈕跟輪條開始的bool
    /// </summary>
  
    public IEnumerator Co_Slot_timeOut(IDate _IDate, IShow _Ishow, IDateEvent _IDateEvent, IUIControlMethod _IUIMethod,IMove[] _ReelMoves, SlotGrid CommonGrid, SlotGrid BonusGrid)
    {
        
        if (_ReelMoves[Slot_mantissa].tempi == Loopcount)//如果最後一個輪條達到滾動次數
        {
            Debug.Log("啟動Co_Slot_timeOut");

            if (StCoShow)
            {
                StCoShow = false;
                _Show = WinShow(_Ishow, _IDate);
                StartCoroutine(_Show);
                Debug.Log("StART Win SHow");
               
            }

            yield return new WaitUntil(()=> WinShowOk == true);
            Debug.Log("WinShowOk :" + WinShowOk);

            if (_IDate.BonusCount== FreeGameCount && NowFreeCount<FreeGameCount)//如果_IDate.BonusCount 等於設定的 免費遊戲數
            {

                Debug.Log("開啟Bonus開場表演");
                TempAnimator = _Ishow.BonusAnimator;
                Animator_Name = "BonusShow2";
                _Ishow.BonusAnimator.SetBool("ShowBool",true);
                BonusStateInfo_B = true;
                Debug.Log("BonusStateInfo.normalizedTime :" + BonusStateInfo.normalizedTime);
                yield return new WaitUntil(() => _Ishow.BonusAnimator.GetBool("ShowBool") == false);
                Debug.Log("-------------- : Bonus 動畫是否播放完畢 WaitUntil: -----------------" + _Ishow.BonusAnimator.GetBool("ShowBool"));

                for (int i=0;i<FreeGameCount;i++)
                {

                    for (int j = 0; j < _ReelMoves.Length; j++)//輪條轉動次數初始化
                    {
                        _ReelMoves[j].tempi = 0;

                        _ReelMoves[j].Date_Temp = 0;

                        Debug.Log(string.Format("輪調{0}內滾動次數{1}", j, _ReelMoves[j].tempi));

                    }

                    StCoShow = true;
                    UseBonus = true;
                    St_Roll = true;
                    _CoAll_Slot_Roll = CoAll_Slot_Roll(BonusGrid._grids[NowFreeCount], _ReelMoves);
                    StartCoroutine(_CoAll_Slot_Roll);
                    yield return new WaitUntil(()=> _ReelMoves[Slot_mantissa].tempi == Loopcount);

                    if (StCoShow)
                    {
                        StCoShow = false;
                        _Show = WinShow(_Ishow, _IDate,NowFreeCount);
                        StartCoroutine(_Show);
                        Debug.Log("StART Win SHow");
                        yield return new WaitUntil(() => WinShowOk == true);
                        NowFreeCount++;

                    }

                }

                UseBonus = false;
                StCoShow = true;

                if (StCoShow)
                {
                    StCoShow = false;
                    _Show = Coin_EndShow(_Ishow, _IDate);
                    StartCoroutine(_Show);
                    Debug.Log("StART Win SHow");

                }

                yield return new WaitUntil(() => WinShowOk == true);//等待金錢動畫結束
                TempAnimator = _Ishow.BonusEndShow;
                Animator_Name = "BonusEndShow";
                BonusStateInfo_B = true;
                _Ishow.BonusEndShow.SetBool("ShowBool", true);//開啟揮拳動畫
                yield return new WaitUntil(() => _Ishow.BonusEndShow.GetBool("ShowBool") == false);//等待揮拳動畫結束
                _Ishow.VideoImage.gameObject.SetActive(true);
                _Ishow.VideoImage.GetComponent<VideoPlayer>().Play();
                yield return new WaitForSeconds(0.5f);
                _Ishow.VideoImage.GetComponent<RawImage>().enabled = true;
                StEndShow = true;//開啟最後得表演
                StCoShow = true;

            }

            if (_IDate.CycleCount < _IDate.AutoCount && _IDate.PlayerCoin > _IDate.Bet_Coin)//循環次數未到 而且 玩家金額不小於下注金額
            {

                NowFreeCount=0;
                Debug.Log("-----------------------------開始轉動-------------------------");

                for (int i = 0; i < _ReelMoves.Length; i++)//輪條轉動次數初始化
                {
                    _ReelMoves[i].tempi = 0;
                    _ReelMoves[i].Date_Temp = 0;
                    Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _ReelMoves[i].tempi));

                }


                if (_ReelMoves[Slot_mantissa].tempi == 0) //這裡重新生成盤面資料 兌獎 設定預制物 
                {

                    GridCreat_Event(_IDate, _IUIMethod, _IDateEvent, _Ishow,CommonGrid,BonusGrid);
                    B_Slot_timeOut = true;
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Wait");
                    _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0], _ReelMoves);
                    StartCoroutine(_CoAll_Slot_Roll);
                    _IDate.CycleCount += 1;
                    _IDate.AutoSurplus -= 1;
                    _IUIMethod.Auto_text.text = _IDate.AutoSurplus.ToString();
                    _IDate.PlayerCoin -= _IDate.Bet_Coin;
                    _IUIMethod.PlayerCoin_Text.text = "Money:" + _IDate.PlayerCoin;
                    Debug.Log( _IDate.CycleCount + "已循環次數");
                    //Debug.Log("Win_Mpney_Temp" + Win_Money_Temp);
                    StCoShow = true;

                }

            }

            else
            {

                Debug.Log("各輪盤滾動完畢");
                Debug.Log("是否開始滾動：" + Start_Slot);
                _IDate.AutoCount = 1;

                for (int i = 0; i < _ReelMoves.Length; i++)
                {
                    _ReelMoves[i].tempi = 0;

                    _ReelMoves[i].Date_Temp = 0;

                    Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _ReelMoves[i].tempi));

                }

                Start_Slot = false;
                St_Roll = true;
                StCoShow = true;
                NowFreeCount=0;
                _IDate.CycleCount = 1;
                _IDate.AutoSurplus = 0;
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
    public IEnumerator WinShow(IShow _Ishow,IDate _IDate)
    {
        if (_IDate.Win_Coin!=0)
        {

            Debug.Log("進入普盤表演");
            WinShowOk = false;
            Debug.Log("表演中 ：WinShowOk" + WinShowOk);

            IEnumerator Show;
            Show = _Ishow.ShinyShow(_Ishow.UiShow);
            
            Debug.Log("執行普盤表演的： _Ishow.ShinyShow");
            yield return StartCoroutine(Show);

            Show = _Ishow.StartDrawLine(_Ishow.PrepareDrawLine, _Ishow.DrawLineOK);
            StartCoroutine(Show);

            Debug.Log("執行普盤表演的： _Ishow.StartDrawLine");
            yield return new WaitUntil(() => _Ishow.DrawLineOK.Contains(true)==false);
            Debug.Log(" WinShow :" + !_Ishow.DrawLineOK.Contains(true));

            Show = _Ishow.CoinShow(_IDate.Win_Coin);

            Debug.Log("執行普盤表演的： _Ishow.CoinShow");
            _Ishow.Amr_WinShow.SetBool("ShowBool",true);//開啟動畫
            _Ishow.AddCoin = true;//要做加錢動作

            StartCoroutine(Show);

            _Ishow.UiShow.Clear();//清除UiShow資料
            yield return new WaitUntil(() => _Ishow.CoinShow_Bool==true);
            _Ishow.Amr_WinShow.SetBool("ShowBool", false);//關閉動畫
            _IDate.Win_Coin = 0;
            Debug.Log("普盤表演結束");
            WinShowOk = true;
            Debug.Log(" 表演中 ： WinShowOk :" + WinShowOk);

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
    public IEnumerator WinShow(IShow _Ishow, IDate _IDate, int NowFreeCount)
    {
        if (_IDate.BonusWinCoin[NowFreeCount] != 0)
        {
            Debug.Log("進入普盤表演");
            WinShowOk = false;
            Debug.Log("表演中 ：WinShowOk" + WinShowOk);

            IEnumerator Show;
            Show = _Ishow.ShinyShow(_Ishow.BonusUiShow[NowFreeCount]);

            Debug.Log("執行普盤表演的： _Ishow.ShinyShow");
            yield return StartCoroutine(Show);

            Show = _Ishow.StartDrawLine(_Ishow.BonusPrepareDrawline[NowFreeCount], _Ishow.BonusDrawLineOk[NowFreeCount]);
            StartCoroutine(Show);

            Debug.Log("執行普盤表演的： _Ishow.StartDrawLine");
            yield return new WaitUntil(() => _Ishow.BonusDrawLineOk[NowFreeCount].Contains(true) == false);
            Debug.Log(" WinShow :" + !_Ishow.BonusDrawLineOk[NowFreeCount].Contains(true));

            Show = _Ishow.CoinShow(_IDate.BonusWinCoin[NowFreeCount]);

            _Ishow.AddCoin = true;//要做加錢動作
            Debug.Log("執行普盤表演的： _Ishow.CoinShow");
            _Ishow.BonusUiShow[NowFreeCount].Clear();//清除閃爍 資料
            _Ishow.Amr_WinShow.SetBool("ShowBool", true);//開啟動畫

            StartCoroutine(Show);

            yield return new WaitUntil(() => _Ishow.CoinShow_Bool == true);
            _Ishow.Amr_WinShow.SetBool("ShowBool", false);//關閉動畫
            _IDate.BonusWinCoin[NowFreeCount] = 0;
            Debug.Log("普盤表演結束");
            WinShowOk = true;
            Debug.Log(" 表演中 ： WinShowOk :" + WinShowOk);

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
    public IEnumerator Coin_EndShow(IShow _Ishow, IDate _IDate)
    {
        WinShowOk = false;
        Debug.Log(" 開始Bonus最後的金錢表演 ： WinShowOk :" + WinShowOk);

        IEnumerator Show;
        Show = _Ishow.CoinShow(_IDate.Total_BonusWinCoin);

        _Ishow.AddCoin = false;//不要做加錢動作 單純展示

        StartCoroutine(Show);
        _Ishow.BonusWinBackSprite.gameObject.SetActive(true);

        yield return new WaitUntil(() => _Ishow.CoinShow_Bool == true);

        _Ishow.BonusWinBackSprite.gameObject.SetActive(false);
        _IDate.Total_BonusWinCoin = 0;
        WinShowOk = true;
        Debug.Log(" 結束Bonus最後的金錢表演 ： WinShowOk :" + WinShowOk);

    }

    #endregion

    #region 盤面 表演物件生成
    /// <summary>
    /// 盤面,表演物件 生成 
    /// </summary>
    /// <param name="_IDate"></param>
    /// <param name="_IUIMethod"></param>
    /// <param name="_IDateEvent"></param>
    /// <param name="_Ishow"></param>
    public void GridCreat_Event(IDate _IDate,IUIControlMethod _IUIMethod, IDateEvent _IDateEvent, IShow _Ishow, SlotGrid CommonGrid,SlotGrid BonusGrid)
    {

        _IDate.BonusCount = 0;//預設大獎中獎圖數為0

        if (_IUIMethod.StAddBonusDate)//如果有按下 直接中大獎按鈕
        {

            _IDateEvent.Add_BonusDate(_IUIMethod.StAddBonusDate, CommonGrid);//將普盤的盤面資料 灌成 有中Bonus的盤面

        }
        else
        {
            CommonGrid.CreatGrid();
            //CommonGrid._GridMethod(CommonGrid);//普盤 生成盤面

        }

        _IDate.Win_Coin = _IDateEvent.WInChack(_IDate.PrizeDate, CommonGrid._grids[CommonGrid._girdcount - 1]);
        Debug.Log("_IDate.Win_Coin :" + _IDate.Win_Coin);

        for (int i = 0; i < _IDate.Temp.Count; i++)
        {

            _Ishow.ObjectPool( _IDate.Temp[i], _Ishow.LineRenderOutPool, _Ishow.PrepareDrawLine);
            _Ishow.ListShiny( _IDate.Temp[i], _Ishow.UiShow);

        }

        _IDate.Temp.Clear();
        Debug.Log(string.Format("普盤預制物生成完畢： _Ishow.LineRenderOutPool名字：{0} , _Ishow.PrepareDrawLine 內容數量：{1}", _Ishow.LineRenderOutPool.gameObject.name, _Ishow.PrepareDrawLine.Count));
        Debug.Log(string.Format("單圖閃爍已放進陣列等待表演：_Ishow.UiShow : 當前數量 {0} 張圖", _Ishow.UiShow.Count));

        if (_IDate.BonusCount == FreeGameCount)//如果有Bonus獎
        {

            // BonusGrid._GridMethod(BonusGrid);//Bonus盤 生成盤面
            BonusGrid.CreatGrid();

            for (int i = 0; i < BonusGrid._girdcount; i++) //Bonus 各個盤面的兌獎
            {

                _IDate.BonusWinCoin[i] = _IDateEvent.WInChack(_IDate.PrizeDate, BonusGrid._grids[i]);
                Debug.Log(string.Format(" _IDate.BonusWinCoin[{0}] : {1}", i, _IDate.Win_Coin));

                for (int j = 0; j < _IDate.Temp.Count; j++)
                {

                    _Ishow.ObjectPool( _IDate.Temp[j], _Ishow.BonusLineRenderOutPool.transform.GetChild(i), _Ishow.BonusPrepareDrawline[i]);
                    _Ishow.ListShiny( _IDate.Temp[j], _Ishow.BonusUiShow[i]);

                }

                _IDate.Temp.Clear();
                Debug.Log(string.Format("Bonus盤預制物生成完畢： _Ishow.BonusLineRenderOutPool.transform.GetChild(i)：{0} , _Ishow.BonusPrepareDrawline[{1}] 內容數量：{2}", _Ishow.BonusLineRenderOutPool.transform.GetChild(i).name, i, _Ishow.BonusPrepareDrawline[i].Count));
                Debug.Log(string.Format("Bonus單圖閃爍已放進陣列等待表演：_Ishow.BonusUiShow[{0}] : 當前數量 {1} 張圖", i, _Ishow.BonusUiShow[i].Count));

            }

            for (int i = 0; i < _IDate.BonusWinCoin.Count; i++)//將各個盤面的Bonus獎的中獎金額總和起來
            {

                _IDate.Total_BonusWinCoin += _IDate.BonusWinCoin[i];

            }

        }


        _IDateEvent.DateSave(CommonGrid, BonusGrid, FreeGameCount);
        St_Roll = true;
        Debug.Log("是否開始滾動：" + Start_Slot);

    }
    #endregion

    #region UPdate執行內容
    public void UpdateMethod(IShow _IShow,IDate _IDate,IDateEvent _Ide,IUIControlMethod _IUIMethod,IMove[] _ReelMoves, SlotGrid CommonGrid, SlotGrid BonusGrid)
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


        GetAnimatiorStayInfo();


        if (B_Slot_timeOut == true && _ReelMoves[Slot_mantissa].tempi == Loopcount)
        {
            Debug.Log("執行 Co_Slot_timeOut ");
            B_Slot_timeOut = false;
            _Slot_timeOut = Co_Slot_timeOut(_IDate, _IShow, _Ide, _IUIMethod, _ReelMoves, CommonGrid, BonusGrid);
            StartCoroutine(_Slot_timeOut);

        }

        if (!Start_Slot)
        {

            Button_PressAndHold(_SlotDate, _UIControlMethod);
            Button_Reduce_Press(_SlotDate, _UIControlMethod);

        }

        if (UseBonus)
        {

            _IShow.RecoverComply(_IShow.BonusLineRenderOutPool.GetChild(NowFreeCount), _IShow.BonusPrepareDrawline[NowFreeCount], _IShow.BonusDrawLineOk[NowFreeCount]);//BONUS的LineRender回收

        }

        _IShow.RecoverComply(_IShow.LineRenderOutPool, _IShow.PrepareDrawLine, _IShow.DrawLineOK);//普通盤的LineRender回收

        if (StEndShow)
        {
            if (_IShow.VideoImage.gameObject.activeInHierarchy)//判斷播影片是否開啟
            {

                Debug.Log("VideoImage.gameObject.active :" + _IShow.VideoImage.gameObject.activeInHierarchy);

                if (_IShow.EndShowPlayer.frame != 0 && _IShow.EndShowPlayer.frameCount != 0)
                {

                    if ((ulong)_IShow.EndShowPlayer.frame >= _IShow.EndShowPlayer.frameCount)
                    {
                        _IShow.EndShowPlayer.Pause();//暫停影片
                        Debug.Log("暫停影片");
                        _IShow.VideoImage.GetComponent<RawImage>().enabled = false;
                        _IShow.VideoImage.gameObject.SetActive(false);//關掉RawImage
                        StEndShow = false;
                        _IDate.Total_BonusWinCoin = 0;
                    
                    }

                }

            }

        }

    }
    #endregion

    #region 取得動畫的StateInfo
    /// <summary>
    /// 取得動畫的StateInfo
    /// </summary>
    public void GetAnimatiorStayInfo()
    {

        //這裡應該可以改成一個方法 透過Manager的 變數  我去更換 它 = 的內容（StateInfo）還有Animater是哪一個  跟一個 String （動畫裡面開關的 名字） 
        //來去作用

        //Manager 宣告一個 Animator  在動畫表演方法 寫說 Animator  ＝ 什麼
        
        //Manager 宣告一個 字串 AnimatorName  在動畫表演方法 寫說 BonusStateInfo ＝ 什麼
        // 然後 動畫的每個控制的Bool 名字都要依樣 

        if (BonusStateInfo_B)
        {

            Debug.Log("BonusStateInfo_B :" + BonusStateInfo_B);
            BonusStateInfo = TempAnimator.GetCurrentAnimatorStateInfo(0);//階層為0
            Debug.Log("BonusStateInfo.normalizedTime " + BonusStateInfo.normalizedTime);

            if (BonusStateInfo.normalizedTime >= 1.0f && BonusStateInfo.IsName(Animator_Name))
            {
                BonusStateInfo_B = false;

                TempAnimator.SetBool("ShowBool", false);

            }

        }

    }
    #endregion


    #region  Options  按鈕事件
    public void Options_Yes(IDate _IDate,IUIControlMethod _IUIMethod, IMove[] _ReelMoves)
    {


        Debug.Log("有無遊戲資料 ：" + PlayerPrefs.HasKey("遊戲資料"));


        if (PlayerPrefs.HasKey("遊戲資料"))
        {

            string GetDate;
            GetDate = PlayerPrefs.GetString("遊戲資料");
            Debug.Log("儲存的資料內容 ： " + GetDate);
            _SlotDate._Slot_SeverDate = JsonUtility.FromJson<Slot_SeveDate>(GetDate);

            int All_Coin;
            All_Coin = _SlotDate._Slot_SeverDate.Player_Coin + _SlotDate._Slot_SeverDate.BonusCoin + _SlotDate._Slot_SeverDate.Win_Coin;
            _IDate.Bet_Coin = _SlotDate._Slot_SeverDate.Bet_Coin;

            if (_SlotDate._Slot_SeverDate.Auto_PlayerSet > _SlotDate._Slot_SeverDate.Auto_NotYet)
            {
                _IDate.AutoSurplus = _SlotDate._Slot_SeverDate.Auto_HasRollcount - 1;

                _IDate.CycleCount = _SlotDate._Slot_SeverDate.Auto_NotYet + 1;
            }
            else
            {

                _IDate.AutoSurplus = 1;
                _IDate.CycleCount = 1;

            }
            _IDate.AutoCount = _SlotDate._Slot_SeverDate.Auto_PlayerSet;
            _IDate.PlayerCoin = All_Coin;
            _IUIMethod.PlayerCoin_Text.text = "Money:" + _IDate.PlayerCoin;
            _IUIMethod.BetMenu_Text.text = _IDate.Bet_Coin.ToString();
            _IUIMethod.Bet_Text.text = _IDate.Bet_Coin.ToString();
            _IUIMethod.Auto_text.text = _IDate.AutoSurplus.ToString();
            for (int i = 0; i < _SlotDate._Slot_SeverDate.Data.Count; i++)
            {


                for (int j = 0; j < _SlotDate._Slot_SeverDate.Data[i]._IntCount.Count; j++)
                {
                    _ReelMoves[i].Self.transform.GetChild(j).GetComponent<Image>().sprite = _SlotDate.Sprite_Pool[_SlotDate._Slot_SeverDate.Data[i]._IntCount[j]];

                    Debug.Log("_SlotDate._Slot_SeverDate.Data[i]._IntCount[j] :" + _SlotDate._Slot_SeverDate.Data[i]._IntCount[j]);

                }

            }

            _IUIMethod.Options.SetActive(false);

        }


    }

    public void Options_No(IDateEvent _Ide, IUIControlMethod _IUIMethod)
    {

        PlayerPrefs.DeleteAll();
        Debug.Log("是否有儲存的資料 ：" + PlayerPrefs.HasKey("遊戲資料"));
        _Ide.Initialization_Slot_Sprite();
        _IUIMethod.Options.SetActive(false);


    }
    #endregion

    #region 要給Playercontrol 的 方法
    public void Del_startGame()
    {

        StartGame(_SlotDate, _UIControlMethod, _SlotDate, _ShowScript,_Reel_Moves, CommonGrid, BonusGrid);

    }

    public void Del_Options_yes()
    {
        Options_Yes(_SlotDate,_UIControlMethod,_Reel_Moves);
    }

    public void Del_Options_No()
    {

        Options_No(_SlotDate, _UIControlMethod);

    }

    #endregion



}
