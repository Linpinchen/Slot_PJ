using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public interface IShow

{
    int TempWinCoin { get; set; }
    bool StRecover { get; set; }
    Image[] WinCoins { get; set; }
    Sprite[] Numbers { get; set; }
    List<List<bool>> BonusDrawLineOk { get; set; }
    List<bool> DrawLineOK { get; set; }
    Transform[] StartPoint { get; }
    Queue<GameObject> DrawLinePool { get; set; }
    List<List<GameObject>> BonusPrepareDrawline { get; set; }
    List<GameObject> PrepareDrawLine { get; set; }
    GameObject GBDrawLine { get; set; }
    Transform InLineRenderPool { get; set; }
    Transform LineRenderOutPool { get; set; }
    Transform BonusLineRenderOutPool { get; set; }
    GameObject BonusLineRenderChildPool { get; set; }
    List<Transform> UiShow { get; set; }
    List<List<Transform>> BonusUiShow { get; set; }
    Animator Amr_WinShow { get; set; }
    Animator BonusAnimator { get; set; }
    Animator BonusEndShow { get; set; }
    VideoPlayer EndShowPlayer { get; set; }
    VideoClip EndShowVideoClip { get; set; }
    RawImage VideoImage { get; set; }
   




    void ObjectPoolInitialization();
    void ObjectPool(Transform initalPosition, List<Transform> PointPosition, Transform EndPoint, Transform StayOutpool, List<GameObject> StayDrawLine);
    void Recover(GameObject recover, bool Stb);
    void InstObjectInitialization(GameObject Reuse, Transform OrangePoint, List<Transform> PointPosition, Transform EndPoint);
    void RecoverComply(Transform OutPool, List<GameObject> StayDrawLine, List<bool> _SHowOk);
    List<Transform> GetRoolWinImg(Reel_Move[]_ReelMove, int[] SlotSequence);
    IEnumerator StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk);
    void ListShiny(List<Transform> _GetRoolWinImg, List<Transform> ReadyShow);
    IEnumerator ShinyShow(List<Transform> ReadyShow);
    IEnumerator CoinShow(int Coin,int FreeGameCount);

   
}
