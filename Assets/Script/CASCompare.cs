using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 放雲端Txt的Bundle資料 與本地比對
/// </summary>
public struct SeverBundleDate
{
    public string[] Name;
    public string[] URL;
    public string[] Version;
    public string Type;


}


/// <summary>
/// 用來當字典查詢得值 ,雲端上TxT解析出來 的Name 是 Ｋey ,這個Class 是Value
/// </summary>
[System.Serializable]
public class ClientBundleDate
{

    [SerializeField]
    public string CLPath;//要讀取的本地路徑
    [SerializeField]
    public string Version;//紀錄版號 ,用於跟雲端上Txt解析的版號做比對
    [SerializeField]
    public string ObjectType;//物件類型


}




public class CASCompare : MonoBehaviour
{


    public Dictionary<string, ClientBundleDate> Di_BundleDate;


    public SeverBundleDate[] bundleDate;


    public IEnumerator _CopySeverDate;

    string TxtURL;

    public Text Errortxt;

    public void init(string TxtURL,Text Errortxt)
    {
        //Manager 設一個String 利用平台偵測 來改變 String 要給予的Txt網址
        this.TxtURL = TxtURL;

        Di_BundleDate = new Dictionary<string, ClientBundleDate>();

        this.Errortxt = Errortxt;

    }



    //測試用 直接取得 字典的值（有用正式的 先下載過雲端上的資料 才來做測試的）
    public IEnumerator CheckAssetBundle()
    {
        string playerPrefs_Key = "Date_Playerprefs";

        if (PlayerPrefs.HasKey(playerPrefs_Key))
        {

            Di_BundleDate = JsonUtility.FromJson<DictionaryToJson<string, ClientBundleDate>>(PlayerPrefs.GetString(playerPrefs_Key)).ToDictionary();//將PlayerPrefs的字串資料,由Json轉回Dictionary

            yield return null;
        }


    }








    ///// <summary>
    /////  //正式使用 - 要抓雲端上的資料來判斷
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerator CheckAssetBundle()
    //{
    //    bool Change_Date;//是否有修改資料
    //    Change_Date = false;


    //    UnityWebRequest request = UnityWebRequest.Get(TxtURL);
    //    yield return request.SendWebRequest();
    //    if (request.isNetworkError)
    //    {

    //        Debug.Log(request.error);

    //    }
    //    else
    //    {

    //        //-----------------------------------------------------------
    //        //建立本地資料夾
    //        string foldername = "MyBundle";
    //        string folderpath = Application.persistentDataPath + "/" + foldername;
    //        if (!Directory.Exists(folderpath))
    //        {

    //            Directory.CreateDirectory(folderpath);
    //            Debug.Log("建立資料夾");
    //        }


    //        Debug.Log("雲端Txt檔 拆解資料");
    //        string s = request.downloadHandler.text.Trim('{', '}', '\n');//先取得全部內容(去除頭尾{}以及 去除完{}空白的那行)
    //        string[] Lines = s.Split(new string[] { "[DateEnd]" }, System.StringSplitOptions.RemoveEmptyEntries);//每個分段做切割放進陣列

    //        if (Lines.Length == 1)
    //        {
    //            Errortxt.text = "載入資料錯誤！！";
    //            Errortxt.transform.GetChild(0).GetComponent<Text>().text = s;
    //            Debug.Log(s);
    //        }
    //        else
    //        {
    //            for (int l = 0; l < Lines.Length; l++)
    //            {
    //                Debug.Log(Lines[l]);
    //                Lines[l] = Lines[l].Trim('\n');
    //                Debug.Log("Trim('\n') :" + Lines[l]);

    //            }
    //            Debug.Log("Lines.Length:" + Lines.Length);
    //            bundleDate = new SeverBundleDate[Lines.Length];

    //            for (int i = 0; i < Lines.Length; i++)
    //            {
    //                int j = 0;
    //                string[] chi_Lines = Lines[i].Split('\n');
    //                bundleDate[i].Type = chi_Lines[0];
    //                bundleDate[i].Name = new string[(chi_Lines.Length - 1) / 3];//去掉頭的string Type  /3的用意是每份資料 都有Name跟URL與Verison
    //                bundleDate[i].URL = new string[(chi_Lines.Length - 1) / 3];//去掉頭的string Type /3的用意是每份資料 都有Name跟URL與Verison
    //                bundleDate[i].Version = new string[(chi_Lines.Length - 1) / 3];//去掉頭的string Type /3的用意是每份資料 都有Name跟URL與Verison

    //                for (int k = 1; k < chi_Lines.Length; k++)//去掉頭--> string Type 所以從1開始
    //                {

    //                    if (k % 3 == 1)
    //                    {
    //                        Debug.Log("k" + k);
    //                        string name = chi_Lines[k].Remove(0, 7);
    //                        bundleDate[i].Name[j] = name;
    //                        Debug.Log(bundleDate[i].Name[j]);

    //                    }
    //                    else if (k % 3 == 2)
    //                    {
    //                        Debug.Log("k" + k);
    //                        string ss = chi_Lines[k].Remove(0, 6);
    //                        string s2 = ss.Trim('"');
    //                        Debug.Log("S2 :" + s2);
    //                        bundleDate[i].URL[j] = s2;
    //                        Debug.Log(bundleDate[i].URL[j]);
    //                    }
    //                    else
    //                    {
    //                        Debug.Log("k" + k);
    //                        string ss = chi_Lines[k].Remove(0, 10);
    //                        string s3 = ss.Trim('"');
    //                        Debug.Log("S3 :" + s3);
    //                        bundleDate[i].Version[j] = s3;
    //                        Debug.Log(bundleDate[i].Version[j]);
    //                        j++;

    //                    }

    //                }

    //                Debug.Log("雲端Txt檔 拆解資料完成");
    //                for (int ii = 0; ii < bundleDate[i].Name.Length; ii++)
    //                {

    //                    Debug.Log("bundleDate.Name:" + bundleDate[i].Name[ii]);

    //                    Debug.Log(" bundleDate.URL:" + bundleDate[i].URL[ii]);

    //                    Debug.Log(" bundleDate.Version:" + bundleDate[i].Version[ii]);
    //                }



    //            }


    //            //判斷遊戲是否有 AssetBundle的 字典資料
    //            string playerPrefs_Key = "Date_Playerprefs";
    //            if (PlayerPrefs.HasKey(playerPrefs_Key))
    //            {
    //                Debug.Log("PlayerPrefs有字典資料");
    //                Errortxt.transform.GetChild(0).GetComponent<Text>().text = "PlayerPrefs有字典資料";
    //                //如果有 讀取PlayerPrefs的字串資料 將字串資料返回Json 利用字典來接收資料


    //                Di_BundleDate = JsonUtility.FromJson<DictionaryToJson<string, ClientBundleDate>>(PlayerPrefs.GetString(playerPrefs_Key)).ToDictionary();//將PlayerPrefs的字串資料,由Json轉回Dictionary

    //                for (int k = 0; k < bundleDate.Length; k++)
    //                {

    //                    for (int i = 0; i < bundleDate[k].Name.Length; i++)//迴圈取得從Txt檔拆解的bundleDate.Name 拿來跟字典做比對
    //                    {


    //                        if (Di_BundleDate.ContainsKey(bundleDate[k].Name[i]))//判斷字典內 是否有 bundleDate.Name第i個值 這個名稱的Key
    //                        {
    //                            Debug.Log("字典內有對應Key");
    //                            Errortxt.transform.GetChild(0).GetComponent<Text>().text += "字典內有對應Key";
    //                            string CL_Verson = Di_BundleDate[bundleDate[k].Name[i]].Version;
    //                            string SE_Version = bundleDate[k].Version[i];

    //                            //比對完字典內是否有Key
    //                            //接著比對本地（字典內）的Version 與 雲端（Txt拆解獲得）得Version 是否相同
    //                            if (CL_Verson == SE_Version)//如果兩者相同
    //                            {

    //                                Debug.Log("字典內有對應Key,且版號相同");
    //                                Errortxt.transform.GetChild(0).GetComponent<Text>().text += "字典內有對應Key,且版號相同";
    //                                continue;

    //                            }
    //                            else// 本地（字典內）與 雲端（Txt拆解獲得）得Version 不相同
    //                            {
    //                                //1.將本地（字典內）的Version 更新成  雲端（Txt拆解獲得）得Version
    //                                //2.覆蓋掉本地舊的 AssetBundle資料
    //                                Debug.Log("字典內指定Key的Value中 , Version與雲端資料不同 開始更新Version以及更新本地AssetBundle資料");
    //                                Errortxt.transform.GetChild(0).GetComponent<Text>().text += "字典內指定Key的Value中 , Version與雲端資料不同 開始更新Version以及更新本地AssetBundle資料";
    //                                Debug.Log(Di_BundleDate[bundleDate[k].Name[i]].Version);


    //                                Di_BundleDate[bundleDate[k].Name[i]].Version = bundleDate[k].Version[i];//將本地（字典內）的Version 更新成  雲端（Txt拆解獲得）得Version

    //                                string path = bundleDate[k].URL[i];


    //                                _CopySeverDate = CopySeverDate(Di_BundleDate[bundleDate[k].Name[i]], path, folderpath, bundleDate[k].Name[i]);

    //                                yield return StartCoroutine(_CopySeverDate);

    //                                Change_Date = true;

    //                            }
    //                        }
    //                        else//字典內 沒有 bundleDate.Name[i] 這個值 的Key
    //                        {

    //                            Debug.Log("字典內無對應Key ,開始新增字典資料");
    //                            Errortxt.transform.GetChild(0).GetComponent<Text>().text += "字典內無對應Key ,開始新增字典資料";

    //                            //字典新增資料
    //                            //複製 雲端上 AssetBundle的 檔案到 本地

    //                            ClientBundleDate _BundleURl = new ClientBundleDate();
    //                            _BundleURl.Version = bundleDate[k].Version[i];
    //                            //_BundleURl.CLPath = bundleDate[k].URL[i];
    //                            _BundleURl.ObjectType = bundleDate[k].Type;

    //                            //讀取雲端資料 複製到本地端
    //                            string path = bundleDate[k].URL[i];

    //                            _CopySeverDate = CopySeverDate(_BundleURl, path, folderpath, bundleDate[k].Name[i]);

    //                            yield return StartCoroutine(_CopySeverDate);


    //                            Di_BundleDate.Add(bundleDate[k].Name[i], _BundleURl);//新增字典資料

    //                            Change_Date = true;


    //                        }



    //                    }

    //                }



    //            }
    //            else //如果PlayerPrefs沒有 AssetBundle的 字串資料 這裡讀取雲端上的Txt檔 然後利用迴圈 將讀取的檔案分段寫入 BundleURl 這個Class 然後 AssetBundle的名字當Key Class當Value寫入字典
    //            {
    //                Debug.Log("PlayerPrefs無字典資料 ,開始新增字典資料");
    //                Errortxt.transform.GetChild(0).GetComponent<Text>().text += "PlayerPrefs無字典資料 ,開始新增字典資料";
    //                //Di_BundleDate = new Dictionary<string, BundleURl>();
    //                for (int k = 0; k < bundleDate.Length; k++)
    //                {

    //                    for (int i = 0; i < bundleDate[k].Name.Length; i++)
    //                    {
    //                        int ii = i;
    //                        //建立字典資料
    //                        ClientBundleDate _BundleURl = new ClientBundleDate();
    //                        _BundleURl.Version = bundleDate[k].Version[ii];
    //                        _BundleURl.ObjectType = bundleDate[k].Type;
    //                        //Di_BundleDate.Add(bundleDate[k].Name[ii], _BundleURl);

    //                        //讀取雲端資料 複製到本地端
    //                        string path = bundleDate[k].URL[ii];

    //                        _CopySeverDate = CopySeverDate(_BundleURl, path, folderpath, bundleDate[k].Name[ii]);

    //                        yield return StartCoroutine(_CopySeverDate);


    //                        Di_BundleDate.Add(bundleDate[k].Name[i], _BundleURl);//新增字典資料


    //                    }

    //                }

    //                Change_Date = true;

    //            }



    //            foreach (var key in Di_BundleDate.Keys)
    //            {

    //                Debuger.Log(string.Format("Name : {0},Di_BundleDatey字典是否有Key : {1} , Value中的本地Path ：{2},Value中的version ：{3} , Value中的 類型 ：{4}", key, Di_BundleDate.ContainsKey(key), Di_BundleDate[key].CLPath, Di_BundleDate[key].Version, Di_BundleDate[key].ObjectType));


    //            }



    //            //這裡判斷字典內容是否有做修改
    //            if (Change_Date)//有做修改
    //            {
    //                Debug.Log("更新字典內容");
    //                Errortxt.transform.GetChild(0).GetComponent<Text>().text += "更新字典內容";
    //                string str = JsonUtility.ToJson(new DictionaryToJson<string, ClientBundleDate>(Di_BundleDate));
    //                Debug.Log("Json資料 ： " + str);
    //                PlayerPrefs.SetString(playerPrefs_Key, str);// PlayerPrefs儲存 接Json的字串

    //                PlayerPrefs.Save();//保持好習慣,手動儲存,以防萬一



    //            }




    //        }


    //    }

    //}




    public IEnumerator CopySeverDate(ClientBundleDate bundleDate,string path,string folderpath,string filename)
    {

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);
        
        yield return unityWebRequest.SendWebRequest();

        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            Debug.Log("www.error :" + unityWebRequest.error);
           
            yield break;//直接跳出這個方法 

        }
        else
        {

            byte[] results = unityWebRequest.downloadHandler.data;
            FileInfo fileInfo = new FileInfo(folderpath + "/" + filename);//FileInfo封裝了檔案的路徑和名稱,可以使用它來獲取檔案的絕對路徑
            FileStream fs = fileInfo.Create();//FileInfo.Creat 用來創建一個新的空文件 , FileStream用於從檔案中讀取與寫入數據
                                              //fs.Write(byte[]內容,開始位置,數據長度);
            fs.Write(results, 0, results.Length);//寫入資料內容
            fs.Flush();//將資料寫入並儲存到硬碟中
            fs.Close();//關閉文件對象
            fs.Dispose();//銷毀文件對象

            bundleDate.CLPath = fileInfo.FullName;//寫入本地讀取路徑

           

        }





    }



}

