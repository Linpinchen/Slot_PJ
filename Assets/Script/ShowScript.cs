using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
[System.Serializable]
public class ShowScript : MonoBehaviour, IShow
{

    IDate _IDate;
    IMove[] _ReelMove;
    ResourceManager _ResourceManager;

    [SerializeField]
    private bool _CoinShow_Bool;

    [SerializeField]
    private int _TempWinCoin;

    [SerializeField]
    private bool _AddCoin;

    [SerializeField]
    private Queue<GameObject> _DrawLinePool;//預置物放置取出的位置

    [SerializeField]
    private List<List<GameObject>> _BonusPrepareDrawline;//放Bonus準備畫線的物件

    [SerializeField]
    private List<GameObject> _PrepareDrawLine;//放準備畫線的物件

    [SerializeField]
    private List<List<bool>> _BonusDrawLineOk;//確認 Bonus每個Linerender都到最後

    [SerializeField]
    private List<bool> _DrawLineOk;//確認每個Linerender都到最後

    [SerializeField]
    private List<Transform> _UiShow;//普盤 裝等待Shiny表演的

    [SerializeField]
    private List<List<Transform>> _BonusUiShow;//Bonus盤 裝等待Shiny表演的

    #region 介面實作
    
    public int TempWinCoin { get { return _TempWinCoin; } set { _TempWinCoin = value; } }

    public bool AddCoin { get { return _AddCoin; } set { _AddCoin = value; } }

    public bool CoinShow_Bool { get { return _CoinShow_Bool; } set { _CoinShow_Bool = value; } }

    public Queue<GameObject> DrawLinePool { get { return _DrawLinePool; } set { _DrawLinePool = value; } }

    public List<List<GameObject>> BonusPrepareDrawline { get { return _BonusPrepareDrawline; } set { _BonusPrepareDrawline = value; } }

    public List<GameObject> PrepareDrawLine { get { return _PrepareDrawLine; } set { _PrepareDrawLine = value; } }

    public List<List<bool>> BonusDrawLineOk { get { return _BonusDrawLineOk; } set { _BonusDrawLineOk = value; } }

    public List<bool> DrawLineOK { get { return _DrawLineOk; } set { _DrawLineOk = value; } }

    public List<Transform> UiShow { get { return _UiShow; } set { _UiShow = value; } }

    public List<List<Transform>> BonusUiShow { get { return _BonusUiShow; } set { _BonusUiShow = value; } }

    #endregion

    #region 表演的初始化方法
    /// <summary>
    /// 表演的初始化方法
    /// </summary>
    /// <param name="_Idate"></param>
    /// <param name="_Iui"></param>
    /// <param name="_ReelMove"></param>
    public void Init(IDate _Idate, ResourceManager _ResourceManager, IMove[] _ReelMove)
    {
        this._ReelMove = _ReelMove;
        this._IDate = _Idate;
        this._ResourceManager = _ResourceManager;
        _CoinShow_Bool = true;

        _BonusUiShow = new List<List<Transform>>();
        _BonusPrepareDrawline = new List<List<GameObject>>();
        _BonusDrawLineOk = new List<List<bool>>();
        _UiShow = new List<Transform>();
        _DrawLineOk = new List<bool>();
        _PrepareDrawLine = new List<GameObject>();

       

    }
    #endregion

    #region 取得連線時輪條上中獎的圖 並裝進陣列回傳
    /// <summary>
    /// 取得連線時輪條上中獎的圖 並裝進陣列回傳
    /// </summary>
    /// <param name="Date"></param>
    /// <param name="SlotSequence"></param>
    /// <returns></returns>
    public List<RectTransform> GetRoolWinImg(int SlotSequence)
    {
        string SNumber;
        List<RectTransform> VVVs;
        VVVs = new List<RectTransform>();
        SNumber = SlotSequence.ToString();

        for (int i = 0; i < _ReelMove.Length; i++)
        {
            char Number = SNumber[i];
            int Inumber = int.Parse(Number.ToString());
            VVVs.Add(_ReelMove[i].Reel_images[Inumber].rectTransform);
            //Debug.Log(VVVs[i]);
        }

        return VVVs;

    }
    #endregion

    #region 將要表演的圖片放入List
    /// <summary>
    /// 將要表演的圖片放入List
    /// </summary>
    /// <param name="_GetRoolWinImg"></param>
    public void ListShiny(int Number, List<Transform> ReadyShow)
    {

        Debuger.Log("ListShiny");
        List<RectTransform> GetTrans = GetRoolWinImg(Number);//取得RectTransform

        for (int i = 0; i < GetTrans.Count; i++)
        {

            if (!ReadyShow.Contains(GetTrans[i]))//檢測ReadyShow 有沒有相同內容
            {

                //Debug.Log("ListShiny . ADD");
                ReadyShow.Add(GetTrans[i]);

            }

        }

    }
    #endregion

    #region 打開Uishow陣列內物件的Animator的Trigger
    /// <summary>
    /// 打開Uishow陣列內物件的Animator的Trigger
    /// </summary>
    public IEnumerator ShinyShow(List<Transform> ReadyShow)
    {
        for (int i = 0; i < ReadyShow.Count; i++)
        {

            Animator Amo;
            Amo = ReadyShow[i].GetComponent<Animator>();
            Amo.SetTrigger("StShiny");

        }

        yield return null;
    }
    #endregion

    #region 預制物初始化
    /// <summary>
    /// 預制物初始化
    /// </summary>
    /// <param name="Reuse"></param>
    /// <param name="OrangePoint"></param>
    /// <param name="PointPosition"></param>
    /// <param name="EndPoint"></param>
    public void InstObjectInitialization(GameObject Reuse, RectTransform OrangePoint, List<RectTransform> PointPosition, RectTransform EndPoint)
    {
        Reuse.GetComponent<DrawLine>().Taget_Point = new List<Transform>();

        for (int i = 0; i < PointPosition.Count; i++)
        {

            Reuse.GetComponent<DrawLine>().Taget_Point.Add(PointPosition[i].transform);

        }

        Reuse.GetComponent<DrawLine>().Temp_point = new List<Transform>();
        Reuse.GetComponent<DrawLine>().Temp_point.Add(OrangePoint);
        Reuse.GetComponent<DrawLine>().Temp_point.Add(Reuse.GetComponent<DrawLine>().gameObject.transform);
        Reuse.GetComponent<DrawLine>().Temp_VV = Reuse.GetComponent<DrawLine>().Taget_Point[0].transform;//PointPosition[0];
        Reuse.GetComponent<DrawLine>().Orange_point = OrangePoint.gameObject;
        Reuse.GetComponent<DrawLine>().Taget_Point.Add(EndPoint);
        //Debuger.Log("Manager__ Temp_point.Count : " + Reuse.GetComponent<DrawLine>().Temp_point.Count);
        //Debuger.Log("Manager__Taget_Point.Count : " + Reuse.GetComponent<DrawLine>().Taget_Point.Count);

    }
    #endregion

    #region 物件池 （判斷物件池內有無物件 有就取出 無就生成）
    /// <summary>
    /// 物件池 （判斷物件池內有無物件 有就取出 無就生成）
    /// </summary>
    /// <param name="initalPosition"></param>
    /// <param name="PointPosition"></param>
    /// <param name="EndPoint"></param>
    public void ObjectPool(int Number, Transform StayOutpool, List<GameObject> StayDrawLine)//起點,中間位置群,終點,畫面上要放進等待表演的父物件,等待要表演的陣列
    {

        string Snumber = Number.ToString();
        char FirstNumber = Snumber[0];
        char EndNumber = Snumber[Snumber.Length - 1];
        RectTransform First = null;
        RectTransform End = null;
        GameObject Reuse;

        switch (FirstNumber)
        {
            case '1':
                {
                    First = _ResourceManager._StartPoint[0];
                    break;

                }
            case '2':
                {
                    First = _ResourceManager._StartPoint[1];
                    break;

                }
            case '3':
                {
                    First = _ResourceManager._StartPoint[2];
                    break;

                }

        }

        switch (EndNumber)
        {
            case '1':
                {
                    End = _ResourceManager._StartPoint[3];
                    break;

                }
            case '2':
                {
                    End = _ResourceManager._StartPoint[4];
                    break;

                }
            case '3':
                {
                    End = _ResourceManager._StartPoint[5];
                    break;

                }

        }

        if (_DrawLinePool.Count > 0)//如果有_DrawLinePool裡有預制物就取出來用
        {

            Reuse = _DrawLinePool.Dequeue();//取出_DrawLinePool的預制物
            Reuse.transform.position = First.position;//設置預制物的位置
            InstObjectInitialization(Reuse, First, GetRoolWinImg(Number), End);
            Reuse.transform.parent = StayOutpool;
            StayDrawLine.Add(Reuse);//放進去等待啟動

        }
        else
        {

            Reuse = Instantiate(_ResourceManager._GBDrawLine, StayOutpool);
            Reuse.transform.position = First.position;
            InstObjectInitialization(Reuse, First, GetRoolWinImg(Number), End);
            Reuse.SetActive(false);
            StayDrawLine.Add(Reuse);//放進去等待啟動

        }

    }
    #endregion

    #region 物件池生成設定
    /// <summary>
    /// 物件池生成設定
    /// </summary>
    public void ObjectPoolInitialization()
    {

        Debuger.Log("執行ObjectPoolInitialization");
        int InitailSize = 5;
        _DrawLinePool = new Queue<GameObject>();

        for (int i = 0; i < InitailSize; i++)
        {
            GameObject InstGb;
            InstGb = Instantiate(_ResourceManager._GBDrawLine, _ResourceManager._InLineRenderPool);//預制物生成
            _DrawLinePool.Enqueue(InstGb);//將生成的預制物方進_DrawLinePool這個Queue裡
            InstGb.SetActive(false);
            Debuger.Log("Queue的數量 _DrawLinePool.Count  : " + _DrawLinePool.Count);

        }

    }
    #endregion

    #region 物件用完回收物件池
    /// <summary>
    /// 物件用完回收物件池
    /// </summary>
    /// <param name="recover"></param>
    /// <param name="Stb"></param>
    public void Recover(GameObject recover, bool Stb)
    {
        Transform OringerTs = gameObject.transform;//OringerTs就是自身座標

        if (!Stb)
        {
            Debuger.Log("---------------------------Start_Recover :------------------------");
            recover.GetComponent<DrawLine>().LI.positionCount = 0;
            recover.GetComponent<DrawLine>().LI.positionCount = 2;
            _DrawLinePool.Enqueue(recover);
            recover.transform.parent = _ResourceManager._InLineRenderPool;
            recover.SetActive(false);

        }

    }
    #endregion

    #region 判斷DrawLine完了沒 ,結束後執行回收（執行Recover）
    /// <summary>
    /// /判斷DrawLine完了沒 ,結束後執行回收（執行Recover）
    /// </summary>
    /// <param name="OutPool"></param>
    /// <param name="StayDrawLine"></param>
    /// <param name="_SHowOk"></param>
    public void RecoverComply(Transform OutPool, List<GameObject> StayDrawLine, List<bool> _SHowOk)
    {

        bool DrawAllBool;
        DrawAllBool = _SHowOk.Contains(true);//確認Lineder都跑完了沒
        //Debuger.Log(_SHowOk.Contains(true));

        if (StayDrawLine.Count != 0)//LineRenderOutPool.childCount != 0
        {

            //Debuger.Log("----------------LineRenderOutPool.childCount : ----------------------"+ LineRenderOutPool.childCount);
            //Debuger.Log("StayDrawLine.Count :" + StayDrawLine.Count);

            if (_SHowOk.Count != 0 && StayDrawLine.Count != 0)//持續更新裡面的bool
            {

                for (int i = 0; i < StayDrawLine.Count; i++)
                {
                    // Debug.Log("_SHowOk[i] 持續更新");
                    _SHowOk[i] = StayDrawLine[i].GetComponent<DrawLine>().StDrawLine;

                }

            }

            //Debuger.Log("--------------DrawStop :--------"+ DrawStop);

            if (!DrawAllBool && _SHowOk.Count != 0)//DrawLine 全部線都表演完
            {
                for (int i = 0; i < OutPool.childCount; i++)
                {

                    Recover(OutPool.transform.GetChild(i).gameObject, DrawAllBool);

                }

                if (OutPool.childCount == 0)
                {

                    StayDrawLine.Clear();
                    _SHowOk.Clear();
                    //Debuger.Log("------------PrepareDrawLine.Count :------------" + PrepareDrawLine.Count);

                }

            }

        }

    }
    #endregion

    #region 啟動畫線
    /// <summary>
    /// 啟動畫線
    /// </summary>
    /// <param name="StayDrawLine"></param>
    /// <param name="_ShowOk"></param>
    /// <returns></returns>
    public IEnumerator StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk)
    {
        for (int i = 0; i < StayDrawLine.Count; i++)
        {

            StayDrawLine[i].SetActive(true);
            StayDrawLine[i].GetComponent<DrawLine>().StDrawLine = true;
            _ShowOk.Add(StayDrawLine[i].GetComponent<DrawLine>().StDrawLine);//把DrawLine的Bool放進陣列統一判斷是否結束

        }

        yield return null;
    }
    #endregion

    #region 金錢表演
    /// <summary>
    /// 金錢表演
    /// </summary>
    /// <param name="Coin"></param>
    /// <param name="FreeGameCount"></param>
    /// <returns></returns>
    public IEnumerator CoinShow(int Coin)
    {
        _CoinShow_Bool = false;

        while (_TempWinCoin <= Coin)
        {
            //Debuger.Log("開始CoinShow 迴圈");

            if (_TempWinCoin <= Coin)
            {
                Debuger.Log("開始CoinShow 數字換圖");
                string SWinCoin = _TempWinCoin.ToString();
                Debuger.Log("SWinCoin(暫存金額) ：" + SWinCoin);
                int ImgCount = SWinCoin.Length;//取得幾位數
                Debuger.Log("本局Win金額 ＝ " + ImgCount + "位數");

                for (int i = 0; i < ImgCount; i++)
                {

                    int TempValu;//用字串處理 取的 Win_Money_Temp各個位數的值
                    TempValu = int.Parse(SWinCoin.Substring(i, 1));
                    _ResourceManager._WinCoins[i].gameObject.SetActive(true);
                    _ResourceManager._WinCoins[i].sprite = _ResourceManager._Numbers[TempValu];

                }

                yield return null;

                if (_TempWinCoin >= Coin)
                {

                    //Debuger.Log("開始CoinShow 迴圈 ： 已到目標 表演金額");
                    yield return new WaitForSeconds(1f);
                    int Win_All_Coin_Temp;
                    yield return new WaitForSeconds(0.5f);

                    for (int i = 0; i < _ResourceManager._WinCoins.Length; i++)
                    {

                        _ResourceManager._WinCoins[i].gameObject.SetActive(false);

                    }

                    _TempWinCoin = 0;

                    if (_AddCoin)
                    {

                        Win_All_Coin_Temp = _IDate.PlayerCoin + Coin;
                        _IDate.PlayerCoin = Win_All_Coin_Temp;
                        _ResourceManager._PlayerCoin_Text.text = "Money:" + Win_All_Coin_Temp.ToString();

                    }

                    Coin = 0;
                    _CoinShow_Bool = true;

                }

            }

            if (Coin - _TempWinCoin < 50)
            {

                _TempWinCoin += 1;
                yield return new WaitForSeconds(0.01f);

            }
            else
            {

                _TempWinCoin += 21;
                yield return new WaitForSeconds(0.01f);

            }

        }

    }
    #endregion

}

