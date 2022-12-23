﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot_data :IDate ,IDateEvent{

    IShow _Ishow;
    IUIControlMethod IUICM;
   
    Reel_Move[] _Reelmove;


    [SerializeField]
    int Coin;//玩家擁有的錢
    [SerializeField]
    int bet_Coin;//押注的錢
    [SerializeField]
    int WinCoin;//普通盤面贏的錢
    [SerializeField]
    int leastBetCount;//最小押注數
    [SerializeField]
    List<int> BonusWin;//Bonus每盤各贏得錢
    [SerializeField]
    int totalBonusWin;//Bonus共贏多少
    [SerializeField]
    int _AutoCount;//Auto循環次數
    [SerializeField]
    int _CycleCount;//以轉動次數
    [SerializeField]
    int _AutoSurplus;//尚未循環次數
    [SerializeField]
    int Bonus_count;//當前 Bonus圖片出現的數量（用來算每個輪條[2][3][4]Bonus圖片出現幾次）
    public Sprite[] Sprite_Pool;//圖片庫
    public Slot_SeveDate _Slot_SeverDate;
	public Slot_SeveDate GetSlotDate;
	public List<intCount> _Date;



    #region IDate 介面實作內容
    public int PlayerCoin
    {
        get{ return Coin; }
        set{ Coin = value; }
    }

	public int Bet_Coin
	{
		get{ return bet_Coin; }
		set{ bet_Coin = value; }
	}

    public int Win_Coin
	{
		get{ return WinCoin; }
		set{ WinCoin = value; }
	}

    public List<int> BonusWinCoin
	{
		get{return BonusWin;}
		set{BonusWin = value;}
	}

    public int Total_BonusWinCoin
	{
		get { return totalBonusWin; }
		set { totalBonusWin = value; }
	}
    public int AutoCount
	{
		get { return _AutoCount; }
		set { _AutoCount = value; }
	}
    public int CycleCount
	{
		get { return _CycleCount; }
		set { _CycleCount = value; }
	}
    public int AutoSurplus
	{
		get { return _AutoSurplus; }
		set { _AutoSurplus = value; }
	}
    public int BonusCount
	{
		get { return Bonus_count; }
		set { Bonus_count = value; }
	}
    public Sprite[] SpritePool
	{
		get { return Sprite_Pool; }
		set { Sprite_Pool = value; }

	}

	public int LeastBetCount
	{
		get { return leastBetCount; }
		set { leastBetCount = value; }

    }

    #endregion


    public void Init(IShow ishow,IUIControlMethod _IUICM ,Reel_Move[] _ReelMoves)
    {

        this._Ishow = ishow;
        this.IUICM = _IUICM;
        this._Reelmove = _ReelMoves;
        _Date = new List<intCount>();



    }



    #region 初始化時 將盤面的圖灌好 跟資料無關只是顯示圖片
    /// <summary>
    /// 初始化時 將盤面的圖灌成跟＿Reel_Sprite_Date資料上的一樣
    /// </summary>
    /// <param name = "Chang_Sprite" ></ param >
       public void Initialization_Slot_Sprite(Reel_Move[]reel_Moves)
    {
        //Debug.Log("依＿Reel_Sprite_Date資料設置圖片");
        int Tempi;
        Tempi = Sprite_Pool.Length - 2;//不要特殊圖

        for (int i = 0; i < reel_Moves.Length; i++)
        {

            for (int j = 0; j < reel_Moves[i].transform.childCount; j++)
            {
                int Changei;
                Changei = Random.Range(0, Tempi);
                reel_Moves[i].transform.GetChild(j).GetComponent<Image>().sprite = Sprite_Pool[Changei];
                //Debug.Log(string.Format("第{0}個輪條的第{1}張圖片,圖片名稱{2}",i,j,_Reel_Moves[i].transform.GetChild(j).GetComponent<Image>().sprite.name));

            }



        }


    }
    #endregion




    #region 普通盤面生成灌入
    /// <summary>
    /// 生成圖片資料
    /// </summary>
    public void Generate_Date_Sprite(SlotGrid slotGrid)
	{
		int GridCount = slotGrid._grids.Count - 1;
		
		for (int i=0;i<slotGrid.reelcount;i++)
		{
			
			int Temp = slotGrid.imagecount[i];

			if (i == 0)//第一個輪條  
			{
				// 需要灌入的圖片資料
				List<Pool_Images> LM = new List<Pool_Images>();
				foreach (Pool_Images p in System.Enum.GetValues(typeof(Pool_Images)))
				{
					LM.Add(p);
				}
				LM.RemoveAt((int)Pool_Images.Universal_Sprite);//輪條1不會有連線共同圖
				int _One_Sprite_pool_Temp = LM.Count - 1;//第一輪用的陣列少掉Bonus圖


				for (int j = 0; j < Temp; j++)
				{
					
					if (slotGrid._grids[GridCount]._Grids[i]._GridInt.Contains(Pool_Images.Bonus))//已經有Bonus圖的話 就不要再出現Bonus圖了
					{
						int Temp_B;
						Temp_B = Random.Range(0,_One_Sprite_pool_Temp);
						slotGrid._grids[GridCount]._Grids[i]._GridInt[j] = LM[Temp_B];
					}
					else
					{
						
						int Tempj = Random.Range(0, LM.Count);
						slotGrid._grids[GridCount]._Grids[i]._GridInt[j] = LM[Tempj];

					}

				}
				
			}

			else//除了第一條輪條外的其他輪條
			{
				

				for (int j = 0; j < Temp; j++)//生成輪條內的圖片
				{
					int Pool_Temp;
					Pool_Temp = System.Enum.GetNames(typeof(Pool_Images)).Length - 1;//圖庫的圖數量減1,填入的圖會少了Bonus圖

					//檢查陣列裡有沒有Bonus圖了
					if (slotGrid._grids[GridCount]._Grids[i]._GridInt.Contains(Pool_Images.Bonus) || Bonus_count >= 3)//已經有Bonus圖的話 就不要再出現Bonus圖了
					{
						
						int Tempj = Random.Range(0, Pool_Temp);
						slotGrid._grids[GridCount]._Grids[i]._GridInt[j] = (Pool_Images)Tempj;//填入的圖會少了Bonus圖
					}
					
					else 
					{

						int Tempj = Random.Range(0, System.Enum.GetNames(typeof(Pool_Images)).Length);
						slotGrid._grids[GridCount]._Grids[i]._GridInt[j] = (Pool_Images)Tempj;//完整的圖庫

					}
					
					
					
				}

			}

			//檢查出現的Bonus圖是否在 輪條[1] [2] [3]的位置
			for (int Slot_i = 1; Slot_i < 4; Slot_i++)
			{

				if (slotGrid._grids[GridCount]._Grids[i]._GridInt[Slot_i] == Pool_Images.Bonus)
				{
					Bonus_count++;//有出現Bonus圖就++
					Debug.Log("大獎數量 ：" + Bonus_count);


				}


			}


		}








	}
    #endregion

    #region Bonus 盤面資料生成灌入
    /// <summary>
    /// Bonus 盤面資料生成
    /// </summary>
    /// <param name="slotGrid"></param>
    public void GenerateBonusDate(SlotGrid slotGrid)//Bonus所有盤面生成 

	{
		int SpecialCount=2;


		for (int i=0;i<slotGrid._grids.Count;i++)
		{

			
			
			
			for (int j=0;j<slotGrid.reelcount;j++)
			{
				int tempi = slotGrid.imagecount[j];

				if (j == 0)//第一個輪條 不會出現共用圖
				{


					for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
					{

						int Tempcount;
						int SpriteCountTemp;

						Tempcount = System.Enum.GetNames(typeof(Pool_Images)).Length - SpecialCount;
						SpriteCountTemp = Random.Range(0, Tempcount);
						slotGrid._grids[i]._Grids[j]._GridInt[SpriteCount] = (Pool_Images)SpriteCountTemp;




					}





				}

				else
				{

					for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
					{

						int Tempcount;
						int SpriteCountTemp;

						Tempcount = Sprite_Pool.Length - 1;
						SpriteCountTemp = Random.Range(0, Tempcount);
						slotGrid._grids[i]._Grids[j]._GridInt[SpriteCount] = (Pool_Images)SpriteCountTemp;




					}

				}
				


			}

		}





	}
    #endregion

    #region 測試用（指定盤面獲得Bonus獎）
    /// <summary>
    /// 測試用（直接有Bonus獎）
    /// </summary>
    /// <param name="BtnTrue"></param>
    /// <param name="Generate_ReelScripts"></param>
    public void Add_BonusDate(bool BtnTrue,SlotGrid _SlotGrid)
	{
		int SPecialImgCount = 2;

		if (BtnTrue)
		{

			for (int i = 0; i < _SlotGrid.reelcount; i++)
			{
				int Gridcount = _SlotGrid._girdcount-1;

				int Temp = _SlotGrid.imagecount[i];
				int _One_Sprite_pool_Temp = System.Enum.GetNames(typeof(Pool_Images)).Length - SPecialImgCount;//第一輪用的陣列少掉特殊圖
				
				
				
				if (i == 0)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 1)
						{
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{
							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}



					}

				}
				else if (i == 1)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 2)
						{
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{
							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}



					}




				}
				else if (i == 2)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 3)
						{
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{
							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}



					}




				}

				else
				{


					for (int j = 0; j < Temp; j++)
					{


						int Temp_B;
						Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
						_SlotGrid._grids[Gridcount]._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;



					}


				}


			}

			Bonus_count = 3;


		}







	}
    #endregion

    #region 資料轉成Int儲存
    /// <summary>
    /// 資料轉成Int儲存
    /// </summary>
    /// <param name="_ReelMove"></param>
    public void DateTypeChange(SlotGrid _SlotGrid)
	{
		int Reelcount;
		Reelcount = _SlotGrid.reelcount;//有多少輪條
		int GridCount = _SlotGrid._girdcount - 1;//最後的盤面在List中的值


		for (int i=0;i< Reelcount; i++)
		{
			int tempi =_SlotGrid.imagecount[i];
			for (int j=0;j<tempi;j++)
			{
				//把_SlotGrid的Enum資料轉成Int放進_Date
				_Date[i]._IntCount[j] = (int)_SlotGrid._grids[GridCount]._Grids[i]._GridInt[j];
				

			}

		}

	}

	#endregion



	#region 連線圖片種類與連線數判斷
	/// <summary>
	/// 連線圖片種類與連線數判斷
	/// </summary>
	/// <param name="Sprite_Tipe"></param>
	/// <param name="Win_Sprite"></param>
	/// <param name="Line_Count"></param>
	public void Win_Sprite(Pool_Images _poolimage, int Line_Count, ref int WinMoneys)
	{

		if (_poolimage==Pool_Images.Fish)
		{
			float Win_temp=0;
			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 1;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 3;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 10;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);
		}

		if (_poolimage==Pool_Images.Apple)
		{
			float Win_temp=0;

			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 0.9f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 2.7f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 9f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);

		}


		if (_poolimage == Pool_Images.Beer)
		{
			float Win_temp = 0;

			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 0.8f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 2.4f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 8f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);

		}


		if (_poolimage == Pool_Images.Cheese)
		{
			float Win_temp = 0;

			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 0.7f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 2.1f;
				WinMoneys += (int)Mathf.Round(Win_temp);
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 7f;
				WinMoneys += (int)Mathf.Round(Win_temp);
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);
		}

		if (_poolimage == Pool_Images.Eggs)
		{
			float Win_temp = 0;

			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 0.6f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 1.8f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 6f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);

		}

		if (_poolimage == Pool_Images.Cherry)
		{
			float Win_temp = 0;

			if (Line_Count == 3)
			{
				Win_temp = bet_Coin * 0.5f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 4)
			{
				Win_temp = bet_Coin * 1.5f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}
			else if (Line_Count == 5)
			{
				Win_temp = bet_Coin * 5f;
				WinMoneys += (int)Mathf.Round(Win_temp); ;
			}

			Debug.Log("Win_Sprite.name:" + _poolimage.ToString() + "連線數" + Line_Count);
			Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_Coin, Win_temp));
			Debug.Log("當前贏多少錢" + WinMoneys);

		}





	}

    #endregion



    #region 連線判斷
    /// <summary>
    /// 連線判斷
    /// </summary>
    /// <param name="Date"></param>
    public void Win_Line(GridIntS _Gridints, List<Transform> ShinyReadyShow, ref int Win_Money, Transform _LRStayOutPool, List<GameObject> _LRStayDrawLine)
    {
        //33
        if ((_Gridints._Grids[0]._GridInt[3] == _Gridints._Grids[1]._GridInt[3] || _Gridints._Grids[1]._GridInt[3] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[3] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[3];//最左的初始中獎圖

            //333
            if (_Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite || _PoolImages == _Gridints._Grids[2]._GridInt[3])
            {

                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                   (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))//33333

                {
                    Debug.Log("Win");
                    Debug.Log("33333");
                    int Line_count = 5;
                    Win_Sprite( _PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                else if (_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//3333
                {
                    Debug.Log("Win");
                    Debug.Log("3333");
                    int Line_count = 4;
                    Win_Sprite( _PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                else//333
                {
                    Debug.Log("Win");
                    Debug.Log("333");
                    int Line_count = 3;
                    Win_Sprite( _PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }



                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 3, 3, 3, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow. ListShiny(_GetRoolWinImg, ShinyReadyShow);
                

            }

            //332
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {

                if ((_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))//33211
                {
                    Debug.Log("Win");
                    Debug.Log("33211");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)//3321
                {

                    Debug.Log("Win");
                    Debug.Log("3321");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else//332
                {
                    Debug.Log("Win");
                    Debug.Log("332");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }


                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 3, 2, 1, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);


            }
            //331
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] ==Pool_Images.Universal_Sprite)
            {

                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))//33133
                {

                    Debug.Log("Win");
                    Debug.Log("33133");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//3313
                {
                    Debug.Log("Win");
                    Debug.Log("3313");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//331
                {
                    Debug.Log("Win");
                    Debug.Log("331");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 3, 1, 3, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);
            }


        }
        //32
        if ((_Gridints._Grids[0]._GridInt[3] == _Gridints._Grids[1]._GridInt[2] || _Gridints._Grids[1]._GridInt[2] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[3] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[3];//最左的初始中獎圖
                                                          //321
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)//321
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))//32123
                {
                    Debug.Log("Win");
                    Debug.Log("32123");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else if( _PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//3212
                {

                    Debug.Log("Win");
                    Debug.Log("3212");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else//321
                {
                    Debug.Log("Win");
                    Debug.Log("321");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 2, 1, 2, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //322
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] ==Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))//32223
                {
                    Debug.Log("Win");
                    Debug.Log("32223");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//3222
                {
                    Debug.Log("Win");
                    Debug.Log("3222");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else//322
                {

                    Debug.Log("Win");
                    Debug.Log("322");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 2, 2, 2, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //323
            if (_PoolImages == _Gridints._Grids[2]._GridInt[3] || _Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))//32323
                {
                    Debug.Log("Win");
                    Debug.Log("32323");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//3232
                {
                    Debug.Log("Win");
                    Debug.Log("3232");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else//323
                {
                    Debug.Log("Win");
                    Debug.Log("323");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 3, 2, 3, 2, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[2], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);
            }
        }
        //23
        if ((_Gridints._Grids[0]._GridInt[2] == _Gridints._Grids[1]._GridInt[3] || _Gridints._Grids[1]._GridInt[3] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[2] != Pool_Images.Universal_Sprite)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[2];//最左的初始中獎圖

            //233
            if (_PoolImages == _Gridints._Grids[2]._GridInt[3] || _Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] ==Pool_Images.Universal_Sprite))//23332
                {
                    Debug.Log("Win");
                    Debug.Log("23332");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if ( _PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//2333
                {
                    Debug.Log("Win");
                    Debug.Log("2333");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//233
                {
                    Debug.Log("Win");
                    Debug.Log("233");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 3, 3, 3, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);


            }
            //232
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//23212
                {

                    Debug.Log("Win");
                    Debug.Log("23212");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)//2321
                {

                    Debug.Log("Win");
                    Debug.Log("2321");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else
                {
                    Debug.Log("Win");
                    Debug.Log("232");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 3, 2, 1, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //231
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//23132
                {
                    Debug.Log("Win");
                    Debug.Log("23132");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//2313
                {
                    Debug.Log("Win");
                    Debug.Log("2313");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else
                {
                    Debug.Log("Win");
                    Debug.Log("231");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 3, 1, 3, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }




        }
        //22
        if ((_Gridints._Grids[0]._GridInt[2] == _Gridints._Grids[1]._GridInt[2] || _Gridints._Grids[1]._GridInt[2] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[2] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[2];//最左的初始中獎圖
                                                          //223
            if (_PoolImages == _Gridints._Grids[2]._GridInt[3] || _Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//22322
                {

                    Debug.Log("Win");
                    Debug.Log("22322");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                {

                    Debug.Log("Win");
                    Debug.Log("2232");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else
                {

                    Debug.Log("Win");
                    Debug.Log("223");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 2, 3, 2, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //222
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {

                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//22222
                {
                    Debug.Log("Win");
                    Debug.Log("22222");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//2222
                {
                    Debug.Log("Win");
                    Debug.Log("2222");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//222
                {
                    Debug.Log("Win");
                    Debug.Log("222");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 2, 2, 2, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //221
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//22122
                {
                    Debug.Log("Win");
                    Debug.Log("22122");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//2212
                {

                    Debug.Log("Win");
                    Debug.Log("2212");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//221
                {

                    Debug.Log("Win");
                    Debug.Log("221");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 2, 1, 2, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }

        }
        //21
        if ((_Gridints._Grids[0]._GridInt[2] == _Gridints._Grids[1]._GridInt[1] || _Gridints._Grids[1]._GridInt[1] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[2] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[2];//最左的初始中獎圖
                                                          //212
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//21232
                {

                    Debug.Log("Win");
                    Debug.Log("21232");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//2123
                {
                    Debug.Log("Win");
                    Debug.Log("2123");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//212
                {
                    Debug.Log("Win");
                    Debug.Log("212");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 1, 2, 3, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //211
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[2] || _Gridints._Grids[4]._GridInt[2] == Pool_Images.Universal_Sprite))//21112
                {
                    Debug.Log("Win");
                    Debug.Log("21112");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)//2111
                {
                    Debug.Log("Win");
                    Debug.Log("2111");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//211
                {
                    Debug.Log("Win");
                    Debug.Log("211");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 2, 1, 1, 1, 2 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[1], _GetRoolWinImg, _Ishow.StartPoint[4], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }


        }
        //12
        if ((_Gridints._Grids[0]._GridInt[1] == _Gridints._Grids[1]._GridInt[2] || _Gridints._Grids[1]._GridInt[2] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[1] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[1];//最左的初始中獎圖
                                                          //123
            if (_PoolImages == _Gridints._Grids[2]._GridInt[3] || _Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite)
            {

                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))//12321
                {
                    Debug.Log("Win");
                    Debug.Log("12321");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                {

                    Debug.Log("Win");
                    Debug.Log("1232");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else//123
                {
                    Debug.Log("Win");
                    Debug.Log("123");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 2, 3, 2, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //122
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))//12221
                {
                    Debug.Log("Win");
                    Debug.Log("12221");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                {
                    Debug.Log("Win");
                    Debug.Log("1222");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else
                {

                    Debug.Log("Win");
                    Debug.Log("122");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 2, 2, 2, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //121
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))//12121
                {
                    Debug.Log("Win");
                    Debug.Log("12121");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[2] || _Gridints._Grids[3]._GridInt[2] == Pool_Images.Universal_Sprite)//1212
                {

                    Debug.Log("Win");
                    Debug.Log("1212");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else//121
                {
                    Debug.Log("Win");
                    Debug.Log("121");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 2, 1, 2, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }

        }
        //11
        if ((_Gridints._Grids[0]._GridInt[1] == _Gridints._Grids[1]._GridInt[1] || _Gridints._Grids[1]._GridInt[1] == Pool_Images.Universal_Sprite) && _Gridints._Grids[0]._GridInt[1] != Pool_Images.Bonus)
        {
            Pool_Images _PoolImages = _Gridints._Grids[0]._GridInt[1];//最左的初始中獎圖

            //113
            if (_PoolImages == _Gridints._Grids[2]._GridInt[3] || _Gridints._Grids[2]._GridInt[3] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))
                {
                    Debug.Log("Win");
                    Debug.Log("11311");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)//1131
                {

                    Debug.Log("Win");
                    Debug.Log("1131");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else
                {
                    Debug.Log("Win");
                    Debug.Log("113");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);

                }

                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 1, 3, 1, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }
            //112
            if (_PoolImages == _Gridints._Grids[2]._GridInt[2] || _Gridints._Grids[2]._GridInt[2] ==Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[3] || _Gridints._Grids[4]._GridInt[3] == Pool_Images.Universal_Sprite))
                {
                    Debug.Log("Win");
                    Debug.Log("11233");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[3] || _Gridints._Grids[3]._GridInt[3] == Pool_Images.Universal_Sprite)//1123
                {

                    Debug.Log("Win");
                    Debug.Log("1123");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else
                {
                    Debug.Log("Win");
                    Debug.Log("112");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }


                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 1, 2, 3, 3 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[5], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }

            //111
            if (_PoolImages == _Gridints._Grids[2]._GridInt[1] || _Gridints._Grids[2]._GridInt[1] == Pool_Images.Universal_Sprite)
            {
                if ((_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                    &&
                    (_PoolImages == _Gridints._Grids[4]._GridInt[1] || _Gridints._Grids[4]._GridInt[1] == Pool_Images.Universal_Sprite))//11111
                {
                    Debug.Log("Win");
                    Debug.Log("11111");
                    int Line_count = 5;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }
                else if (_PoolImages == _Gridints._Grids[3]._GridInt[1] || _Gridints._Grids[3]._GridInt[1] == Pool_Images.Universal_Sprite)
                {

                    Debug.Log("Win");
                    Debug.Log("1111");
                    int Line_count = 4;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }

                else//111
                {
                    Debug.Log("Win");
                    Debug.Log("111");
                    int Line_count = 3;
                    Win_Sprite(_PoolImages, Line_count, ref Win_Money);
                    Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money);
                }


                int[] RoolWinNumber;
                RoolWinNumber = new int[] { 1, 1, 1, 1, 1 };
                List<Transform> _GetRoolWinImg;
                _GetRoolWinImg = _Ishow.GetRoolWinImg(_Reelmove, RoolWinNumber);
                _Ishow.ObjectPool(_Ishow.StartPoint[0], _GetRoolWinImg, _Ishow.StartPoint[3], _LRStayOutPool, _LRStayDrawLine);
                _Ishow.ListShiny(_GetRoolWinImg, ShinyReadyShow);

            }

        }

        if (Bonus_count == 3)
        {

            Debug.Log("恭喜中大獎");


        }




    }
    #endregion



    #region 資料的儲存以及讀取
    /// <summary>
    /// 將資料轉成Jason並且儲存
    /// </summary>
    public void DateSave(SlotGrid CommonGrid,SlotGrid BonusGrid)//
    {

        if (Bonus_count == 3)
        {
            int i = BonusGrid._girdcount - 1;
           DateTypeChange(BonusGrid);

        }
        else
        {

            DateTypeChange(CommonGrid);

        }

        _Slot_SeverDate.Bet_Coin =bet_Coin;
        _Slot_SeverDate.Auto_HasRollcount = _AutoSurplus;
        _Slot_SeverDate.Auto_PlayerSet = _AutoCount;
        _Slot_SeverDate.Auto_NotYet = _CycleCount;
        _Slot_SeverDate.Player_Coin = Coin;
        _Slot_SeverDate.BonusCoin = Total_BonusWinCoin;
        _Slot_SeverDate.Win_Coin = WinCoin;
        _Slot_SeverDate.Data = _Date;
        string SaveDateToJason;
        SaveDateToJason = JsonUtility.ToJson(_Slot_SeverDate);
        PlayerPrefs.SetString("遊戲資料", SaveDateToJason);
        PlayerPrefs.Save();
        Debug.Log("資料儲存完成");
        Debug.Log("資料內容 ：" + SaveDateToJason);
        Debug.Log("目前是否有儲存到資料 ：" + PlayerPrefs.HasKey("遊戲資料")); ;

    }

    /// <summary>
    /// 將資料讀取
    /// </summary>
    public void GetDateSave()
    {
        Debug.Log("有無遊戲資料 ：" + PlayerPrefs.HasKey("遊戲資料"));
        if (PlayerPrefs.HasKey("遊戲資料"))
        {
            IUICM.Options.SetActive(true);

        }
       
    }
    #endregion



}

#region 資料儲存用的Class
/// <summary>
/// 資料儲存用的Class
/// </summary>
[System.Serializable]
public class Slot_SeveDate
{
	
	public int Win_Coin;
	public int BonusCoin;
	public int Bet_Coin;
	public int Player_Coin;
	public int Auto_HasRollcount;
	public int Auto_NotYet;
	public int Auto_PlayerSet;
	public List<intCount> Data;
	





}
#endregion


#region 圖片種類的Enum
public enum Pool_Images
{

	Fish,
	Apple,
	Beer,
	Cheese,
	Eggs,
	Cherry,
	Universal_Sprite,
	Bonus,

}
#endregion


#region 盤面儲存用可以在畫面上看到的 多維陣列
[System.Serializable]
public class intCount
{

	public List<int> _IntCount = new List<int>();

}
#endregion

#region 顯視盤面用的一些Class（多維陣列）
[System.Serializable]
public class  GridInt//輪條
{

	public List<Pool_Images> _GridInt ;

  
}

[System.Serializable]
public class GridIntS//單一盤面
{

	public List<GridInt> _Grids;


}
#endregion

#region  盤面的Class 模板
/// <summary>
/// 盤面
/// </summary>
[System.Serializable]
public class SlotGrid
{
	public delegate void GridContentFun<T>(T _Enum);

	int Girdcount;//盤面數
	int Reelcount;//有幾條
	List<int> ImageCount;//每條幾張圖
	[SerializeField]
	List<GridIntS> Grid;//盤面內容
	GridContentFun<SlotGrid> GridMethod;
	

	public SlotGrid(int _Girdocunt,int _ReelCount,List<int> _ImageCount,GridContentFun<SlotGrid> Enm)
	{
		this.Girdcount = _Girdocunt;
		this.Reelcount = _ReelCount;
		//ImageCount = new List<int>();
		this.ImageCount = _ImageCount;
		Grid = new List<GridIntS>();
		this.GridMethod = Enm;
		Reel_Initialization();

	}

	

	public int _girdcount
	{
		get { return Girdcount; }
		//set { Girdcount = value; }
	}
	public int reelcount
	{

		get { return Reelcount; }

	}
	public List<int> imagecount
	{

		get { return ImageCount; }

	}
	public List<GridIntS> _grids
	{
		get { return Grid; }

	}

	public GridContentFun<SlotGrid> _GridMethod
	{

		get { return GridMethod; }

	}



	/// <summary>
	/// 生成資料空間
	/// </summary>
	void Reel_Initialization()
	{
		for (int k=0;k<Girdcount;k++)
		{
			Grid.Add(new GridIntS());
			Grid[k]._Grids = new List<GridInt>();
			for (int i = 0; i < Reelcount; i++)
			{
				Grid[k]._Grids.Add(new GridInt());
				Grid[k]._Grids[i]._GridInt = new List<Pool_Images>();

				
				for (int j = 0; j < ImageCount[i]; j++)
				{
					Grid[k]._Grids[i]._GridInt.Add(Pool_Images.Fish);

				}

				//Debug.Log(string.Format("盤面資料生成,輪條數：{0},各輪條內圖片數量：{1}", Grid[k]._Grids.Count, ImageCount));
			}

		}

	}



  



}
#endregion