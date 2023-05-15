using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIControlMethod : IUIControlMethod
{


    IDate _IDate;

    Button_EventTrigger EventPlus;

    Button_EventTrigger EventReduce;


    ResourceManager _ResourceManager;

    [SerializeField]
    bool _StAddBonusDate;

   
    public bool StAddBonusDate { get { return _StAddBonusDate; } set { _StAddBonusDate = value; } }

   
    #region 介面操作初始化方法
    /// <summary>
    /// 介面操作初始化方法
    /// </summary>
    /// <param name="Idate"></param>
    /// <param name="EventPlus"></param>
    /// <param name="EventReduce"></param>
    public UIControlMethod(IDate Idate, Button_EventTrigger EventPlus, Button_EventTrigger EventReduce, ResourceManager _ResourceManager)
    {

        Debuger.Log("UIControlInit");
        this._IDate = Idate;
        this.EventPlus = EventPlus;
        this.EventReduce = EventReduce;
        this._ResourceManager = _ResourceManager;

    }
    #endregion

    #region AddBonus 的 Bool 開關
    /// <summary>
    ///  AddBonus 的 Bool 開關
    /// </summary>
    public void AddBonus()
    {

        if (_StAddBonusDate)
        {
            _StAddBonusDate = false;

        }
        else if (!_StAddBonusDate)
        {

            _StAddBonusDate = true;

        }

    }
    #endregion

    #region Bet小視窗開關
    /// <summary>
    /// Bet小視窗開關
    /// </summary>
    public void BetMenuSwitch()
    {

        bool BetMenu = _ResourceManager._Bet_Menu.activeInHierarchy;

        switch (BetMenu)
        {

            case true:
                {
                    _ResourceManager._Bet_Menu.SetActive(false);
                    break;
                }
            default:
                {
                    _ResourceManager._Bet_Menu.SetActive(true);
                    _ResourceManager._Auto_Menu.SetActive(false);
                    break;
                }

        }

    }
    #endregion

    #region Auto小視窗開關
    /// <summary>
    /// Auto小視窗開關
    /// </summary>
    public void AutoMenuSwitch()
    {

        bool AutoMenu = _ResourceManager._Auto_Menu.activeInHierarchy;

        switch (AutoMenu)
        {

            case true:
                {
                    _ResourceManager._Auto_Menu.SetActive(false);
                    break;
                }
            default:
                {
                    _ResourceManager._Auto_Menu.SetActive(true);
                    _ResourceManager._Bet_Menu.SetActive(false);
                    break;
                }

        }

    }
    #endregion

    #region  Auto次數增加
    /// <summary>
    /// Auto次數增加
    /// </summary>
    public void AutoPlus()
    {

        if (_IDate.AutoCount < 9999)
        {
            _IDate.AutoCount++;

            _IDate.AutoSurplus = _IDate.AutoCount;

            _ResourceManager._Auto_text.text = _IDate.AutoSurplus.ToString();
        }

    }
    #endregion

    #region Auto數 減少
    /// <summary>
    /// Auto數 減少
    /// </summary>
    public void AutoReduce()
    {
        if (_IDate.AutoCount > 1)
        {
            _IDate.AutoCount--;
            _IDate.AutoSurplus = _IDate.AutoCount;
            _ResourceManager._Auto_text.text = _IDate.AutoSurplus.ToString();

        }

    }
    #endregion

    #region Auto數 清除
    /// <summary>
    /// Auto數 清除
    /// </summary>
    public void Auto_Clear()
    {

        if (_IDate.AutoCount > 1)
        {
            _IDate.AutoCount = 1;
            _IDate.AutoSurplus = 1;
            _ResourceManager._Auto_text.text = _IDate.AutoSurplus.ToString();

        }

    }
    #endregion

    #region Auto數 暫停
    /// <summary>
    /// Auto數 暫停
    /// </summary>
    public void Auto_pause()
    {

        _IDate.AutoCount = 1;

    }
    #endregion

    #region Bet 數 加注
    /// <summary>
    /// Bet 數 加注
    /// </summary>
    public void BetPlus()
    {

        Debuger.Log("_IDate.PlayerCoin:" + _IDate.PlayerCoin);
        int tx;
        int coin_temp;
        tx = _IDate.PlayerCoin % _IDate.LeastBetCount;//總金額除最小下注數的餘數
        coin_temp = _IDate.PlayerCoin - tx;//總金額減掉（總金額除最小下注數的餘數） 就是以為最小下注數單位最大可以下注的金額

        if (_IDate.Bet_Coin < coin_temp)
        {
            if (EventPlus.Down_Time < 1)//在不是長按的情況下
            {

                _IDate.Bet_Coin += _IDate.LeastBetCount;//玩家下注的錢加100
                _ResourceManager._Bet_Text.text = _IDate.Bet_Coin.ToString();
                _ResourceManager._BetMenu_Text.text = _ResourceManager._Bet_Text.text;

            }

        }

    }
    #endregion

    #region Bet 數 減少押注
    /// <summary>
    /// Bet 數 減少押注
    /// </summary>
    public void BetReduce()
    {
        if (_IDate.Bet_Coin >= _IDate.LeastBetCount)
        {

            if (EventReduce.Down_Time < 1)//在不是長按的情況下
            {
                _IDate.Bet_Coin -= 100;
                _ResourceManager._Bet_Text.text = _IDate.Bet_Coin.ToString();
                _ResourceManager._BetMenu_Text.text = _ResourceManager._Bet_Text.text;

            }

        }

    }
    #endregion

    #region 說明畫面向左換圖
    /// <summary>
    /// 說明畫面向左換圖
    /// </summary>
    public void InfoLeft()
    {
        int i = System.Array.IndexOf(_ResourceManager._InFoSprites, _ResourceManager._Img_Introduction.sprite);

        if (i > 0)
        {

            //Debuger.Log(i);
            i -= 1;
            _ResourceManager._Img_Introduction.sprite = _ResourceManager._InFoSprites[i];

        }

    }
    #endregion

    #region 說明畫面向右換圖
    /// <summary>
    /// 說明畫面向右換圖
    /// </summary>
    public void InfoRight()
    {
        int i = System.Array.IndexOf(_ResourceManager._InFoSprites, _ResourceManager._Img_Introduction.sprite);
        int SPLength;
        SPLength = _ResourceManager._InFoSprites.Length - 1;

        if (i < SPLength)
        {
            //Debuger.Log(i);
            i += 1;
            _ResourceManager._Img_Introduction.sprite = _ResourceManager._InFoSprites[i];

        }

    }
    #endregion

    #region 打開說明畫面
    /// <summary>
    /// 打開說明畫面
    /// </summary>
    public void OpenINFO()
    {

        _ResourceManager._InfoBackSprite.gameObject.SetActive(true);
        _ResourceManager._Img_Introduction.sprite = _ResourceManager._InFoSprites[0];

    }
    #endregion

    #region 離開說明畫面
    /// <summary>
    /// 離開說明畫面
    /// </summary>
    public void OutInfo()
    {

        _ResourceManager._InfoBackSprite.gameObject.SetActive(false);

    }
    #endregion

    #region 最大押注
    /// <summary>
    /// 最大押注
    /// </summary>
    public void Bet_MaxCoin()
    {

        int tx;
        int coin_temp;
        tx = _IDate.PlayerCoin % _IDate.LeastBetCount;//總金額除100的餘數
        coin_temp = _IDate.PlayerCoin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
        _IDate.Bet_Coin = coin_temp;
        _ResourceManager._Bet_Text.text = _IDate.Bet_Coin.ToString();
        _ResourceManager._BetMenu_Text.text = _ResourceManager._Bet_Text.text;

    }
    #endregion


    #region 按鈕長按(+)
    /// <summary>
    ///按鈕長按 
    /// </summary>
    public void Button_PressAndHold()
    {

        if (EventPlus.b)
        {
            int tx;
            int coin_temp;
            tx = _IDate.PlayerCoin % 100;//總金額除100的餘數
            coin_temp = _IDate.PlayerCoin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
            if (_IDate.Bet_Coin < coin_temp)
            {

                _IDate.Bet_Coin = _IDate.Bet_Coin + (EventPlus.Return_Valu * 100);
                EventPlus.b = false;
                Debuger.Log("下注金額 ： " + _IDate.Bet_Coin);
                _ResourceManager._Bet_Text.text = _IDate.Bet_Coin.ToString();
                _ResourceManager._BetMenu_Text.text = _ResourceManager._Bet_Text.text;

            }

        }

    }
    #endregion




    #region 按鈕長按（-）
    /// <summary>
    /// 按鈕長按（-）
    /// </summary>
    public void Button_Reduce_Press()
    {

        if (EventReduce.b)
        {

            if (_IDate.Bet_Coin >= 100)
            {

                _IDate.Bet_Coin = _IDate.Bet_Coin - (EventReduce.Return_Valu * 100);
                EventReduce.b = false;
                Debuger.Log("下注金額 ： " + _IDate.Bet_Coin);
                _ResourceManager._Bet_Text.text = _IDate.Bet_Coin.ToString();
                _ResourceManager._BetMenu_Text.text = _ResourceManager._Bet_Text.text;

            }

        }

    }
    #endregion


    #region 將資料讀取
    /// <summary>
    /// 將資料讀取
    /// </summary>
    public void GetDateSave()
    {
        Debuger.Log("有無遊戲資料 ：" + PlayerPrefs.HasKey("遊戲資料"));

        if (PlayerPrefs.HasKey("遊戲資料"))
        {

            _ResourceManager._Options.SetActive(true);

        }

    }
    #endregion

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

}
