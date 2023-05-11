using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ResourceManager : MonoBehaviour
{
    //-----------------SlotDate-------------------
    public Sprite[] Sprite_Pool;//獎項圖片庫

    //-----------------UIControl-------------------
    public GameObject _Bet_Menu;//押注小視窗
    public GameObject _Auto_Menu;//自動循環小視窗
    public GameObject _Options;//遊戲紀錄是否繼承畫面
    public Text _Bet_Text;//押注按鈕式的押注金額
    public Text _BetMenu_Text;//押注小視窗的押注總金額
    public Text _Auto_text;//自動循環小視窗的 循環次數
    public Text _PlayerCoin_Text;//玩家當前擁有的金額文字
    public Sprite[] _InFoSprites;//INfo畫面要用到的替換圖
    public Image _InfoBackSprite;//INfo畫面的背景Image
    public Image _Img_Introduction;//INfo畫面要用到的替換圖的Image
    public Image _Img_Operational;//Operational畫面的Image

    //-----------------遊戲畫面加按鈕-------------------

    public Button StartGame_Button;
    public Button Bet_Button;
    public Button Auto_Button;
    public Button Bet_Plus_Button;
    public Button Bet_Reduce_Button;
    public Button Bet_MaxCoin_Button;
    public Button Auto_Clear_Button;
    public Button Auto_pause_Button;
    public Button Auto_Plus_Button;
    public Button Auto_Reduce_Button;
    public Button BonusDateCreat_Button;
    public Button InFoButton_Button;
    public Button InfoOutButton_Button;
    public Button ButtonLeft_Button;
    public Button ButtonRight_Button;
    public Button Options_Yes_Button;
    public Button Options_No_Button;
    public Button OpenOperational;
    public Button OutOperational;
    //----------------- Show -------------------


    public Sprite[] _Numbers;
    public Image[] _WinCoins;
    public Image _BonusWinBackSprite;
    public RectTransform[] _StartPoint;
    public Queue<GameObject> _DrawLinePool;//預置物放置取出的位置
    public List<List<GameObject>> _BonusPrepareDrawline;
    public List<GameObject> _PrepareDrawLine;
    public GameObject _GBDrawLine;//預置物
    public Transform _InLineRenderPool;//預置物放置在Hierarchy 顯示的位置
    public Transform _LineRenderOutPool;// 普盤用的預置物放置在Hierarchy 待表演 顯示的位置
    public Transform _BonusLineRenderOutPool;// Bonus盤用的預置物放置在Hierarchy 待表演 顯示的位置
    public List<Transform> _UiShow;//普盤 裝等待Shiny表演的
    public List<List<Transform>> _BonusUiShow;//Bonus盤 裝等待Shiny表演的
    public Animator _Amr_WinShow;//跳錢得背景動畫
    public Animator _BonusAnimator;//Bonus開頭的動畫表演
    public Animator _BonusEndShow;//Bonus最後的總跳錢背景動畫
    public VideoPlayer _EndShowPlayer;
    public VideoClip _EndShowVideoClip;
    public RawImage _VideoImage;



    public Transform Parent_WinSHow;
    public Transform Parent_PointImage;
    public Transform Parent_BonusEnd;
    public Transform Parent_BonusShow;
    public Transform Parent_BonusEndShow;

    public CASCompare _CSAComper;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public ResourceManager(string TxtUrl)
    //{

    //    _CSAComper = new CASCompare(TxtUrl);


    //}


    public void init(CASCompare _CSAComper)
    {


        this._CSAComper = _CSAComper;



    }





    public void LoadClientAssetBundel(Reel_Move[] reel_Moves)
    {

        AssetBundle DrawLine_Prefber = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[0]].CLPath);
        Object obj_DrawLine = DrawLine_Prefber.LoadAsset("DrawLine");
        _GBDrawLine = obj_DrawLine as GameObject;
        DrawLine_Prefber.Unload(false);



        AssetBundle Prefber_WinShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[1]].CLPath);
        Object obj_WinShow = Prefber_WinShow.LoadAsset("Obj_WinShow");
        GameObject _Prefber_WinShow = obj_WinShow as GameObject;
        GameObject gameObject_WinShow = Instantiate(_Prefber_WinShow, Parent_WinSHow);
        Prefber_WinShow.Unload(false);

        AssetBundle An_WinShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[0]].CLPath);
        RuntimeAnimatorController _An_WinShowControl = An_WinShowControl.LoadAsset<RuntimeAnimatorController>("WinShow");
        An_WinShowControl.Unload(false);

        AssetBundle An_WinShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[0]].CLPath);
        AnimationClip _An_WinShowClip = An_WinShowClip.LoadAsset<AnimationClip>("WinShowClip");
        gameObject_WinShow.GetComponent<Animator>().runtimeAnimatorController = _An_WinShowControl;
        An_WinShowClip.Unload(false);




        for (int i=0;i < reel_Moves.Length;i++)
        {
            Transform parent = reel_Moves[i].transform.parent;

            AssetBundle Prefber_RoolImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[2]].CLPath);
            Object obj_RoolImage = Prefber_RoolImage.LoadAsset("Image_Rool");
            GameObject _RoolImage = obj_RoolImage as GameObject;
            GameObject gameObject__RoolImage = Instantiate(_RoolImage, parent);
            Prefber_RoolImage.Unload(false);

            AssetBundle An_RoolControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[3]].CLPath);
            RuntimeAnimatorController _An_RoolControl = An_RoolControl.LoadAsset<RuntimeAnimatorController>("Rool");
            An_RoolControl.Unload(false);

            AssetBundle An_RoolClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[3]].CLPath);
            AnimationClip _An_RoolClip = An_RoolClip.LoadAsset<AnimationClip>("RoolClip");
            gameObject__RoolImage.GetComponent<Animator>().runtimeAnimatorController = _An_RoolControl;
            gameObject__RoolImage.SetActive(false);
            An_RoolClip.Unload(false);

            for (int j =1;j< reel_Moves[i].transform.childCount-1; j++)
            {

                Transform child = reel_Moves[i].transform.GetChild(j);

                AssetBundle An_UiShinyControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[1]].CLPath);
                RuntimeAnimatorController _An_UiShinyControl = An_UiShinyControl.LoadAsset<RuntimeAnimatorController>("UiShiny");
                An_UiShinyControl.Unload(false);

                AssetBundle An_UiShinyClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[1]].CLPath);
                AnimationClip _An_UiShinyClip = An_UiShinyClip.LoadAsset<AnimationClip>("UiShinyClip");
                child.GetComponent<Animator>().runtimeAnimatorController = _An_UiShinyControl;
                An_UiShinyClip.Unload(false);

            }


        }



        AssetBundle Prefber_PointImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[3]].CLPath);
        Object obj_PointImage = Prefber_PointImage.LoadAsset("Images_initalPositionS");
        GameObject _Prefber_PointImage = obj_PointImage as GameObject;
        Instantiate(_Prefber_PointImage, Parent_PointImage);
        Prefber_PointImage.Unload(false);



        AssetBundle Prefber_BonusEnd = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[4]].CLPath);
        Object obj_BonusEnd = Prefber_BonusEnd.LoadAsset("Image_BonusEnd");
        GameObject _Prefber_BonusEnd = obj_BonusEnd as GameObject;
        GameObject gameObject_BonusEnd = Instantiate(_Prefber_BonusEnd, Parent_BonusEnd);
        gameObject_BonusEnd.SetActive(false);
        Prefber_BonusEnd.Unload(false);



        AssetBundle Prefber_BonusShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[5]].CLPath);
        Object obj_BonusShow = Prefber_BonusShow.LoadAsset("Obj_BonusShow");
        GameObject _Prefber_BonusShow = obj_BonusShow as GameObject;
        GameObject gameObject_BonusShow = Instantiate(_Prefber_BonusShow, Parent_BonusShow);
        Prefber_BonusShow.Unload(false);

        AssetBundle An_BonusShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[4]].CLPath);
        RuntimeAnimatorController _An_BonusShowControl = An_BonusShowControl.LoadAsset<RuntimeAnimatorController>("BonusShow");
        An_BonusShowControl.Unload(false);

        AssetBundle An_BonusShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[4]].CLPath);
        AnimationClip[] _An_BonusShowClip = An_BonusShowClip.LoadAllAssets<AnimationClip>();
        gameObject_BonusShow.GetComponent<Animator>().runtimeAnimatorController = _An_BonusShowControl;
        An_BonusShowClip.Unload(false);



        AssetBundle Prefber_BonusEndShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[6]].CLPath);
        Object obj_BonusEndShow = Prefber_BonusEndShow.LoadAsset("Obj_BonusEndShow");
        GameObject _Prefber_BonusEndShow = obj_BonusEndShow as GameObject;
        GameObject gameObject_BonusEndShow = Instantiate(_Prefber_BonusEndShow, Parent_BonusEndShow);
        Prefber_BonusEndShow.Unload(false);

        AssetBundle An_BonusEndShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[5]].CLPath);
        RuntimeAnimatorController _An_BonusEndShowControl = An_BonusEndShowControl.LoadAsset<RuntimeAnimatorController>("BonusEndShow");
        An_BonusEndShowControl.Unload(false);

        AssetBundle An_BonusEndShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[5]].CLPath);
        AnimationClip _An_BonusEndShowClip = An_BonusEndShowClip.LoadAsset<AnimationClip>("BonusEndShowClip");
        gameObject_BonusEndShow.GetComponent<Animator>().runtimeAnimatorController = _An_BonusEndShowControl;
        An_BonusEndShowClip.Unload(false);


        AssetBundle An_UiEnlargeControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[2]].CLPath);
        RuntimeAnimatorController _An_UiEnlargeControl = An_UiEnlargeControl.LoadAsset<RuntimeAnimatorController>("UiEnlarge");
        An_UiEnlargeControl.Unload(false);

        AssetBundle An_UiEnlargeClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[2]].CLPath);
        AnimationClip _An_UiEnlargeClip = An_UiEnlargeClip.LoadAsset<AnimationClip>("UiEnlargeClip");

        StartGame_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        Bet_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        Auto_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        InFoButton_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        An_UiEnlargeClip.Unload(false);


        AssetBundle _Sprite_Pool = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[0]].CLPath);
        Sprite_Pool = _Sprite_Pool.LoadAllAssets<Sprite>();

        AssetBundle _WinShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[1]].CLPath);
        Sprite[] WinShowSprites = _WinShowSprites.LoadAllAssets<Sprite>();

        AssetBundle _ScoreBoardSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[2]].CLPath);
        Sprite ScoreBoardSprite = _WinShowSprites.LoadAsset<Sprite>("IMG_ScoreBoard");
        _Options.transform.GetChild(0).GetComponent<Image>().sprite = ScoreBoardSprite;
        _Bet_Menu.GetComponent<Image>().sprite = ScoreBoardSprite;
        _Auto_Menu.GetComponent<Image>().sprite= ScoreBoardSprite;

        AssetBundle _RoolShowSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[3]].CLPath);
        Sprite RoolShowSprite = _RoolShowSprite.LoadAsset<Sprite>("RoolShowSprite");

        AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[4]].CLPath);
        Sprite Point = _Point.LoadAsset<Sprite>("Point");

        AssetBundle _NumbersSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[5]].CLPath);
        _Numbers = _NumbersSprites.LoadAllAssets<Sprite>();

        AssetBundle InfoBackSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[6]].CLPath);
        _InfoBackSprite.sprite = InfoBackSprite.LoadAsset<Sprite>("SpriteInfoBack");

        AssetBundle InFoSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[7]].CLPath);
        _InFoSprites = InFoSprites.LoadAllAssets<Sprite>();

        AssetBundle Info_OutSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[8]].CLPath);
        _InfoBackSprite.transform.GetChild(3).GetComponent<Image>().sprite = Info_OutSprite.LoadAsset<Sprite>("IMG_InfoOut");

        AssetBundle Info_RightSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[9]].CLPath);
        _InfoBackSprite.transform.GetChild(2).GetComponent<Image>().sprite = Info_RightSprite.LoadAsset<Sprite>("IMG_InfoRight");

        AssetBundle Info_LeftSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[10]].CLPath);
        _InfoBackSprite.transform.GetChild(1).GetComponent<Image>().sprite = Info_LeftSprite.LoadAsset<Sprite>("IMG_InfoLeft");

        AssetBundle ControlUi_MoneySprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[11]].CLPath);
        _PlayerCoin_Text.transform.parent.GetComponent<Image>().sprite = ControlUi_MoneySprite.LoadAsset<Sprite>("MoneyBack");


        AssetBundle ControlUi_ButtonSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[12]].CLPath);

       Sprite[] _ControlUi_ButtonSprites= ControlUi_ButtonSprites.LoadAllAssets<Sprite>();
        StartGame_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];
        Bet_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        OpenOperational.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Bet_Menu.transform.GetChild(0).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Bet_Menu.transform.GetChild(1).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Bet_Menu.transform.GetChild(2).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];

        Auto_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        _Auto_Menu.transform.GetChild(0).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        _Auto_Menu.transform.GetChild(1).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        _Auto_Menu.transform.GetChild(2).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        _Auto_Menu.transform.GetChild(4).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        Options_Yes_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        Options_No_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];

        InFoButton_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[3];



        AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[13]].CLPath);
        Sprite[] _BonusShow1Sprites= BonusShow1Sprites.LoadAllAssets<Sprite>();


        AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[14]].CLPath);
        Sprite[] _BonusShow2Sprites = BonusShow2Sprites.LoadAllAssets<Sprite>();


        AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[15]].CLPath);
        Sprite[] _BonusEndShowSprites = BonusEndShowSprites.LoadAllAssets<Sprite>();

        AssetBundle BackImageSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[16]].CLPath);
        Sprite _BackImageSprite = BackImageSprite.LoadAsset<Sprite>("BackSprite");
        _Img_Operational.transform.parent.GetChild(1).GetComponent<Image>().sprite = _BackImageSprite;
        _Options.GetComponent<Image>().sprite = _BackImageSprite;

        AssetBundle ABCSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[17]].CLPath);
        Sprite[] _ABCSprites = ABCSprites.LoadAllAssets<Sprite>();

        AssetBundle RawImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[1].Name[0]].CLPath);
        _VideoImage.texture = RawImage.LoadAsset<Texture>("VideoTexture");

        AssetBundle MyVideo = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[2].Name[0]].CLPath);
        VideoClip _MyVideo = MyVideo.LoadAsset<VideoClip>("MYVIDEO");
        _EndShowVideoClip = _MyVideo;
        _VideoImage.GetComponent<VideoPlayer>().clip = _MyVideo;







    }













}
