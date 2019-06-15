/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       商店
*	       功能：物品的买卖
*	       涉及的功能模块：经济系统、背包系统
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

namespace PackageSys
{
	public class ShopPanel : Inventory 
	{
        private static ShopPanel instance;
        public static ShopPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Canvas").transform.Find("ShopPanel").GetComponent<ShopPanel>();
                }
                return instance;
            }
            
        }
        public int[] ItemIDArray;
        public override void Start()
        {
            base.Start();
            //商店的初始化
            InitShop();
        }

        private void InitShop()
        {
            foreach (int id in ItemIDArray)
            {
                StoreItem(id);
            }
        }
        /// <summary>
        /// 购买物品
        /// </summary>
        public void BuyItem(Item item)
        {
            bool isSuccess;
           
            //先判断钱是否足够
            if (Player.Instance.CoinAmount>=item.BuyPrice)
            {
                //背包有空间，购买成功，扣除金币
                isSuccess = KnapsackPanel.Instance.StoreItem(item);
                if (isSuccess)
                {
                    Player.Instance.ConsumeCoin(item.BuyPrice);
                }
            }
            else
            {
                Debug.Log("对不起，您的余额已不足，请滚去赚钱");
            }
        }

        /// <summary>
        /// 出售物品
        /// </summary>
        /// <param name="item"></param>
        public void SellItem()
        {
            //弹出出售面板
            SellPanel.Instance.OpenSellPanel();
            
        }
    }
}
