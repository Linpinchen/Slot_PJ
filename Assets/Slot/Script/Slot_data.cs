using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_data {
	public int Coin;//玩家擁有的錢
	public int Bet_Coin;//押注的錢
	public Sprite[] Sprite_Pool;//圖片庫
	public int Bonus_count;//當前 Bonus圖片出現的數量（用來算每個輪條[2][3][4]Bonus圖片出現幾次）
	public Slot_SeveDate _Slot_SeverDate;
	public Slot_SeveDate GetSlotDate;
	public List<intCount> _Date;

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
    public void Date_Save(SlotGrid _SlotGrid)
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
public class  GridInt
{

	public List<Pool_Images> _GridInt ;

  
}

[System.Serializable]
public class GridIntS
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