using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlayer
{
    public const string ALL_DATA = "all_data";
    public const string ALL_DATA1 = "all_data1";
    public const string ALL_DATA2 = "all_data2";
    private static ALLData allData;
    private static ALLData1 allData1;
    private static ALLData2 allData2;
    static DataPlayer()
    {
        allData = JsonUtility.FromJson<ALLData>(PlayerPrefs.GetString(ALL_DATA));
        allData1 = JsonUtility.FromJson<ALLData1>(PlayerPrefs.GetString(ALL_DATA1));
        allData2 = JsonUtility.FromJson<ALLData2>(PlayerPrefs.GetString(ALL_DATA2));
        if (allData == null)
        {
            var leveldefault = 1;
            allData = new ALLData
            {
                levelList = new List<int> { leveldefault },
            };
            SaveData();

        }
        if (allData1 == null)
        {
            var leveldefault1 = 1;
            allData1 = new ALLData1
            {
                DGlevelList = new List<int> { leveldefault1 },
            };
            SaveData1();

        }
        if (allData2 == null)
        {
            //bool leveldefault2 = false;
            allData2 = new ALLData2
            {
                CheckListLevel = new bool[20],
                //ChecklistLevel[1] = leveldefault2;
                //CheckListLevel[0] = leveldefault2;
            };

            SaveData2();

        }

    }

    private static void SaveData()
    {
        var data = JsonUtility.ToJson(allData);
        PlayerPrefs.SetString(ALL_DATA, data);
    }
    private static void SaveData1()
    {
        var data1 = JsonUtility.ToJson(allData1);
        PlayerPrefs.SetString(ALL_DATA1, data1);
    }
    private static void SaveData2()
    {
        var data2 = JsonUtility.ToJson(allData2);
        PlayerPrefs.SetString(ALL_DATA2, data2);
    }

    public static void Add_Level(int id)
    {
        allData.addLevel(id);
        SaveData();
    }
    public static void Add_ListCheckLevel(int index,bool value)
    {
        allData2.addCheckList(index, value);
        SaveData2();
    }
    public static void Add_DG (int DG)
    {
        allData1.addDG(DG);
        SaveData1();
    }

    public static void Remove_Level()
    {
        allData.RemoveLevel();
        SaveData();
    }

    public static void Remove_DG()
    {
        allData1.RemoveDG();
        SaveData1();
    }
    public static List<int> getLevel()
    {
        return allData.getListlevel();
    }

    public static List<int> getDG()
    {
        return allData1.getListDG();
    }

    public static bool[] getCheckList()
    {
        return allData2.getListCheckLevel();
    }
    //public static List SetListBT()
}

public class ALLData
{
    public List<int> levelList;

    public void addLevel(int id)
    {
        levelList.Add(id);

    }
    public void RemoveLevel()
    {
        levelList.Remove(levelList.Count - 1);
    }
    public List<int> getListlevel()
    {
        return levelList;
    }

}

public class ALLData1
{
    public List<int> DGlevelList;

    public void addDG(int id)
    {
        DGlevelList.Add(id);

    }
    public void RemoveDG()
    {
        DGlevelList.RemoveAt(DGlevelList.Count - 1);
    }

    public List<int> getListDG()
    {
        return DGlevelList;
    }
}

public class ALLData2
{
    public bool[] CheckListLevel;

    public void addCheckList(int index,bool value)
    {
        CheckListLevel[index] = value;

    }


    public bool[] getListCheckLevel()
    {
        return CheckListLevel;
    }
}