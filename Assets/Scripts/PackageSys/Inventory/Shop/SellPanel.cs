/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       商店的出售面板，从属与商店面板，可供玩家进行出售相关操作
*	       1、修改出售的数量（通过+和-进行）
*	       2、确定和取消按钮
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
    public class SellPanel : MonoBehaviour
    {
        private static SellPanel instance;
        public static SellPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = instance = GameObject.Find("Canvas").transform.Find("ShopPanel/SellPanel").GetComponent<SellPanel>();
                }
                return instance;
            }
        }
        private Button btnAdd;
        private Button btnSub;
        private Button btnConfirm;
        private Button btnCancel;
        private Text txtSellNum;            //显示的出售数量
        private int sellNum;                //出售的数量
        private Item sellItem;              //出售的物品
        private int sellItemAmount;         //出售物品当前持有个数
        private bool isInit=false;

        private void OnEnable()
        {
            if (!isInit)
            {
                btnAdd = transform.Find("SellNum/Add").GetComponent<Button>();
                btnSub = transform.Find("SellNum/Sub").GetComponent<Button>();
                btnConfirm = transform.Find("Confirm").GetComponent<Button>();
                btnCancel = transform.Find("Cancel").GetComponent<Button>();
                txtSellNum = transform.Find("SellNum/InputField/Text").GetComponent<Text>();

                btnAdd.onClick.AddListener(AddNum);
                btnSub.onClick.AddListener(SubNum);
                btnConfirm.onClick.AddListener(Confirm);
                btnCancel.onClick.AddListener(Cancel);
                isInit = true;
            }
            
        }

        /// <summary>
        /// 打开出售面板，需要传入一个持有物品的总数
        /// </summary>
        /// <param name="sellNum"></param>
        public void OpenSellPanel()
        {
            gameObject.SetActive(true);
            sellNum = 1;
            sellItem = InventoryManager.Instance.PickedItem.Item;
            sellItemAmount = InventoryManager.Instance.PickedItem.Amount;
            txtSellNum.text = sellNum.ToString();
            InventoryManager.Instance.PickedItemPanelHide();
            
        }

        private void AddNum()
        {
            if (sellNum < sellItemAmount)
            {
                sellNum++;
                txtSellNum.text = sellNum.ToString();
            }
        }
        private void SubNum()
        {
            if (sellNum > 1)
            {
                sellNum--;
                txtSellNum.text = sellNum.ToString();
            }
            
        }

        private void Confirm()
        {
            //计算出售总价格
            Player.Instance.GetCoin(sellItem.SellPrice * sellNum);
            //处理持有物品的数量显示
            sellItemAmount -= sellNum;
            //持有物品全部卖出，直接隐藏pickedItem
            //if (sellItemAmount == 0)
            //{
            //    InventoryManager.Instance.PickedItemPanelHide();
            //}
            //持有物品没有全部卖出，显示剩余数量
            //else
            if(sellItemAmount>0)
            {
                InventoryManager.Instance.SetPickedItem(sellItem, sellItemAmount);
                //InventoryManager.Instance.SubPickedItemAmount(sellNum);
            }
                
            gameObject.SetActive(false);
        }

        private void Cancel()
        {
            InventoryManager.Instance.PickedItemDisplay();
            gameObject.SetActive(false);
        }

    }
}
