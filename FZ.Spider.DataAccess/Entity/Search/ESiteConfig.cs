using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using log4net;
using FZ.Spider.Common;
namespace FZ.Spider.DAL.Entity.Search
{
    public class ESiteConfig
    {
        private static ILog logger = LogManager.GetLogger(typeof(ESiteConfig).FullName);
        #region 字段
        #region 获取分类设置
        private string m_GetList_Reg_GetCategoryUrl = string.Empty;
        private string m_GetList_Str_GetCodeBeginAndEnd = string.Empty;
        private string m_GetList_Reg_GetCode = string.Empty;
        #endregion

        #region 全局配置项
        /// <summary>
        /// 配置ID
        /// </summary>
        private int m_OrdID = 0; 
        /// <summary>
        /// 站点ID
        /// </summary>
        private int m_SiteID = 0;
        /// <summary>
        /// 分类ID
        /// </summary>
        private int m_CategoryID = 0;
        /// <summary>
        /// 分类ID集
        /// </summary>
        private string m_CategoryIDS = string.Empty;
        /// <summary>
        /// 测试状态
        /// </summary>
        private bool m_TestStatus = false;
        private string m_SpiderTemplet = string.Empty;
        #endregion

        #region 列表页配置 
        /// <summary>
        /// 列表页面正则表达式
        /// </summary>
        private string m_List_Reg_GetProductList = string.Empty;
        /// <summary>
        /// 列表页面代码提取起止标识(用字符=====分割的两个字符串)
        /// </summary>
        private string m_List_Str_GetCodeBeginAndEnd = string.Empty; 
        /// <summary>
        /// 产品Url转换正则表达式
        /// </summary>
        private string m_List_Reg_ChangeProductUrl = string.Empty;
        /// <summary>
        /// 列表页Post数据
        /// </summary>
        private string m_List_Str_PostData = string.Empty;
        private string m_List_Reg_GetCode = string.Empty;
        private string m_List_Reg_GetProductCode = string.Empty;
        #endregion

        #region 列表分页配置
        /// <summary>
        /// 分页号码替换正则表达式(pno)
        /// </summary>
        private string m_PageNo_Reg_RepalcePno = string.Empty;
        /// <summary>
        /// 分页算法：开始页====开始页|页大小====页大小|最大页号
        /// </summary>
        private PagingConfig m_PageNo_Reg_PagingConfig =new PagingConfig();  
        /// <summary>
        /// 列表页面截取最大页号时起止标识(用字符=====分割的两个字符串)
        /// </summary>
        private string m_PageNo_Str_GetCodeBeginAndEnd= string.Empty;
        private string m_PageNo_Reg_GetCode = string.Empty;
        #endregion
 
        #region 产品页面配置
        /// <summary>
        /// 产品页面代码截取起止标识(用字符=====分割的两个字符串)
        /// </summary>
        private string m_Product_Str_GetCodeBeginAndEnd = string.Empty;
        /// <summary>
        /// 产品页Post数据
        /// </summary>
        private string m_Product_Str_PostData = string.Empty;        
        /// <summary>
        /// 是否分析产品页面
        /// </summary>
        private bool m_Product_Bool_IsAnalysis = true;
        /// <summary>
        /// 获得产品参数的正则表达式集合
        /// </summary>
        private string m_Product_Reg_GetProductParameter = string.Empty; 
        #endregion 

        #region 价格更新配置
        /// <summary>
        /// 价格更新时提取价格起止标识(用字符=====分割的两个字符串)
        /// </summary>
        private string m_UpdatePrice_Str_GetCodeBeginAndEnd = string.Empty;
        /// <summary>
        /// 价格更新时得到价格的链接或价格的正则表达式集合
        /// </summary>
        private string m_UpdatePrice_Reg_GetPrice = string.Empty;
        #endregion
        private string m_Comment_Reg_GetProductCommentParameter = string.Empty;
        #endregion

        #region 属性
        #region 获取分类设置
        /// <summary>
        /// 提取分类Url正则表达式
        /// </summary>
        public string GetList_Reg_GetCategoryUrl
        {
            get { return m_GetList_Reg_GetCategoryUrl; }
            set { m_GetList_Reg_GetCategoryUrl = value; }
        }
        /// <summary>
        /// 提取分类Url页起止字符串(用字符=====分割的两个字符串)
        /// </summary>
        public string GetList_Str_GetCodeBeginAndEnd
        {
            get { return m_GetList_Str_GetCodeBeginAndEnd; }
            set { m_GetList_Str_GetCodeBeginAndEnd = value; }
        }
        /// <summary>
        /// 提取分类Url时提取页面代码的正则表达式
        /// </summary>
        public string GetList_Reg_GetCode
        {
            get { return m_GetList_Reg_GetCode; }
            set { m_GetList_Reg_GetCode = value; }
        }
        #endregion

        #region 全局配置表
        /// <summary>
        /// 配置ID
        /// </summary>
        public int OrdID
        {
            get { return m_OrdID; }
            set { m_OrdID = value; }
        }
        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteID
        {
            get { return m_SiteID; }
            set { m_SiteID = value; }
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryID
        {
            get { return m_CategoryID; }
            set { m_CategoryID = value; }
        }
        /// <summary>
        /// 分类ID集
        /// </summary>
        public string CategoryIDS
        {
            get { return m_CategoryIDS; }
            set { m_CategoryIDS = value; }
        }
        /// <summary>
        /// 测试状态
        /// </summary>
        public bool TestStatus
        {
            get { return m_TestStatus; }
            set { m_TestStatus = value; }
        }
        /// <summary>
        /// 爬虫模板配置文件
        /// </summary>
        public string SpiderTemplet
        {
            get { return m_SpiderTemplet; }
            set { m_SpiderTemplet = value; }
        }
        #endregion
 
        #region 列表页配置
        /// <summary>
        /// 列表页面正则表达式
        /// </summary>
        public string List_Reg_GetProductList
        {
            get { return m_List_Reg_GetProductList; }
            set { m_List_Reg_GetProductList = value; }
        }
        /// <summary>
        /// 列表页面代码提取起止标识(用字符=====分割的两个字符串)
        /// </summary>
        public string List_Str_GetCodeBeginAndEnd
        {
            get { return m_List_Str_GetCodeBeginAndEnd; }
            set { m_List_Str_GetCodeBeginAndEnd = value; }
        }        
        /// <summary>
        /// 产品Url转换正则表达式
        /// </summary>
        public string List_Reg_ChangeProductUrl
        {
            get { return m_List_Reg_ChangeProductUrl; }
            set { m_List_Reg_ChangeProductUrl = value; }
        }
        /// <summary>
        /// 列表页面post提交数据
        /// </summary>
        public string List_Str_PostData
        {
            get { return m_List_Str_PostData; }
            set { m_List_Str_PostData = value; }
        }
        /// <summary>
        /// 列表页面代码提取代码正则表达式
        /// </summary>
        public string List_Reg_GetCode
        {
            get { return m_List_Reg_GetCode; }
            set { m_List_Reg_GetCode = value; }
        }
        /// <summary>
        /// 列表页面代码提取产品代码正则表达式
        /// </summary>
        public string List_Reg_GetProductCode
        {
            get { return m_List_Reg_GetProductCode; }
            set { m_List_Reg_GetProductCode=value; }
        }
        #endregion

        #region 列表分页配置
        /// <summary>
        /// 分页号码替换字符,将分页参数添加进URL，如(?page=[pno]),以便分页数字替换[pno]
        /// </summary>
        public string PageNo_Reg_RepalcePno
        {
            get { return m_PageNo_Reg_RepalcePno; }
            set { m_PageNo_Reg_RepalcePno = value; }
        }        
        
        /// <summary>
        /// 列表页面截取最大页号时提取页面代码的正则表达式
        /// </summary>
        public string PageNo_Reg_GetCode
        {
            get { return m_PageNo_Reg_GetCode; }
            set { m_PageNo_Reg_GetCode = value; }
        }
        /// <summary>
        /// 列表页面截取最大页号时起止标识(用字符=====分割的两个字符串)
        /// </summary>
        public string PageNo_Str_GetCodeBeginAndEnd
        {
            get { return m_PageNo_Str_GetCodeBeginAndEnd; }
            set { m_PageNo_Str_GetCodeBeginAndEnd = value; }
        }
        /// <summary>
        /// 分页算法
        /// </summary>
        public PagingConfig PageNo_Reg_PagingConfig
        {
            get { return m_PageNo_Reg_PagingConfig; }
            set { m_PageNo_Reg_PagingConfig = value; }
        }

        
        #endregion 

        #region 产品页面配置
        /// <summary>
        /// 产品页面代码截取起止标识(用字符=====分割的两个字符串)
        /// </summary>
        public string Product_Str_GetCodeBeginAndEnd
        {
            get { return m_Product_Str_GetCodeBeginAndEnd; }
            set { m_Product_Str_GetCodeBeginAndEnd = value; }
        }  
        /// <summary>
        /// 产品页面Post提交数据
        /// </summary>
        public string Product_Str_PostData
        {
            get { return m_Product_Str_PostData; }
            set { m_Product_Str_PostData = value; }
        }        
        /// <summary>
        /// 是否分析产品页面
        /// </summary>
        public bool Product_Bool_IsAnalysis
        {
            get { return m_Product_Bool_IsAnalysis; }
            set { m_Product_Bool_IsAnalysis = value; }
        }
        /// <summary>
        /// 获得产品参数的正则表达式集合
        /// </summary>
        public string Product_Reg_GetProductParameter
        {
            get { return m_Product_Reg_GetProductParameter; }
            set { m_Product_Reg_GetProductParameter = value; }
        } 
        #endregion 

        #region 价格更新更新
        /// <summary>
        /// 产品商家价格更新 页面截取起止标识(用字符=====分割的两个字符串)
        /// </summary>
        public string UpdatePrice_Str_GetCodeBeginAndEnd
        {
            get { return m_UpdatePrice_Str_GetCodeBeginAndEnd; }
            set { m_UpdatePrice_Str_GetCodeBeginAndEnd = value; }
        } 
        /// <summary>
        /// 价格更新时得到价格的链接或价格的正则表达式集合
        /// </summary>
        public string UpdatePrice_Reg_GetPrice
        {
            get { return m_UpdatePrice_Reg_GetPrice; }
            set { m_UpdatePrice_Reg_GetPrice = value; }
        } 
        #endregion

        /// <summary>
        /// 产品评论正则表达式集合
        /// </summary>
        public string Comment_Reg_GetProductCommentParameter
        {
            get { return m_Comment_Reg_GetProductCommentParameter; }
            set { m_Comment_Reg_GetProductCommentParameter = value; }
        }
        #endregion

        public DateTime LastChangeTime
        {
            get;
            set;
        }
        public ESiteConfig()
        {
        }
        public ESiteConfig(System.Data.IDataReader dr, int CategoryID)
        {           
            this.m_OrdID = (int)dr["OrdID"];
            this.m_SiteID = (int)dr["SiteID"];
            this.m_CategoryID = CategoryID;
            this.m_CategoryIDS =  dr["CategoryIDS"].ToString();
            this.m_TestStatus = Convert.ToBoolean(dr["TestStatus"]);
            this.m_SpiderTemplet = dr["SpiderTemplet"].ToString();
            SpiderTempletParser();
            this.LastChangeTime = Convert.ToDateTime(dr["LastChangeTime"]);
            
        }
        public ESiteConfig(System.Data.IDataReader dr)
        { 
            this.m_OrdID = (int)dr["OrdID"];
            this.m_SiteID = (int)dr["SiteID"]; 
            this.m_CategoryIDS = dr["CategoryIDS"].ToString();
            this.m_TestStatus = Convert.ToBoolean(dr["TestStatus"]);
            this.m_SpiderTemplet = dr["SpiderTemplet"].ToString();
            SpiderTempletParser();
            this.LastChangeTime = Convert.ToDateTime(dr["LastChangeTime"]);
        }
        private void SpiderTempletParser()
        {
            if (m_SpiderTemplet == string.Empty) return;
            string code = Regex.Replace(m_SpiderTemplet.Trim(), @"/[\*]{5}[^\*]*?[\*]{5}/", "",RegexOptions.IgnoreCase,TimeSpan.FromSeconds(30));
            string[] configs = code.Trim().Split(new string[]{"<!---------->"},StringSplitOptions.RemoveEmptyEntries);
            string configSpiltString = "=====||=====";
            foreach(string config in configs)
            {
                if (config.IndexOf(configSpiltString) > 0)
                {
                    string[] kv = config.Trim().Split(new string[] { configSpiltString }, StringSplitOptions.RemoveEmptyEntries);
                    if (kv.Length == 2)
                    {
                        string key = kv[0].Trim().ToLower();
                        string value = kv[1].Trim();
                        switch (key)
                        {
                            //list url
                            case "getlist_reg_getcategoryurl":                      
                                this.m_GetList_Reg_GetCategoryUrl =value;
                                break;
                            case "getlist_str_getcodebeginandend":
                                this.m_GetList_Str_GetCodeBeginAndEnd =value;
                                break;
                            case "getlist_reg_getcode":
                                this.m_GetList_Reg_GetCode = value; 
                                break;
                             //list page
                            case "list_reg_getproductlist":
                                this.m_List_Reg_GetProductList = value; 
                                break;
                            case "list_str_getcodebeginandend":
                                this.m_List_Str_GetCodeBeginAndEnd = value; 
                                break;
                            case "list_str_postdata":
                                this.m_List_Str_PostData = value; 
                                break;
                            case "list_reg_changeproducturl":
                                this.m_List_Reg_ChangeProductUrl = value; 
                                break;
                            case "list_reg_getcode":
                                this.m_List_Reg_GetCode = value;
                                break;
                            case "list_reg_getproductcode":
                                this.m_List_Reg_GetProductCode = value;
                                break;
                            //list page no
                            case "pageno_str_getcodebeginandend":
                                this.m_PageNo_Str_GetCodeBeginAndEnd = value;
                                break;
                            case "pageno_reg_repalcepno":
                                this.m_PageNo_Reg_RepalcePno = value;
                                break;
                            case "pageno_reg_pagingconfig":
                                this.m_PageNo_Reg_PagingConfig = new PagingConfig(value);
                                break;
                            case "pageno_reg_getcode":
                                this.m_PageNo_Reg_GetCode = value;
                                break;
                            //product page
                            case "product_str_getcodebeginandend":
                                this.m_Product_Str_GetCodeBeginAndEnd = value;
                                break;
                            case "product_str_postdata":
                                this.m_Product_Str_PostData = value;
                                break;
                            case "product_bool_isanalysis":
                                bool.TryParse(value,out this.m_Product_Bool_IsAnalysis);
                                break;
                            case "product_reg_getproductparameter":
                                this.m_Product_Reg_GetProductParameter = value;
                                break;                           
                            //product price
                            case "updateprice_str_getcodebeginandend":
                                this.m_UpdatePrice_Str_GetCodeBeginAndEnd = value;
                                break;
                            case "updateprice_reg_getprice":
                                this.m_UpdatePrice_Reg_GetPrice = value;
                                break; 

                            case "comment_reg_getproductcommentparameter":
                                this.m_Comment_Reg_GetProductCommentParameter = value;
                                break;
                        }

                    }
                }
            }
        }
    }
    public class PagingConfig
    {
        private static ILog logger = LogManager.GetLogger(typeof(PagingConfig).FullName);

        public string Reg_GetMaxNoOrMaxCount = string.Empty;
        /// <summary>
        /// 值为:PageNo 或者 ItemNo
        /// </summary>
        public string PageUrlStyle = string.Empty;
        /// <summary>
        /// 正则匹配到的值的类型为:MaxPageNo或者MaxItemNo
        /// </summary>
        public string MatchNumberType = string.Empty;
        /// <summary>
        /// 直接设置最大页码
        /// </summary>
        public int DefaultMaxPageNo = 0;
        /// <summary>
        /// BeginPageNo或者BeginItemNo
        /// </summary>
        public int BeginNo = 0;
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize = 0;
        public PagingConfig()
        {
        
        }
        public PagingConfig(string config)
        {
            string[] items = config.Trim().Split(new string[] { "=====" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                string[] kv = item.Trim().Split(new string[] { "====" }, StringSplitOptions.None);
                if (kv.Length != 2)
                {
                    logger.Error("分页算法配置出错!(" + item + ")");
                    continue;
                }
                string key = kv[0].Trim().ToLower();
                string value = kv[1].Trim();
                switch (key)
                {
                    case "reg_getmaxnoormaxcount":
                        Reg_GetMaxNoOrMaxCount = value;
                        break;
                    case "matchnumbertype":
                        MatchNumberType = value;
                        break;
                    case "pageurlstyle":
                        PageUrlStyle = value;
                        break;
                    case "defaultmaxpageno":
                        DefaultMaxPageNo =CommonFun.StrToInt(value);
                        break;
                    case "beginno":
                        BeginNo = CommonFun.StrToInt(value);
                        break;
                    case "pagesize":
                        PageSize = CommonFun.StrToInt(value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
