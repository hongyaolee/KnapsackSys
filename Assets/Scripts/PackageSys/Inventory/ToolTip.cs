/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       ToolTip物品信息提示类
*	       功能：1、鼠标放在物品图标上时，显示物品描述信息
*	             2、ToolTip本身的显示和隐藏
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
	public class ToolTip : MonoBehaviour 
	{
        Text toolTipText;
        Text contentText;
        CanvasGroup canvasGroup;
        float targetAlpha;
        public float smoothing = 6;

        private void Start()
        {
            toolTipText = GetComponent<Text>();
            contentText = transform.Find("Content").GetComponent<Text>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            //通过插值实现显示和隐藏的渐变效果
            if (canvasGroup.alpha != targetAlpha)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing*Time.deltaTime);
                if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01)
                {
                    canvasGroup.alpha = targetAlpha;
                }
            }
        }


        /// <summary>
        /// 隐藏tooltip
        /// </summary>
        public void HideToolTip()
        {
            
            targetAlpha = 0;
        }
        /// <summary>
        /// 显示tooltip
        /// </summary>
        public void DisplayToolTip(string text)
        {
            toolTipText.text = text;
            contentText.text = text;
            targetAlpha = 1;
        }

        /// <summary>
        /// 设置ToolTip的局部坐标，即在canvas中的位置
        /// </summary>
        /// <param name="position"></param>
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

    }
}
