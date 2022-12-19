using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IUIControl

{
    Button StartGame { get; }
    Button Bet_Button { get; }
    Button Auto_Button { get; }
    GameObject Bet_Menu { get; }
    GameObject Auto_Menu { get; }
    GameObject Options { get; }
    Button Bet_Plus { get; }
    Button Bet_Reduce { get; }
    Button Bet_MaxCoin { get; }
    Button Auto_Clear_Button { get; }
    Button Auto_pause_Button { get; }
    Button Auto_Plus { get; }
    Button Auto_Reduce { get; }
    Button BonusDateCreat { get; }
    Button InFoButton { get; }
    Button InfoOutButton { get; }
    Button ButtonLeft { get; }
    Button ButtonRight { get; }
    Button Options_Yes { get; }
    Button Options_No { get; }
    Text Bet_Text { get; }
    Text BetMenu_Text { get; }
    Text Auto_text { get; }
    Sprite[] InFoSprites { get; }
    Image InfoBackSprite { get; }
    Image Img_Introduction { get; }
}
