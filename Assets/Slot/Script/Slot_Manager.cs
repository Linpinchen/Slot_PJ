using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


//public class Slot_Manager : MonoBehaviour {
//	public Slot_data _SlotDate;//玩家金錢及盤面資料腳本
//	public float RollSpeed;//輪條移動速度
//	public Button start_btn;//開始遊戲按鈕
//	public Button Bet_Button;//下注按鈕
//	public Button Auto_Button;//自動循環按鈕
//	public GameObject Bet_Menu;//下注選單
//	public GameObject Auto_Menu;//自動循環選單
//	public Button Bet_Plus;//加注按鈕
//	public Button Bet_Reduce;//減注按鈕
//	public Button Bet_MaxCoin;//最大加注按鈕
//	public Button Auto_Clear_Button;//清除自動循環數的按鈕
//	public Button Auto_pause_Button;//停止自動循環數的按鈕
//	public Button Auto_Plus;//自動循環增加按鈕
//	public Button Auto_Reduce;//自動循環減少
//	public bool Start_Slot;
//	public bool Bet_Button_Bool;
//	public bool Auto_Button_bool;
//	public int Loopcount;//循環次數
//	public Reel_Move[] _Reel_Moves;
//	public IEnumerator CoRool;
//	public IEnumerator _CoAll_Slot_Roll;
//	public IEnumerator _Show;
//	public IEnumerator _Slot_timeOut;
//	public int Slot_mantissa;//輪條陣列內最後的數
//	public Text Player_coin_Text;//玩家的總金額ＴＥＸＴ
//	public Text BET_text;
//	public Text Bet_text_out;
//	public Text Auto_text;
//	public int Auto_Count;//自動循環次數
//	public int Cycle_Count;//已跑的循環次數
//	public int Auto_Temp_Count;//記錄當前跑幾次(剩餘的次數)
//	public int Win_Money_Temp;//記錄贏多少錢
//	public int Win_BonusMoney_Temp;//紀錄Bonus贏多少錢
//	public List<int> Win_BonusMoney_Date;//存放各個Bonus盤面贏的Win_BonusMoney_Temp
//	public int Total_BonusWinCoin;
//	public bool St_Roll;
//	public Button_EventTrigger _ButtonPlus_EventTrigger;
//	public Button_EventTrigger _ButtonReduce_EventTrigger;
//	public Queue<GameObject> DrawLinePool;//物件池用
//	public List<GameObject> PrepareDrawLine;//放準備表演的LineRender陣列
//	public List<Bonus_PrepareDrawLinePool> _Bonus_PrepareDrawLinePool;//放各個Bonus盤面的DrawLine表演用的預制物
//	public GameObject GBDrawLine;//要生成的物件
//	public Transform InLineRenderPool;//生成後要放入的父物件
//	public Transform LineRenderOutPool;//父物件 用來放入等待要表演的預制物
//	public Transform BonusLineRenderOutPool;//Bonus用,用來放Bonus等待要表演的預制物
//	public GameObject BonusLineRenderChildPool;
//	public Transform[] StartEndPoint;
//	public bool StRecover;//判斷 預制物是否已回收
//	public List<bool> ShowOK;//用來確認每個LineRender畫完線了
//	public List<Bonus_ShowOk> _Bonus_ShowOK;//用來確認執行Bonus時Bonus每個盤面的每個LineRender畫完線了
//	public bool StCoShow;//確保CoShow只有一個
//	public List<Transform> UiShow;
//	public List<BonusShinyDate> _BonusShinyDate;
//	public Texture2D[] MIcon;
//	public Animator Amr_WinShow;
//	public bool ShowOver;//判斷表演是否都結束
//	public Image[] WinCoins;
//	public Sprite[] Numbers;
//	public int TempWinCoin;//金錢表演用的暫存Int
//	public IEnumerator _CoShowWinCoin;
//	public bool StCoShowWinCoin;
//	public bool CoinShowOK;
//	public bool BonusShow;//有沒有BonusShow
//	public Animator BonusAnimator;
//	public Animator BonusEndShow;
//	public AnimatorStateInfo BonusStateInfo;//動畫當前狀態
//	public AnimatorStateInfo WinShowStateInfo;
//	public int BonusCount;
//	public int FreeGameCount;
//	public bool BonusRolling;
//	public bool Slot_BonusGoRoll;
//	public Button BonusDateBtn;
//	public bool StAddBonusDate;
//	public bool StBonusShowOk;
//	public bool UseBonus;
//	public bool FreeCountPlus;
//	public VideoPlayer EndShowPlayer;
//	public VideoClip EndShowVideoClip;
//	public bool StEndShow;
//	public Image BONUSWIN;
//	public Sprite[] Sprites;
//	public Button ButtonINFO;
//	public Image ImagaInfo;
//	public Button InfoOutButton;
//	public Button ButtonLeft;
//	public Button ButtonRight;
//	public Image Img_Introduction;
//	public GameObject Options;
//	public Button Options_Yes;
//	public Button Options_No;
//	public RawImage VideoImage;
//	public SlotGrid CommonGrid ;
//	public SlotGrid BonusGrid ;
	
//	// Use this for initialization
	
//    void Start ()
//	{


		//SlotGridCreat();



		//BonusDateBtn.onClick.AddListener(delegate ()
		//{

		//	if (StAddBonusDate)
		//	{
		//		StAddBonusDate = false;


		//	}
		//	else if(!StAddBonusDate)
		//	{

		//		StAddBonusDate = true;

		//	}

		//});



  //      Slot_Initialization();


		//GetDateSave();


		//Button_Control();


		//ObjectPoolInitialization();//物件池生成




//		start_btn.onClick.AddListener(delegate()
//		{
		
//			if (Start_Slot==false&&_SlotDate.Bet_Coin!=0&&_SlotDate.Coin>=100)
//			{
//				int Autoi;
//				Autoi = int.Parse(Auto_text.text);
//				Auto_Temp_Count = Autoi;
				
//				St_Roll = true;

//				_SlotDate.Coin -= _SlotDate.Bet_Coin;
//				Player_coin_Text.text = "Money:" + _SlotDate.Coin;

//				if (Auto_Temp_Count > 1 )
//				{
//					Auto_Count = Auto_Temp_Count;
					

//				}
				
//				_SlotDate.Bonus_count = 0;
//				if (StAddBonusDate)
//				{
//					_SlotDate.Add_BonusDate(StAddBonusDate, CommonGrid);

//				}
//				else 
//				{
//					//_SlotDate.Generate_Date_Sprite(CommonGrid);//生成盤面
//					CommonGrid._GridMethod(CommonGrid);//生成盤面
//				}
			
//				Win_Line(_SlotDate._Reel_Sprite_Date,_SlotDate.Sprite_Pool[6],UiShow,ref Win_Money_Temp,LineRenderOutPool,PrepareDrawLine, _SlotDate.Sprite_Pool[7]);//兌獎


//				if (_SlotDate.Bonus_count==3)//如果有Bonus獎
//				{
//					BonusShow = true;
					
//					//_SlotDate.GenerateBonusDate(BonusCount, _Reel_Moves);//生成所有Bonus盤面

//					for (int i=0;i<BonusGrid._girdcount;i++)
//					{

//						Win_Line(_SlotDate._AllBonusSpriteDate[i]._BonusSpriteDate,_SlotDate.Sprite_Pool[6],_BonusShinyDate[i].BonusShinyPoint,ref Win_BonusMoney_Temp
//								 ,BonusLineRenderOutPool.transform.GetChild(i),_Bonus_PrepareDrawLinePool[i].Bonus_PrepareDrawLine, _SlotDate.Sprite_Pool[7]);//兌獎

//						Win_BonusMoney_Date[i]=Win_BonusMoney_Temp;
//						Win_BonusMoney_Temp = 0;

//					}

//					for (int i=0;i<Win_BonusMoney_Date.Count;i++)//將各個盤面的Bonus獎的中獎金額總和起來
//					{

//						Total_BonusWinCoin += Win_BonusMoney_Date[i];



//					}

//				}
//i


	//			DateSave();

	//			_CoAll_Slot_Roll = CoAll_Slot_Roll();
	//			StartCoroutine(_CoAll_Slot_Roll);

				

	//			Start_Slot = true;
	//			Debug.Log("是否開始滾動：" + Start_Slot);

	//		}

			


	//	});
		

	//}
   

	// Update is called once per frame
	
//    void Update ()
//	{

//        if (Input.GetMouseButtonDown(0))
//        {
            
//            Cursor.SetCursor(MIcon[1], new Vector2(0, 5), CursorMode.Auto);
//            //Debug.Log("Mouse : Down");
//        }
//        if (Input.GetMouseButtonUp(0))
//        {
          
//            Cursor.SetCursor(MIcon[0], new Vector2(0, 5), CursorMode.Auto);
//            //Debug.Log("Mouse : Up");
//        }


//		if (BonusShow && ShowOver && Slot_BonusGoRoll)//這裡要多一個判斷  現在問題出在 shower 還沒到表演前 一直都會是True 所以導致 下面一般輪的滾動 都沒執行到
//		{


//			for (int i = 0; i < _Reel_Moves.Length; i++)//各輪條移動
//			{
//				//Debug.Log("----------------------- : BONUS 各輪條移動 : --------------------------");
//				if (i == 0)
//				{

//					_Reel_Moves[i].Reel_Move_(RollSpeed, _SlotDate.BonusOneSpritePool, Loopcount, _SlotDate._AllBonusSpriteDate[FreeGameCount]._BonusSpriteDate, i);

//				}
//				else
//				{
//					_Reel_Moves[i].Reel_Move_(RollSpeed, _SlotDate.BonusSpritePool, Loopcount, _SlotDate._AllBonusSpriteDate[FreeGameCount]._BonusSpriteDate, i);

//				}



//			}




//		}
//		else
//		{

//			for (int i = 0; i < _Reel_Moves.Length; i++)//各輪條移動
//			{

//				if (i == 0)
//				{

//					_Reel_Moves[i].Reel_Move_(RollSpeed, _SlotDate._One_Sprite_pool, Loopcount, _SlotDate._Reel_Sprite_Date, i);

//				}
//				else
//				{
//					_Reel_Moves[i].Reel_Move_(RollSpeed, _SlotDate.Sprite_Pool, Loopcount, _SlotDate._Reel_Sprite_Date, i);

//				}



//			}




//		}


//		if (Start_Slot == true)
//		{
//			_Slot_timeOut = Co_Slot_timeOut();
//			StartCoroutine(_Slot_timeOut);

//		}


//		if (!Start_Slot)
//		{

//			Button_PressAndHold();

//			Button_Reduce_Press();

//		}
		


//		if (UseBonus)
//		{
//			RecoverComply(BonusLineRenderOutPool.GetChild(FreeGameCount), _Bonus_PrepareDrawLinePool[FreeGameCount].Bonus_PrepareDrawLine, _Bonus_ShowOK[FreeGameCount]._ShowOk);

//		}
		

		
	
		
//		RecoverComply(LineRenderOutPool, PrepareDrawLine, ShowOK);




//		if (StEndShow)
//		{

//            BonusEndShow.SetBool("StBonusEndShow", true);

//            AnimatorStateInfo BonusEndStateInfo;
//            BonusEndStateInfo = BonusEndShow.GetCurrentAnimatorStateInfo(0);
//            if (BonusEndStateInfo.normalizedTime >= 1.0f && BonusEndStateInfo.IsName("BonusEndShow"))
//            {
//				BONUSWIN.gameObject.SetActive(true);
//				StRecover = true;


//				if (VideoImage.gameObject.activeInHierarchy)//判斷播影片的Camera是否開啟
//				{

//					//Debug.Log("VideoImage.gameObject.active :" + VideoImage.gameObject.active);

//					if (EndShowPlayer.frame != 0 && EndShowPlayer.frameCount != 0)
//					{

//						if ((ulong)EndShowPlayer.frame >= EndShowPlayer.frameCount)
//						{

//							EndShowPlayer.Pause();//暫停影片
//							Debug.Log("暫停影片");
//							BonusEndShow.SetBool("StBonusEndShow", false);
//							BONUSWIN.gameObject.SetActive(false);
//							VideoImage.GetComponent<RawImage>().enabled = false;
//							VideoImage.gameObject.SetActive(false);//關掉RawImage
//							StEndShow = false;
//							TempWinCoin = 0;
//							Total_BonusWinCoin = 0;
//							StRecover = false;
//						}




//					}

//				}
				


//			}



//        }


	

//        if (StCoShowWinCoin && StRecover)
//        {
//			if (StEndShow)
//			{

//				StCoShowWinCoin = false;
//				_CoShowWinCoin = End_TotalBonusWinCoin(Total_BonusWinCoin);
//				StartCoroutine(_CoShowWinCoin);

//			}
//			else if (UseBonus)
//			{
//				StCoShowWinCoin = false;
//				_CoShowWinCoin = CoShowWinCoin(Win_BonusMoney_Date[FreeGameCount]);
//				StartCoroutine(_CoShowWinCoin);

//			}
//			else
//			{
//				StCoShowWinCoin = false;
//				_CoShowWinCoin = CoShowWinCoin(Win_Money_Temp);
//				StartCoroutine(_CoShowWinCoin);

//			}
			
		
//		}


//	}

//    #region 資料初始化
//    /// <summary>
//    /// 資料初始化
//    /// </summary>
    
//    public void Slot_Initialization ()
//	{

//		for (int i=0;i<_Reel_Moves.Length;i++)
//		{

//			_Reel_Moves[i].Init(Loopcount,_SlotDate.Sprite_Pool,RollSpeed,i);




//		}



//		StEndShow = false;
//		EndShowVideoClip = EndShowPlayer.GetComponent<VideoPlayer>().clip;

//		FreeCountPlus = false;
//		UseBonus = false;
//		StBonusShowOk = false;
//		StAddBonusDate = false;
//		Slot_BonusGoRoll = false;
//		BonusRolling = false;
//		BonusCount = 3;
//		Win_BonusMoney_Date = new List<int>();
		
//		for (int i=0;i<BonusCount;i++)
//		{
//			Win_BonusMoney_Date.Add(0);
//			_BonusShinyDate.Add(new BonusShinyDate());

//			_BonusShinyDate[i].BonusShinyPoint = new List<Transform>();

//			_Bonus_PrepareDrawLinePool.Add(new Bonus_PrepareDrawLinePool());

//			_Bonus_PrepareDrawLinePool[i].Bonus_PrepareDrawLine = new List<GameObject>();

//			_Bonus_ShowOK.Add(new Bonus_ShowOk());
//			_Bonus_ShowOK[i]._ShowOk = new List<bool>();

			
//		}

		
//		FreeGameCount = 0;
//		ShowOver = true;
//		BonusShow = false;
//		CoinShowOK = true;
//		UiShow = new List<Transform>();
//		StCoShow = true;
//		ShowOK = new List<bool>();
//		//StRecover = true;
//		StRecover = false;
//		StCoShowWinCoin = false;
//		St_Roll = false;
//		_Slot_timeOut = Co_Slot_timeOut();
//		PrepareDrawLine = new List<GameObject>();
//		Cycle_Count = 1;

//		Bet_Button_Bool = false;
//		Auto_Button_bool = false;
//		_SlotDate.Coin = 1099;
//		Debug.Log("Player_coin" + _SlotDate.Coin);

//		Player_coin_Text.text = "Money :"+_SlotDate.Coin.ToString();

//		Auto_Count = 1;

//		Slot_mantissa = _Reel_Moves.Length - 1;//紀錄陣列的最大編號
		
//		_SlotDate.Reel_List_Initialization(_Reel_Moves);
//		_SlotDate.BonusDate_Initialization(BonusCount, _Reel_Moves);


//		if (!PlayerPrefs.HasKey("遊戲資料"))
//		{
//			Initialization_Slot_Sprite(_SlotDate._Reel_Sprite_Date);

//		}
		
		

//		Start_Slot = false;
//		Debug.Log("是否開始滾動："+Start_Slot);
//	}
//	#endregion

//	#region 盤面物件創造
//	/// <summary>
//	/// 盤面物件創造
//	/// </summary>
//	public void SlotGridCreat()
//	{


//		int Reelcount;
//		Reelcount = _Reel_Moves.Length;


//		List<int> ii = new List<int>();

//		for (int i = 0; i < _Reel_Moves.Length; i++)
//		{
//			int Chcount = _Reel_Moves[i].transform.childCount;
//			ii.Add(Chcount);
//			Debug.Log(ii);
//		}

//		CommonGrid = new SlotGrid(1, Reelcount, ii,_SlotDate.Generate_Date_Sprite);

//		CommonGrid._GridMethod(CommonGrid);

//		BonusGrid = new SlotGrid(BonusCount,Reelcount,ii,_SlotDate.GenerateBonusDate);

//		BonusGrid._GridMethod(BonusGrid);

//	}
//    #endregion

//    #region 初始化時 將盤面的圖灌好
//    /// <summary>
//    /// 初始化時 將盤面的圖灌成跟＿Reel_Sprite_Date資料上的一樣
//    /// </summary>
//    /// <param name="Chang_Sprite"></param>
// //   public void Initialization_Slot_Sprite(List<Reel_Sprite_Date> Chang_Sprite)
//	//{
//	//	//Debug.Log("依＿Reel_Sprite_Date資料設置圖片");
//	//	int Tempi;
//	//	Tempi = _SlotDate.Sprite_Pool.Length - 2;
		
//	//	for (int i=0;i<_Reel_Moves.Length;i++)
//	//	{
			
//	//		for (int j=0;j<_Reel_Moves[i].transform.childCount;j++)
//	//		{
//	//			int Changei;
//	//			Changei = Random.Range(0, Tempi);
//	//			_Reel_Moves[i].transform.GetChild(j).GetComponent<Image>().sprite = _SlotDate.Sprite_Pool[Changei];
//	//			//Debug.Log(string.Format("第{0}個輪條的第{1}張圖片,圖片名稱{2}",i,j,_Reel_Moves[i].transform.GetChild(j).GetComponent<Image>().sprite.name));

//	//		}

			

//	//	}


//	//}
//	#endregion


//	# region 放各個Bonus盤面的Shiny表演圖用的
//	/// <summary>
//	/// 放各個Bonus盤面的Shiny表演圖用的
//	/// </summary>
//	[System.Serializable]
//	public class BonusShinyDate
//	{

//		public List<Transform> BonusShinyPoint;

//	}
//	#endregion

//	#region  放各個Bonus盤面的DrawLine表演用的預制物
//	/// <summary>
//	/// 放各個Bonus盤面的DrawLine表演用的預制物
//	/// </summary>
//	[System.Serializable]
//	public class Bonus_PrepareDrawLinePool
//	{

//		public List<GameObject> Bonus_PrepareDrawLine;

//	}
//	#endregion

//	#region 確認各個Bonus盤面的每條DrawLine都跑完
//	/// <summary>
//	/// 確認各個Bonus盤面的每條DrawLine都跑完
//	/// </summary>
//	[System.Serializable]
//	public class Bonus_ShowOk
//	{

//		public List<bool> _ShowOk;

//	}
//    #endregion

//    #region 讓各輪條開始轉動的開關
//    /// <summary>
//    /// 讓各輪條開始轉動的開關
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator CoAll_Slot_Roll()
//	{
//		if (St_Roll)
//		{

//			for (int i = 0; i < _Reel_Moves.Length; i++)
//			{
//				_Reel_Moves[i].strool = true;
//				Debug.Log(string.Format("輪條編號：{0},是否開啟{1}", i, _Reel_Moves[i].b));
//				yield return new WaitForSeconds(0.5f);


//			}
//			St_Roll = false;
//		}
		


//	}
//	#endregion

//	#region 按鈕控制
//	/// <summary>
//	/// 按鈕控制
//	/// </summary>
//	public void Button_Control()
//	{
//		int i = 0;

//		//Bet小視窗開啟
//		Bet_Button.onClick.AddListener(delegate ()
//		{
//			if (Bet_Button_Bool == false)
//			{
//				Bet_Button_Bool = true;
//				if (Bet_Button_Bool)
//				{

//					Bet_Menu.SetActive(true);
//					if (Auto_Button_bool)
//					{
//						Auto_Menu.SetActive(false);
//						Auto_Button_bool = false;
//					}
//				}


//			}
//			else
//			{

//				Bet_Button_Bool = false;
//				Bet_Menu.SetActive(false);

//			}





//		});
//		//加注
//		Bet_Plus.onClick.AddListener(delegate ()
//		{
//			if (!Start_Slot)
//			{

//				Debug.Log("_SlotDate.Coin:" + _SlotDate.Coin);
//				int tx;
//				int coin_temp;
//				tx = _SlotDate.Coin % 100;//總金額除100的餘數
//				coin_temp = _SlotDate.Coin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
//				if (_SlotDate.Bet_Coin < coin_temp)
//				{
//					if (_ButtonPlus_EventTrigger.Down_Time < 1)//在不是長按的情況下
//					{

//						_SlotDate.Bet_Coin += 100;//玩家下注的錢加100


//						BET_text.text = _SlotDate.Bet_Coin.ToString();
//						Bet_text_out.text = BET_text.text;


//					}


//				}



//			}







//		});
//		//減少下注
//		Bet_Reduce.onClick.AddListener(delegate
//		{
//			if (!Start_Slot)
//			{

//				if (_SlotDate.Bet_Coin >= 100)
//				{
//					if (_ButtonReduce_EventTrigger.Down_Time < 1)//在不是長按的情況下
//					{

//						_SlotDate.Bet_Coin -= 100;
//						BET_text.text = _SlotDate.Bet_Coin.ToString();
//						Bet_text_out.text = BET_text.text;

//					}

//				}


//			}

			


//		});
//		//最大下注
//		Bet_MaxCoin.onClick.AddListener(delegate ()
//		{
//			if (!Start_Slot)
//			{

//				int tx;
//				int coin_temp;
//				tx = _SlotDate.Coin % 100;//總金額除100的餘數
//				coin_temp = _SlotDate.Coin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
//				_SlotDate.Bet_Coin = coin_temp;
//				BET_text.text = _SlotDate.Bet_Coin.ToString();
//				Bet_text_out.text = BET_text.text;

//			}
			
//		});


//		//開啟Auto小視窗
//		Auto_Button.onClick.AddListener(delegate ()
//		{

//			if (Auto_Button_bool == false)
//			{
//				Auto_Button_bool = true;
//				if (Auto_Button_bool)
//				{
//					Auto_Menu.SetActive(true);
//					if (Bet_Button_Bool)
//					{
//						Bet_Menu.SetActive(false);
//						Bet_Button_Bool = false;
//					}

//				}


//			}
//			else
//			{
//				Auto_Button_bool = false;

//				Auto_Menu.SetActive(false);
//			}


//		});
//		Auto_Plus.onClick.AddListener(delegate()
//		{
//			if (Auto_Count<9999)
//			{

//				Auto_Count++;
//				Auto_Temp_Count = Auto_Count;
//				Auto_text.text =Auto_Temp_Count.ToString();
//			}
			


//		});

//		Auto_Reduce.onClick.AddListener(delegate()
//		{
//			if (Auto_Count>0)
//			{
//				Auto_Count--;
//				Auto_Temp_Count = Auto_Count;
//				Auto_text.text = Auto_Temp_Count.ToString();
//			}



//		});

//		Auto_Clear_Button.onClick.AddListener(delegate()
//		{

//			if (Auto_Count>1)
//			{
//				Auto_Count = 1;
//				Auto_Temp_Count = 1;
//				Auto_text.text = Auto_Temp_Count.ToString();

//			}


//		});

//		Auto_pause_Button.onClick.AddListener(delegate()
//		{
//			if (Start_Slot)
//			{

//				Auto_Count = 1;


//			}


//		});

//		///開啟INFO視窗
//		ButtonINFO.onClick.AddListener(delegate()
//		{

//			if (!Start_Slot)
//			{

//				ImagaInfo.gameObject.SetActive(true);
//				Img_Introduction.sprite = Sprites[i];
//			}



//		});
//		InfoOutButton.onClick.AddListener(delegate()
//		{



//			ImagaInfo.gameObject.SetActive(false);

//		});
//		ButtonRight.onClick.AddListener(delegate
//		{
//			int SPLength;
//			SPLength = Sprites.Length - 1;
//			if (i<SPLength)
//			{
//				//Debug.Log(i);
//				i += 1;
//				Img_Introduction.sprite = Sprites[i];


//			}




//		});
//		ButtonLeft.onClick.AddListener(delegate
//		{

//			if (i > 0)
//			{
//				//Debug.Log(i);
//				i -= 1;
//				Img_Introduction.sprite = Sprites[i];


//			}

//		});

//	}
//	#endregion

//	#region 按鈕長按(+)
//	/// <summary>
//	///按鈕長按 
//	/// </summary>
//	public void Button_PressAndHold()
//	{

		
//		if (_ButtonPlus_EventTrigger.b)
//		{
//			int tx;
//			int coin_temp;
//			tx = _SlotDate.Coin % 100;//總金額除100的餘數
//			coin_temp = _SlotDate.Coin - tx;//總金額減掉除100的餘數 就是以100為單位最大可以下注的金額
//			if (_SlotDate.Bet_Coin < coin_temp)
//			{
				
//				_SlotDate.Bet_Coin = _SlotDate.Bet_Coin + (_ButtonPlus_EventTrigger.Return_Valu * 100);
//				_ButtonPlus_EventTrigger.b = false;
//				Debug.Log(_SlotDate.Bet_Coin);
//				BET_text.text = _SlotDate.Bet_Coin.ToString();
//				Bet_text_out.text = BET_text.text;
//			}
//		}




//	}
//	#endregion

//	#region 按鈕長按（-）
//	/// <summary>
//	/// 按鈕長按（-）
//	/// </summary>
//	public void Button_Reduce_Press()
//	{


//		if (_ButtonReduce_EventTrigger.b)
//		{
			
//			if (_SlotDate.Bet_Coin >= 100)
//			{
				
//				_SlotDate.Bet_Coin = _SlotDate.Bet_Coin - (_ButtonReduce_EventTrigger.Return_Valu * 100);
//				_ButtonReduce_EventTrigger.b = false;
//				Debug.Log(_SlotDate.Bet_Coin);
//				BET_text.text = _SlotDate.Bet_Coin.ToString();
//				Bet_text_out.text = BET_text.text;
//			}
//		}





//	}
//    #endregion

//    #region 連線圖片種類與連線數判斷
//    /// <summary>
//    /// 連線圖片種類與連線數判斷
//    /// </summary>
//    /// <param name="Sprite_Tipe"></param>
//    /// <param name="Win_Sprite"></param>
//    /// <param name="Line_Count"></param>
//    public void Win_Sprite(Sprite[] Sprite_Tipe, Sprite Win_Sprite,int Line_Count,int bet_coin,ref int WinMoneys)
//	{
		
//			if (Win_Sprite == Sprite_Tipe[0])
//			{

//				if (Line_Count == 3)
//				{
//					int Win_temp;
//					Win_temp = bet_coin * 1;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}
//				else if (Line_Count == 4)
//				{
//					int Win_temp;
//					Win_temp = bet_coin * 3;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 10;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//			}

//			if (Win_Sprite == Sprite_Tipe[1])
//			{

//				if (Line_Count == 3)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 0.9f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 4)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 2.7f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 9f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}

//			}


//			if (Win_Sprite == Sprite_Tipe[2])
//			{

//				if (Line_Count == 3)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 0.8f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 4)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 2.4f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 8f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}

//			}


//			if (Win_Sprite == Sprite_Tipe[3])
//			{

//				if (Line_Count == 3)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 0.7f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 4)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 2.1f;
//					WinMoneys += (int)Mathf.Round(Win_temp);
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 7f;
//					WinMoneys += (int)Mathf.Round(Win_temp);
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}

//			}

//			if (Win_Sprite == Sprite_Tipe[4])
//			{

//				if (Line_Count == 3)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 0.6f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 4)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 1.8f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 6f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}

//			}

//			if (Win_Sprite == Sprite_Tipe[5])
//			{

//				if (Line_Count == 3)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 0.5f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}
//				else if (Line_Count == 4)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 1.5f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);

//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);

//				}
//				else if (Line_Count == 5)
//				{
//					float Win_temp;
//					Win_temp = bet_coin * 5f;
//					WinMoneys += (int)Mathf.Round(Win_temp); ;
//					Debug.Log("Win_Sprite.name:" + Win_Sprite.name + "連線數" + Line_Count);
//					Debug.Log(string.Format("玩家下注的錢:{0},玩家贏的錢：{1}", bet_coin, Win_temp));
//					Debug.Log("當前贏多少錢" + WinMoneys);
//				}

//			}

		

		

//	}

//    #endregion

//    #region 連線判斷
//	/// <summary>
//    /// 連線判斷
//    /// </summary>
//    /// <param name="Date"></param>
//    public void Win_Line(List<Reel_Sprite_Date> Date,Sprite Universal_Sprite,List<Transform> ReadyShow,ref int Win_Money,Transform _StayOutPool,List<GameObject>_StayDrawLine, Sprite BonusSprite)
//	{
//		//33
//		if ((Date[0].Sever_Sprites[3]== Date[1].Sever_Sprites[3]||Date[1].Sever_Sprites[3]==Universal_Sprite) && Date[0].Sever_Sprites[3]!=BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[3];//最左的初始中獎圖

//			//333
//			if ( Date[2].Sever_Sprites[3]==Universal_Sprite || Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[3])
//			{
				
//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3]==Universal_Sprite )&& (Date[0].Sever_Sprites[3]==Date[4].Sever_Sprites[3]||Date[4].Sever_Sprites[3]==Universal_Sprite) )//33333

//				{
//					Debug.Log("Win");
//					Debug.Log("33333");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count,_SlotDate.Bet_Coin,ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ："+ Win_Money_Temp);
//				}
				
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//3333
//				{
//					Debug.Log("Win");
//					Debug.Log("3333");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				else//333
//				{
//					Debug.Log("Win");
//					Debug.Log("333");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}



//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] {3,3,3,3,3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date,RoolWinNumber);
//				ObjectPool(StartEndPoint[2],_GetRoolWinImg,StartEndPoint[5],_StayOutPool,_StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);
				

//			}

//			//332
//			if (Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[2]||Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{

//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite) &&  (Date[0].Sever_Sprites[3]==Date[4].Sever_Sprites[1]||Date[4].Sever_Sprites[1]==Universal_Sprite))//33211
//				{
//					Debug.Log("Win");
//					Debug.Log("33211");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite)//3321
//				{

//					Debug.Log("Win");
//					Debug.Log("3321");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else//332
//				{
//					Debug.Log("Win");
//					Debug.Log("332");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}


//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 3, 3, 2, 1, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[2], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);


//			}
//			//331
//			if (Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[1]||Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{

//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[3]||Date[3].Sever_Sprites[3]==Universal_Sprite) && (Date[0].Sever_Sprites[3] == Date[4].Sever_Sprites[3]||Date[4].Sever_Sprites[3]==Universal_Sprite))//33133
//				{

//					Debug.Log("Win");
//					Debug.Log("33133");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//3313
//				{
//					Debug.Log("Win");
//					Debug.Log("3313");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//331
//				{
//					Debug.Log("Win");
//					Debug.Log("331");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 3, 3, 1, 3, 3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[2], _GetRoolWinImg, StartEndPoint[5], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);
//			}


//		}
//		//32
//		if ((Date[0].Sever_Sprites[3] == Date[1].Sever_Sprites[2]||Date[1].Sever_Sprites[2]==Universal_Sprite) && Date[0].Sever_Sprites[3] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[3];//最左的初始中獎圖
//			//321
//			if (Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[1]||Date[2].Sever_Sprites[1]==Universal_Sprite)//321
//			{
//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2]||Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[3] == Date[4].Sever_Sprites[3]||Date[4].Sever_Sprites[3]==Universal_Sprite))//32123
//				{
//					Debug.Log("Win");
//					Debug.Log("32123");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//3212
//				{

//					Debug.Log("Win");
//					Debug.Log("3212");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else//321
//				{
//					Debug.Log("Win");
//					Debug.Log("321");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 3, 2, 1, 2, 3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[2], _GetRoolWinImg, StartEndPoint[5], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//322
//			if (Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[2]||Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2]||Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[3] == Date[4].Sever_Sprites[3]||Date[4].Sever_Sprites[3]==Universal_Sprite))//32223
//				{
//					Debug.Log("Win");
//					Debug.Log("32223");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//3222
//				{
//					Debug.Log("Win");
//					Debug.Log("3222");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else//322
//				{

//					Debug.Log("Win");
//					Debug.Log("322");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 3, 2, 2, 2, 3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[2], _GetRoolWinImg, StartEndPoint[5], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//323
//			if (Date[0].Sever_Sprites[3] == Date[2].Sever_Sprites[3] || Date[2].Sever_Sprites[3]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2]||Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[3] == Date[4].Sever_Sprites[3]||Date[4].Sever_Sprites[3]==Universal_Sprite))//32323
//				{
//					Debug.Log("Win");
//					Debug.Log("32323");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[3] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//3232
//				{
//					Debug.Log("Win");
//					Debug.Log("3232");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else//323
//				{
//					Debug.Log("Win");
//					Debug.Log("323");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 3, 2, 3, 2, 3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[2], _GetRoolWinImg, StartEndPoint[5], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);
//			}
//		}
//		//23
//		if ((Date[0].Sever_Sprites[2] == Date[1].Sever_Sprites[3] || Date[1].Sever_Sprites[3]==Universal_Sprite) && Date[0].Sever_Sprites[2] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[2];//最左的初始中獎圖

//			//233
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[3] || Date[2].Sever_Sprites[3]==Universal_Sprite)
//            {
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//23332
//				{
//					Debug.Log("Win");
//					Debug.Log("23332");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//2333
//				{
//					Debug.Log("Win");
//					Debug.Log("2333");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//233
//				{
//					Debug.Log("Win");
//					Debug.Log("233");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 3, 3, 3, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);


//			}
//			//232
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[2]||Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//23212
//				{

//					Debug.Log("Win");
//					Debug.Log("23212");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite)//2321
//				{

//					Debug.Log("Win");
//					Debug.Log("2321");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else
//				{
//					Debug.Log("Win");
//					Debug.Log("232");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 3, 2, 1, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//231
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[1]||Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//23132
//				{
//					Debug.Log("Win");
//					Debug.Log("23132");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//2313
//				{
//					Debug.Log("Win");
//					Debug.Log("2313");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else
//				{
//					Debug.Log("Win");
//					Debug.Log("231");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 3, 1, 3, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}




//		}
//		//22
//		if ((Date[0].Sever_Sprites[2] == Date[1].Sever_Sprites[2] || Date[1].Sever_Sprites[2]==Universal_Sprite) && Date[0].Sever_Sprites[2] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[2];//最左的初始中獎圖
//			//223
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[3] || Date[2].Sever_Sprites[3]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//22322
//				{

//					Debug.Log("Win");
//					Debug.Log("22322");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)
//				{

//					Debug.Log("Win");
//					Debug.Log("2232");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else
//				{

//					Debug.Log("Win");
//					Debug.Log("223");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 2, 3, 2, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//222
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[2] || Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{

//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//22222
//				{
//					Debug.Log("Win");
//					Debug.Log("22222");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//2222
//				{
//					Debug.Log("Win");
//					Debug.Log("2222");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//222
//				{
//					Debug.Log("Win");
//					Debug.Log("222");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 2, 2, 2, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//221
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[1] || Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//22122
//				{
//					Debug.Log("Win");
//					Debug.Log("22122");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//2212
//				{

//					Debug.Log("Win");
//					Debug.Log("2212");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//221
//				{

//					Debug.Log("Win");
//					Debug.Log("221");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 2, 1, 2, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}

//		}
//		//21
//		if ((Date[0].Sever_Sprites[2] == Date[1].Sever_Sprites[1] || Date[1].Sever_Sprites[1]==Universal_Sprite) && Date[0].Sever_Sprites[2] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[2];//最左的初始中獎圖
//			//212
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[2] || Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//21232
//				{

//					Debug.Log("Win");
//					Debug.Log("21232");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//2123
//				{
//					Debug.Log("Win");
//					Debug.Log("2123");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//212
//				{
//					Debug.Log("Win");
//					Debug.Log("212");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 1, 2, 3, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//211
//			if (Date[0].Sever_Sprites[2] == Date[2].Sever_Sprites[1] || Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1]==Universal_Sprite) && (Date[0].Sever_Sprites[2] == Date[4].Sever_Sprites[2] || Date[4].Sever_Sprites[2]==Universal_Sprite))//21112
//				{
//					Debug.Log("Win");
//					Debug.Log("21112");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[2] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite)//2111
//				{
//					Debug.Log("Win");
//					Debug.Log("2111");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//211
//				{
//					Debug.Log("Win");
//					Debug.Log("211");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 2, 1, 1, 1, 2 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[1], _GetRoolWinImg, StartEndPoint[4], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}


//		}
//		//12
//		if ((Date[0].Sever_Sprites[1] == Date[1].Sever_Sprites[2] || Date[1].Sever_Sprites[2]==Universal_Sprite) && Date[0].Sever_Sprites[1] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[1];//最左的初始中獎圖
//			//123
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[3] || Date[2].Sever_Sprites[3]==Universal_Sprite)
//			{

//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[1] || Date[4].Sever_Sprites[1]==Universal_Sprite))//12321
//				{
//					Debug.Log("Win");
//					Debug.Log("12321");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)
//				{

//					Debug.Log("Win");
//					Debug.Log("1232");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//123
//				{
//					Debug.Log("Win");
//					Debug.Log("123");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 2, 3, 2, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//122
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[2] || Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[1] || Date[4].Sever_Sprites[1]==Universal_Sprite))//12221
//				{
//					Debug.Log("Win");
//					Debug.Log("12221");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)
//				{
//					Debug.Log("Win");
//					Debug.Log("1222");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else
//				{

//					Debug.Log("Win");
//					Debug.Log("122");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 2, 2, 2, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//121
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[1] || Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[1] || Date[4].Sever_Sprites[1]==Universal_Sprite))//12121
//				{
//					Debug.Log("Win");
//					Debug.Log("12121");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[2] || Date[3].Sever_Sprites[2] == Universal_Sprite)//1212
//				{

//					Debug.Log("Win");
//					Debug.Log("1212");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else//121
//				{
//					Debug.Log("Win");
//					Debug.Log("121");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 2, 1, 2, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}

//		}
//		//11
//		if ((Date[0].Sever_Sprites[1] == Date[1].Sever_Sprites[1] || Date[1].Sever_Sprites[1]==Universal_Sprite) && Date[0].Sever_Sprites[1] != BonusSprite)
//		{
//			Sprite Sprite_Temp = Date[0].Sever_Sprites[1];//最左的初始中獎圖

//			//113
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[3] || Date[2].Sever_Sprites[3]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[1] || Date[4].Sever_Sprites[1]==Universal_Sprite))
//				{
//					Debug.Log("Win");
//					Debug.Log("11311");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite)//1131
//				{

//					Debug.Log("Win");
//					Debug.Log("1131");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else
//				{
//					Debug.Log("Win");
//					Debug.Log("113");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}

//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 1, 3, 1, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}
//			//112
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[2] || Date[2].Sever_Sprites[2]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[3] || Date[4].Sever_Sprites[3]==Universal_Sprite))
//				{
//					Debug.Log("Win");
//					Debug.Log("11233");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);

//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[3] || Date[3].Sever_Sprites[3] == Universal_Sprite)//1123
//				{

//					Debug.Log("Win");
//					Debug.Log("1123");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else
//				{
//					Debug.Log("Win");
//					Debug.Log("112");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}


//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 1, 2, 3, 3 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[5], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}

//			//111
//			if (Date[0].Sever_Sprites[1] == Date[2].Sever_Sprites[1] || Date[2].Sever_Sprites[1]==Universal_Sprite)
//			{
//				if ((Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1]==Universal_Sprite) && (Date[0].Sever_Sprites[1] == Date[4].Sever_Sprites[1] || Date[4].Sever_Sprites[1]==Universal_Sprite))//11111
//				{
//					Debug.Log("Win");
//					Debug.Log("11111");
//					int Line_count = 5;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}
//				else if (Date[0].Sever_Sprites[1] == Date[3].Sever_Sprites[1] || Date[3].Sever_Sprites[1] == Universal_Sprite)
//				{

//					Debug.Log("Win");
//					Debug.Log("1111");
//					int Line_count = 4;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}

//				else//111
//				{
//					Debug.Log("Win");
//					Debug.Log("111");
//					int Line_count = 3;
//					Win_Sprite(_SlotDate.Sprite_Pool, Sprite_Temp, Line_count, _SlotDate.Bet_Coin, ref Win_Money);
//					Debug.Log("Win_Line方法上顯示的錢 ：" + Win_Money_Temp);
//				}


//				int[] RoolWinNumber;
//				RoolWinNumber = new int[] { 1, 1, 1, 1, 1 };
//				List<Transform> _GetRoolWinImg;
//				_GetRoolWinImg = GetRoolWinImg(Date, RoolWinNumber);
//				ObjectPool(StartEndPoint[0], _GetRoolWinImg, StartEndPoint[3], _StayOutPool, _StayDrawLine);
//				ListShiny(_GetRoolWinImg, ReadyShow);

//			}

//		}

//		if (_SlotDate.Bonus_count==3)
//		{

//			Debug.Log("恭喜中大獎");
			

//		}




//	}
//    #endregion

//    #region 判斷滾輪次數是否達成條件  沒達成條件就繼續滾
//    /// <summary>
//    /// 當Start_Slot為true時而且轉動次數到達上限 初始化開始按鈕跟輪條開始的bool
//    /// </summary>
    
//    public IEnumerator Co_Slot_timeOut()
//	{

//			if (_Reel_Moves[Slot_mantissa].tempi == Loopcount)//是否轉完一輪(最後的輪調是否達到次數)
//			{
//				if (_Reel_Moves[Slot_mantissa].strool != false)
//				{

//					for (int i = 0; i < _Reel_Moves.Length; i++) //這裏做停輪動作
//					{

//						_Reel_Moves[i].strool = false;

//					}


//				}


//				if (BonusShow && UseBonus && StCoShow )//設一個bool 底下執行Bonus內容玩時 bool＝true ,然後這裡關掉 FreeGame的次數在這裡增加
//				{
//					Debug.Log("---------------------------：Bonus Show 開始執行 :--------------------------------------");
//					StCoShow = false;
//					_Show = Co_Show(_BonusShinyDate[FreeGameCount].BonusShinyPoint, _Bonus_PrepareDrawLinePool[FreeGameCount].Bonus_PrepareDrawLine,_Bonus_ShowOK[FreeGameCount]._ShowOk, Win_BonusMoney_Date[FreeGameCount]);
//					yield return StartCoroutine(_Show);
//					FreeCountPlus = true;
//				}

//				else if (StCoShow)

//				{
//					StCoShow = false;
//					_Show = Co_Show(UiShow,PrepareDrawLine,ShowOK,Win_Money_Temp);
//					yield return StartCoroutine(_Show);

//				}



//				if (CoinShowOK && StBonusShowOk && UseBonus && FreeCountPlus)
//				{
//					FreeCountPlus = false;
//					CoinShowOK = false;
//					StBonusShowOk = false;
//					FreeGameCount += 1;
//					BonusRolling = false;

                
//					BonusShow = true;
//					CoinShowOK = true;
//					Debug.Log("FreeGameCount :" + FreeGameCount);

//				}





//				if (BonusShow && ShowOver && !BonusRolling)
//					{
//						if (FreeGameCount<BonusCount)
//						{
						

//							if (FreeGameCount == 0)
//							{
//								BonusAnimator.SetBool("StBonusShow", true);

//								BonusStateInfo = BonusAnimator.GetCurrentAnimatorStateInfo(0);//階層為0
//								if (BonusStateInfo.normalizedTime >= 1.0f && BonusStateInfo.IsName("BonusShow2"))
//								{

//									BonusAnimator.SetBool("StBonusShow", false);

//									Debug.Log("-------------- : Bonus 動畫是否播放完畢 : -----------------"+BonusAnimator.GetBool("StBonusShow"));
//								}


//							}

//							if (!BonusAnimator.GetBool("StBonusShow"))
//							{
//								Debug.Log("-------------- : Bonus 動畫 （ 播放完畢 ） : -----------------" );
//								BonusRoll();
//								UseBonus = true;
//								StCoShow = true;
//								Slot_BonusGoRoll = true;
//								StBonusShowOk = true;
//							}




//						}

//						else if (FreeGameCount>=BonusCount)
//						{
//							Debug.Log("關閉");

//							for (int i=0;i<_BonusShinyDate.Count;i++)
//							{

//								_BonusShinyDate[i].BonusShinyPoint.Clear();

//							}
						
//							StEndShow=true;
//							StCoShowWinCoin = true;
//							BonusShow = false;
//							UseBonus = false;
//							StBonusShowOk = false;
//							Slot_BonusGoRoll = false;


//						}

//					}



//					if (ShowOver && !BonusShow && !StEndShow)
//					{



//						if (Cycle_Count < Auto_Count && _SlotDate.Coin > _SlotDate.Bet_Coin)
//						{

//							ShowOver = false;
						
//								Debug.Log("-----------------------------開始轉動-------------------------");
//								UiShow.Clear();
//								StRecover = true;

//								for (int i = 0; i < _Reel_Moves.Length; i++)
//								{
//									_Reel_Moves[i].tempi = 0;
//									_Reel_Moves[i].Date_Temp = 0;
//									Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

//								}

//								if (_Reel_Moves[Slot_mantissa].tempi == 0)
//								{
//									_SlotDate.Bonus_count = 0;
//									if (StAddBonusDate)
//									{
//										_SlotDate.Add_BonusDate(StAddBonusDate, _Reel_Moves);

//									}
//									else
//									{
//										//_SlotDate.Generate_Date_Sprite(_Reel_Moves);//生成盤面
//									}

//									Win_Line(_SlotDate._Reel_Sprite_Date, _SlotDate.Sprite_Pool[6],UiShow,ref Win_Money_Temp,LineRenderOutPool,PrepareDrawLine,  _SlotDate.Sprite_Pool[7]);//兌獎 預置物生成放入

//									if (_SlotDate.Bonus_count == 3)//如果有Bonus獎
//									{
//										BonusShow = true;

//									//	_SlotDate.GenerateBonusDate(BonusCount, _Reel_Moves);//生成所有Bonus盤面

//										for (int i = 0; i < _SlotDate._AllBonusSpriteDate.Count; i++)
//										{

//											Win_Line(_SlotDate._AllBonusSpriteDate[i]._BonusSpriteDate, _SlotDate.Sprite_Pool[6], _BonusShinyDate[i].BonusShinyPoint, ref Win_BonusMoney_Temp
//													 , BonusLineRenderOutPool.transform.GetChild(i), _Bonus_PrepareDrawLinePool[i].Bonus_PrepareDrawLine, _SlotDate.Sprite_Pool[7]);//兌獎

//											Win_BonusMoney_Date[i] = Win_BonusMoney_Temp;
//											Win_BonusMoney_Temp = 0;

//										}
//										for (int i=0;i<Win_BonusMoney_Date.Count;i++)//將各個盤面的Bonus獎的中獎金額總和起來
//										{

//											Total_BonusWinCoin += Win_BonusMoney_Date[i];



//										}


//									}
									
//									St_Roll = true;
//									yield return new WaitForSeconds(1f);
//									Debug.Log("Wait");
//									_CoAll_Slot_Roll = CoAll_Slot_Roll();
//									StartCoroutine(_CoAll_Slot_Roll);
//									Cycle_Count += 1;
//									Auto_Temp_Count -= 1;
//									Auto_text.text = Auto_Temp_Count.ToString();
//									_SlotDate.Coin -= _SlotDate.Bet_Coin;
//									Player_coin_Text.text = "Money:" + _SlotDate.Coin;
//									Debug.Log(Cycle_Count + "已循環次數");
//									DateSave();
//									Debug.Log("Win_Mpney_Temp" + Win_Money_Temp);
//									StCoShow = true;
//									StRecover = false;
//									ShowOver = true;
//									FreeGameCount = 0;
//								}

//						}

//						else
//						{


//							Debug.Log("各輪盤滾動完畢");
//							Debug.Log("是否開始滾動：" + Start_Slot);
//							Auto_Count = 1;

//							if (CoinShowOK == true)
//							{
//								for (int i = 0; i < _Reel_Moves.Length; i++)
//								{
//									_Reel_Moves[i].tempi = 0;
//									_Reel_Moves[i].Date_Temp = 0;
//									Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));
//								}
//								Start_Slot = false;
//								St_Roll = true;
//								UiShow.Clear();
//								StCoShow = true;
//								FreeGameCount = 0;
//								Cycle_Count = 1;
//								Auto_Temp_Count = 0;
//							}


//						}



//					}
				
				
				
//			}

		


//	}
//	#endregion

//	#region 表演方法
//	/// <summary>
//	/// 表演方法
//	/// </summary>
//	/// <returns></returns>
//	public IEnumerator Co_Show(List<Transform>_UiShow,List<GameObject>StayDrawLine,List<bool>_ShowOk,int WinMoneyTemp)//把特效變更金錢文字寫在這  然後Slot_timeOut改成協程 等待這個方法做完 再繼續作用
//	{
//		if (WinMoneyTemp != 0)//有中獎
//		{
//			ShowOver = false;
//			CoinShowOK = false;
//			ShinyShow(_UiShow);

//			StartDrawLine(StayDrawLine, _ShowOk);

//			StCoShowWinCoin = true;


//		}
		
//		yield return null;
      
//	}
//	#endregion

//	#region 物件池相關
//	/// <summary>
//	/// 物件池生成設定
//	/// </summary>
//	public void ObjectPoolInitialization()//物件池生成設定
//	{
		
//		int InitailSize = 5;

//		DrawLinePool = new Queue<GameObject>();

//		for (int i=0;i<InitailSize;i++)
//		{
//			GameObject InstGb;

//			InstGb = Instantiate(GBDrawLine,InLineRenderPool);//預制物生成
//			DrawLinePool.Enqueue(InstGb);//將生成的預制物方進DrawLinePool這個Queue裡
//			InstGb.SetActive(false);
//			Debug.Log("DrawLinePool.Count:"+DrawLinePool.Count);
//		}

//	}
//	/// <summary>
//	/// 物件池 （判斷物件池內有無物件 有就取出 無就生成）
//	/// </summary>
//	/// <param name="initalPosition"></param>
//	/// <param name="PointPosition"></param>
//	/// <param name="EndPoint"></param>
//	public void ObjectPool(Transform initalPosition,List<Transform> PointPosition,Transform EndPoint,Transform StayOutpool,List<GameObject>StayDrawLine)//物件池 （判斷物件池內有無物件 有就取出 無就生成）
//	{
//		GameObject Reuse;
//		if (DrawLinePool.Count > 0)//如果有DrawLinePool裡有預制物就取出來用
//		{

			
//			Reuse = DrawLinePool.Dequeue();//取出DrawLinePool的預制物
//			Reuse.transform.position = initalPosition.position;//設置預制物的位置
		
//			InstObjectInitialization(Reuse,initalPosition,PointPosition,EndPoint);
//			Reuse.transform.parent = StayOutpool;
//			StayDrawLine.Add(Reuse);//放進去等待啟動
		
//		}
//		else
//		{

//			Reuse = Instantiate(GBDrawLine, StayOutpool);
//			Reuse.transform.position = initalPosition.position;
		
//			InstObjectInitialization(Reuse,initalPosition,PointPosition,EndPoint);
//			Reuse.SetActive(false);
//			StayDrawLine.Add(Reuse);//放進去等待啟動


//		}

		
//	}
//	/// <summary>
//	/// 物件用完回收物件池
//	/// </summary>
//	/// <param name="recover"></param>
//	/// <param name="Stb"></param>
//	public void Recover(GameObject recover,bool Stb)//物件用完回收物件池
//	{
//		Transform OringerTs = gameObject.transform;//OringerTs就是自身座標
//		if (!Stb)
//		{
//			Debug.Log("---------------------------Start_Recover :------------------------");
     
//            recover.GetComponent<DrawLine>().LI.positionCount = 0;
//            recover.GetComponent<DrawLine>().LI.positionCount = 2;

//            DrawLinePool.Enqueue(recover);
//			recover.transform.parent = InLineRenderPool;
//			recover.SetActive(false);

//		}



//	}
//	/// <summary>
//	/// 預制物初始化
//	/// </summary>
//	/// <param name="Reuse"></param>
//	/// <param name="OrangePoint"></param>
//	/// <param name="PointPosition"></param>
//	/// <param name="EndPoint"></param>
//	public void InstObjectInitialization(GameObject Reuse,Transform OrangePoint,List<Transform> PointPosition,Transform EndPoint)//預制物初始化
//	{
//		Reuse.GetComponent<DrawLine>().Taget_Point = new List<Transform>();
		
//		for (int i = 0; i < PointPosition.Count; i++)
//		{

//			Reuse.GetComponent<DrawLine>().Taget_Point.Add(PointPosition[i].transform);


//		}
//		Reuse.GetComponent<DrawLine>().Temp_point = new List<Transform>();
//		Reuse.GetComponent<DrawLine>().Temp_point.Add(OrangePoint);
//		Reuse.GetComponent<DrawLine>().Temp_point.Add(Reuse.GetComponent<DrawLine>().gameObject.transform);
//		Reuse.GetComponent<DrawLine>().Temp_VV = Reuse.GetComponent<DrawLine>().Taget_Point[0].transform;//PointPosition[0];
//		Reuse.GetComponent<DrawLine>().Orange_point = OrangePoint.gameObject;
//		Reuse.GetComponent<DrawLine>().Taget_Point.Add(EndPoint);
//		//Debug.Log("Manager__ Temp_point.Count : " + Reuse.GetComponent<DrawLine>().Temp_point.Count);
//		//Debug.Log("Manager__Taget_Point.Count : " + Reuse.GetComponent<DrawLine>().Taget_Point.Count);
//	}

//	/// <summary>
//	/// /判斷DrawLine完了沒 ,結束後執行回收（執行Recover）
//	/// </summary>
//	/// <param name="OutPool"></param>
//	/// <param name="StayDrawLine"></param>
//	/// <param name="_SHowOk"></param>
//	public void RecoverComply(Transform OutPool,List<GameObject>StayDrawLine,List<bool>_SHowOk)//判斷DrawLine完了沒 ,結束後執行回收（執行Recover）
//	{

//		bool DrawAllBool;
//		DrawAllBool = _SHowOk.Contains(true);


//		if (StayDrawLine.Count != 0)//LineRenderOutPool.childCount != 0
//		{
//			//Debug.Log("----------------LineRenderOutPool.childCount : ----------------------"+ LineRenderOutPool.childCount);
//			//Debug.Log("StayDrawLine.Count :" + StayDrawLine.Count);

//			if (_SHowOk.Count != 0 && StayDrawLine.Count != 0)//持續更新裡面的bool
//			{


//				for (int i = 0; i < StayDrawLine.Count; i++)
//				{
//					//Debug.Log("_SHowOk[i] 持續更新");
//					_SHowOk[i] = StayDrawLine[i].GetComponent<DrawLine>().StDrawLine;


//				}




//			}

		

//			//Debug.Log("--------------DrawStop :--------"+ DrawStop);

//			if (!DrawAllBool && _SHowOk.Count != 0)//DrawLine 全部線都表演完
//			{
//				for (int i = 0; i < OutPool.childCount; i++)
//				{

//					Recover(OutPool.transform.GetChild(i).gameObject, DrawAllBool); //這裡因為Bonus每個盤面生成的物件都放這 所以出了問題


//				}

//				if (OutPool.childCount == 0)
//				{

//					StayDrawLine.Clear();
//					_SHowOk.Clear();
//					//Debug.Log("------------PrepareDrawLine.Count :------------" + PrepareDrawLine.Count);
//					StRecover = true;
//				}

//			}






//		}

//	}





//		#endregion

//	#region 執行LineRender相關
//		/// <summary>
//		/// 取得連線時輪條上中獎的圖 並裝進陣列回傳
//		/// </summary>
//		/// <param name="Date"></param>
//		/// <param name="SlotSequence"></param>
//		/// <returns></returns>
//		public List<Transform> GetRoolWinImg(List<Reel_Sprite_Date> Date, int [] SlotSequence)//取得連線時輪條上中獎的圖 並裝進陣列回傳
//	{

//		List<Transform> VVVs;
//		VVVs = new List<Transform>();
//        for (int i = 0; i < Date.Count; i++)
//        {

//            VVVs.Add(_Reel_Moves[i].transform.GetChild(SlotSequence[i]));
//            Debug.Log(VVVs[i]);
//        }
//		return VVVs;
       

//    }

//	/// <summary>
//    /// 執行陣列內的LineRender
//    /// </summary>
//	public void StartDrawLine(List<GameObject>StayDrawLine,List<bool>_ShowOk)
//	{
		
		
//		for (int i=0;i< StayDrawLine.Count;i++)
//		{
//			StayDrawLine[i].SetActive(true);
//			StayDrawLine[i].GetComponent<DrawLine>().StDrawLine = true;
//			_ShowOk.Add(StayDrawLine[i].GetComponent<DrawLine>().StDrawLine);

//		}
	
//	}
//	#endregion

//	#region ShinyShow相關
//	/// <summary>
//	/// 將要表演的圖片放入List
//	/// </summary>
//	/// <param name="_GetRoolWinImg"></param>
//	public void ListShiny(List<Transform> _GetRoolWinImg,List<Transform>ReadyShow )//將要表演的圖片放入List
//	{

//		for (int i=0;i<_GetRoolWinImg.Count;i++)
//		{
//			if (!ReadyShow.Contains(_GetRoolWinImg[i]))
//			{
//				ReadyShow.Add(_GetRoolWinImg[i]);

//			}


//		}



//	}

//	/// <summary>
//	/// 打開Uishow陣列內物件的Animator的Trigger
//	/// </summary>
//	public void ShinyShow(List<Transform> ReadyShow)//打開Uishow陣列內物件的Animator的Trigger
//	{
//		for (int i=0;i< ReadyShow.Count;i++)
//		{ Animator Amo;
//			Amo = ReadyShow[i].GetComponent<Animator>();
//			Amo.SetTrigger("StShiny");


//		}

//	}
//    #endregion


//    #region 贏錢表演
//	/// <summary>
//    /// 贏錢表演 表演完更新畫面金錢
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerator CoShowWinCoin(int WinMoney)
//	{
		
//			if (TempWinCoin <= WinMoney)
//			{
//				Amr_WinShow.SetBool("StWinShow", true);
//				string SWinCoin = TempWinCoin.ToString();
//				//Debug.Log("SWinCoin(暫存金額) ：" + SWinCoin);
//				int ImgCount = SWinCoin.Length;//取得幾位數
//				//Debug.Log("本局Win金額 ＝ " + ImgCount + "位數");
//				for (int i = 0; i < ImgCount; i++)
//				{
//					int TempValu;//用字串處理 取的 Win_Money_Temp各個位數的值
//					TempValu = int.Parse(SWinCoin.Substring(i, 1));
//					WinCoins[i].gameObject.SetActive(true);
//					WinCoins[i].sprite = Numbers[TempValu];



//				}
//				yield return null;




//				if (WinMoney - TempWinCoin < 50)
//				{
//					TempWinCoin += 1;
//					yield return new WaitForSeconds(0.01f);

//				}
//				else
//				{

//					TempWinCoin += 21;
//					yield return new WaitForSeconds(0.01f);
//				}


//				StCoShowWinCoin = true;


//			}
//			else if (TempWinCoin > WinMoney)
//			{
//				yield return new WaitForSeconds(1f);
//				int Win_All_Coin_Temp;
//				Amr_WinShow.SetBool("StWinShow",false);
//				yield return new WaitForSeconds(0.2f);
//				for (int i = 0; i < WinCoins.Length; i++)
//				{
//					WinCoins[i].gameObject.SetActive(false);


//				}

//				TempWinCoin = 0;
//				Win_All_Coin_Temp = _SlotDate.Coin + WinMoney;
//				_SlotDate.Coin = Win_All_Coin_Temp;
//				Player_coin_Text.text = "Money:" + Win_All_Coin_Temp.ToString();
//				if (StBonusShowOk)
//				{

//					Win_BonusMoney_Date[FreeGameCount] = 0;

//				}
//				else
//				{

//					Win_Money_Temp = 0;

//				}
				
//				yield return new WaitForSeconds(0.5f);


//				StRecover = false;
//				CoinShowOK = true;
//				ShowOver = true;
//				//StBonusShowOk = true;

//			}








//    }
//    #endregion

//    #region Bonus表演相關
//	/// <summary>
//    /// Bonus 的 金錢表演
//    /// </summary>
//    /// <param name="WinMoney"></param>
//    /// <returns></returns>
//    public IEnumerator End_TotalBonusWinCoin(int WinMoney)
//	{


//		if (TempWinCoin <= WinMoney)
//		{
			
//			string SWinCoin = TempWinCoin.ToString();
//			//Debug.Log("SWinCoin(暫存金額) ：" + SWinCoin);
//			int ImgCount = SWinCoin.Length;//取得幾位數
//			//Debug.Log("本局Win金額 ＝ " + ImgCount + "位數");
//			for (int i = 0; i < ImgCount; i++)
//			{
//				int TempValu;//用字串處理 取的 Win_Money_Temp各個位數的值
//				TempValu = int.Parse(SWinCoin.Substring(i, 1));
//				WinCoins[i].gameObject.SetActive(true);
//				WinCoins[i].sprite = Numbers[TempValu];



//			}
//			yield return null;




//			if (WinMoney - TempWinCoin < 50)
//			{
//				TempWinCoin += 1;
//				yield return new WaitForSeconds(0.01f);

//			}
//			else
//			{

//				TempWinCoin += 21;
//				yield return new WaitForSeconds(0.01f);
//			}


//			StCoShowWinCoin = true;


//		}
//		else if (TempWinCoin > WinMoney)
//		{
//			yield return new WaitForSeconds(0.3f);

//			for (int i = 0; i < WinCoins.Length; i++)
//			{
//				WinCoins[i].gameObject.SetActive(false);

//			}

			
//			yield return new WaitForSeconds(0.5f);

//			VideoImage.gameObject.SetActive(true);
//			VideoImage.GetComponent<VideoPlayer>().Play();
//			yield return new WaitForSeconds(0.5f);
//			VideoImage.GetComponent<RawImage>().enabled=true;

			
//			//StRecover = false;
//			CoinShowOK = true;
//			ShowOver = true;
//			//StBonusShowOk = true;
			
//		}






//	}



//	/// <summary>
//    /// Bonus的啟動各輪
//    /// </summary>

//    public void BonusRoll()
//	{

//		if (FreeGameCount < BonusCount)
//		{
//			Debug.Log("-----------------------BONUS 輪條歸零 ----------------------------");
//			for (int i = 0; i < _Reel_Moves.Length; i++)
//			{
//				_Reel_Moves[i].tempi = 0;
//				_Reel_Moves[i].Date_Temp = 0;
//				Debug.Log(string.Format("輪調{0}內滾動次數{1}", i, _Reel_Moves[i].tempi));

//			}
//            if (_Reel_Moves[Slot_mantissa].tempi == 0)
//            {
//                St_Roll = true;
//				_CoAll_Slot_Roll = CoAll_Slot_Roll();
//				StartCoroutine(_CoAll_Slot_Roll);
//				Debug.Log("-----------------------BONUS 輪條開始轉動 ----------------------------");
//				StCoShow = true;
//				StRecover = false;
//				ShowOver = true;
//				BonusRolling = true;
//			}


		
//		}

//	}

//    #endregion

//    #region 資料的儲存以及讀取
//    /// <summary>
//    /// 將資料轉成Jason並且儲存
//    /// </summary>
//    public void DateSave()//
//	{

//		if (_SlotDate.Bonus_count == 3)
//		{
//			int i = _SlotDate._AllBonusSpriteDate.Count - 1;
//			_SlotDate.Date_Save(_SlotDate._AllBonusSpriteDate[i]._BonusSpriteDate);

//		}
//		else
//		{

//			_SlotDate.Date_Save(_SlotDate._Reel_Sprite_Date);

//		}

//		_SlotDate._Slot_SeverDate.Bet_Coin = _SlotDate.Bet_Coin;
//		_SlotDate._Slot_SeverDate.Auto_HasRollcount = Auto_Temp_Count;
//		_SlotDate._Slot_SeverDate.Auto_PlayerSet = Auto_Count;
//		_SlotDate._Slot_SeverDate.Auto_NotYet = Cycle_Count;
//		_SlotDate._Slot_SeverDate.Player_Coin = _SlotDate.Coin;
//		_SlotDate._Slot_SeverDate.BonusCoin = Total_BonusWinCoin;
//		_SlotDate._Slot_SeverDate.Win_Coin = Win_Money_Temp;
//		_SlotDate._Slot_SeverDate.Data = _SlotDate._Date;
//		 string SaveDateToJason;
//		SaveDateToJason = JsonUtility.ToJson(_SlotDate._Slot_SeverDate);
//		PlayerPrefs.SetString("遊戲資料",SaveDateToJason);
//		PlayerPrefs.Save();
//		Debug.Log("資料儲存完成");
//		Debug.Log("資料內容 ：" + SaveDateToJason);
//		Debug.Log("目前是否有儲存到資料 ："+PlayerPrefs.HasKey("遊戲資料")); ;

//	}

//	/// <summary>
//    /// 將資料讀取
//    /// </summary>
//	public void GetDateSave()
//	{
//		Debug.Log("有無遊戲資料 ："+PlayerPrefs.HasKey("遊戲資料"));
//		if (PlayerPrefs.HasKey("遊戲資料"))
//		{
//			Options.SetActive(true);
			

//		}
//		Options_Yes.onClick.AddListener(delegate()
//		{
//			if (PlayerPrefs.HasKey("遊戲資料"))
//			{
				
//				string GetDate;
//				GetDate = PlayerPrefs.GetString("遊戲資料");
//				Debug.Log("儲存的資料內容 ： " + GetDate);
//				_SlotDate.GetSlotDate = JsonUtility.FromJson<Slot_SeveDate>(GetDate);

//				int All_Coin;
//				All_Coin = _SlotDate.GetSlotDate.Player_Coin + _SlotDate.GetSlotDate.BonusCoin + _SlotDate.GetSlotDate.Win_Coin;
//				_SlotDate.Bet_Coin = _SlotDate.GetSlotDate.Bet_Coin;
//				if (_SlotDate.GetSlotDate.Auto_PlayerSet > _SlotDate.GetSlotDate.Auto_NotYet)
//				{
//					Auto_Temp_Count = _SlotDate.GetSlotDate.Auto_HasRollcount - 1;

//					Cycle_Count = _SlotDate.GetSlotDate.Auto_NotYet + 1;
//				}
//				else
//				{

//					Auto_Temp_Count = 1;
//					Cycle_Count = 1;

//				}
//				Auto_Count = _SlotDate.GetSlotDate.Auto_PlayerSet;
//				_SlotDate.Coin = All_Coin;
//				Player_coin_Text.text = "Money:" + _SlotDate.Coin;
//				Bet_text_out.text = _SlotDate.Bet_Coin.ToString();
//				BET_text.text = _SlotDate.Bet_Coin.ToString();
//				Auto_text.text =Auto_Temp_Count.ToString();
//				for (int i = 0; i < _SlotDate.GetSlotDate.Data.Count; i++)
//				{


//					for (int j = 0; j < _SlotDate.GetSlotDate.Data[i]._IntCount.Count; j++)
//					{
//						_Reel_Moves[i].transform.GetChild(j).GetComponent<Image>().sprite = _SlotDate.Sprite_Pool[_SlotDate.GetSlotDate.Data[i]._IntCount[j]];

//						Debug.Log("_SlotDate.GetSlotDate.Data[i]._IntCount[j] :" + _SlotDate.GetSlotDate.Data[i]._IntCount[j]);





//					}





//				}

//				Options.SetActive(false);

//			}

//		});

//		Options_No.onClick.AddListener(delegate()
//		{

//			PlayerPrefs.DeleteAll();
//			Debug.Log("是否有儲存的資料 ："+ PlayerPrefs.HasKey("遊戲資料"));
//			Initialization_Slot_Sprite(_SlotDate._Reel_Sprite_Date);
//			Options.SetActive(false);
//		});


//	}
//    #endregion

//}
