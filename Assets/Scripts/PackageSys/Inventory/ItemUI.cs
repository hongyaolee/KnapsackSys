/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       物品UI，挂在物品对象上（Slot的子物体）
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
	public class ItemUI : MonoBehaviour 
	{
        /// <summary>
        /// ItemUI对应的item
        /// </summary>
        public Item Item { get;private set; } 
        /// <summary>
        /// item当前数量
        /// </summary>
        public int Amount { get;private set; }

//#region UI组件(通过属性来获取UI组件，可以避免因为没有初始化而报空的情况)
        private Image itemImage;
        private Image ItemImage
        {
            get
            {
                if (itemImage == null)
                    itemImage = gameObject.GetComponent<Image>();
                return itemImage;
            }
        }
        
        private Text amountText;
        private Text AmountText
        {
            get
            {
                if (amountText == null)
                    amountText = gameObject.GetComponentInChildren<Text>();
                return amountText;
            }
        }
 //       #endregion
        private float targetScale;
        private Vector3 animateScale;
        private float smothing;

        public ItemUI(Item item,int amount) { }

        private void Start()
        {
            //为什么变量在定义时赋值无效？要在start里面才能赋值？？？？？？？？？？
            targetScale = 1;
            animateScale = new Vector3(1.2f, 1.2f, 1.2f);
            smothing = 3;
        }

        private void Update()
        {
            //鼠标放在物品槽时的动画
            if (transform.localScale.x != targetScale)
            {
                //动画
                float scale = Mathf.Lerp(transform.localScale.x, targetScale, smothing * Time.deltaTime);
                //update里面new对象是否合适？new太多会增加GC负担，是否有代替方案？
                transform.localScale = new Vector3(scale,scale,scale);
                if (Mathf.Abs(transform.localScale.x - targetScale) < 0.02f)
                {
                    transform.localScale = new Vector3(targetScale, targetScale, targetScale);
                }
            }
        }

        /// <summary>
        /// 设置itemUI
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void SetItemUI(Item item)
        {
            transform.localScale = animateScale;
            Item = item;
            //更新UI
            SetSprite();
            if (Amount > 1)
                AmountText.text = Amount.ToString();
            else
                AmountText.text = "";
        }

        public void SetItemUI(Item item, int amount)
        {
            Amount = amount;
            SetItemUI(item);
        }

        /// <summary>
        /// 设置itemUI，此方法主要用在pickedItem对象
        /// </summary>
        /// <param name="itemUI"></param>
        public void SetItemUI(ItemUI itemUI)
        {
            transform.localScale = animateScale;
            Item = itemUI.Item;
            Amount = itemUI.Amount;
            //更新UI
            SetSprite();
            UpdateAmount();
        }

        /// <summary>
        /// 更新Amount的UI
        /// </summary>
        private void UpdateAmount()
        {
            if (Amount > 1)
                AmountText.text = Amount.ToString();
            else
                AmountText.text = "";
        }
        /// <summary>
        /// item数量增加
        /// </summary>
        /// <param name="amount"></param>
        public void AddAmount(int amount = 0)
        {
            transform.localScale = animateScale;
            Amount += amount;
            UpdateAmount();
        }

        public void SubAmount(int amount)
        {
            transform.localScale = animateScale;
            Amount -= amount;
            UpdateAmount();
        }

        public void Display()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 设置itemUI的位置，主要用在item的拖拽和交换时跟随鼠标移动
        /// </summary>
        /// <param name="position"></param>
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        private void SetSprite()
        {
            ItemImage.sprite = Resources.Load<Sprite>(Item.Sprite);
        }


	}
}
