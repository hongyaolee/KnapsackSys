/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       解析锻造配方json配置文件
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PackageSys
{
    [Serializable]
    public class Formula
    {
        public int[] ItemID;
        public int[] ItemAmount;
        public int ProductItemID;

        public Formula(int[] itemId, int[] itemAmount, int productItemId)
        {
            ItemID = itemId;
            ItemAmount = itemAmount;
            ProductItemID = productItemId;
        }
        public Formula() { }

        /// <summary>
        /// 配方匹配，锻造系统的材料是否满足当前配方的锻造需求
        /// 匹配要求：1、种类一致
        ///           2、提供的材料满足配方中每种材料数量需求
        /// </summary>
        /// <param name="matchList">锻造系统传过来的待匹配id列表</param>
        public Formula MatchFormula(Dictionary<int,int> matchList)
        {
            List<Formula> formulaList = InventoryManager.Instance.ForgeListAll;
            foreach (Formula formula in formulaList)
            {
                //匹配两个列表长度是否相同
                if (formula.ItemID.Length == matchList.Count)
                {
                    //记录匹配的物品数量，列表中成功匹配一个，数量+1
                    int count=0;
                    //对配方每一个id是否都包含在锻造id列表中
                    for (int i = 0; i < formula.ItemID.Length; i++)
                    {
                        if (matchList.ContainsKey(formula.ItemID[i]))
                        {
                            //如果提供的材料不足配方的需求，则返回false，锻造失败
                            if (formula.ItemAmount[i] > matchList[formula.ItemID[i]])
                                return null;
                            count++;
                        }
                    }
                    //配方所有材料匹配成功
                    if (count == formula.ItemID.Length)
                        return formula;
                }
                
            }
            return null;
        }
        /// <summary>
        /// 根据id得到该id对应的消耗数量
        /// </summary>
        /// <param name="id"></param>
        public int GetFormulaConsumeCountByID(int id)
        {
            for (int i = 0; i < ItemID.Length; i++)
            {
                if (id == ItemID[i])
                {
                    return ItemAmount[i];
                }
            }
            return -1;
        }

        /// <summary>
        /// 解析Formula的json，并将解析数据存到配方集合中
        /// </summary>
        /// <param name="FormulaListAll"></param>
        public static void FormulaJsonParse(List<Formula> FormulaListAll)
        {
            TextAsset formulaListText = Resources.Load<TextAsset>("FormulaListJson");
            //Debug.Log(formulaListText);
            FormulaStruct formulaArray = JsonUtility.FromJson<FormulaStruct>(formulaListText.text);
            foreach (var item in formulaArray.FormulaList)
            {
                Formula formula = new Formula();
                formula.ItemID = item.ItemID;
                formula.ItemAmount = item.ItemAmount;
                formula.ProductItemID = item.ProductItemID;
                FormulaListAll.Add(formula);
            }
        }
    }
    [Serializable]
    public class FormulaStruct
    {
        /// <summary>
        /// FormulaList
        /// 参数1：ItemID
        /// 参数2：ItemAmount
        /// </summary>
        //public Dictionary<int, int> FormulaList = new Dictionary<int, int>();
        
        public List<Formula> FormulaList = new List<Formula>();
    }

}
