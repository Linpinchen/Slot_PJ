using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerControl
{
    public delegate void StartGameMethod();

    //public StartGameMethod startGameMethod;
    //public StartGameMethod Option_Yes;
    //public StartGameMethod Option_No;

    //public Button StartGame_Button;
    //public Button Bet_Button;
    //public Button Auto_Button;
    //public Button Bet_Plus_Button;
    //public Button Bet_Reduce_Button;
    //public Button Bet_MaxCoin_Button;
    //public Button Auto_Clear_Button;
    //public Button Auto_pause_Button;
    //public Button Auto_Plus_Button;
    //public Button Auto_Reduce_Button;
    //public Button BonusDateCreat_Button;
    //public Button InFoButton_Button;
    //public Button InfoOutButton_Button;
    //public Button ButtonLeft_Button;
    //public Button ButtonRight_Button;
    //public Button Options_Yes_Button;
    //public Button Options_No_Button;
    //public Button OpenOperational;
    //public Button OutOperational;


    // 按鈕透過素材腳本拿取

    public void PlayerControl_Init(IUIControlMethod _UIMethod, Slot_Manager _Manager,ResourceManager _ResourceManager)
    {
        //遊戲開始按鈕
        _ResourceManager.StartGame_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _Manager.StartGame();

        });

        //遊戲資料 - 確定讀取 按鈕
        _ResourceManager.Options_Yes_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _Manager.Options_Yes();

        });

        //遊戲資料 - 不要讀取 按鈕
        _ResourceManager.Options_No_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _Manager.Options_No();

        });


        //測試用必中Bonus按鈕
        _ResourceManager.BonusDateCreat_Button.onClick.AddListener(delegate
        {

            _UIMethod.AddBonus();

        });

        //開啟 - 押注小視窗 - 按鈕
        _ResourceManager.Bet_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.BetMenuSwitch();

        });

        //開啟 - Auto小視窗 - 按鈕
        _ResourceManager.Auto_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.AutoMenuSwitch();

        });

        //押注 - 加注 - 按鈕
        _ResourceManager.Bet_Plus_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {

                _UIMethod.BetPlus();

            }

        });

        //押注 - 減注 - 按鈕
        _ResourceManager.Bet_Reduce_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {

                _UIMethod.BetReduce();

            }

        });

        //押注 - 最大押注 - 按鈕
        _ResourceManager.Bet_MaxCoin_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {
                _UIMethod.Bet_MaxCoin();
            }

        });

        //Auto - 清除循環次數 - 按鈕
        _ResourceManager.Auto_Clear_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.Auto_Clear();

        });

        //Auto - 停止循環 - 按鈕
        _ResourceManager.Auto_pause_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.Auto_pause();

        });

        //Auto - 循環次數增加 - 按鈕
        _ResourceManager.Auto_Plus_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {

                _UIMethod.AutoPlus();

            }

        });

        //Auto - 循環次數減少加 - 按鈕
        _ResourceManager.Auto_Reduce_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {

                _UIMethod.AutoReduce();

            }

        });

        //遊戲介紹視窗開啟
        _ResourceManager.InFoButton_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            if (!_Manager.Start_Slot)
            {
                _UIMethod.OpenINFO();
            }

        });

        //遊戲介紹視窗 - 離開視窗
        _ResourceManager.InfoOutButton_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.OutInfo();

        });

        //遊戲介紹視窗 - 向左換圖
        _ResourceManager.ButtonLeft_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.InfoLeft();

        });

        //遊戲介紹視窗 - 向右換圖
        _ResourceManager.ButtonRight_Button.onClick.AddListener(delegate
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _UIMethod.InfoRight();

        });

        _ResourceManager.OpenOperational.onClick.AddListener(delegate ()
        {

            AudioManager.inst.PlayAddSFX("SFX", 1);
            _ResourceManager._Img_Operational.gameObject.SetActive(true);

        });

        _ResourceManager.OutOperational.onClick.AddListener(delegate ()
        {
            AudioManager.inst.PlayAddSFX("SFX", 1);
            _ResourceManager._Img_Operational.gameObject.SetActive(false);

        });

    }

}
