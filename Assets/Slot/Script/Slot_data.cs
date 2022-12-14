using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_data : MonoBehaviour {
	public List<Reel_Sprite_Date> _Reel_Sprite_Date;
	public int Coin;//玩家擁有的錢
	public int Bet_Coin;//押注的錢
	public Sprite[] Sprite_Pool;//圖片庫
	public Sprite[] _One_Sprite_pool;//第一個輪條用的圖片庫
	public Sprite[] BonusOneSpritePool;
	public Sprite[] BonusSpritePool;
	public int Bonus_count;//當前 Bonus圖片出現的數量（用來算每個輪條[2][3][4]Bonus圖片出現幾次）
	public List<All_BonusSpriteDate> _AllBonusSpriteDate;
	public Slot_SeveDate _Slot_SeverDate;
	public Slot_SeveDate GetSlotDate;
	public List<intCount> _Date;
	// Use this for initialization
	void Start () {

		
	}

    // Update is called once per frame
    void Update () {
		
	}


	/// <summary>
	/// 初始化輪條資料的空間
	/// </summary>
	public void Reel_List_Initialization(Reel_Move[] ReelScripts)
	{

		_Slot_SeverDate = new Slot_SeveDate();


		for (int i=0;i<ReelScripts.Length;i++)
		{
			_Reel_Sprite_Date.Add(new Reel_Sprite_Date());
			_Reel_Sprite_Date[i].Sever_Sprites = new List<Sprite>();
			_Date.Add(new intCount());
			_Date[i]._IntCount = new List<int>();
			int Tempi = ReelScripts[i].transform.childCount;
			for (int j=0;j<Tempi;j++)
			{
				_Reel_Sprite_Date[i].Sever_Sprites.Add(null);
				_Date[i]._IntCount.Add(0);
			}
			//Debug.Log(string.Format("盤面資料生成,輪條數：{0},各輪條內圖片數量：{1}", ReelScripts.Length,Tempi));
		}


		
	}

	/// <summary>
    /// 生成圖片資料
    /// </summary>
	public void Generate_Date_Sprite(Reel_Move[] Generate_ReelScripts)
	{

		for (int i=0;i<Generate_ReelScripts.Length;i++)
		{
			
			int Temp = Generate_ReelScripts[i].transform.childCount;

			if (i == 0)//第一個輪條  
			{

				for (int j = 0; j < Temp; j++)
				{
					int _One_Sprite_pool_Temp = _One_Sprite_pool.Length - 1;//第一輪用的陣列少掉Bonus圖
					

					if (_Reel_Sprite_Date[i].Sever_Sprites.Contains(_One_Sprite_pool[6]))//已經有Bonus圖的話 就不要再出現Bonus圖了
					{
						
						//Debug.Log("_Reel_Sprite_Date[i].Sever_Sprites.Contains(_One_Sprite_pool[6])" + _Reel_Sprite_Date[i].Sever_Sprites.Contains(_One_Sprite_pool[6]));
						int Temp_B;
						Temp_B = Random.Range(0,_One_Sprite_pool_Temp);
						
						_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Temp_B];

					}
					else
					{
						Debug.Log("_Reel_Sprite_Date[i].Sever_Sprites.Contains(_One_Sprite_pool[6])" + _Reel_Sprite_Date[i].Sever_Sprites.Contains(_One_Sprite_pool[6]));
						int Tempj = Random.Range(0, _One_Sprite_pool.Length);
						_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Tempj];


					}


				}
				//檢查出現的Bonus圖是否在 輪條[1] [2] [3]的位置
				for (int Slot_i = 1; Slot_i < 4; Slot_i++)
				{
					
					if (_Reel_Sprite_Date[i].Sever_Sprites[Slot_i] == Sprite_Pool[7])
					{
						Bonus_count++;//有出現Bonus圖就++
						Debug.Log("大獎數量 ：" + Bonus_count);


					}


				}



			}

			else//除了第一條輪條外的其他輪條
			{

				for (int j = 0; j < Temp; j++)//生成輪條內的圖片
				{
					int Pool_Temp;
					Pool_Temp = Sprite_Pool.Length - 1;//圖庫的圖數量減1,填入的圖會少了Bonus圖

					//檢查陣列裡有沒有Bonus圖了
					if (_Reel_Sprite_Date[i].Sever_Sprites.Contains(Sprite_Pool[7]) || Bonus_count >= 3)//已經有Bonus圖的話 就不要再出現Bonus圖了
					{
						
						int Tempj = Random.Range(0, Pool_Temp);
						_Reel_Sprite_Date[i].Sever_Sprites[j] = Sprite_Pool[Tempj];//填入的圖會少了Bonus圖



					}
					
					else 
					{

						int Tempj = Random.Range(0, Sprite_Pool.Length);
						_Reel_Sprite_Date[i].Sever_Sprites[j] = Sprite_Pool[Tempj];//完整的圖庫

					}
					
					
					_Reel_Sprite_Date[i].Sever_Sprites.Contains(Sprite_Pool[7]);
					//Debug.Log("_Reel_Sprite_Date[i].Sever_Sprites.Contains(Sprite_Pool[7]);" + _Reel_Sprite_Date[i].Sever_Sprites.Contains(Sprite_Pool[7]));
				}

				for (int Slot_i = 1; Slot_i < 4; Slot_i++)
				{
					if (_Reel_Sprite_Date[i].Sever_Sprites[Slot_i] == Sprite_Pool[7])
					{
						Bonus_count++;
						Debug.Log("目前大獎的數量 ：" + Bonus_count);

					}


				}


			}



		}








	}

	public void BonusDate_Initialization(int BonusCount, Reel_Move[] ReelScripts)
	{

		for (int i = 0; i < BonusCount; i++)
		{
			_AllBonusSpriteDate.Add(new All_BonusSpriteDate());
			_AllBonusSpriteDate[i]._BonusSpriteDate = new List<Reel_Sprite_Date>();
			int tempi = ReelScripts[i].transform.childCount;
			for (int j = 0; j < ReelScripts.Length; j++)
			{
				_AllBonusSpriteDate[i]._BonusSpriteDate.Add(new Reel_Sprite_Date());
				_AllBonusSpriteDate[i]._BonusSpriteDate[j].Sever_Sprites = new List<Sprite>();
				for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
				{

					
					_AllBonusSpriteDate[i]._BonusSpriteDate[j].Sever_Sprites.Add(null);




				}

			}




		}


	}






	public void GenerateBonusDate(int BonusCount, Reel_Move[] ReelScripts)//Bonus所有盤面生成 

	{

		for (int i=0;i<BonusCount;i++)
		{

			
			
			int tempi = ReelScripts[i].transform.childCount;
			for (int j=0;j<ReelScripts.Length;j++)
			{

				if (j == 0)//第一個輪條 不會出現共用圖
				{


					for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
					{

						int Tempcount;
						int SpriteCountTemp;

						Tempcount = Sprite_Pool.Length - 2;
						SpriteCountTemp = Random.Range(0, Tempcount);
						_AllBonusSpriteDate[i]._BonusSpriteDate[j].Sever_Sprites[SpriteCount]=Sprite_Pool[SpriteCountTemp];




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
						_AllBonusSpriteDate[i]._BonusSpriteDate[j].Sever_Sprites[SpriteCount]=Sprite_Pool[SpriteCountTemp];




					}

				}
				


			}

		}





	}




	public void Add_BonusDate(bool BtnTrue,Reel_Move[] Generate_ReelScripts)
	{

		if (BtnTrue)
		{

			for (int i = 0; i < Generate_ReelScripts.Length; i++)
			{


				int Temp = Generate_ReelScripts[i].transform.childCount;
				int _One_Sprite_pool_Temp = _One_Sprite_pool.Length - 1;//第一輪用的陣列少掉Bonus圖
				int Temp_B;
				Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
				
				if (i == 0)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 1)
						{
							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[6];

						}
						else
						{

							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Temp_B];

						}



					}

				}
				else if (i == 1)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 2)
						{
							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[6];

						}
						else
						{
							int Temp_c = Random.Range(0, Sprite_Pool.Length - 2);
							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Temp_c];

						}



					}




				}
				else if (i == 2)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 3)
						{
							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[6];

						}
						else
						{
							int Temp_c = Random.Range(0, Sprite_Pool.Length - 2);
							_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Temp_c];

						}



					}




				}

				else
				{


					for (int j = 0; j < Temp; j++)
					{



						int Temp_c = Random.Range(0, Sprite_Pool.Length - 2);
						_Reel_Sprite_Date[i].Sever_Sprites[j] = _One_Sprite_pool[Temp_c];



					}


				}


			}

			Bonus_count = 3;


		}







	}


	public void Date_Save(List<Reel_Sprite_Date> _ReelMove)
	{
		List<Sprite> SS;
		SS = new List<Sprite>();
		for (int k = 0; k < Sprite_Pool.Length; k++)
		{

			SS.Add(Sprite_Pool[k]);


		} ;
		
		for (int i=0;i<_ReelMove.Count;i++)
		{
			int tempi = _ReelMove[i].Sever_Sprites.Count;
			for (int j=0;j<tempi;j++)
			{
				
				int Dateindex;

				Dateindex = SS.IndexOf(_ReelMove[i].Sever_Sprites[j]);

				_Date[i]._IntCount[j] = Dateindex;
				

			}





		}



		





	}


}



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


[System.Serializable]
public class Reel_Sprite_Date
{
    public List<Sprite> Sever_Sprites;

}

[System.Serializable]
public class All_BonusSpriteDate
{


	public List<Reel_Sprite_Date> _BonusSpriteDate;


}
[System.Serializable]
public class intCount
{

	public List<int> _IntCount = new List<int>();

}

[System.Serializable]
public class  GridInt
{

	public List<int> _GridInt ;


}

[System.Serializable]
public class GridIntS
{

	public List<GridInt> _Grids;


}


//------------------------------------------------------------------------------------------------------------------
[System.Serializable]
public class SlotGrid
{
	int Girdcount;//盤面數
	int Rollcount;
	int ImageCount;
	[SerializeField]
	List<GridIntS> Grid;//盤面內容


	public SlotGrid(int _Girdocunt,int _ReelMove,int _ImageCount)//建構子
	{
		this.Girdcount = _Girdocunt;
		this.Rollcount = _ReelMove;
		this.ImageCount = _ImageCount;
		Grid = new List<GridIntS>();
		Reel_Initialization();

	}

	public int _REgirdcount
	{
		get { return Girdcount; }
		set { Girdcount = value; }
	}

	public List<GridIntS> _REGrids
	{
		get { return Grid; }

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
			for (int i = 0; i < Rollcount; i++)
			{
				Grid[k]._Grids.Add(new GridInt());
				Grid[k]._Grids[i]._GridInt = new List<int>();

				
				for (int j = 0; j < ImageCount; j++)
				{
					Grid[k]._Grids[i]._GridInt.Add(0);

				}

				//Debug.Log(string.Format("盤面資料生成,輪條數：{0},各輪條內圖片數量：{1}", Grid[k]._Grids.Count, ImageCount));
			}

		}

	}





}