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

    public Image SlotBack;//遊戲背景圖

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
