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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackageSys
{
	public class ChestPanel : Inventory 
	{
        private static ChestPanel instance;
        public static ChestPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Canvas").transform.Find("ChestPanel").GetComponent<ChestPanel>();
                }
                return instance;
            }

        }

	}
}
