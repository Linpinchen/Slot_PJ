using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowScript : MonoBehaviour,IShow
{
    bool _StRecover;
    Transform[] _StartPoint;
    Queue<GameObject> _DrawLinePool;
    List<List<GameObject>> _BonusPrepareDrawline;
    List<GameObject> _PrepareDrawLine;
    GameObject _GBDrawLine;
    Transform _InLineRenderPool;
    Transform _LineRenderOutPool;
    Transform _BonusLineRenderOutPool;
    GameObject _BonusLineRenderChildPool;
    List<Transform> _UiShow;
    Animator _Amr_WinShow;
    Animator _BonusAnimator;
    Animator _BonusEndShow;
    VideoPlayer _EndShowPlayer;
    VideoClip _EndShowVideoClip;
    RawImage _VideoImage;
   

 

    public bool StRecover { get { return _StRecover; } set { StRecover = value; }  }

    public Transform[] StartPoint { get{ return _StartPoint; } }

    public Queue<GameObject> DrawLinePool { get {return _DrawLinePool; } set { _DrawLinePool = value; } }

    public List<List<GameObject>> BonusPrepareDrawline { get { return _BonusPrepareDrawline; } set { _BonusPrepareDrawline = value; } }

    public List<GameObject> PrepareDrawLine { get {return _PrepareDrawLine; } set { _PrepareDrawLine = value; } }

    public GameObject GBDrawLine { get { return _GBDrawLine; } set { _GBDrawLine = value; } }

    public Transform InLineRenderPool { get {return _InLineRenderPool; } set { _InLineRenderPool=value; } }

    public Transform LineRenderOutPool { get { return _LineRenderOutPool; } set { _LineRenderOutPool=value; } }

    public Transform BonusLineRenderOutPool { get { return _BonusLineRenderOutPool; } set { _BonusLineRenderOutPool=value; } }

    public GameObject BonusLineRenderChildPool { get { return _BonusLineRenderChildPool; } set { _BonusLineRenderChildPool = value; } }

    public List<Transform> UiShow { get { return _UiShow; } set { _UiShow = value; } }

    public Animator Amr_WinShow { get { return _Amr_WinShow; } set { _Amr_WinShow = value; } }

    public Animator BonusAnimator { get {return _BonusAnimator; } set { _BonusAnimator = value; } }

    public Animator BonusEndShow { get { return _BonusEndShow; } set { _BonusEndShow = value; } }

    public VideoPlayer EndShowPlayer { get { return _EndShowPlayer; } set { _EndShowPlayer = value; } }

    public VideoClip EndShowVideoClip { get { return _EndShowVideoClip; } set { _EndShowVideoClip = value; } }

    public RawImage VideoImage { get { return _VideoImage; } set { _VideoImage = value; } }









    public List<Transform> GetRoolWinImg(Reel_Move[] _ReelMove, int[] SlotSequence)
    {


        List<Transform> VVVs;
        VVVs = new List<Transform>();
        for (int i = 0; i < _ReelMove.Length; i++)
        {

            VVVs.Add(_ReelMove[i].Reel_images[i].rectTransform);
            Debug.Log(VVVs[i]);
        }
        return VVVs;



    }


    public void InstObjectInitialization(GameObject Reuse, Transform OrangePoint, List<Transform> PointPosition, Transform EndPoint)
    {
        Reuse.GetComponent<DrawLine>().Taget_Point = new List<Transform>();

        for (int i = 0; i < PointPosition.Count; i++)
        {

            Reuse.GetComponent<DrawLine>().Taget_Point.Add(PointPosition[i].transform);


        }
        Reuse.GetComponent<DrawLine>().Temp_point = new List<Transform>();
        Reuse.GetComponent<DrawLine>().Temp_point.Add(OrangePoint);
        Reuse.GetComponent<DrawLine>().Temp_point.Add(Reuse.GetComponent<DrawLine>().gameObject.transform);
        Reuse.GetComponent<DrawLine>().Temp_VV = Reuse.GetComponent<DrawLine>().Taget_Point[0].transform;//PointPosition[0];
        Reuse.GetComponent<DrawLine>().Orange_point = OrangePoint.gameObject;
        Reuse.GetComponent<DrawLine>().Taget_Point.Add(EndPoint);
        //Debug.Log("Manager__ Temp_point.Count : " + Reuse.GetComponent<DrawLine>().Temp_point.Count);
        //Debug.Log("Manager__Taget_Point.Count : " + Reuse.GetComponent<DrawLine>().Taget_Point.Count);
    }


    public void ListShiny(List<Transform> _GetRoolWinImg, List<Transform> ReadyShow)
    {

        for (int i = 0; i < _GetRoolWinImg.Count; i++)
        {
            if (!ReadyShow.Contains(_GetRoolWinImg[i]))
            {
                ReadyShow.Add(_GetRoolWinImg[i]);

            }


        }

    }


    public void ShinyShow(List<Transform> ReadyShow)
    {
        for (int i = 0; i < ReadyShow.Count; i++)
        {
            Animator Amo;
            Amo = ReadyShow[i].GetComponent<Animator>();
            Amo.SetTrigger("StShiny");


        }
    }


    public void ObjectPool(Transform initalPosition, List<Transform> PointPosition, Transform EndPoint, Transform StayOutpool, List<GameObject> StayDrawLine)
    {
        GameObject Reuse;
        if (DrawLinePool.Count > 0)//如果有DrawLinePool裡有預制物就取出來用
        {


            Reuse = DrawLinePool.Dequeue();//取出DrawLinePool的預制物
            Reuse.transform.position = initalPosition.position;//設置預制物的位置

            InstObjectInitialization(Reuse, initalPosition, PointPosition, EndPoint);
            Reuse.transform.parent = StayOutpool;
            StayDrawLine.Add(Reuse);//放進去等待啟動

        }
        else
        {

            Reuse = Instantiate(GBDrawLine, StayOutpool);
            Reuse.transform.position = initalPosition.position;

            InstObjectInitialization(Reuse, initalPosition, PointPosition, EndPoint);
            Reuse.SetActive(false);
            StayDrawLine.Add(Reuse);//放進去等待啟動


        }

    }


    public void ObjectPoolInitialization()
    {
        int InitailSize = 5;

        DrawLinePool = new Queue<GameObject>();

        for (int i = 0; i < InitailSize; i++)
        {
            GameObject InstGb;

            InstGb = Instantiate(GBDrawLine, InLineRenderPool);//預制物生成
            DrawLinePool.Enqueue(InstGb);//將生成的預制物方進DrawLinePool這個Queue裡
            InstGb.SetActive(false);
            Debug.Log("DrawLinePool.Count:" + DrawLinePool.Count);
        }
    }


    public void Recover(GameObject recover, bool Stb)
    {
        Transform OringerTs = gameObject.transform;//OringerTs就是自身座標
        if (!Stb)
        {
            Debug.Log("---------------------------Start_Recover :------------------------");

            recover.GetComponent<DrawLine>().LI.positionCount = 0;
            recover.GetComponent<DrawLine>().LI.positionCount = 2;

            DrawLinePool.Enqueue(recover);
            recover.transform.parent = InLineRenderPool;
            recover.SetActive(false);

        }

    }


    public void RecoverComply(Transform OutPool, List<GameObject> StayDrawLine, List<bool> _SHowOk)
    {

        bool DrawAllBool;
        DrawAllBool = _SHowOk.Contains(true);


        if (StayDrawLine.Count != 0)//LineRenderOutPool.childCount != 0
        {
            //Debug.Log("----------------LineRenderOutPool.childCount : ----------------------"+ LineRenderOutPool.childCount);
            //Debug.Log("StayDrawLine.Count :" + StayDrawLine.Count);

            if (_SHowOk.Count != 0 && StayDrawLine.Count != 0)//持續更新裡面的bool
            {


                for (int i = 0; i < StayDrawLine.Count; i++)
                {
                    //Debug.Log("_SHowOk[i] 持續更新");
                    _SHowOk[i] = StayDrawLine[i].GetComponent<DrawLine>().StDrawLine;


                }




            }



            //Debug.Log("--------------DrawStop :--------"+ DrawStop);

            if (!DrawAllBool && _SHowOk.Count != 0)//DrawLine 全部線都表演完
            {
                for (int i = 0; i < OutPool.childCount; i++)
                {

                    Recover(OutPool.transform.GetChild(i).gameObject, DrawAllBool); //這裡因為Bonus每個盤面生成的物件都放這 所以出了問題


                }

                if (OutPool.childCount == 0)
                {

                    StayDrawLine.Clear();
                    _SHowOk.Clear();
                    //Debug.Log("------------PrepareDrawLine.Count :------------" + PrepareDrawLine.Count);
                    StRecover = true;
                }

            }


        }
    }


    public void StartDrawLine(List<GameObject> StayDrawLine, List<bool> _ShowOk)
    {
        for (int i = 0; i < StayDrawLine.Count; i++)
        {
            StayDrawLine[i].SetActive(true);
            StayDrawLine[i].GetComponent<DrawLine>().StDrawLine = true;
            _ShowOk.Add(StayDrawLine[i].GetComponent<DrawLine>().StDrawLine);

        }
    }
}
