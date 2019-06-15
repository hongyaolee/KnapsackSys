/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       锻造面板，锻造系统的核心
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PackageSys
{
	public class ForgePanel : Inventory 
	{
        private static ForgePanel instance;
        public static ForgePanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Canvas").transform.Find("ForgePanel").GetComponent<ForgePanel>();
                }
                return instance;
            }

        }
        public ProductSlot productSlot;
        public Button btnForge;
        public override void Start()
        {
            base.Start();
            slotList = GetComponentsInChildren<ForgeSlot>();
            productSlot = GetComponentInChildren<ProductSlot>();

            btnForge = transform.Find("btnForge").GetComponent<Button>();
            btnForge.onClick.AddListener(ForgeItem);
        }

        /// <summary>
        /// 锻造物品
        /// 1、得到锻造原材料列表
        /// 2、与配方列表进行匹配，得到锻造的配方
        /// 3、锻造操作，消耗原料，得到成品
        /// </summary>
        public void ForgeItem()
        {
            Formula formula = new Formula();
            //锻造材料列表，参数1：物品id，参数2：参数1对应的数量
            Dictionary<int, int> forgeList = new Dictionary<int, int>();
           
            //得到锻造槽原材料id列表
            foreach (ForgeSlot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                    forgeList.Add(itemUI.Item.Id, itemUI.Amount);
                }
            }
            //锻造槽的原料与配方列表进行匹配
            formula = formula.MatchFormula(forgeList);
            if (formula == null) return;
            //匹配成功，根据返回的配方进行合成
            foreach (ForgeSlot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    //减少原料消耗的数量
                    slot.SubItemAmount(formula.GetFormulaConsumeCountByID(slot.GetItemID()));
                    
                }
                    

            }
            //合成成品
            productSlot.StoreItemByID(formula.ProductItemID);
        }

        /// <summary>
        /// 检查提供的材料是否满足配方需求
        /// </summary>
        /// <param name="formula"></param>
        private bool CheckIsMaterialEnough(Formula formula)
        {
            //先检查锻造材料种类的个数，如果与配方中的个数不相符，则直接返回false
            int materialNum = 0;
            foreach (ForgeSlot slot in slotList)
            {
                if (slot.transform.childCount > 0)
                {
                    materialNum++;
                }
            }
            if (materialNum != formula.ItemID.Length)
                return false;
            //检查每种锻造材料的个数是否达到配方的需求，有一个数量不足，即返回false
            foreach (ForgeSlot slot in slotList)
            {
                
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                if (itemUI.Amount < formula.GetFormulaConsumeCountByID(itemUI.Item.Id))
                {
                    return false;
                }
            }

            //全部需求满足，返回true
            return true;
        }
    }
}
