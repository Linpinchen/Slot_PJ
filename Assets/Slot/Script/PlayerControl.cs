using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerControl
{
    public delegate void StartGameMethod();

    public StartGameMethod startGameMethod;
    public StartGameMethod BonusDateCreat;
    public StartGameMethod Option_Yes;
    public StartGameMethod Option_No;


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


    public void PlayerControl_Init(IUIControlMethod _UIMethod,Slot_Manager _Manager)
	{
        //遊戲開始按鈕
        StartGame_Button.onClick.AddListener(delegate
        {


            startGameMethod();



        });

        //遊戲資料 - 確定讀取 按鈕
        Options_Yes_Button.onClick.AddListener(delegate
        {

            Option_Yes();

        });

        //遊戲資料 - 不要讀取 按鈕
        Options_No_Button.onClick.AddListener(delegate
        {

            Option_No();

        });


        //測試用必中Bonus按鈕
        BonusDateCreat_Button.onClick.AddListener(delegate
        {

            _UIMethod.AddBonus();

        });

        //開啟 - 押注小視窗 - 按鈕
        Bet_Button.onClick.AddListener(delegate
        {

            _UIMethod.BetMenuSwitch();

        });

        //開啟 - Auto小視窗 - 按鈕
        Auto_Button.onClick.AddListener(delegate
        {

            _UIMethod.AutoMenuSwitch();

        });

        //押注 - 加注 - 按鈕
        Bet_Plus_Button.onClick.AddListener(delegate
        {
            if (!_Manager.Start_Slot)
            {
                _UIMethod.BetPlus();

            }
           

        });

        //押注 - 減注 - 按鈕
        Bet_Reduce_Button.onClick.AddListener(delegate
        {
            if (!_Manager.Start_Slot)
            {

                _UIMethod.BetReduce();

            }
            
        });

        //押注 - 最大押注 - 按鈕
        Bet_MaxCoin_Button.onClick.AddListener(delegate
        {
            if (!_Manager.Start_Slot)
            {
                _UIMethod.Bet_MaxCoin();
            }

        });

        //Auto - 清除循環次數 - 按鈕
        Auto_Clear_Button.onClick.AddListener(delegate
        {

            _UIMethod.Auto_Clear();

        });

        //Auto - 停止循環 - 按鈕
        Auto_pause_Button.onClick.AddListener(delegate
        {
            _UIMethod.Auto_pause();

        });

        //Auto - 循環次數增加 - 按鈕
        Auto_Plus_Button.onClick.AddListener(delegate
        {

            _UIMethod.AutoPlus();

        });

        //Auto - 循環次數減少加 - 按鈕
        Auto_Reduce_Button.onClick.AddListener(delegate
        {

            _UIMethod.AutoReduce();

        });

        //遊戲介紹視窗開啟
        InFoButton_Button.onClick.AddListener(delegate
        {
            if (!_Manager.Start_Slot)
            {
                _UIMethod.OpenINFO();
            }

        });

        //遊戲介紹視窗 - 離開視窗
        InfoOutButton_Button.onClick.AddListener(delegate
        {

            _UIMethod.OutInfo();

        });

        //遊戲介紹視窗 - 向左換圖
        ButtonLeft_Button.onClick.AddListener(delegate
        {

            _UIMethod.InfoLeft();


        });

        //遊戲介紹視窗 - 向右換圖
        ButtonRight_Button.onClick.AddListener(delegate
        {

            _UIMethod.InfoRight();

        });


      


    }
	
}
