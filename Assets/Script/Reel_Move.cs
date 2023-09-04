using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Reel_Move : MonoBehaviour, IMove
{

    //bool _strool;//是否要滾動

    Vector2 _originalv2;//原始座標

    RectTransform _ReelV2;//移動用的座標

    List<Image> _Reel_images; //掛著這個腳本的物件 的 子物件Image

    [SerializeField]
    int _Finaltempi;//計算最終盤面換圖次數 

    float _Speed;//移動速度

    Sprite[] _Sprites;//滾動圖的圖庫 （換圖用）


    List<int> _FinalChangeSprite;//要換的最終圖片資料


    GameObject _Self;//自己本身這個物件


    Tweener tweener;


    int _Reelnumber;//輪條編號

    bool _MoveEnd;//滾動狀況 false為開始滾動

    //--------------------(新增得內容)-----------------------------------

    bool ChangeFinalSprite;//是否變更為最終盤面


    List<int> _RoolSprite;//滾動的圖片資料放這 （也是用這個判斷需不需要滾動）


    public delegate void DecideRool(IMove move, int SpriteLenght,ref bool b);

    public DecideRool _DecideRool;//是否需要增加滾動圖片資料

    public delegate void CheckEndRool(IMove move);

    public CheckEndRool _CheckEndRool;//輪條是否結束滾動 將輪條是否結束的狀態 傳回Manager

    bool _DecideRool_B;//是否加過額外圖了

    //多用一個委派 作為停輪檢測 由Manager寫方法 給各輪條的腳本

    // 像是Manager 有一個Bool[]  謝一個方法 void (IMove move , bool b ) { bool[ move. ReelNumber] = b ; }
    //各輪條都有一個bool b  開始滾動時 b =True 並 執行委派, 當停輪時 b=false , 並執行委派 


    #region 介面實作內容
    //public bool strool { get { return _strool; } set { _strool = value; } }
    public Vector2 originalv2 { get { return _originalv2; } set { _originalv2 = value; } }
    public RectTransform ReelV2 { get { return _ReelV2; } set { _ReelV2 = value; } }
    public List<Image> Reel_images { get { return _Reel_images; } set { _Reel_images = value; } }
 
    public float Speed { get { return _Speed; } set { _Speed = value; } }
    public int Reelnumber { get { return _Reelnumber; }set { _Reelnumber = value; } }
    //public int Date_Temp { get { return _Date_Temp; } set { _Date_Temp = value; } }
    //public int Roolcount { get { return _Roolcount; } set { _Roolcount = value; } }
    public Sprite[] Sprites { get { return _Sprites; } set { _Sprites = value; } }
    public List<int> FinalChangeSprite { get { return _FinalChangeSprite; } set { _FinalChangeSprite = value; } }
    public List<int> RoolSprite { get {return _RoolSprite; } set { _RoolSprite = value;  } }
    public GameObject Self { get { return _Self; } set { _Self = value; } }
    public bool MoveEnd { get { return _MoveEnd; } }

    #endregion

    void Start()
    {

       

  
        originalv2 = gameObject.GetComponent<RectTransform>().anchoredPosition;//取得原始座標

        //Debug.Log("originalv2" + originalv2);

        ReelV2 = gameObject.GetComponent<RectTransform>();//移動用的座標

        Reel_images = new List<Image>();//取得掛著這個腳本的物件 的 子物件Image
       

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Reel_images.Add(gameObject.transform.GetChild(i).GetComponent<Image>());

        }

        tweener = _ReelV2.DOLocalMoveY(0, 1);

        tweener.SetEase(Ease.OutElastic);

        tweener.SetAutoKill(false);

        tweener.Pause();

    }


    void Update()
    {

        Move();

    }


    #region 初始設定方法
    /// <summary>
    /// 輪條初始設定方法
    /// </summary>
    /// <param name="Roolcount"></param>
    /// <param name="sprites"></param>
    /// <param name="Speed"></param>
    public void Init( Sprite[] sprites, float Speed,int Reelnumber, DecideRool _DecideRool, CheckEndRool _CheckEndRool)//(int Roolcount, Sprite[] sprites, float Speed )
    {

        _Reelnumber = Reelnumber;

        ChangeFinalSprite = false;
        _Self = this.gameObject;
        //this.Roolcount = Roolcount;
        this._Sprites = sprites;
        this.Speed = Speed;
        _FinalChangeSprite = new List<int>();

        _RoolSprite = new List<int>();


        for (int i = 0; i < this.transform.childCount; i++)
        {

            _FinalChangeSprite.Add(0);

        }

        this._DecideRool = _DecideRool;

        this._CheckEndRool = _CheckEndRool;

        _Finaltempi = 0;

        //this._DecideRool = _DecideRool;//  執行產圖方法 用這樣 _DecideRool(this.transform.childCount, _Sprites.Length)

        _MoveEnd = true;
        _DecideRool_B = false;


    }
    #endregion

   

    #region 移動方法
    //public void Move()
    //{
    //    if (tempi < Roolcount && _strool == true)//改成if(RoolSprite.count!=0) 有資料就滾動
    //    {
    //        int Date_Chang_Count = Roolcount - gameObject.transform.childCount;//換圖次數-子物件數＝隨機換圖次數 ,（因為最後要留單輪條子物件數來灌入Date的盤面資料）
    //        ReelV2.anchoredPosition += new Vector2(0, -2.5f * Speed * Time.deltaTime) /* * Speed * Time.deltaTime*/;

    //        if (ReelV2.anchoredPosition.y <= -180)
    //        {
    //            if (tempi != Roolcount - 1)
    //            {
    //                ReelV2.anchoredPosition = originalv2;//回歸初始座標

    //            }
    //            else
    //            {
    //                tweener.Restart();

    //            }
    //            int ri;
    //            ri = Random.Range(0, _Sprites.Length);//換圖用隨機值

    //            for (int i = 0; i < gameObject.transform.childCount; i++)
    //            {

    //                if (i < gameObject.transform.childCount - 1)//這裡做前四張圖 換成各自的上一個位置的圖片
    //                {

    //                    Reel_images[i].sprite = Reel_images[i + 1].sprite;//n的圖片換成n+1的

    //                }

    //                else//最後一張圖要隨機給圖 如果到需要灌入Date資料的次數時
    //                {

    //                    if (tempi >= Date_Chang_Count)
    //                    {

    //                        Reel_images[i].sprite = _Sprites[_ChangeSprite[Date_Temp]];
    //                        tempi++;
    //                        Date_Temp++;
    //                        //Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount + "Sprites :" + ChangeSprite[i]);

    //                        if (tempi == Roolcount)//這裡做重置的動作
    //                        {
    //                            Debuger.Log("-----------------------重置-------------------------------------------------------");
    //                            strool = false;
    //                            //tempi = 0;
    //                            Date_Temp = 0;
                                

    //                        }

    //                    }
    //                    else
    //                    {

    //                        Reel_images[i].sprite = _Sprites[ri];
    //                        tempi++;
    //                        // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);

    //                    }

    //                }

    //            }

    //        }

    //    }

    //}
    #endregion

    #region 移動方法
    public void Move()
    {
        if ((_RoolSprite!=null && _RoolSprite.Count!=0) || ChangeFinalSprite)//改成if(RoolSprite.count!=0) 有資料 或變更為 給最終盤面 就滾動
        {

            _MoveEnd = false;

            _CheckEndRool(this);

            //想一下 RoolSprite.Count＝=0 代表隨機圖結束 結束後要接著給 最終盤資料 

            //int Date_Chang_Count = Roolcount - gameObject.transform.childCount;//換圖次數-子物件數＝隨機換圖次數 ,（因為最後要留單輪條子物件數來灌入Date的盤面資料）
            ReelV2.anchoredPosition += new Vector2(0, -2.5f * Speed * Time.deltaTime) /* * Speed * Time.deltaTime*/;

            if (ReelV2.anchoredPosition.y <= -180)
            {
                if (_Finaltempi == _FinalChangeSprite.Count - 1)
                {
                    Debug.Log("Use DoTween ");
                    tweener.Restart();
                }
                else
                {
                    ReelV2.anchoredPosition = originalv2;//回歸初始座標

                }
              
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {

                    if (i < gameObject.transform.childCount - 1)//這裡做前四張圖 換成各自的上一個位置的圖片
                    {

                        Reel_images[i].sprite = Reel_images[i + 1].sprite;//n的圖片換成n+1的

                    }

                    else//最後一張圖要隨機給圖 如果到需要灌入Date資料的次數時
                    {

                        if (ChangeFinalSprite)
                        {

                            Reel_images[i].sprite = _Sprites[_FinalChangeSprite[_Finaltempi]];
                            _Finaltempi++;
                          
                            //Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount + "Sprites :" + ChangeSprite[i]);

                            if (_Finaltempi == _FinalChangeSprite.Count)//這裡做重置的動作
                            {
                               

                                Debuger.Log("-----------------------重置-------------------------------------------------------");
                                //strool = false;
                                //tempi = 0;
                                _Finaltempi = 0;

                                ChangeFinalSprite = false;
                                _MoveEnd = true;
                                _CheckEndRool(this);
                                _DecideRool_B = false;
                            }

                        }
                        else//讀取滾動資料圖
                        {
                           
                            Reel_images[i].sprite = _Sprites[_RoolSprite[0]];
                            _RoolSprite.RemoveAt(0);

                            if (!_DecideRool_B)
                            {

                                _DecideRool(this, _RoolSprite.Count, ref _DecideRool_B);
                            }
                           



                            if (_RoolSprite.Count==0)
                            {
                                ChangeFinalSprite = true;
                            }
                            
                            
                           
                            // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);

                        }

                    }

                }

            }

        }

    }
    #endregion

}
