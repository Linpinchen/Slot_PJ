using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeverScript 
{
	//帶修改 將BonusCount (Bonus圖出現次數) 以及 ShineReel（要閃圖的輪條） 都跟著放在SeverDate這個Class 讓Sever 傳去給Date腳本接

	public int  Bonus_count;//Bonus圖出現次數

	public int _GridReel;//最終盤面輪條數量

	public int ShineReel;//要閃圖的輪條

	public List<int> _ImageCount;//最終盤面各輪條內的圖片數量

	public GridIntS SeverGrids;//最終盤資料


	public SeverDate _SeverDate;//要轉成Json傳到客端的內容


	//讓Sever 傳 Json 出來 要傳一個 盤面陣列的資料  陣列[0]等於普盤 [1~3]是Bonus盤



	public SeverScript(int _GridReel, List<int> _ImageCount)
	{

		
		this._GridReel = _GridReel;

		this._ImageCount = _ImageCount;

		Bonus_count = 0;

		SeverGrids = new GridIntS();
		SeverGrids._Grids = new List<GridInt>();

		for (int i = 0; i < _GridReel; i++)
		{
			

			SeverGrids._Grids.Add(new GridInt());


			SeverGrids._Grids[i]._GridInt = new List<Pool_Images>();

			for (int j = 0; j < _ImageCount[i]; j++)
			{
				
				SeverGrids._Grids[i]._GridInt.Add(Pool_Images.Fish);

			}

		}


		_SeverDate = new SeverDate();

	}



	#region 普通盤面生成灌入
	/// <summary>
	/// 生成圖片資料
	/// </summary>
	public void Generate_Date_Sprite()
	{
		
		for (int i = 0; i < SeverGrids._Grids.Count; i++)//輪條數
		{

			int Temp = SeverGrids._Grids[i]._GridInt.Count;//輪條圖片數

			if (i == 0)//第一個輪條  
			{
				// 需要灌入的圖片資料
				List<Pool_Images> _PoolImages = new List<Pool_Images>();
				foreach (Pool_Images p in System.Enum.GetValues(typeof(Pool_Images)))
				{
					_PoolImages.Add(p);
				}

				_PoolImages.RemoveAt((int)Pool_Images.Universal_Sprite);//輪條1不會有連線共同圖
				int _One_Sprite_pool_Temp = _PoolImages.Count - 1;//第一輪用的陣列少掉Bonus圖

				for (int j = 0; j < Temp; j++)
				{

					if (SeverGrids._Grids[i]._GridInt.Contains(Pool_Images.Bonus))//已經有Bonus圖的話 就不要再出現Bonus圖了
					{
						int Temp_NoBonus;
						Temp_NoBonus = Random.Range(0, _One_Sprite_pool_Temp);
						SeverGrids._Grids[i]._GridInt[j] = _PoolImages[Temp_NoBonus];

					}
					else
					{

						int Tempj = Random.Range(0, _PoolImages.Count);
						SeverGrids._Grids[i]._GridInt[j] = _PoolImages[Tempj];

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
					if (SeverGrids._Grids[i]._GridInt.Contains(Pool_Images.Bonus) || Bonus_count >= 3)//已經有Bonus圖的話或是盤面上已經有3個Bonus 就不要再出現Bonus圖了,
					{

						int Tempj = Random.Range(0, Pool_Temp);
						SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Tempj;//填入的圖會少了Bonus圖

					}
					else
					{

						int Tempj = Random.Range(0, System.Enum.GetNames(typeof(Pool_Images)).Length);
						SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Tempj;//完整的圖庫

					}

				}

			}
			//檢查出現的Bonus圖是否在 輪條[1] [2] [3]的位置
			for (int Slot_i = 1; Slot_i < 4; Slot_i++)
			{

				if (SeverGrids._Grids[i]._GridInt[Slot_i] == Pool_Images.Bonus)
				{
					Bonus_count++;//有出現Bonus圖就++
					if (Bonus_count == 2)
					{
						ShineReel = i;

					}
					Debuger.Log("大獎數量 ：" + Bonus_count);

				}

			}

		}

		//將計算完成的盤面 轉成Json 放進 SeverDate的 List<String> 中 做回傳
		string ret = JsonUtility.ToJson(SeverGrids);
		_SeverDate.SeverJson.Add(ret);

	}
	#endregion



	#region Bonus 盤面資料生成灌入
	/// <summary>
	/// Bonus 盤面資料生成
	/// </summary>
	/// <param name="slotGrid"></param>
	public void GenerateBonusDate(int FreeGameCount)//Bonus所有盤面生成 
	{
		int SpecialCount = 2;

		for (int i = 0; i < FreeGameCount; i++)
		{

			for (int j = 0; j <SeverGrids._Grids.Count; j++)
			{

				int tempi = SeverGrids._Grids[j]._GridInt.Count;

				if (j == 0)//第一個輪條 不會出現共用圖
				{

					for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
					{

						int Tempcount;
						int SpriteCountTemp;
						Tempcount = System.Enum.GetNames(typeof(Pool_Images)).Length - SpecialCount;
						SpriteCountTemp = Random.Range(0, Tempcount);
						SeverGrids._Grids[j]._GridInt[SpriteCount] = (Pool_Images)SpriteCountTemp;

					}

				}
				else
				{

					for (int SpriteCount = 0; SpriteCount < tempi; SpriteCount++)
					{

						int Tempcount;
						int SpriteCountTemp;
						Tempcount = System.Enum.GetNames(typeof(Pool_Images)).Length - 1;
						SpriteCountTemp = Random.Range(0, Tempcount);
						SeverGrids._Grids[j]._GridInt[SpriteCount] = (Pool_Images)SpriteCountTemp;

					}

				}

			}

			string ret = JsonUtility.ToJson(SeverGrids);
			_SeverDate.SeverJson.Add(ret);
		}


		

	}
	#endregion



	#region 測試用（指定盤面獲得Bonus獎）
	/// <summary>
	/// 測試用（直接有Bonus獎）
	/// </summary>
	/// <param name="BtnTrue"></param>
	/// <param name="Generate_ReelScripts"></param>
	public void Add_BonusDate(bool BtnTrue)
	{
		int SPecialImgCount = 2;

		if (BtnTrue)
		{

			for (int i = 0; i < SeverGrids._Grids.Count; i++)
			{
				
				int Temp = SeverGrids._Grids[i]._GridInt.Count;
				int _One_Sprite_pool_Temp = System.Enum.GetNames(typeof(Pool_Images)).Length - SPecialImgCount;//第一輪用的陣列少掉特殊圖

				if (i == 0)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 1)
						{

							SeverGrids._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{

							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}

					}

				}
				else if (i == 1)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 2)
						{

							SeverGrids._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{

							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}

					}

				}
				else if (i == 2)
				{

					for (int j = 0; j < Temp; j++)
					{

						if (j == 3)
						{
							SeverGrids._Grids[i]._GridInt[j] = Pool_Images.Bonus;

						}
						else
						{
							int Temp_B;
							Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
							SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

						}

					}

				}

				else
				{

					for (int j = 0; j < Temp; j++)
					{

						int Temp_B;
						Temp_B = Random.Range(0, _One_Sprite_pool_Temp);
						SeverGrids._Grids[i]._GridInt[j] = (Pool_Images)Temp_B;

					}

				}

			}

			Bonus_count = 3;
			ShineReel = 1;
		}


		string ret = JsonUtility.ToJson(SeverGrids);
		_SeverDate.SeverJson.Add(ret);
		

	}
	#endregion


	#region Sever盤面生成
	/// <summary>
	/// 生成Sever 盤面 並回傳一個 由包含List<String>的Class轉成的Json資料
	/// </summary>
	/// <param name="StAddBonusDate"></param>
	/// <param name="FreeGameCount"></param>
	/// <returns></returns>
	public string SeverGridCreat(bool StAddBonusDate,int FreeGameCount)
	{
		ShineReel = 0;

		Bonus_count = 0;//預設大獎中獎圖數為0


		_SeverDate.SeverJson.Clear();
		


		if (StAddBonusDate)//如果有按下 直接中大獎按鈕
		{

			Add_BonusDate(StAddBonusDate);//生成直接中Bonus的盤面資料

		}
		else
		{
			Generate_Date_Sprite();//生成普通盤面資料

		}

		if (Bonus_count == 3)//如果有Bonus獎
		{


			GenerateBonusDate(FreeGameCount);//Bonus盤 生成盤面


		}



		if (_SeverDate.SeverJson == null)
		{
			Debug.Log("!!!!!!!!!!  _SeverDate.SeverJson==null  !!!!!!!!!!!!!!!!!");

		}
		else
		{
			Debug.Log("!!!!!!!!!!  _SeverDate.SeverJson!=null  !!!!!!!!!!!!!!!!!");
			Debug.Log("長度為 ：" + _SeverDate.SeverJson.Count);
		}



		string ret = JsonUtility.ToJson(_SeverDate);
		

		return ret;
	}
	#endregion

	/// <summary>
    /// 回傳當前BonusCount
    /// </summary>
    /// <returns></returns>
	public int RetBonusCount()
	{
		return Bonus_count;

	}
	public int RetShineReel()
	{

		return ShineReel;
	}



}


public class SeverDate
{

	public List<string> SeverJson;


	public SeverDate()
	{

		SeverJson = new List<string>();


	}


}
