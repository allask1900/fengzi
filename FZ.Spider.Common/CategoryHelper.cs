using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.Common
{
    public class CategoryHelper
    {
        /// <summary>
        /// 分类级别 0 1 2 3 4 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="level"></param>
        /// <returns>CategoryID</returns>
        public static string GetCategoryIDByLevel(int CategoryID,int level)
        {

            if (CategoryID > 100)
                return CategoryID.ToString().Substring(0, 2 * (level+1));
            else
                return CategoryID.ToString();
        }
        /// <summary>
        /// 得到父类型
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetCategoryID(int CategoryID, int level)
        {
            int myLevel = GetCategoryIDLevel(CategoryID);
            if (myLevel > level)
            {
                for (int i = 0; i <myLevel  - level; i++)
                {
                    CategoryID = CategoryID / 100;
                } 
            }
            return CategoryID;
           
        }

        public static int GetCategoryIDLevel(int CategoryID)
        {
            if (CategoryID>10&&CategoryID < 99)
                return 0;
            else if (CategoryID > 1000 && CategoryID < 9999)
                return 1;
            else if (CategoryID > 100000 && CategoryID < 999999)
                return 2;
            else if (CategoryID > 10000000 && CategoryID < 99999999)
                return 3;
            else if (CategoryID > 1000000000)
                return 4;
            else
                return -1;

        }
    }
}
