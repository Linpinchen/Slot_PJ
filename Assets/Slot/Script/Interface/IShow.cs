using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public interface IShow

{
    int TempWinCoin { get; set; }
    bool StRecover { get; set; }
    bool CoinShow_Bool { get; set; }
    Image[] WinCoins { get; set; }
    Sprite[] Numbers { get; set; }
    List<List<bool>> BonusDrawLineOk { get; set; }
    List<bool> DrawLineOK { get; set; }
    RectTransform[] StartPoint { get; }
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
    void ObjectPool(Reel_Move[] _ReelMove, int Number, Transform StayOutpool, List<GameObject> StayDrawLine);
    void Recover(GameObject recover, bool Stb);
    void InstObjectInitialization(GameObject Reuse, RectTransform OrangePoint, List<RectTransform> PointPosition, RectTransform EndPoint);
    void RecoverComply(Transform OutPool, List<GameObject> StayDrawLine, List<bool> _SHowOk);
    List<RectTransform> GetRoolWinImg(Reel_Move[] _ReelMove, int SlotSequence);
    IEnumerator StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk);
    void ListShiny(Reel_Move[] _ReelMove, int Number, List<Transform> ReadyShow);
    IEnumerator ShinyShow(List<Transform> ReadyShow);
    IEnumerator CoinShow(int Coin);

   
}
