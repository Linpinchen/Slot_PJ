using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictionaryToJson<Tkey, Tvalue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<Tkey> Keys;

    [SerializeField]
    List<Tvalue> Values;

    Dictionary<Tkey, Tvalue> MyDictionary;



    public DictionaryToJson(Dictionary<Tkey,Tvalue> MyDictionary )
    {

        this.MyDictionary = MyDictionary;


    }


    public Dictionary<Tkey, Tvalue> ToDictionary ()
    {



        return MyDictionary;


    }



    public void OnBeforeSerialize()
    {

        Keys = new List<Tkey>(MyDictionary.Keys);
        Values = new List<Tvalue>(MyDictionary.Values);
       
    }


    public void OnAfterDeserialize()
    {

        var count = System.Math.Min(Keys.Count,Values.Count);

        MyDictionary = new Dictionary<Tkey, Tvalue>(count);

        for ( var i=0;i<count;i++)
        {
            MyDictionary.Add(Keys[i],Values[i]);

        }


    }


}
