/***
*	
*	Title：背包系统
	       PackageSys
*	
*	Description:
*	       背包面板脚本
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
	public class KnapsackPanel : Inventory 
	{
        private static KnapsackPanel instance;
        public static KnapsackPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Canvas").transform.Find("PackagePanel").GetComponent<KnapsackPanel>();
                    
                }
                return instance;
            }
        }

        public override void Start()
        {
            base.Start();
            for (int i = 0; i < 11; i++)
            {
                StoreItem(1);
            }
            for (int i = 0; i < 11; i++)
            {
                StoreItem(2);
            }

            StoreItem(100);
            StoreItem(101);
            StoreItem(102);
            StoreItem(200);
            StoreItem(201);
            StoreItem(202);
            StoreItem(203);
            for (int i = 0; i < 101; i++)
            {
                StoreItem(300);
            }
        }
    }
}
