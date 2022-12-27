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




    public float RollSpeed;//輪條移動速度
    public bool Start_Slot;
    public bool Bet_Button_Bool;
    public bool Auto_Button_bool;
    public int Loopcount;//循環次數

    public bool B_Slot_timeOut;//讓_Slot_timeOut只會執行一次

    public bool WinShowOk;

    public bool DrawLineTF;

    public IEnumerator CoRool;
    public IEnumerator _CoAll_Slot_Roll;
    public IEnumerator _Show;
    public IEnumerator _Slot_timeOut;
    public int Slot_mantissa;//輪條陣列內最後的數
    //public Text Player_coin_Text;//玩家的總金額ＴＥＸＴ
    public bool St_Roll;
    public Button_EventTrigger _ButtonPlus_EventTrigger;
    public Button_EventTrigger _ButtonReduce_EventTrigger;
    //public List<Bonus_PrepareDrawLinePool> _Bonus_PrepareDrawLinePool;//放各個Bonus盤面的DrawLine表演用的預制物
    public bool StRecover;//判斷 預制物是否已回收
    //public List<bool> ShowOK;//用來確認每個LineRender畫完線了
    //public List<Bonus_ShowOk> _Bonus_ShowOK;//用來確認執行Bonus時Bonus每個盤面的每個LineRender畫完線了
    public bool StCoShow;//確保CoShow只有一個
    public List<BonusShinyDate> _BonusShinyDate;
    public Texture2D[] MIcon;
    public bool ShowOver;//判斷表演是否都結束
    public Image[] WinCoins;
    public Sprite[] Numbers;
    public int TempWinCoin;//金錢表演用的暫存Int
    public IEnumerator _CoShowWinCoin;
    public bool StCoShowWinCoin;
    public bool CoinShowOK;
    public bool BonusShow;//有沒有BonusShow
   // public AnimatorStateInfo BonusStateInfo;//動畫當前狀態
   // public AnimatorStateInfo WinShowStateInfo;
   // public int BonusCount;

    public int FreeGameCount;//小遊戲要玩的次數
    public int NowFreeCount;//當前遊玩次數

    public bool BonusRolling;
    public bool Slot_BonusGoRoll;
    public bool StAddBonusDate;
    public bool StBonusShowOk;
    public bool UseBonus;

    public bool FreeCountPlus;

    public VideoPlayer EndShowPlayer;
    public VideoClip EndShowVideoClip;
    public bool StEndShow;
    public Image BONUSWIN;//Bonus總贏錢背景圖
    public Sprite[] Sprites;//Info的圖片庫
    public Image ImagaInfo;//Info背景圖
    public Image Img_Introduction;//Info視窗中間要換介紹Sprite的image
    public RawImage VideoImage;
    public SlotGrid CommonGrid;
    public SlotGrid BonusGrid;

    // ReelMove 移動下降高度要改用RecTransForm的 下降高度 以父物件的中心延伸水平的座標當中心點 當整條輪條的中心點 來下移一張圖片高度

    //要迴圈做各個ReelMove 變數的設定
    // 要執行Slot_date的Init (連線判斷  設一個String[] 裡面輸入盤面連線號  然後迴圈 去判定有沒有中獎 而且 要設一個方法用字串處理 如果有連線到 用字串處理 查頭尾的數字 來決定 DrawLine 起點跟終點  )
    // PlayerControl 的 delegate  要在這給方法 （開始鈕 還有 要不要讀取存檔的兩個按鈕）
    // 這裡的 coroutine delegate 是 要放表演方法的

    //重點 ！！ 要看一下LineRender的 方法 好像怪怪的 或是 ShowScript有多設到無用參數 應該是要有 ㄧ個TranSform 是生成後統一放的位置  然後 普通盤有一個 取出來等待表演的TranSform  Bonus也有一個 只是 有多帶子物件 放每個盤面的 然後一個Queen 放做物件取出跟放回
    //Queen 是生成物的總管 取出跟放回都是他
    // 取出後會有List<Game>（普通盤） 跟 List<List<Game>>（Bonus盤）  來放等待表演的物件 Transform 那個只是方便觀察 實際物件資料在這
    // Shiny 的表演也是 會有List放 等待表演的物件的Transform 
    void Start()
    {

        Slot_Initialization(_SlotDate,_SlotDate,_ShowScript,_UIControlMethod);

        SlotGridCreat(_SlotDate);

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

        if ( B_Slot_timeOut == true  &&  _Reel_Moves[Slot_mantissa].tempi==Loopcount )
        {
            Debug.Log("執行 Co_Slot_timeOut ");

            B_Slot_timeOut = false;

            _Slot_timeOut = Co_Slot_timeOut(_SlotDate, _ShowScript, _SlotDate, _UIControlMethod);

            StartCoroutine(_Slot_timeOut);

        }

        if (!Start_Slot)
        {

            Button_PressAndHold(_SlotDate,_UIControlMethod);

            Button_Reduce_Press(_SlotDate, _UIControlMethod);

        }




       // _ShowScript.RecoverComply(_ShowScript.LineRenderOutPool, _ShowScript.PrepareDrawLine, _ShowScript.DrawLineOK);




    }

    #region 資料初始化
    /// <summary>
    /// 資料初始化
    /// </summary>

    public void Slot_Initialization(IDate _IDate,IDateEvent _IDateEvent,IShow _Ishow,IUIControlMethod _IuiMethod)
    {
        DrawLineTF = false;

           B_Slot_timeOut = true;

        WinShowOk = true;

        _SlotDate.Init(_IuiMethod,_Reel_Moves);

        _UIControlMethod.UIControlInit(_IDate,_ButtonPlus_EventTrigger,_ButtonReduce_EventTrigger);

        _PlayerControl.PlayerControl_Init(_IuiMethod,this);

        _PlayerControl.startGameMethod = Del_startGame;//指定開始按鈕要執行得事件

        for (int i = 0; i < _Reel_Moves.Length; i++)
        {

            _Reel_Moves[i].Init(Loopcount, _IDate.SpritePool, RollSpeed);

        }

        StEndShow = false;

        EndShowVideoClip = EndShowPlayer.GetComponent<VideoPlayer>().clip;

        FreeCountPlus = false;

        UseBonus = false;

        StBonusShowOk = false;

        StAddBonusDate = false;

        Slot_BonusGoRoll = false;

        BonusRolling = false;

        FreeGameCount = 3;

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
       

        for (int i = 0; i < FreeGameCount; i++)
        {

            _IDate.BonusWinCoin.Add(0);

            _Ishow.BonusUiShow.Add(new List<Transform>());
           
            _Ishow.BonusPrepareDrawline.Add(new List<GameObject>());

            _Ishow.BonusDrawLineOk.Add(new List<bool>());

        }

        _Ishow.ObjectPoolInitialization();//物件池 預置物生成

        ShowOver = true;

        BonusShow = false;

        CoinShowOK = true;

        _Ishow.UiShow = new List<Transform>();
       
        StCoShow = true;

        _Ishow.DrawLineOK = new List<bool>();
     
        StRecover = false;

        StCoShowWinCoin = false;

        St_Roll = false;
    
        _Ishow.PrepareDrawLine = new List<GameObject>();
      
        _IDate.CycleCount = 1;
    
        Bet_Button_Bool = false;

        Auto_Button_bool = false;

        _IDate.PlayerCoin = 1099;
 
        Debug.Log("Player_coin"+_IDate.PlayerCoin);
      
        _IuiMethod.PlayerCoin_Text.text = "Money :" + _IDate.PlayerCoin.ToString();
    
        _IDate.AutoCount = 1;


        Slot_mantissa = _Reel_Moves.Length - 1;//紀錄陣列的最大編號

        if (!PlayerPrefs.HasKey("遊戲資料"))
        {

           _IDateEvent.Initialization_Slot_Sprite();

        }

        Start_Slot = false;

        Debug.Log("是否開始滾動：" + Start_Slot);

    }
    #endregion

    #region 盤面物件創造
    /// <summary>
    /// 盤面物件創造
    /// </summary>
    public void SlotGridCreat(IDate _IDate)
    {

        int Reelcount;

        Reelcount = _Reel_Moves.Length;

        List<int> ii = new List<int>();

        for (int i = 0; i < _Reel_Moves.Length; i++)
        {
            int Chcount = _Reel_Moves[i].transform.childCount;
            ii.Add(Chcount);
            //Debug.Log(ii);
        }

        CommonGrid = new SlotGrid(1, Reelcount, ii, _SlotDate.Generate_Date_Sprite);

   
        BonusGrid = new SlotGrid(FreeGameCount, Reelcount, ii, _SlotDate.GenerateBonusDate);

    }
    #endregion

    #region 放各個Bonus盤面的Shiny表演圖用的
    /// <summary>
    /// 放各個Bonus盤面的Shiny表演圖用的
    /// </summary>
    [System.Serializable]
    public class BonusShinyDate
    {

        public List<Transform> BonusShinyPoint;

    }
    #endregion

    #region  放各個Bonus盤面的DrawLine表演用的預制物
    /// <summary>
    /// 放各個Bonus盤面的DrawLine表演用的預制物
    /// </summary>
    [System.Serializable]
    public class Bonus_PrepareDrawLinePool
    {

        public List<GameObject> Bonus_PrepareDrawLine;

    }
    #endregion

    #region 確認各個Bonus盤面的每條DrawLine都跑完
    /// <summary>
    /// 確認各個Bonus盤面的每條DrawLine都跑完
    /// </summary>
    [System.Serializable]
    public class Bonus_ShowOk
    {

        public List<bool> _ShowOk;

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

            Debug.Log("CoAll_Slot_Roll() :St_Roll 開始轉動");

            for (int i = 0; i < _Reel_Moves.Length; i++)
            {

                for (int j=0;j<_Reel_Moves[i].transform.childCount;j++)
                {

                    _Reel_Moves[i].ChangeSprite[j] = (int)_Grints._Grids[i]._GridInt[j];

                }

               // Debug.Log("CoAll_Slot_Roll() :迴圈");

                _Reel_Moves[i].strool = true;

                Debug.Log(string.Format("輪條編號：{0},是否開啟{1}", i, _Reel_Moves[i].strool));

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
            if (_SlotDate.Bet_Coin < coin_temp)
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

            if (_SlotDate.Bet_Coin >= 100)
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

    #region 表演方法
    /// <summary>
    /// 表演方法
    /// </summary>
    /// <returns></returns>
    public IEnumerator Co_Show(IShow _IShow,List<Transform> _UiShow, List<GameObject> StayDrawLine, List<bool> _ShowOk, int WinMoneyTemp)//把特效變更金錢文字寫在這  然後Slot_timeOut改成協程 等待這個方法做完 再繼續作用
    {
        if (WinMoneyTemp != 0)//有中獎
        {
            ShowOver = false;
            CoinShowOK = false;
            _IShow.ShinyShow(_UiShow);

            _IShow.StartDrawLine(StayDrawLine, _ShowOk);

            StCoShowWinCoin = true;


        }

        yield return null;

    }
    #endregion

    #region 贏錢表演
    /// <summary>
    /// 贏錢表演 表演完更新畫面金錢
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoShowWinCoin(int WinMoney,IShow _IShow,IDate _IDate,IUIControlMethod _IUIMethod)
    {

        if (TempWinCoin <= WinMoney)
        {
            _IShow.Amr_WinShow.SetBool("StWinShow", true);
            string SWinCoin = TempWinCoin.ToString();
            //Debug.Log("SWinCoin(暫存金額) ：" + SWinCoin);
            int ImgCount = SWinCoin.Length;//取得幾位數
                                           //Debug.Log("本局Win金額 ＝ " + ImgCount + "位數");
            for (int i = 0; i < ImgCount; i++)
            {
                int TempValu;//用字串處理 取的 Win_Money_Temp各個位數的值
                TempValu = int.Parse(SWinCoin.Substring(i, 1));
                WinCoins[i].gameObject.SetActive(true);
                WinCoins[i].sprite = Numbers[TempValu];



            }
            yield return null;




            if (WinMoney - TempWinCoin < 50)
            {
                TempWinCoin += 1;
                yield return new WaitForSeconds(0.01f);

            }
            else
            {

                TempWinCoin += 21;
                yield return new WaitForSeconds(0.01f);
            }


            StCoShowWinCoin = true;


        }
        else if (TempWinCoin > WinMoney)
        {
            yield return new WaitForSeconds(1f);
            int Win_All_Coin_Temp;
            _IShow.Amr_WinShow.SetBool("StWinShow", false);
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < WinCoins.Length; i++)
            {
                WinCoins[i].gameObject.SetActive(false);


            }

            TempWinCoin = 0;
            Win_All_Coin_Temp = _IDate.PlayerCoin + WinMoney;
            _IDate.PlayerCoin = Win_All_Coin_Temp;
            _IUIMethod.PlayerCoin_Text.text = "Money:" + Win_All_Coin_Temp.ToString();
            if (StBonusShowOk)
            {

                _IDate.BonusWinCoin[FreeGameCount] = 0;

            }
            else
            {

                _IDate.Win_Coin = 0;

            }

            yield return new WaitForSeconds(0.5f);


            StRecover = false;
            CoinShowOK = true;
            ShowOver = true;
            //StBonusShowOk = true;

        }



    }
    #endregion

    #region Bonus表演相關
    /// <summary>
    /// Bonus 的 金錢表演
    /// </summary>
    /// <param name="WinMoney"></param>
    /// <returns></returns>
    public IEnumerator End_TotalBonusWinCoin(int WinMoney)
    {


        if (TempWinCoin <= WinMoney)
        {

            string SWinCoin = TempWinCoin.ToString();
            //Debug.Log("SWinCoin(暫存金額) ：" + SWinCoin);
            int ImgCount = SWinCoin.Length;//取得幾位數
                                           //Debug.Log("本局Win金額 ＝ " + ImgCount + "位數");
            for (int i = 0; i < ImgCount; i++)
            {
                int TempValu;//用字串處理 取的 Win_Money_Temp各個位數的值
                TempValu = int.Parse(SWinCoin.Substring(i, 1));
                WinCoins[i].gameObject.SetActive(true);
                WinCoins[i].sprite = Numbers[TempValu];



            }
            yield return null;




            if (WinMoney - TempWinCoin < 50)
            {
                TempWinCoin += 1;
                yield return new WaitForSeconds(0.01f);

            }
            else
            {

                TempWinCoin += 21;
                yield return new WaitForSeconds(0.01f);
            }


            StCoShowWinCoin = true;


        }
        else if (TempWinCoin > WinMoney)
        {
            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < WinCoins.Length; i++)
            {
                WinCoins[i].gameObject.SetActive(false);

            }


            yield return new WaitForSeconds(0.5f);

            VideoImage.gameObject.SetActive(true);
            VideoImage.GetComponent<VideoPlayer>().Play();
            yield return new WaitForSeconds(0.5f);
            VideoImage.GetComponent<RawImage>().enabled = true;


            //StRecover = false;
            CoinShowOK = true;
            ShowOver = true;
            //StBonusShowOk = true;

        }


    }



    /// <summary>
    /// Bonus的啟動各輪
    /// </summary>

    public void BonusRoll(IDate _IDate )
    {

        if (FreeGameCount < _IDate.BonusCount)
        {
            Debug.Log("-----------------------BONUS 輪條歸零 ----------------------------");
            for (int i = 0; i < _Reel_Moves.Length; i++)
            {
                _Reel_Moves[i].tempi = 0;
                _Reel_Moves[i].Date_Temp = 0;
                Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

            }
            if (_Reel_Moves[Slot_mantissa].tempi == 0)
            {
                St_Roll = true;
                _CoAll_Slot_Roll = CoAll_Slot_Roll(BonusGrid._grids[1]);
                StartCoroutine(_CoAll_Slot_Roll);
                Debug.Log("-----------------------BONUS 輪條開始轉動 ----------------------------");
                StCoShow = true;
                StRecover = false;
                ShowOver = true;
                BonusRolling = true;
            }



        }

    }

    #endregion

    #region 要給Playercontrol 的 方法
    public void Del_startGame()
    {

        StartGame(_SlotDate,_UIControlMethod,_SlotDate,_ShowScript,CommonGrid,BonusGrid);

    }
    #endregion

    #region 開始按鈕要執行得內容
    public void StartGame(IDate _IDate,IUIControlMethod IuiMethod ,IDateEvent _IDateEvent,IShow _Ishow,SlotGrid CommonGrid,SlotGrid BonusGrid)
    {
        Debug.Log("執行StartGame");

        if (Start_Slot == false && _IDate.Bet_Coin != 0 && _IDate.PlayerCoin >= _IDate.LeastBetCount)//如果Start_Slot == false（代表按鈕有作用 現在並無作動） 而且 下注金額不等於0 而且  玩家金額要大於最低押注金額
        {

            //int Autoi;
            //Autoi = int.Parse(IuiMethod.Auto_text.text);
            //_IDate.AutoSurplus = Autoi;

            St_Roll = true;

            _IDate.PlayerCoin -= _IDate.Bet_Coin;

            IuiMethod.PlayerCoin_Text.text = "Money:" + _IDate.PlayerCoin;

            if (_IDate.AutoSurplus > 1)
            {

                _IDate.AutoCount = _IDate.AutoSurplus;

            }

            _IDate.BonusCount = 0;//預設大獎中獎圖數為0

            if (StAddBonusDate)//如果有按下 直接中大獎按鈕
            {

               _IDateEvent.Add_BonusDate(StAddBonusDate, CommonGrid);//將普盤的盤面資料 灌成 有中Bonus的盤面

            }
            else
            {
              
                CommonGrid._GridMethod(CommonGrid);//普盤 生成盤面

            }

            //int WinCoin = _IDate.Win_Coin;

            //_IDateEvent.WInChack(_IDate.PrizeDate, CommonGrid._grids[CommonGrid._girdcount - 1], _IDate.Win_Coin);//兌獎
            _IDate.Win_Coin= _IDateEvent.WInChack(_IDate.PrizeDate, CommonGrid._grids[CommonGrid._girdcount - 1], _IDate.Win_Coin);
            Debug.Log("_IDate.Win_Coin :" + _IDate.Win_Coin);

            for (int i=0;i<_IDate.Temp.Count;i++)
            {

                _Ishow.ObjectPool(_Reel_Moves,_IDate.Temp[i], _Ishow.LineRenderOutPool, _Ishow.PrepareDrawLine);

                _Ishow.ListShiny(_Reel_Moves, _IDate.Temp[i],_Ishow.UiShow);

            }

            _IDate.Temp.Clear();

            Debug.Log( string.Format("普盤預制物生成完畢： _Ishow.LineRenderOutPool名字：{0} , _Ishow.PrepareDrawLine 內容數量：{1}", _Ishow.LineRenderOutPool.gameObject.name, _Ishow.PrepareDrawLine.Count) );

            Debug.Log( string.Format("單圖閃爍已放進陣列等待表演：_Ishow.UiShow : 當前數量 {0} 張圖", _Ishow.UiShow.Count));

            if (_IDate.BonusCount == FreeGameCount)//如果有Bonus獎
            {

                BonusShow = true;

                BonusGrid._GridMethod(BonusGrid);//Bonus盤 生成盤面

                for (int i = 0; i < BonusGrid._girdcount; i++) //Bonus 各個盤面的兌獎
                {

                    //int BonusWin = _IDate.BonusWinCoin[i];

                    //_IDateEvent.WInChack(_IDate.PrizeDate, BonusGrid._grids[i], _IDate.BonusWinCoin[i]);//Bonus兌獎

                    _IDate.BonusWinCoin[i]= _IDateEvent.WInChack(_IDate.PrizeDate, BonusGrid._grids[i], _IDate.BonusWinCoin[i]);

                    Debug.Log(string.Format(" _IDate.BonusWinCoin[{0}] : {1}",i, _IDate.Win_Coin) );

                    for (int j = 0; j < _IDate.Temp.Count; j++)
                    {

                        _Ishow.ObjectPool(_Reel_Moves, _IDate.Temp[j], _Ishow.BonusLineRenderOutPool.transform.GetChild(i), _Ishow.BonusPrepareDrawline[i]);

                        _Ishow.ListShiny(_Reel_Moves, _IDate.Temp[j], _Ishow.BonusUiShow[i]);

                    }

                    _IDate.Temp.Clear();

                    Debug.Log(string.Format("Bonus盤預制物生成完畢： _Ishow.BonusLineRenderOutPool.transform.GetChild(i)：{0} , _Ishow.BonusPrepareDrawline[{1}] 內容數量：{2}", _Ishow.BonusLineRenderOutPool.transform.GetChild(i).name,i, _Ishow.BonusPrepareDrawline[i].Count));

                    Debug.Log(string.Format("Bonus單圖閃爍已放進陣列等待表演：_Ishow.BonusUiShow[{0}] : 當前數量 {1} 張圖",i, _Ishow.BonusUiShow[i].Count));

                }

                for (int i = 0; i < _IDate.BonusWinCoin.Count; i++)//將各個盤面的Bonus獎的中獎金額總和起來
                {

                   _IDate.Total_BonusWinCoin += _IDate.BonusWinCoin[i];

                }

            }

            _IDateEvent. DateSave(CommonGrid,BonusGrid);

            _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0]);

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
    [System.Obsolete]
    public IEnumerator Co_Slot_timeOut(IDate _Idate, IShow _Ishow, IDateEvent _Ide,IUIControlMethod _IUIMethod)
    {
        
        if (_Reel_Moves[Slot_mantissa].tempi==Loopcount)//如果最後一個輪條達到滾動次數
        {
            Debug.Log("啟動Co_Slot_timeOut");

            if (StCoShow)
            {
                StCoShow = false;

                _Show = WinShow(_Ishow,_Idate);

                StartCoroutine(_Show);

                Debug.Log("StART Win SHow");
            }


            yield return new WaitUntil(()=> WinShowOk = true);

            Debug.Log("WinShowOk :" + WinShowOk);

            //if (ShowOver && !BonusShow && !StEndShow)//表演結束
            //{
            if (WinShowOk && !BonusShow && !StEndShow)//表演結束
            {


                if (_Idate.CycleCount < _Idate.AutoCount && _Idate.PlayerCoin > _Idate.Bet_Coin)//循環次數未到 而且 玩家金額不小於下注金額
                {

                    ShowOver = false;

                    Debug.Log("-----------------------------開始轉動-------------------------");

                    //_Ishow.UiShow.Clear();//目前先關起來 測試表演

                    StRecover = true;

                    for (int i = 0; i < _Reel_Moves.Length; i++)//輪條轉動次數初始化
                    {
                        _Reel_Moves[i].tempi = 0;

                        _Reel_Moves[i].Date_Temp = 0;

                        Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

                    }


                    if (_Reel_Moves[Slot_mantissa].tempi == 0) //這裡重新生成盤面資料 兌獎 設定預制物 
                    {
                        _Idate.BonusCount = 0;

                        if (StAddBonusDate)//如果有按下 直接中大獎按鈕
                        {
                            _Ide.Add_BonusDate(StAddBonusDate, CommonGrid);//將普盤的盤面資料 灌成 有中Bonus的盤面

                        }
                        else
                        {

                            CommonGrid._GridMethod(CommonGrid);//普盤 生成盤面
                        }

                        //int WinCoin = _Idate.Win_Coin;

                        _Ide.WInChack(_Idate.PrizeDate, CommonGrid._grids[CommonGrid._girdcount - 1],_Idate.Win_Coin);

                        if (_Idate.BonusCount == FreeGameCount)//如果有Bonus獎
                        {
                            BonusShow = true;

                            for (int i = 0; i < BonusGrid._girdcount; i++) //Bonus 各個盤面的兌獎
                            {

                                //int BonusWin = _Idate.BonusWinCoin[i];

                                _Ide.WInChack(_Idate.PrizeDate, BonusGrid._grids[i], _Idate.BonusWinCoin[i]);

                            }

                            for (int i = 0; i < _Idate.BonusWinCoin.Count; i++)//將各個盤面的Bonus獎的中獎金額總和起來
                            {

                                _Idate.Total_BonusWinCoin += _Idate.BonusWinCoin[i];

                            }

                        }

                        _Ide.DateSave(CommonGrid, BonusGrid); //資料儲存

                        B_Slot_timeOut = true;

                        //Start_Slot = true;

                        //Debug.Log("是否開始滾動：" + Start_Slot);

                        St_Roll = true;

                        yield return new WaitForSeconds(1f);

                        Debug.Log("Wait");

                        _CoAll_Slot_Roll = CoAll_Slot_Roll(CommonGrid._grids[0]);

                        StartCoroutine(_CoAll_Slot_Roll);

                        _Idate.CycleCount += 1;

                        _Idate.AutoSurplus -= 1;

                        _IUIMethod.Auto_text.text = _Idate.AutoSurplus.ToString();

                        _Idate.PlayerCoin -= _Idate.Bet_Coin;

                        _IUIMethod.PlayerCoin_Text.text = "Money:" + _Idate.PlayerCoin;

                        Debug.Log(_Idate.CycleCount + "已循環次數");

                        _Ide.DateSave(CommonGrid,BonusGrid);

                        //Debug.Log("Win_Mpney_Temp" + Win_Money_Temp);

                        StCoShow = true;

                        StRecover = false;

                        ShowOver = true;

                        FreeGameCount = 0;

                    }

                }

                else
                {

                    Debug.Log("各輪盤滾動完畢");

                    Debug.Log("是否開始滾動：" + Start_Slot);

                    _Idate.AutoCount = 1;

                    if (CoinShowOK == true)
                    {
                        for (int i = 0; i < _Reel_Moves.Length; i++)
                        {
                            _Reel_Moves[i].tempi = 0;

                            _Reel_Moves[i].Date_Temp = 0;

                            Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

                        }

                        Start_Slot = false;

                        St_Roll = true;

                       // _Ishow.UiShow.Clear(); //目前先關起來 測試表演

                        StCoShow = true;

                        FreeGameCount = 0;

                        _Idate.CycleCount = 1;

                        _Idate.AutoSurplus = 0;

                    }

                }

            }

        }

    }
    #endregion

    #region (舊)  判斷滾輪次數是否達成條件  沒達成條件就繼續滾
    /// <summary>
    /// 當Start_Slot為true時而且轉動次數到達上限 初始化開始按鈕跟輪條開始的bool
    /// </summary>
    //[System.Obsolete]
    //public IEnumerator Co_Slot_timeOut(IDate _Idate, IShow _Ishow, IDateEvent _Ide)
    //{

    //    if (_Reel_Moves[Slot_mantissa].tempi == Loopcount)//是否轉完一輪(最後的輪調是否達到次數)
    //    {
    //        if (_Reel_Moves[Slot_mantissa].strool != false)
    //        {

    //            for (int i = 0; i < _Reel_Moves.Length; i++) //這裏做停輪動作
    //            {

    //                _Reel_Moves[i].strool = false;

    //            }


    //        }


    //        if (BonusShow && UseBonus && StCoShow)//設一個bool 底下執行Bonus內容玩時 bool＝true ,然後這裡關掉 FreeGame的次數在這裡增加
    //        {
    //            Debug.Log("---------------------------：Bonus Show 開始執行 :--------------------------------------");
    //            StCoShow = false;
    //            //_Show = Co_Show(_BonusShinyDate[FreeGameCount].BonusShinyPoint, _Bonus_PrepareDrawLinePool[FreeGameCount].Bonus_PrepareDrawLine, _Bonus_ShowOK[FreeGameCount]._ShowOk, Win_BonusMoney_Date[FreeGameCount]);
    //            yield return StartCoroutine(_Show);
    //            FreeCountPlus = true;
    //        }

    //        else if (StCoShow)

    //        {
    //            StCoShow = false;
    //            //_Show = Co_Show(UiShow, PrepareDrawLine, ShowOK, Win_Money_Temp);
    //            yield return StartCoroutine(_Show);

    //        }



    //        if (CoinShowOK && StBonusShowOk && UseBonus && FreeCountPlus)
    //        {
    //            FreeCountPlus = false;
    //            CoinShowOK = false;
    //            StBonusShowOk = false;
    //            FreeGameCount += 1;
    //            BonusRolling = false;


    //            BonusShow = true;
    //            CoinShowOK = true;
    //            Debug.Log("FreeGameCount :" + FreeGameCount);

    //        }





    //        if (BonusShow && ShowOver && !BonusRolling)
    //        {
    //            if (FreeGameCount < _Idate.BonusCount)
    //            {


    //                if (FreeGameCount == 0)
    //                {
    //                    _Ishow.BonusAnimator.SetBool("StBonusShow", true);

    //                    BonusStateInfo = BonusAnimator.GetCurrentAnimatorStateInfo(0);//階層為0
    //                    if (BonusStateInfo.normalizedTime >= 1.0f && BonusStateInfo.IsName("BonusShow2"))
    //                    {

    //                        BonusAnimator.SetBool("StBonusShow", false);

    //                        Debug.Log("-------------- : Bonus 動畫是否播放完畢 : -----------------" + BonusAnimator.GetBool("StBonusShow"));
    //                    }


    //                }

    //                if (!BonusAnimator.GetBool("StBonusShow"))
    //                {
    //                    Debug.Log("-------------- : Bonus 動畫 （ 播放完畢 ） : -----------------");
    //                    BonusRoll();
    //                    UseBonus = true;
    //                    StCoShow = true;
    //                    Slot_BonusGoRoll = true;
    //                    StBonusShowOk = true;
    //                }




    //            }

    //            else if (FreeGameCount >= _Idate.BonusCount)
    //            {
    //                Debug.Log("關閉");

    //                for (int i = 0; i < _BonusShinyDate.Count; i++)
    //                {

    //                    _BonusShinyDate[i].BonusShinyPoint.Clear();

    //                }

    //                StEndShow = true;
    //                StCoShowWinCoin = true;
    //                BonusShow = false;
    //                UseBonus = false;
    //                StBonusShowOk = false;
    //                Slot_BonusGoRoll = false;


    //            }

    //        }



    //        if (ShowOver && !BonusShow && !StEndShow)
    //        {



    //            if (_Idate.CycleCount < _Idate.AutoCount && _Idate.PlayerCoin > _Idate.Bet_Coin)
    //            {

    //                ShowOver = false;

    //                Debug.Log("-----------------------------開始轉動-------------------------");
    //                UiShow.Clear();
    //                StRecover = true;

    //                for (int i = 0; i < _Reel_Moves.Length; i++)
    //                {
    //                    _Reel_Moves[i].tempi = 0;
    //                    _Reel_Moves[i].Date_Temp = 0;
    //                    Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

    //                }

    //                if (_Reel_Moves[Slot_mantissa].tempi == 0)
    //                {
    //                    _SlotDate.Bonus_count = 0;
    //                    if (StAddBonusDate)
    //                    {
    //                        _SlotDate.Add_BonusDate(StAddBonusDate, _Reel_Moves);

    //                    }
    //                    else
    //                    {
    //                        _SlotDate.Generate_Date_Sprite(_Reel_Moves);//生成盤面
    //                    }

    //                    Win_Line(_SlotDate._Reel_Sprite_Date, _SlotDate.Sprite_Pool[6], UiShow, ref Win_Money_Temp, LineRenderOutPool, PrepareDrawLine, _SlotDate.Sprite_Pool[7]);//兌獎 預置物生成放入

    //                    if (_SlotDate.Bonus_count == 3)//如果有Bonus獎
    //                    {
    //                        BonusShow = true;

    //                        _SlotDate.GenerateBonusDate(BonusCount, _Reel_Moves);//生成所有Bonus盤面

    //                        for (int i = 0; i < _SlotDate._AllBonusSpriteDate.Count; i++)
    //                        {

    //                            Win_Line(_SlotDate._AllBonusSpriteDate[i]._BonusSpriteDate, _SlotDate.Sprite_Pool[6], _BonusShinyDate[i].BonusShinyPoint, ref Win_BonusMoney_Temp
    //                                     , BonusLineRenderOutPool.transform.GetChild(i), _Bonus_PrepareDrawLinePool[i].Bonus_PrepareDrawLine, _SlotDate.Sprite_Pool[7]);//兌獎

    //                            Win_BonusMoney_Date[i] = Win_BonusMoney_Temp;
    //                            Win_BonusMoney_Temp = 0;

    //                        }
    //                        for (int i = 0; i < Win_BonusMoney_Date.Count; i++)//將各個盤面的Bonus獎的中獎金額總和起來
    //                        {

    //                            Total_BonusWinCoin += Win_BonusMoney_Date[i];



    //                        }


    //                    }

    //                    St_Roll = true;
    //                    yield return new WaitForSeconds(1f);
    //                    Debug.Log("Wait");
    //                    _CoAll_Slot_Roll = CoAll_Slot_Roll();
    //                    StartCoroutine(_CoAll_Slot_Roll);
    //                    Cycle_Count += 1;
    //                    Auto_Temp_Count -= 1;
    //                    Auto_text.text = Auto_Temp_Count.ToString();
    //                    _SlotDate.Coin -= _SlotDate.Bet_Coin;
    //                    Player_coin_Text.text = "Money:" + _SlotDate.Coin;
    //                    Debug.Log(Cycle_Count + "已循環次數");
    //                    _Ide.DateSave();
    //                    Debug.Log("Win_Mpney_Temp" + Win_Money_Temp);
    //                    StCoShow = true;
    //                    StRecover = false;
    //                    ShowOver = true;
    //                    FreeGameCount = 0;
    //                }

    //            }

    //            else
    //            {


    //                Debug.Log("各輪盤滾動完畢");
    //                Debug.Log("是否開始滾動：" + Start_Slot);
    //                _Idate.AutoCount = 1;

    //                if (CoinShowOK == true)
    //                {
    //                    for (int i = 0; i < _Reel_Moves.Length; i++)
    //                    {
    //                        _Reel_Moves[i].tempi = 0;
    //                        _Reel_Moves[i].Date_Temp = 0;
    //                        Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));
    //                    }
    //                    Start_Slot = false;
    //                    St_Roll = true;
    //                    _Ishow.UiShow.Clear();
    //                    StCoShow = true;
    //                    FreeGameCount = 0;
    //                    _Idate.CycleCount = 1;
    //                    _Idate.AutoSurplus = 0;
    //                }


    //            }



    //        }



    //    }


    //}
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
        Debug.Log("進入普盤表演");

        WinShowOk = false;

        IEnumerator Show;

        Show= _Ishow.ShinyShow(_Ishow.UiShow);

       // StartCoroutine(Show);

        Debug.Log("執行普盤表演的： _Ishow.ShinyShow");

        yield return StartCoroutine(Show); 

        Show = _Ishow.StartDrawLine(_Ishow.PrepareDrawLine,_Ishow.DrawLineOK);

        StartCoroutine(Show);

        DrawLineTF = true;

        Debug.Log("執行普盤表演的： _Ishow.StartDrawLine");

        yield return new WaitUntil(()=>!_Ishow.DrawLineOK.Contains(true));

        DrawLineTF = false;

        //Show = _Ishow.CoinShow(_IDate.Win_Coin, NowFreeCount);

        //Debug.Log("執行普盤表演的： _Ishow.CoinShow");

        //yield return new WaitUntil(()=>_Ishow.TempWinCoin>=_IDate.Win_Coin);

        //Debug.Log("普盤表演結束");

        WinShowOk = true;


    }
    #endregion

    #region UPdate執行內容
    public void UpdateMethod(IShow _IShow,IDate _IDate,IUIControlMethod _IUIMethod)
    {

        if (UseBonus)
        {
            _IShow.RecoverComply(_IShow.BonusLineRenderOutPool.GetChild(FreeGameCount), _IShow.BonusPrepareDrawline[FreeGameCount], _IShow.BonusDrawLineOk[FreeGameCount]);//BONUS的LineRender回收

        }

          _IShow.RecoverComply(_IShow.LineRenderOutPool, _IShow.PrepareDrawLine, _IShow.DrawLineOK);//普通盤的LineRender回收

        if (StEndShow)
        {

           _IShow.BonusEndShow.SetBool("StBonusEndShow", true);

            AnimatorStateInfo BonusEndStateInfo;
            BonusEndStateInfo = _IShow.BonusEndShow.GetCurrentAnimatorStateInfo(0);
            if (BonusEndStateInfo.normalizedTime >= 1.0f && BonusEndStateInfo.IsName("BonusEndShow"))
            {
                BONUSWIN.gameObject.SetActive(true);
                StRecover = true;


                if (VideoImage.gameObject.activeInHierarchy)//判斷播影片的Camera是否開啟
                {

                    //Debug.Log("VideoImage.gameObject.active :" + VideoImage.gameObject.active);

                    if (EndShowPlayer.frame != 0 && EndShowPlayer.frameCount != 0)
                    {

                        if ((ulong)EndShowPlayer.frame >= EndShowPlayer.frameCount)
                        {
                            EndShowPlayer.Pause();//暫停影片
                            Debug.Log("暫停影片");
                            _IShow.BonusEndShow.SetBool("StBonusEndShow", false);
                            BONUSWIN.gameObject.SetActive(false);
                            VideoImage.GetComponent<RawImage>().enabled = false;
                            VideoImage.gameObject.SetActive(false);//關掉RawImage
                            StEndShow = false;
                            TempWinCoin = 0;
                            _IDate.Total_BonusWinCoin = 0;
                            StRecover = false;
                        }

                    }

                }

            }

        }

        if (StCoShowWinCoin && StRecover)
        {
            if (StEndShow)
            {

                StCoShowWinCoin = false;
                _CoShowWinCoin = End_TotalBonusWinCoin(_IDate.Total_BonusWinCoin);
                StartCoroutine(_CoShowWinCoin);

            }
            else if (UseBonus)
            {
                StCoShowWinCoin = false;
                _CoShowWinCoin = CoShowWinCoin(_IDate.BonusWinCoin[FreeGameCount],_IShow,_IDate,_IUIMethod);
                StartCoroutine(_CoShowWinCoin);

            }
            else
            {
                StCoShowWinCoin = false;
                _CoShowWinCoin = CoShowWinCoin(_IDate.Win_Coin, _IShow, _IDate, _IUIMethod);
                StartCoroutine(_CoShowWinCoin);

            }


        }


    }
    #endregion

  








}
