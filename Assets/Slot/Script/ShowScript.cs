using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
[System.Serializable]
public class ShowScript : MonoBehaviour,IShow
{

    IDate _IDate;
    IUIControlMethod _IUIMethod;

    [SerializeField]
    Sprite[] _Numbers;

    [SerializeField]
    Image[] _WinCoins;

    [SerializeField]
    int _TempWinCoin;

    [SerializeField]
    bool _StRecover;

    [SerializeField]
    Transform[] _StartPoint;

    [SerializeField]
    Queue<GameObject> _DrawLinePool;//預置物放置取出的位置

    [SerializeField]
    List<List<GameObject>> _BonusPrepareDrawline;

    [SerializeField]
    List<GameObject> _PrepareDrawLine;

    [SerializeField]
    List<List<bool>> _BonusDrawLineOk;//確認 Bonus每個Linerender都到最後

    [SerializeField]
    List<bool> _DrawLineOk;//確認每個Linerender都到最後

    [SerializeField]
    GameObject _GBDrawLine;//預置物

    [SerializeField]
    Transform _InLineRenderPool;//預置物放置在Hierarchy 顯示的位置

    [SerializeField]
    Transform _LineRenderOutPool;// 普盤用的預置物放置在Hierarchy 待表演 顯示的位置

    [SerializeField]
    Transform _BonusLineRenderOutPool;// Bonus盤用的預置物放置在Hierarchy 待表演 顯示的位置

    [SerializeField]
    GameObject _BonusLineRenderChildPool;

    [SerializeField]
    List<Transform> _UiShow;//普盤 裝等待Shiny表演的

    [SerializeField]
    List<List<Transform>> _BonusUiShow;//Bonus盤 裝等待Shiny表演的

    [SerializeField]
    Animator _Amr_WinShow;//跳錢得背景動畫

    [SerializeField]
    Animator _BonusAnimator;//Bonus開頭的動畫表演

    [SerializeField]
    Animator _BonusEndShow;//Bonus最後的總跳錢背景動畫

    [SerializeField]
    VideoPlayer _EndShowPlayer;

    [SerializeField]
    VideoClip _EndShowVideoClip;

    [SerializeField]
    RawImage _VideoImage;

    public Sprite[] Numbers { get { return _Numbers; }set { _Numbers = value; } }

    public Image[] WinCoins { get { return _WinCoins; } set { _WinCoins = value; } }

    public int TempWinCoin { get { return _TempWinCoin; }set { _TempWinCoin = value; } }

    public bool StRecover { get { return _StRecover; } set { StRecover = value; }  }

    public Transform[] StartPoint { get{ return _StartPoint; } }

    public Queue<GameObject> DrawLinePool { get {return _DrawLinePool; } set { _DrawLinePool = value; } }

    public List<List<GameObject>> BonusPrepareDrawline { get { return _BonusPrepareDrawline; } set { _BonusPrepareDrawline = value; } }

    public List<GameObject> PrepareDrawLine { get {return _PrepareDrawLine; } set { _PrepareDrawLine = value; } }

    public List<List<bool>> BonusDrawLineOk { get { return _BonusDrawLineOk; } set { _BonusDrawLineOk = value; } }

    public List<bool> DrawLineOK { get {return _DrawLineOk;} set {_DrawLineOk=value;} }

    public GameObject GBDrawLine { get { return _GBDrawLine; } set { _GBDrawLine = value; } }

    public Transform InLineRenderPool { get {return _InLineRenderPool; } set { _InLineRenderPool=value; } }

    public Transform LineRenderOutPool { get { return _LineRenderOutPool; } set { _LineRenderOutPool=value; } }

    public Transform BonusLineRenderOutPool { get { return _BonusLineRenderOutPool; } set { _BonusLineRenderOutPool=value; } }

    public GameObject BonusLineRenderChildPool { get { return _BonusLineRenderChildPool; } set { _BonusLineRenderChildPool = value; } }

    public List<Transform> UiShow { get { return _UiShow; } set { _UiShow = value; } }

    public List<List<Transform>> BonusUiShow { get { return _BonusUiShow; }set { _BonusUiShow = value; } }

    public Animator Amr_WinShow { get { return _Amr_WinShow; } set { _Amr_WinShow = value; } }

    public Animator BonusAnimator { get {return _BonusAnimator; } set { _BonusAnimator = value; } }

    public Animator BonusEndShow { get { return _BonusEndShow; } set { _BonusEndShow = value; } }

    public VideoPlayer EndShowPlayer { get { return _EndShowPlayer; } set { _EndShowPlayer = value; } }

    public VideoClip EndShowVideoClip { get { return _EndShowVideoClip; } set { _EndShowVideoClip = value; } }

    public RawImage VideoImage { get { return _VideoImage; } set { _VideoImage = value; } }


    /// <summary>
    /// 取得連線時輪條上中獎的圖 並裝進陣列回傳
    /// </summary>
    /// <param name="Date"></param>
    /// <param name="SlotSequence"></param>
    /// <returns></returns>
    public List<Transform> GetRoolWinImg(Reel_Move[] _ReelMove, int[] SlotSequence)
    {

        List<Transform> VVVs;
        VVVs = new List<Transform>();
        for (int i = 0; i < _ReelMove.Length; i++)
        {

            VVVs.Add(_ReelMove[i].Reel_images[i].rectTransform);
            Debug.Log(VVVs[i]);
        }
        return VVVs;

    }

    /// <summary>
    /// 將要表演的圖片放入List
    /// </summary>
    /// <param name="_GetRoolWinImg"></param>
    public void ListShiny(List<Transform> _GetRoolWinImg, List<Transform> ReadyShow)
    {

        Debug.Log("ListShiny");

        for (int i = 0; i < _GetRoolWinImg.Count; i++)
        {
            if (!ReadyShow.Contains(_GetRoolWinImg[i]))
            {
                Debug.Log("ListShiny . ADD");
                ReadyShow.Add(_GetRoolWinImg[i]);

            }

        }

    }

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
            yield return null;

        }
    }


    /// <summary>
    /// 預制物初始化
    /// </summary>
    /// <param name="Reuse"></param>
    /// <param name="OrangePoint"></param>
    /// <param name="PointPosition"></param>
    /// <param name="EndPoint"></param>
    public void InstObjectInitialization(GameObject Reuse, Transform OrangePoint, List<Transform> PointPosition, Transform EndPoint)
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
        //Debug.Log("Manager__ Temp_point.Count : " + Reuse.GetComponent<DrawLine>().Temp_point.Count);
        //Debug.Log("Manager__Taget_Point.Count : " + Reuse.GetComponent<DrawLine>().Taget_Point.Count);
    }


    /// <summary>
    /// 物件池 （判斷物件池內有無物件 有就取出 無就生成）
    /// </summary>
    /// <param name="initalPosition"></param>
    /// <param name="PointPosition"></param>
    /// <param name="EndPoint"></param>
    public void ObjectPool(Transform initalPosition, List<Transform> PointPosition, Transform EndPoint, Transform StayOutpool, List<GameObject> StayDrawLine)
    {
        GameObject Reuse;
        if (DrawLinePool.Count > 0)//如果有DrawLinePool裡有預制物就取出來用
        {

            Reuse = DrawLinePool.Dequeue();//取出DrawLinePool的預制物
            Reuse.transform.position = initalPosition.position;//設置預制物的位置

            InstObjectInitialization(Reuse, initalPosition, PointPosition, EndPoint);
            Reuse.transform.parent = StayOutpool;
            StayDrawLine.Add(Reuse);//放進去等待啟動

        }
        else
        {

            Reuse = Instantiate(GBDrawLine, StayOutpool);
            Reuse.transform.position = initalPosition.position;

            InstObjectInitialization(Reuse, initalPosition, PointPosition, EndPoint);
            Reuse.SetActive(false);
            StayDrawLine.Add(Reuse);//放進去等待啟動


        }

    }

    /// <summary>
    /// 物件池生成設定
    /// </summary>
    public void ObjectPoolInitialization()
    {
        int InitailSize = 5;

        DrawLinePool = new Queue<GameObject>();

        for (int i = 0; i < InitailSize; i++)
        {
            GameObject InstGb;

            InstGb = Instantiate(GBDrawLine, InLineRenderPool);//預制物生成
            DrawLinePool.Enqueue(InstGb);//將生成的預制物方進DrawLinePool這個Queue裡
            InstGb.SetActive(false);
            Debug.Log("DrawLinePool.Count:" + DrawLinePool.Count);
        }
    }

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
            Debug.Log("---------------------------Start_Recover :------------------------");

            recover.GetComponent<DrawLine>().LI.positionCount = 0;
            recover.GetComponent<DrawLine>().LI.positionCount = 2;

            DrawLinePool.Enqueue(recover);
            recover.transform.parent = InLineRenderPool;
            recover.SetActive(false);

        }

    }

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


        if (StayDrawLine.Count != 0)//LineRenderOutPool.childCount != 0
        {
            //Debug.Log("----------------LineRenderOutPool.childCount : ----------------------"+ LineRenderOutPool.childCount);
            //Debug.Log("StayDrawLine.Count :" + StayDrawLine.Count);

            if (_SHowOk.Count != 0 && StayDrawLine.Count != 0)//持續更新裡面的bool
            {

                for (int i = 0; i < StayDrawLine.Count; i++)
                {
                    //Debug.Log("_SHowOk[i] 持續更新");
                    _SHowOk[i] = StayDrawLine[i].GetComponent<DrawLine>().StDrawLine;

                }

            }

            //Debug.Log("--------------DrawStop :--------"+ DrawStop);

            if (!DrawAllBool && _SHowOk.Count != 0)//DrawLine 全部線都表演完
            {
                for (int i = 0; i < OutPool.childCount; i++)
                {

                    Recover(OutPool.transform.GetChild(i).gameObject, DrawAllBool); //這裡因為Bonus每個盤面生成的物件都放這 所以出了問題

                }

                if (OutPool.childCount == 0)
                {

                    StayDrawLine.Clear();
                    _SHowOk.Clear();
                    //Debug.Log("------------PrepareDrawLine.Count :------------" + PrepareDrawLine.Count);
                    StRecover = true;
                }

            }


        }
    }


    public IEnumerator StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk)
    {
        for (int i = 0; i < StayDrawLine.Count; i++)
        {
            StayDrawLine[i].SetActive(true);
            StayDrawLine[i].GetComponent<DrawLine>().StDrawLine = true;
            _ShowOk.Add(StayDrawLine[i].GetComponent<DrawLine>().StDrawLine);

        }
        yield return null;
    }


    public IEnumerator CoinShow(int Coin,int FreeGameCount)
    {

        while (TempWinCoin < Coin)
        {

            if (TempWinCoin <= Coin)
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

            }
            else if (TempWinCoin >= Coin)
            {
                yield return new WaitForSeconds(1f);
                int Win_All_Coin_Temp;

                yield return new WaitForSeconds(0.2f);
                for (int i = 0; i < WinCoins.Length; i++)
                {
                    WinCoins[i].gameObject.SetActive(false);


                }

                TempWinCoin = 0;
                Win_All_Coin_Temp = _IDate.PlayerCoin + Coin;
                _IDate.PlayerCoin = Win_All_Coin_Temp;
                _IUIMethod.PlayerCoin_Text.text = "Money:" + Win_All_Coin_Temp.ToString();
                if (_IDate.BonusCount==3)
                {

                    _IDate.BonusWinCoin[FreeGameCount] = 0;

                }
                else
                {

                    _IDate.Win_Coin = 0;

                }

                yield return new WaitForSeconds(0.5f);


            }

            if (Coin - TempWinCoin < 50)
            {
                TempWinCoin += 1;
                yield return new WaitForSeconds(0.01f);

            }
            else
            {

                TempWinCoin += 21;
                yield return new WaitForSeconds(0.01f);
            }

        }


        

    }



}


