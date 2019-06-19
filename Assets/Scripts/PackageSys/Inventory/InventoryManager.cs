/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       Inventory管理类
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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PackageSys
{
    
	public class InventoryManager : MonoBehaviour 
	{
        private static InventoryManager _instance;
        public static InventoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
                }
                return _instance;
            }
            set { }
        }
        public List<Item> ItemListAll = new List<Item>();
        /// <summary>
        /// 锻造配方列表
        /// List表示所有配方的集合
        /// </summary>
        public List<Formula> ForgeListAll = new List<Formula>();           
        //toolTip
        private ToolTip toolTip;
        private bool isTooltipShow=false;

        private Canvas canvas;

        private Vector2 positionOffset=new Vector2(10,-10);             //鼠标位置与tooltip的偏移量
        //pickedItem
        private bool isPickedItem = false;
        public bool IsPickedItem
        {
            get { return isPickedItem; }
        }
        private ItemUI pickedItem;                                      //鼠标选中背包中的item时显示，用于背包中item的拖拽和交换
        public ItemUI PickedItem
        {
            get { return pickedItem; }
        }
        private DiscardPanel discardPanel;

        private Button btn_Packsack;
        private Button btn_Chest;
        private Button btn_Character;
        private Button btn_Shop;
        private Button btn_Forge;
        private Button btn_Save;
        private Button btn_Load;
        
        private GameObject goPacksack;
        private GameObject goChest;
        private GameObject goCharacter;
        private GameObject goShop;
        private GameObject goForge;

        private bool isInit = false;

        private void Awake()
        {
            ItemForJson.JsonParse(ItemListAll);
            Formula.FormulaJsonParse(ForgeListAll);
        }

        private void Start()
        {
            toolTip = GameObject.FindObjectOfType<ToolTip>();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            pickedItem = canvas.transform.Find("PickedItem").GetComponent<ItemUI>();
            pickedItem.Hide();
            discardPanel = canvas.transform.Find("DiscardPanel").GetComponent<DiscardPanel>();
            //背包
            btn_Packsack = canvas.transform.Find("btn_Packsack").GetComponent<Button>();
            btn_Packsack.onClick.AddListener(OpenPacksack);
            goPacksack = canvas.transform.Find("PackagePanel").gameObject;
            goPacksack.SetActive(true);
            //仓库
            btn_Chest = canvas.transform.Find("btn_Chest").GetComponent<Button>();
            btn_Chest.onClick.AddListener(OpenChest);
            goChest = canvas.transform.Find("ChestPanel").gameObject;
            goChest.SetActive(true);
            //角色
            btn_Character = canvas.transform.Find("btn_Character").GetComponent<Button>();
            btn_Character.onClick.AddListener(OpenCharacter);
            goCharacter = canvas.transform.Find("CharacterPanel").gameObject;
            goCharacter.SetActive(true);
            //商店
            btn_Shop = canvas.transform.Find("btn_Shop").GetComponent<Button>();
            btn_Shop.onClick.AddListener(OpenShop);
            goShop = canvas.transform.Find("ShopPanel").gameObject;
            goShop.SetActive(true);
            //锻造
            btn_Forge = canvas.transform.Find("btn_Forge").GetComponent<Button>();
            btn_Forge.onClick.AddListener(OpenForge);
            goForge = canvas.transform.Find("ForgePanel").gameObject;
            goForge.SetActive(true);

            //保存
            btn_Save = canvas.transform.Find("btn_Save").GetComponent<Button>();
            btn_Save.onClick.AddListener(Save);
            //加载
            btn_Load = canvas.transform.Find("btn_Load").GetComponent<Button>();
            btn_Load.onClick.AddListener(Load);
        }
        private void Update()
        {
            if (!isInit)
            {
                goPacksack.SetActive(false);
                goChest.SetActive(false);
                goCharacter.SetActive(false);
                goShop.SetActive(false);
                goForge.SetActive(false);
                isInit = true;
            }
            if (isPickedItem)
            {
                Vector2 position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
                PickedItem.SetLocalPosition(position);
            }
            else if (isTooltipShow)
            {
                Vector2 position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
                toolTip.SetLocalPosition(position+ positionOffset);
            }
            if (isPickedItem && Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(-1)==false)
            {
                discardPanel.DiscardPanelDisplay();
            }
        }

        /// <summary>
        /// 显示tooltip
        /// </summary>
        /// <param name="text"></param>
        public void DisplayToolTip(string text)
        {
            //如果当前鼠标上有pickedItem，则不显示tooltip
            if (isPickedItem) return;

            isTooltipShow = true;
            toolTip.DisplayToolTip(text);
        }
        /// <summary>
        /// 隐藏tooltip
        /// </summary>
        public void HideToolTip()
        {
            isTooltipShow = false;
            toolTip.HideToolTip();
        }

        /// <summary>
        /// 根据物品id得到物品Item对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Item GetItemByID(int id)
        {
            foreach (var item in ItemListAll)
            {
                if (item.Id == id)
                {
                    return item;
                } 
            }
            return null;
        }

        public void SetPickedItem(ItemUI itemUI)
        {
            PickedItem.SetItemUI(itemUI);
            isPickedItem = true;
            PickedItem.Display();
            //鼠标捡起物品时不显示tooltip
            toolTip.HideToolTip();
        }
        public void SetPickedItem(Item item, int amount)
        {
            PickedItem.SetItemUI(item,amount);
            isPickedItem = true;
            PickedItem.Display();
            //鼠标捡起物品时不显示tooltip
            toolTip.HideToolTip();
        }

        public void SubPickedItemAmount(int num)
        {
            PickedItem.SubAmount(num);
        }

        /// <summary>
        /// 隐藏pickedItem
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void PickedItemPanelHide()
        {
            //pickedItem的处理
            isPickedItem = false;
            PickedItem.Hide();
            
        }
        public void PickedItemDisplay()
        {
            isPickedItem = true;
            PickedItem.Display();
        }

        public void OpenPacksack()
        {
            goPacksack.SetActive(true);
        }

        public void OpenChest()
        {
            goChest.SetActive(true);
            goPacksack.SetActive(true);
            goCharacter.SetActive(false);
            goShop.SetActive(false);
            goForge.SetActive(false);
        }
        public void OpenCharacter()
        {
            goCharacter.SetActive(true);
            goPacksack.SetActive(true);
            goChest.SetActive(false);
            goShop.SetActive(false);
            goForge.SetActive(false);
            //角色属性面板
            CharacterPanel.Instance.UpdatePlayerProperty();
        }

        public void OpenShop()
        {
            goShop.SetActive(true);
            goPacksack.SetActive(true);
            goCharacter.SetActive(false);
            goChest.SetActive(false);
            goForge.SetActive(false);
        }
        public void OpenForge()
        {
            goForge.SetActive(true);
            goPacksack.SetActive(true);
            goCharacter.SetActive(false);
            goChest.SetActive(false);
            goShop.SetActive(false);
        }

        /// <summary>
        /// 存档
        /// </summary>
        public void Save()
        {
            KnapsackPanel.Instance.Save();
            ChestPanel.Instance.Save();
            CharacterPanel.Instance.Save();
            Player.Instance.Save();
            
        }
        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            KnapsackPanel.Instance.Load();
            ChestPanel.Instance.Load();
            CharacterPanel.Instance.Load();
            Player.Instance.Load();
        }
    }
}
