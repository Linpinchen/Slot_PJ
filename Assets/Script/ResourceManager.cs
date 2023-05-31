using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ResourceManager : MonoBehaviour
{


    public Sprite[] img;

    public Image LoadChack_Image;
    public Text _Errortext;
    //public Text VideoText;


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
    public GameObject _GBDrawLine;//預置物
    public Transform _InLineRenderPool;//預置物放置在Hierarchy 顯示的位置
    public Transform _LineRenderOutPool;// 普盤用的預置物放置在Hierarchy 待表演 顯示的位置
    public Transform _BonusLineRenderOutPool;// Bonus盤用的預置物放置在Hierarchy 待表演 顯示的位置
    public Animator _Amr_WinShow;//跳錢得背景動畫
    public Animator _BonusAnimator;//Bonus開頭的動畫表演
    public Animator _BonusEndShow;//Bonus最後的總跳錢背景動畫
    public VideoPlayer _EndShowPlayer;
    public VideoClip _EndShowVideoClip;
    public RawImage _VideoImage;



    public Transform Parent_WinShow;
    public Transform Parent_PointImage;
    public Transform Parent_ImageBonusEnd;
    public Transform Parent_BonusShow;
    public Transform Parent_BonusEndShow;

    public CASCompare _CSAComper;




    public void init(string path)
    {


        _CSAComper.init(path, _Errortext);



    }



    //測試用...先從字典抓路徑來載檔案 不然Google雲端很雷


    //預置物得部分 要改成 連圖片都載完後再生成 不然會掉圖 
    

    public void LoadClientAssetBundel(Reel_Move[] reel_Moves)
    {
        _Errortext.text = "正在下載檔案";


        AssetBundle DrawLine_Prefber = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["DrawLine_Prefber"].CLPath);
        //AssetBundle DrawLine_Prefber = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[0]].CLPath);
        Object obj_DrawLine = DrawLine_Prefber.LoadAsset("DrawLine");
        _GBDrawLine = obj_DrawLine as GameObject;
        DrawLine_Prefber.Unload(false);
        Debug.Log("Load-DrawLine_Prefber");
        _Errortext.text = "正在下載檔案"+ "Load-DrawLine_Prefber";

        AssetBundle _WinShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["WinShowSprites"].CLPath);
        //AssetBundle _WinShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[1]].CLPath);
        Sprite[] WinShowSprites = _WinShowSprites.LoadAllAssets<Sprite>();
        //_WinShowSprites.Unload(false);
        Debug.Log("Load-_WinShowSprites");
        _Errortext.text = "正在下載檔案" + "Load-_WinShowSprites";

        AssetBundle Prefber_WinShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_WinShow"].CLPath);
        //AssetBundle Prefber_WinShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[1]].CLPath);
        Object obj_WinShow = Prefber_WinShow.LoadAsset("Obj_WinShow");
        GameObject _Prefber_WinShow = obj_WinShow as GameObject;
        //GameObject gameObject_WinShow = Instantiate(_Prefber_WinShow, Parent_WinShow);
        //Prefber_WinShow.Unload(false);
        Debug.Log("Load-Prefber_WinShow");
        _Errortext.text = "正在下載檔案" + "Load-Prefber_WinShow";

        AssetBundle An_WinShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_WinShowControl"].CLPath);
        //AssetBundle An_WinShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[0]].CLPath);
        RuntimeAnimatorController _An_WinShowControl = An_WinShowControl.LoadAsset<RuntimeAnimatorController>("WinShow");
        An_WinShowControl.Unload(false);
        Debug.Log("Load-An_WinShowControl");
        _Errortext.text = "正在下載檔案" + "Load-An_WinShowControl";

        AssetBundle An_WinShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_WinShowClip"].CLPath);
        //AssetBundle An_WinShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[0]].CLPath);
        AnimationClip _An_WinShowClip = An_WinShowClip.LoadAsset<AnimationClip>("WinShowClip");
        An_WinShowClip.Unload(false);

        _Errortext.text = "正在下載檔案" + "WinShowClip";


        GameObject gameObject_WinShow = Instantiate(_Prefber_WinShow, Parent_WinShow);
        gameObject_WinShow.GetComponent<Animator>().runtimeAnimatorController = _An_WinShowControl;
        _Amr_WinShow = gameObject_WinShow.GetComponent<Animator>();
        _WinShowSprites.Unload(false);
        Prefber_WinShow.Unload(false);
        //An_WinShowClip.Unload(false);
        Debug.Log("Load-An_WinShowClip");
        _Errortext.text = "正在下載檔案" + "Load-An_WinShowClip";


        //AssetBundle Prefber_RoolImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_RoolImage"].CLPath);
        //Object obj_RoolImage = Prefber_RoolImage.LoadAsset("Image_Rool");
        //GameObject _RoolImage = obj_RoolImage as GameObject;
        //Prefber_RoolImage.Unload(false);

        //AssetBundle An_RoolControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_RoolControl"].CLPath);
        //RuntimeAnimatorController _An_RoolControl = An_RoolControl.LoadAsset<RuntimeAnimatorController>("Rool");
        //An_RoolControl.Unload(false);




        //AssetBundle An_RoolClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_RoolClip"].CLPath);
        //AnimationClip _An_RoolClip = An_RoolClip.LoadAsset<AnimationClip>("RoolClip");
        //An_RoolClip.Unload(false);


        //AssetBundle _RoolShowSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["RoolShowSprite"].CLPath);
        ////AssetBundle _RoolShowSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[3]].CLPath);
        //Sprite[] RoolShowSprite = _RoolShowSprite.LoadAllAssets<Sprite>();
        //img = RoolShowSprite;
        //_RoolShowSprite.Unload(false);
        //Debug.Log("Load- _RoolShowSprite");






        AssetBundle An_UiShinyControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiShinyControl"].CLPath);
        RuntimeAnimatorController _An_UiShinyControl = An_UiShinyControl.LoadAsset<RuntimeAnimatorController>("UiShiny");
        An_UiShinyControl.Unload(false);

        _Errortext.text = "正在下載檔案" + "UiShiny";

        AssetBundle An_UiShinyClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiShinyClip"].CLPath);
        AnimationClip _An_UiShinyClip = An_UiShinyClip.LoadAsset<AnimationClip>("UiShinyClip");
        An_UiShinyClip.Unload(false);

        _Errortext.text = "正在下載檔案" + "UiShinyClip";





        for (int i=0;i < reel_Moves.Length;i++)
        {
            Transform parent = reel_Moves[i].transform.parent;

            //AssetBundle Prefber_RoolImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_RoolImage"].CLPath);
            ////AssetBundle Prefber_RoolImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[2]].CLPath);
            //Object obj_RoolImage = Prefber_RoolImage.LoadAsset("Image_Rool");
            //GameObject _RoolImage = obj_RoolImage as GameObject;


            //GameObject gameObject__RoolImage = Instantiate(_RoolImage, parent);


            //Prefber_RoolImage.Unload(false);

            //AssetBundle An_RoolControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_RoolControl"].CLPath);
            ////AssetBundle An_RoolControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[3]].CLPath);
            //RuntimeAnimatorController _An_RoolControl = An_RoolControl.LoadAsset<RuntimeAnimatorController>("Rool");
            //An_RoolControl.Unload(false);

            //AssetBundle An_RoolClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_RoolClip"].CLPath);
            //AnimationClip _An_RoolClip = An_RoolClip.LoadAsset<AnimationClip>("RoolClip");


            //gameObject__RoolImage.GetComponent<Animator>().runtimeAnimatorController = _An_RoolControl;


            //gameObject__RoolImage.SetActive(false);
            //An_RoolClip.Unload(false);

            for (int j =1;j< reel_Moves[i].transform.childCount-1; j++)
            {

                Transform child = reel_Moves[i].transform.GetChild(j);


                //AssetBundle An_UiShinyControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiShinyControl"].CLPath);
                ////AssetBundle An_UiShinyControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[1]].CLPath);
                //RuntimeAnimatorController _An_UiShinyControl = An_UiShinyControl.LoadAsset<RuntimeAnimatorController>("UiShiny");
                //An_UiShinyControl.Unload(false);

                //AssetBundle An_UiShinyClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiShinyClip"].CLPath);
                ////AssetBundle An_UiShinyClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[1]].CLPath);
                //AnimationClip _An_UiShinyClip = An_UiShinyClip.LoadAsset<AnimationClip>("UiShinyClip");
                child.GetComponent<Animator>().runtimeAnimatorController = _An_UiShinyControl;
                //An_UiShinyClip.Unload(false);

            }


        }

       
       
       

       
        

        Debug.Log("Load-Prefber_RoolImage");
        Debug.Log("Load-An_RoolControl");
        Debug.Log("Load- An_RoolClip");

        _Errortext.text = "正在下載檔案" + "Load-Prefber_RoolImage"+ "Load-An_RoolControl"+ "Load- An_RoolClip";

        AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["PointImage"].CLPath);
        //AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[4]].CLPath);
        Sprite Point = _Point.LoadAsset<Sprite>("Point");
        //_Point.Unload(false);
        Debug.Log("Load- _Point");
        _Errortext.text = "正在下載檔案" + "Load- _Point";

        AssetBundle Prefber_PointImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_PointImage"].CLPath);
        //AssetBundle Prefber_PointImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[3]].CLPath);
        Object obj_PointImage = Prefber_PointImage.LoadAsset("Images_initalPositionS");
        GameObject _Prefber_PointImage = obj_PointImage as GameObject;
        _Errortext.text = "正在下載檔案" + "Images_initalPositionS";

        //AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["PointImage"].CLPath);
        ////AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[4]].CLPath);
        //Sprite Point = _Point.LoadAsset<Sprite>("Point");
        //_Point.Unload(false);
        //Debug.Log("Load- _Point");



        GameObject PointImage = Instantiate(_Prefber_PointImage, Parent_PointImage);
        int count = PointImage.transform.childCount;
        Debug.Log("預製物Prefber_PointImage的子物件數 ： " + count);
        _StartPoint = new RectTransform[count];
        for (int i=0;i<count;i++)
        {
            int Tempi = i + 1;
            Debug.Log("Image_initalPosition" + Tempi);

            _StartPoint[i] = PointImage.transform.Find("Image_initalPosition" + Tempi).GetComponent<RectTransform>();


        }
        Prefber_PointImage.Unload(false);
        _Point.Unload(false);
        Debug.Log("Load- Prefber_PointImage");

        _Errortext.text = "正在下載檔案" + "Load- Prefber_PointImage";


        AssetBundle ABCSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ABCSprites"].CLPath);
        //AssetBundle ABCSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[17]].CLPath);
        Sprite[] _ABCSprites = ABCSprites.LoadAllAssets<Sprite>();
        //ABCSprites.Unload(false);
        Debug.Log("Load-  ABCSprites");

        _Errortext.text = "正在下載檔案" + "Load-  ABCSprites";

        AssetBundle _ScoreBoardSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ScoreBoardSprite"].CLPath);
        //AssetBundle _ScoreBoardSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[2]].CLPath);
        Sprite ScoreBoardSprite = _ScoreBoardSprite.LoadAsset<Sprite>("IMG_ScoreBoard");
        _Options.transform.GetChild(0).GetComponent<Image>().sprite = ScoreBoardSprite;
        _Bet_Menu.GetComponent<Image>().sprite = ScoreBoardSprite;
        _Auto_Menu.GetComponent<Image>().sprite = ScoreBoardSprite;
        //_ScoreBoardSprite.Unload(false);
        Debug.Log("Load-_ScoreBoardSprite");

        _Errortext.text = "正在下載檔案" + "Load-_ScoreBoardSprite";




        AssetBundle Prefber_BonusEnd = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_BonusEnd"].CLPath);
        //AssetBundle Prefber_BonusEnd = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[4]].CLPath);
        Object obj_BonusEnd = Prefber_BonusEnd.LoadAsset("Image_BonusEnd");
        GameObject _Prefber_BonusEnd = obj_BonusEnd as GameObject;
        GameObject gameObject_BonusEnd = Instantiate(_Prefber_BonusEnd, Parent_ImageBonusEnd);
        _BonusWinBackSprite = gameObject_BonusEnd.GetComponent<Image>();
        gameObject_BonusEnd.SetActive(false);
        Prefber_BonusEnd.Unload(false);
        _ScoreBoardSprite.Unload(false);
        Debug.Log("Load- Prefber_BonusEnd");

        _Errortext.text = "正在下載檔案" + "Load- Prefber_BonusEnd";


        AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow1Sprites"].CLPath);
        //AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[13]].CLPath);
        Sprite[] _BonusShow1Sprites = BonusShow1Sprites.LoadAllAssets<Sprite>();
        //BonusShow1Sprites.Unload(false);
        Debug.Log("Load-BonusShow1Sprites");

        _Errortext.text = "正在下載檔案" + "Load-BonusShow1Sprites";

        AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow2Sprites"].CLPath);
        //AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[14]].CLPath);
        Sprite[] _BonusShow2Sprites = BonusShow2Sprites.LoadAllAssets<Sprite>();
        //BonusShow2Sprites.Unload(false);
        Debug.Log("Load- BonusShow2Sprites");


        _Errortext.text = "正在下載檔案" + "Load- BonusShow2Sprites";


        AssetBundle Prefber_BonusShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_BonusShow"].CLPath);
        //AssetBundle Prefber_BonusShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[5]].CLPath);
        Object obj_BonusShow = Prefber_BonusShow.LoadAsset("Obj_BonusShow");
        GameObject _Prefber_BonusShow = obj_BonusShow as GameObject;
        //GameObject gameObject_BonusShow = Instantiate(_Prefber_BonusShow, Parent_BonusShow);
        //Prefber_BonusShow.Unload(false);
        Debug.Log("Load- Prefber_BonusShow");

        _Errortext.text = "正在下載檔案" + "Load- Prefber_BonusShow";


        AssetBundle An_BonusShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_BonusShowControl"].CLPath);
        //AssetBundle An_BonusShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[4]].CLPath);
        RuntimeAnimatorController _An_BonusShowControl = An_BonusShowControl.LoadAsset<RuntimeAnimatorController>("BonusShow");
        An_BonusShowControl.Unload(false);
        Debug.Log("Load- An_BonusShowControl");

        _Errortext.text = "正在下載檔案" + "Load- An_BonusShowControl";


        AssetBundle An_BonusShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_BonusShowClip"].CLPath);
        //AssetBundle An_BonusShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[4]].CLPath);
        AnimationClip[] _An_BonusShowClip = An_BonusShowClip.LoadAllAssets<AnimationClip>();
        An_BonusShowClip.Unload(false);


        _Errortext.text = "正在下載檔案" + "An_BonusShowClip";
        //AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow1Sprites"].CLPath);
        ////AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[13]].CLPath);
        //Sprite[] _BonusShow1Sprites = BonusShow1Sprites.LoadAllAssets<Sprite>();
        //BonusShow1Sprites.Unload(false);
        //Debug.Log("Load-BonusShow1Sprites");



        //AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow2Sprites"].CLPath);
        ////AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[14]].CLPath);
        //Sprite[] _BonusShow2Sprites = BonusShow2Sprites.LoadAllAssets<Sprite>();
        //BonusShow2Sprites.Unload(false);
        //Debug.Log("Load- BonusShow2Sprites");





        GameObject gameObject_BonusShow = Instantiate(_Prefber_BonusShow, Parent_BonusShow);
        gameObject_BonusShow.GetComponent<Animator>().runtimeAnimatorController = _An_BonusShowControl;
        _BonusAnimator = gameObject_BonusShow.GetComponent<Animator>();
        Prefber_BonusShow.Unload(false);
        BonusShow1Sprites.Unload(false);
        BonusShow2Sprites.Unload(false);
        ABCSprites.Unload(false);
        //An_BonusShowClip.Unload(false);
        Debug.Log("Load- An_BonusShowClip");

        _Errortext.text = "正在下載檔案" + "Load- An_BonusShowClip";


        AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusEndShowSprites"].CLPath);
        //AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[15]].CLPath);
        Sprite[] _BonusEndShowSprites = BonusEndShowSprites.LoadAllAssets<Sprite>();

        AssetBundle Prefber_BonusEndShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["Prefber_BonusEndShow"].CLPath);
        //AssetBundle Prefber_BonusEndShow = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[5].Name[6]].CLPath);
        Object obj_BonusEndShow = Prefber_BonusEndShow.LoadAsset("Obj_BonusEndShow");
        GameObject _Prefber_BonusEndShow = obj_BonusEndShow as GameObject;
        //GameObject gameObject_BonusEndShow = Instantiate(_Prefber_BonusEndShow, Parent_BonusEndShow);
        //Prefber_BonusEndShow.Unload(false);
        Debug.Log("Load- Prefber_BonusEndShow");
        _Errortext.text = "正在下載檔案" + "Load- Prefber_BonusEndShow";

        AssetBundle An_BonusEndShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_BonusEndShowControl"].CLPath);
        //AssetBundle An_BonusEndShowControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[5]].CLPath);
        RuntimeAnimatorController _An_BonusEndShowControl = An_BonusEndShowControl.LoadAsset<RuntimeAnimatorController>("BonusEndShow");
        An_BonusEndShowControl.Unload(false);
        Debug.Log("Load- An_BonusEndShowControl");
        _Errortext.text = "正在下載檔案" + "Load- An_BonusEndShowControl";


        AssetBundle An_BonusEndShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_BonusEndShowClip"].CLPath);
        //AssetBundle An_BonusEndShowClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[5]].CLPath);
        AnimationClip _An_BonusEndShowClip = An_BonusEndShowClip.LoadAsset<AnimationClip>("BonusEndShowClip");
        An_BonusEndShowClip.Unload(false);


        //AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusEndShowSprites"].CLPath);
        ////AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[15]].CLPath);
        //Sprite[] _BonusEndShowSprites = BonusEndShowSprites.LoadAllAssets<Sprite>();

        BonusEndShowSprites.Unload(false);
        Prefber_BonusEndShow.Unload(false);
        Debug.Log("Load- BonusEndShowSprites");

        _Errortext.text = "正在下載檔案" + "Load- BonusEndShowSprites";

        GameObject gameObject_BonusEndShow = Instantiate(_Prefber_BonusEndShow, Parent_BonusEndShow);
        gameObject_BonusEndShow.GetComponent<Animator>().runtimeAnimatorController = _An_BonusEndShowControl;
        _BonusEndShow = gameObject_BonusEndShow.GetComponent<Animator>();
        //An_BonusEndShowClip.Unload(false);
        Debug.Log("Load- An_BonusEndShowClip");
        _Errortext.text = "正在下載檔案" + "Load- An_BonusEndShowClip";

        AssetBundle An_UiEnlargeControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiEnlargeControl"].CLPath);
        //AssetBundle An_UiEnlargeControl = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[3].Name[2]].CLPath);
        RuntimeAnimatorController _An_UiEnlargeControl = An_UiEnlargeControl.LoadAsset<RuntimeAnimatorController>("UiEnlarge");

        StartGame_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        Bet_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        Auto_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        InFoButton_Button.GetComponent<Animator>().runtimeAnimatorController = _An_UiEnlargeControl;

        An_UiEnlargeControl.Unload(false);
        Debug.Log("Load- An_UiEnlargeControl");

        _Errortext.text = "正在下載檔案" + "Load- An_UiEnlargeControl";

        AssetBundle An_UiEnlargeClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["An_UiEnlargeClip"].CLPath);
        //AssetBundle An_UiEnlargeClip = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[4].Name[2]].CLPath);
        AnimationClip _An_UiEnlargeClip = An_UiEnlargeClip.LoadAsset<AnimationClip>("UiEnlargeClip");

        An_UiEnlargeClip.Unload(false);
        Debug.Log("Load- An_UiEnlargeClip");

        _Errortext.text = "正在下載檔案" + "Load- An_UiEnlargeClip";

        AssetBundle _Sprite_Pool = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["SlotSprites"].CLPath);
        //AssetBundle _Sprite_Pool = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[0]].CLPath);
        Sprite_Pool = _Sprite_Pool.LoadAllAssets<Sprite>();
        _Sprite_Pool.Unload(false);
        Debug.Log("Load- _Sprite_Pool");
        _Errortext.text = "正在下載檔案" + "Load- _Sprite_Pool";

        //AssetBundle _WinShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["WinShowSprites"].CLPath);
        ////AssetBundle _WinShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[1]].CLPath);
        //Sprite[] WinShowSprites = _WinShowSprites.LoadAllAssets<Sprite>();
        //_WinShowSprites.Unload(false);
        //Debug.Log("Load-_WinShowSprites");


        //AssetBundle _ScoreBoardSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ScoreBoardSprite"].CLPath);
        ////AssetBundle _ScoreBoardSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[2]].CLPath);
        //Sprite ScoreBoardSprite = _ScoreBoardSprite.LoadAsset<Sprite>("IMG_ScoreBoard");
        //_Options.transform.GetChild(0).GetComponent<Image>().sprite = ScoreBoardSprite;
        //_Bet_Menu.GetComponent<Image>().sprite = ScoreBoardSprite;
        //_Auto_Menu.GetComponent<Image>().sprite= ScoreBoardSprite;
        //_ScoreBoardSprite.Unload(false);
        //Debug.Log("Load-_ScoreBoardSprite");



        //AssetBundle _RoolShowSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["RoolShowSprite"].CLPath);
        ////AssetBundle _RoolShowSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[3]].CLPath);
        //Sprite[] RoolShowSprite = _RoolShowSprite.LoadAllAssets<Sprite>();
        //img = RoolShowSprite;
        //_RoolShowSprite.Unload(false);
        //Debug.Log("Load- _RoolShowSprite");



        //AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["PointImage"].CLPath);
        ////AssetBundle _Point = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[4]].CLPath);
        //Sprite Point = _Point.LoadAsset<Sprite>("Point");
        //_Point.Unload(false);
        //Debug.Log("Load- _Point");


        AssetBundle _NumbersSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["NumbersSprites"].CLPath);
        //AssetBundle _NumbersSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[5]].CLPath);
        _Numbers = _NumbersSprites.LoadAllAssets<Sprite>();
        _NumbersSprites.Unload(false);
        Debug.Log("Load- _NumbersSprites");
        _Errortext.text = "正在下載檔案" + "Load- _NumbersSprites";

        AssetBundle InfoBackSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["InfoBackSprite"].CLPath);
        //AssetBundle InfoBackSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[6]].CLPath);
        _InfoBackSprite.sprite = InfoBackSprite.LoadAsset<Sprite>("SpriteInfoBack");
        InfoBackSprite.Unload(false);
        Debug.Log("Load- InfoBackSprite");
        _Errortext.text = "正在下載檔案" + "Load- InfoBackSprite";

        AssetBundle InFoSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ImageInfoSprites"].CLPath);
        //AssetBundle InFoSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[7]].CLPath);
        _InFoSprites = InFoSprites.LoadAllAssets<Sprite>();
        InFoSprites.Unload(false);
        Debug.Log("Load- InFoSprites");
        _Errortext.text = "正在下載檔案" + "Load- InFoSprites";

        AssetBundle Info_OutSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ImageInfo_OutSprite"].CLPath);
        //AssetBundle Info_OutSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[8]].CLPath);
        _InfoBackSprite.transform.GetChild(3).GetComponent<Image>().sprite = Info_OutSprite.LoadAsset<Sprite>("IMG_InfoOut");
        Info_OutSprite.Unload(false);
        Debug.Log("Load-  Info_OutSprite");
        _Errortext.text = "正在下載檔案" + "Load-  Info_OutSprite";

        AssetBundle Info_RightSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ImageInfo_RightSprite"].CLPath);
        //AssetBundle Info_RightSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[9]].CLPath);
        _InfoBackSprite.transform.GetChild(2).GetComponent<Image>().sprite = Info_RightSprite.LoadAsset<Sprite>("IMG_InfoRight");
        Info_RightSprite.Unload(false);
        Debug.Log("Load-   Info_RightSprite");
        _Errortext.text = "正在下載檔案" + "Load-   Info_RightSprite";

        AssetBundle Info_LeftSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ImageInfo_LeftSprite"].CLPath);
        //AssetBundle Info_LeftSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[10]].CLPath);
        _InfoBackSprite.transform.GetChild(1).GetComponent<Image>().sprite = Info_LeftSprite.LoadAsset<Sprite>("IMG_InfoLeft");
        Info_LeftSprite.Unload(false);
        Debug.Log("Load-  Info_LeftSprite");
        _Errortext.text = "正在下載檔案" + "Load-  Info_LeftSprite";

        AssetBundle ControlUi_MoneySprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ControlUi_MoneySprite"].CLPath);
        //AssetBundle ControlUi_MoneySprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[11]].CLPath);
        _PlayerCoin_Text.transform.parent.GetComponent<Image>().sprite = ControlUi_MoneySprite.LoadAsset<Sprite>("MoneyBack");
        ControlUi_MoneySprite.Unload(false);
        Debug.Log("Load-ControlUi_MoneySprite");
        _Errortext.text = "正在下載檔案" + "Load-ControlUi_MoneySprite";


        AssetBundle ControlUi_ButtonSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ControlUi_ButtonSprites"].CLPath);
        //AssetBundle ControlUi_ButtonSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[12]].CLPath);

        Sprite[] _ControlUi_ButtonSprites= ControlUi_ButtonSprites.LoadAllAssets<Sprite>();
        StartGame_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[3];
        Bet_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];
        OpenOperational.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];
        _Bet_Menu.transform.GetChild(0).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];
        _Bet_Menu.transform.GetChild(1).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];
        _Bet_Menu.transform.GetChild(2).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[0];

        Auto_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Auto_Menu.transform.GetChild(0).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Auto_Menu.transform.GetChild(1).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Auto_Menu.transform.GetChild(2).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        _Auto_Menu.transform.GetChild(4).GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        Options_Yes_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];
        Options_No_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[1];

        InFoButton_Button.GetComponent<Image>().sprite = _ControlUi_ButtonSprites[2];
        ControlUi_ButtonSprites.Unload(false);
        Debug.Log("Load-ControlUi_ButtonSprites");
        _Errortext.text = "正在下載檔案" + "Load-ControlUi_ButtonSprites";


        //AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow1Sprites"].CLPath);
        ////AssetBundle BonusShow1Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[13]].CLPath);
        //Sprite[] _BonusShow1Sprites= BonusShow1Sprites.LoadAllAssets<Sprite>();
        //BonusShow1Sprites.Unload(false);
        //Debug.Log("Load-BonusShow1Sprites");


        //AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusShow2Sprites"].CLPath);
        ////AssetBundle BonusShow2Sprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[14]].CLPath);
        //Sprite[] _BonusShow2Sprites = BonusShow2Sprites.LoadAllAssets<Sprite>();
        //BonusShow2Sprites.Unload(false);
        //Debug.Log("Load- BonusShow2Sprites");


        //AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BonusEndShowSprites"].CLPath);
        ////AssetBundle BonusEndShowSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[15]].CLPath);
        //Sprite[] _BonusEndShowSprites = BonusEndShowSprites.LoadAllAssets<Sprite>();
        //BonusEndShowSprites.Unload(false);
        //Debug.Log("Load- BonusEndShowSprites");


        AssetBundle BackImageSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["BackImageSprite"].CLPath);
        //AssetBundle BackImageSprite = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[16]].CLPath);
        Sprite _BackImageSprite = BackImageSprite.LoadAsset<Sprite>("BackSprite");
        _Img_Operational.transform.parent.GetChild(1).GetComponent<Image>().sprite = _BackImageSprite;
        _Options.GetComponent<Image>().sprite = _BackImageSprite;
        BackImageSprite.Unload(false);
        Debug.Log("Load- BackImageSprite");
        _Errortext.text = "正在下載檔案" + "Load- BackImageSprite";

        //AssetBundle ABCSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["ABCSprites"].CLPath);
        ////AssetBundle ABCSprites = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[0].Name[17]].CLPath);
        //Sprite[] _ABCSprites = ABCSprites.LoadAllAssets<Sprite>();
        //ABCSprites.Unload(false);
        //Debug.Log("Load-  ABCSprites");


        // AssetBundle RawImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["RawImage"].CLPath);
        // //AssetBundle RawImage = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[1].Name[0]].CLPath);
        // RenderTexture MyTexture = RawImage.LoadAsset<RenderTexture>("VideoTexture");

        // //_VideoImage.texture = RawImage.LoadAsset<Texture>("VideoTexture");
        // _VideoImage.texture = MyTexture;
        // _EndShowPlayer.targetTexture = MyTexture;
        // RawImage.Unload(false);
        // Debug.Log("Load- RawImage");

        // AssetBundle MyVideo = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate["MyVideo"].CLPath);
        // //AssetBundle MyVideo = AssetBundle.LoadFromFile(_CSAComper.Di_BundleDate[_CSAComper.bundleDate[2].Name[0]].CLPath);
        // VideoClip _MyVideo = MyVideo.LoadAsset<VideoClip>("MYVIDEO");
        // _EndShowVideoClip = _MyVideo;
        // _VideoImage.GetComponent<VideoPlayer>().clip = _MyVideo;
        // MyVideo.Unload(false);
        // Debug.Log("Load- MyVideo");

        ////_EndShowPlayer.Pause();

        _Errortext.text = "下載完成";



        LoadChack_Image.gameObject.SetActive(false);



        //string haveVideo = "影片得名字 :"+_VideoImage.GetComponent<VideoPlayer>().clip.name;
        //string haveTexture;
        //string EndShowVideo;


        //if (_VideoImage.texture != null)
        //{

        //    haveTexture = "有貼圖";
        //}
        //else
        //{
        //    haveTexture = "無貼圖";

        //}

        //if (_EndShowPlayer != null)
        //{
        //    EndShowVideo = "VideoClip有影片 ：" + _EndShowPlayer.name;

        //}

        //else
        //{

        //    EndShowVideo = "無影片 ：" ;

        //}


        //VideoText.text += haveVideo;
        //VideoText.text += haveTexture;
        //VideoText.text += EndShowVideo;



    }













}
