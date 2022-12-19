﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControlMethod : IUIControlMethod{


    IDate _IDate;
    Button_EventTrigger EventPlus;
    Button_EventTrigger EventReduce;


   
    GameObject _Bet_Menu;
    GameObject _Auto_Menu;
    GameObject _Options;
    Text _Bet_Text;
    Text _BetMenu_Text;
    Text _Auto_text;
    Sprite[] _InFoSprites;
    Image _InfoBackSprite;
    Image _Img_Introduction;

    public GameObject Bet_Menu { get { return _Bet_Menu; } }

    public GameObject Auto_Menu { get { return _Auto_Menu; } }

    public GameObject Options { get { return _Options; } }

    public Text Bet_Text { get { return _Bet_Text; } }

    public Text BetMenu_Text { get { return _BetMenu_Text; } }

    public Text Auto_text { get { return _Auto_text; } }

    public Sprite[] InFoSprites { get { return _InFoSprites; } }

    public Image InfoBackSprite { get { return _InfoBackSprite; } }

    public Image Img_Introduction { get { return _Img_Introduction; } }





    /// <summary>
    /// Bet小視窗開關
    /// </summary>
    public void BetMenuSwitch()
    {
        bool BetMenu = _Bet_Menu.activeInHierarchy;

        switch (BetMenu)
        {
            case true:
            {
                _Bet_Menu.SetActive(false);
                break;
            }
            default:
            {

                _Bet_Menu.SetActive(true);
                break;

            }
        }
       
    }
    /// <summary>
    /// Auto小視窗開關
    /// </summary>
    public void AutoMenuSwitch()
    {
        bool AutoMenu = _Auto_Menu.activeInHierarchy;

        switch (AutoMenu)
        {
            case true:
                {
                    _Auto_Menu.SetActive(false);
                    break;
                }
            default:
                {

                    _Auto_Menu.SetActive(true);
                    break;

                }
        }
    }
    /// <summary>
    /// Auto次數增加
    /// </summary>
    public void AutoPlus()
    {
        if (_IDate.AutoCount < 9999)
        {

            _IDate.AutoCount++;
            _IDate.AutoSurplus = _IDate.AutoCount;
           _Auto_text.text= _IDate.AutoSurplus.ToString();
        }

    }

    public void AutoReduce()
    {
        if (_IDate.AutoCount > 0)
        {
            _IDate.AutoCount--;
            _IDate.AutoSurplus = _IDate.AutoCount;
            _Auto_text.text = _IDate.AutoSurplus.ToString();
        }

    }

    public void Auto_Clear()
    {
        if (_IDate.AutoCount > 1)
        {
            _IDate.AutoCount = 1;
            _IDate.AutoSurplus = 1;
            Auto_text.text = _IDate.AutoSurplus.ToString();

        }
    }

    public void Auto_pause()
    {
        //if (Start_Slot)
        //{

            _IDate.AutoCount = 1;


        //}
    }

    public void BetPlus()
    {
        //if (!Start_Slot)
        //{

            Debug.Log("_IDate.PlayerCoin:" +_IDate.PlayerCoin);
            int tx;
            int coin_temp;
            tx = _IDate.PlayerCoin % _IDate.LeastBetCount;//總金額除最小下注數的餘數
            coin_temp = _IDate.PlayerCoin - tx;//總金額減掉（總金額除最小下注數的餘數） 就是以為最小下注數單位最大可以下注的金額
            if (_IDate.Bet_Coin < coin_temp)
            {
                if (EventPlus.Down_Time < 1)//在不是長按的情況下
                {

                    _IDate.Bet_Coin += _IDate.LeastBetCount;//玩家下注的錢加100
                    _Bet_Text.text = _IDate.Bet_Coin.ToString();
                    _BetMenu_Text.text = _Bet_Text.text;


                }


            }



        //}
    }

    public void BetReduce()
    {
        if (_IDate.Bet_Coin >= _IDate.LeastBetCount)
        {
            if (EventReduce.Down_Time < 1)//在不是長按的情況下
            {

                _IDate.Bet_Coin -= 100;
                _Bet_Text.text = _IDate.Bet_Coin.ToString();
                _BetMenu_Text.text = _Bet_Text.text;

            }

        }
    }

    public void InfoLeft()
    {
        int i = System.Array.IndexOf(InFoSprites, Img_Introduction.sprite);
        
        if (i > 0)
        {
            //Debug.Log(i);
            i -= 1;
            Img_Introduction.sprite = InFoSprites[i];


        }

    }

    public void InfoRight()
    {
        int i = System.Array.IndexOf(InFoSprites, Img_Introduction.sprite);
        int SPLength;
        SPLength = InFoSprites.Length - 1;
        if (i < SPLength)
        {
            //Debug.Log(i);
            i += 1;
            Img_Introduction.sprite = InFoSprites[i];


        }
    }

    public void OpenINFO()
    {
        //if (!Start_Slot)
        //{

        _InfoBackSprite.gameObject.SetActive(true);
            Img_Introduction.sprite = InFoSprites[0];
        //}

    }

    public void OutInfo()
    {
        _InfoBackSprite.gameObject.SetActive(false);
    }

   public void Bet_MaxCoin()
    {
        //if (!Start_Slot)
        //{

            int tx;
            int coin_temp;
            tx = _IDate.PlayerCoin % _IDate.LeastBetCount;//總金額除100的餘數
            coin_temp = _IDate.PlayerCoin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
            _IDate.Bet_Coin = coin_temp;
            _Bet_Text.text = _IDate.Bet_Coin.ToString();
            _BetMenu_Text.text = _Bet_Text.text;

        //}
    }





    public void UIControlInit(IDate Idate, Button_EventTrigger EventPlus,Button_EventTrigger EventReduce)
    {
        this._IDate = Idate;
        this.EventPlus = EventPlus;
        this.EventReduce = EventReduce;
    }




}