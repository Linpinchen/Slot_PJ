using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel_Move : MonoBehaviour, IMove
{

    bool _strool;//是否要滾動
    Vector2 _originalv2;//原始座標
    RectTransform _ReelV2;//移動用的座標
    List<Image> _Reel_images; //掛著這個腳本的物件 的 子物件Image
    [SerializeField]
    int _tempi;//計算換圖次數
    float _Speed;
    int _Date_Temp;
    [SerializeField]
    int _Roolcount;
    Sprite[] _Sprites;//圖庫
    List<int> _ChangeSprite;//要換的圖片資料
    GameObject _Self;//自己本身這個物件

    #region 介面實作內容
    public bool strool { get { return _strool; } set { _strool = value; } }
    public Vector2 originalv2 { get { return _originalv2; } set { _originalv2 = value; } }
    public RectTransform ReelV2 { get { return _ReelV2; } set { _ReelV2 = value; } }
    public List<Image> Reel_images { get { return _Reel_images; } set { _Reel_images = value; } }
    public int tempi { get { return _tempi; } set { _tempi = value; } }
    public float Speed { get { return _Speed; } set { _Speed = value; } }
    public int Date_Temp { get { return _Date_Temp; } set { _Date_Temp = value; } }
    public int Roolcount { get { return _Roolcount; } set { _Roolcount = value; } }
    public Sprite[] Sprites { get { return _Sprites; } set { _Sprites = value; } }
    public List<int> ChangeSprite { get { return _ChangeSprite; } set { _ChangeSprite = value; } }
    public GameObject Self { get { return _Self; } set { _Self = value; } }
    #endregion

    void Start()
    {


        Date_Temp = 0;
        originalv2 = gameObject.GetComponent<RectTransform>().anchoredPosition;//取得原始座標
        //Debug.Log("originalv2" + originalv2);
        ReelV2 = gameObject.GetComponent<RectTransform>();//移動用的座標
        Reel_images = new List<Image>();//取得掛著這個腳本的物件 的 子物件Image

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Reel_images.Add(gameObject.transform.GetChild(i).GetComponent<Image>());

        }

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
    public void Init(int Roolcount, Sprite[] sprites, float Speed)
    {
        _Self = this.gameObject;
        this.Roolcount = Roolcount;
        this.Sprites = sprites;
        this.Speed = Speed;
        _ChangeSprite = new List<int>();

        for (int i = 0; i < this.transform.childCount; i++)
        {

            _ChangeSprite.Add(0);

        }

    }
    #endregion

    #region 移動方法
    public void Move()
    {
        if (tempi < Roolcount && _strool == true)
        {
            int Date_Chang_Count = Roolcount - gameObject.transform.childCount;//換圖次數-子物件數＝隨機換圖次數 ,（因為最後要留單輪條子物件數來灌入Date的盤面資料）
            ReelV2.anchoredPosition += new Vector2(0, -2.5f * Speed * Time.deltaTime) /* * Speed * Time.deltaTime*/;

            if (ReelV2.anchoredPosition.y <= -180)
            {

                ReelV2.anchoredPosition = originalv2;//回歸初始座標
                int ri;
                ri = Random.Range(0, Sprites.Length);//換圖用隨機值

                for (int i = 0; i < gameObject.transform.childCount; i++)
                {

                    if (i < gameObject.transform.childCount - 1)//這裡做前四張圖 換成各自的上一個位置的圖片
                    {

                        Reel_images[i].sprite = Reel_images[i + 1].sprite;//n的圖片換成n+1的

                    }

                    else//最後一張圖要隨機給圖 如果到需要灌入Date資料的次數時
                    {

                        if (tempi >= Date_Chang_Count)
                        {

                            Reel_images[i].sprite = Sprites[_ChangeSprite[Date_Temp]];
                            tempi++;
                            Date_Temp++;
                            //Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount + "Sprites :" + ChangeSprite[i]);

                            if (tempi == Roolcount)//這裡做重置的動作
                            {

                                strool = false;
                                //tempi = 0;
                                Date_Temp = 0;

                            }

                        }
                        else
                        {

                            Reel_images[i].sprite = Sprites[ri];
                            tempi++;
                            // Debug.Log("tempi:" + tempi + "Roolcount" + Roolcount);

                        }

                    }

                }

            }

        }

    }
    #endregion
}
