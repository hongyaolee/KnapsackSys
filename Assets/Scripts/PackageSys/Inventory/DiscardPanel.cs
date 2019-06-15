/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/

using UnityEngine;
using UnityEngine.UI;

namespace PackageSys
{
	public class DiscardPanel : MonoBehaviour 
	{
        //public static DiscardPanel instance;
        //public static DiscardPanel Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new DiscardPanel();
        //        }
        //        return instance;
        //    }
        //}
        
        private Button btn_Yes;
        private Button btn_No;

        private void OnEnable()
        {
            btn_Yes = transform.Find("btn_Yes").GetComponent<Button>();
            btn_No = transform.Find("btn_No").GetComponent<Button>();
            btn_Yes.onClick.AddListener(DiscardYes);
            btn_No.onClick.AddListener(DiscardNo);
        }
        private void OnDisable()
        {
            btn_Yes.onClick.RemoveAllListeners();
        }

        private void DiscardYes()
        {
            InventoryManager.Instance.PickedItemPanelHide();
            DiscardPanelHide();
        }

        private void DiscardNo()
        {
            DiscardPanelHide();
        }

        public void DiscardPanelHide()
        {
            gameObject.SetActive(false);
        }

        public void DiscardPanelDisplay()
        {
            gameObject.SetActive(true);
        }
    }
}
