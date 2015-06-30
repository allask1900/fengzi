using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FZ.Spider.Configuration;
namespace FZ.Spider.Common
{
    public class UrlHelper
    {
        #region 产品目录转换
        /// <summary>
        /// 得到产品存放数据目录(例:d:\digview.com\www.digview.com\Product\1\120\)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static string GetProductDir(int ProductID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;             
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SitePath);
            sb.Append("Product\\");
            sb.Append(first.ToString());
            sb.Append("\\");
            sb.Append(second.ToString());
            sb.Append("\\"); 
            return Common.CommonFun.CheckDirectory(sb.ToString());
        }
        #endregion

        #region 目录URL转换
        /// <summary>
        /// 得到产品存放数据目录URL(例:http://www.digview.com/Product/1/120/100/)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static string GetProductFileUrl(int ProductID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;
            third = ProductID % 1000;
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/Product/");
            sb.Append(first.ToString());
            sb.Append("/");
            sb.Append(second.ToString());
            sb.Append("/");
            sb.Append(third.ToString());
            sb.Append("/");
            return sb.ToString();
        }
        /// <summary>
        /// 得到图片存放数据目录URL(例:http://Image.digview.com/1/120/100/)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static string GetImageFileUrl(int ProductID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;
            third = ProductID % 1000;
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.ImagesDomain);
            sb.Append("/");
            sb.Append(first.ToString());
            sb.Append("/");
            sb.Append(second.ToString());
            sb.Append("/");
            sb.Append(third.ToString());
            sb.Append("/");
            return sb.ToString();
        }
        #endregion
 
        #region 图片URL转换 
        /// <summary>
        /// 得到产品缩略图片Url(格式:http://image.digview.com/1/120/100/1120100_s.jpg)
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        public static string GetSmallImageUrl(int ProductID,int ImageType)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(GetImageFileUrl(ProductID));
            ///由于抓取故障导致图片名称错
            ///在allstoreprice_movieandmusic..tb_product表中 有192743项如果有图片的话则图片路径不变但是名称应该改为 ProductID - 19000000
            if (ProductID > 20000000 && ProductID < 20745177)
            {
                ProductID = ProductID - 19000000;
            }
            sb.Append(ProductID.ToString());

            if (ImageType == 1 || ImageType==3)
            {
                 sb.Append("_s.jpg");
            }
            else if (ImageType == 2)
            {
                sb.Append("_b.jpg");
            }
            else
            {
                return Configs.DefaultPic_Small;
            }
            return sb.ToString();
                
        }
        /// <summary>
        /// 得到产品大图片Url(格式:http://image.digview.com/1/120/100/1120100_b.jpg)
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        public static string GetBigImageUrl(int ProductID, int ImageType)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(GetImageFileUrl(ProductID));            
            sb.Append(ProductID.ToString());
            if (ImageType == 2 || ImageType == 3)
            {
                sb.Append("_b.jpg");
            }
            else if (ImageType == 1)
            {
                sb.Append("_s.jpg");
            }
            else
            {
                return Configs.DefaultPic_Big;
            }
            return sb.ToString();
        }

        public static string GetSiteLogoUrl(int SiteID)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/Site/");
            sb.Append(SiteID);
            sb.Append("/");
            sb.Append(SiteID);
            sb.Append(".gif");
            return sb.ToString();
        }
        #endregion

        #region 图片路径转换
        /// <summary>
        /// 得到图片存放路径(只有一张 大图 小图)
        /// </summary>
        /// <param name="ProductID">产品ID</param>
        /// <param name="ImageType">0: 只有一张; 1:小图; 2:大图</param>
        /// <returns></returns>
        public static string GetImagePath(int ProductID, int ImageType)
        { 
            if (ImageType == 1)
            {
                return GetImagePath(ProductID) + ProductID + "_s.jpg";
            }
            else if (ImageType == 2)
            {
                return GetImagePath(ProductID) + ProductID + "_b.jpg";
            }
            else
            {
                return GetImagePath(ProductID) + ProductID+".jpg";
            }

        }
        /// <summary>
        /// 图片存放目录 (D:\Project\3jbi\image.digview.com\12\123\234\)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        private static string GetImagePath(int ProductID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;
            third = ProductID % 1000;
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.ImagesPath);
            sb.Append(first.ToString());
            sb.Append("\\");
            sb.Append(second.ToString());
            sb.Append("\\");
            sb.Append(third.ToString());
            sb.Append("\\");
            return Common.CommonFun.CheckDirectory(sb.ToString());
        }
        /// <summary>
        /// 图片存放目录(D:\Project\3jbi\image.digview.com\12\123\234)用于删除无用图片
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static string GetImagePathAndNoCreateDir(int ProductID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;
            third = ProductID % 1000;
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.ImagesPath);
            sb.Append(first.ToString());
            sb.Append("\\");
            sb.Append(second.ToString());
            sb.Append("\\");
            sb.Append(third.ToString());            
            return sb.ToString();
        }
        public static string GetSiteLogoPath(int SiteID)
        {
            return CommonFun.CheckDirectory(Configs.SitePath+"\\Site\\"+SiteID+"\\")+SiteID+".gif";
        }
        #endregion

        #region 产品XML数据文件路径转换
        /// <summary>
        /// 得到产品不同商家XML数据文件路径(格式d:\www.digview.com\product\1\102\12\shoplist.xml)
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>        
        public static string GetProductList(int ProductID)
        {
            return GetProductDir(ProductID)+"shoplist.xml";
        }
        /// <summary>
        /// 得到产品属性XML数据文件路径(格式d:\www.digview.com\product\1\102\12\Specifications.xml)
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>        
        public static string GetProductSpecifications(int ProductID)
        {
            return GetProductDir(ProductID) + "Specifications.xml";
        }
        /// <summary>
        /// 得到产品XML(包括该产品所有信息)数据文件路径(格式d:\www.digview.com\product\1\102\12\100102012.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductXml(int ProductID)
        {
            return GetProductDir(ProductID) + ProductID+".xml";
        }
        /// <summary>
        /// 得到产品信息XML数据文件路径(格式d:\www.digview.com\product\1\102\12\info.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductInfo(int ProductID)
        {
            return GetProductDir(ProductID) + "info.xml";
        }
        /// <summary>
        /// 得到产品评论XML数据文件路径(格式d:\digview.com\www.digview.com\product\1\102\12\comment.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductCommentPath(int ProductID)
        {
            return GetProductDir(ProductID) + "comment.xml";
        }
        /// <summary>
        /// 得到站点评论XML数据文件路径(格式d:\digview.com\www.digview.com\site\1\SiteComment.xml)
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public static string GetSiteCommentPath(int SiteID)
        {
            return Configs.SitePath + "Site\\" + SiteID + "\\SiteComment.xml";
        }
        /// <summary>
        /// http://www/digview.com/seller/sellercomment-1.html
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public static string GetSiteCommentUrl(int SiteID)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/seller/sellercomment-");
            sb.Append(SiteID.ToString());
            sb.Append(".html");
            return sb.ToString();
        }
        #endregion 

        #region 站点数据缓存XML文件路径
        /// <summary>
        /// D:\digview.com\www.digview.com\cache\FileName.xml
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetXmlCacheFile(string FileName)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SitePath);
            sb.Append("cache\\");
            Common.CommonFun.CheckDirectory(sb.ToString());
            sb.Append(FileName);
            sb.Append(".xml");
            return sb.ToString();
        }
        #endregion

        #region 产品URL转换
        /// <summary>
        /// 得到产品转换后路径(格式http://www.digview.com/computers-cpu/product-1230045.html)
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static string GetProductUrl(int ProductID, string FirstCategoryName,string Title)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(FirstCategoryName);
            sb.Append("/");
            sb.Append(ProcessTitle(Title));
            sb.Append("-");
            sb.Append(ProductID);
            sb.Append(".html");
            return sb.ToString().ToLower();
        }
        public static string GetProductUrl(int ProductID, string FirstCategoryName, string PageName, string Title)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(FirstCategoryName);
            sb.Append("/");
            sb.Append(ProcessTitle(Title));            
            sb.Append("-");
            sb.Append(ProductID);
            if (PageName.ToLower() == "reviews")
            {
                sb.Append("-");
                sb.Append(PageName);
            }
            sb.Append(".html");
            return sb.ToString().ToLower();
        }
        /// <summary>
       /// 将字符串转换可用于url(如:Health & Beauty====>health-beauty),两个个单词之间有且只有一个"-"分隔。
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
       public static string ConvertUrlName(string name)
       {
           string urlname = System.Web.HttpUtility.HtmlDecode(name.ToLower());
           urlname = Regex.Replace(urlname, @"[\~\`\!\@\#\$\%\^\&\*\(\)_\+\-\=\{\}\|\[\]\\\:\""\;\<\>\?\,\.\/\s]+", "-");
           if (urlname.IndexOf("--") > -1)
           { 
               return string.Empty;
           }
           String[] words = urlname.Split('-');
           StringBuilder sb = new StringBuilder("");
           foreach (string wd in words)
           {
               if (wd.Length > 2)
               {
                   if (sb.Length > 0)
                       sb.Append("-");
                   sb.Append(wd);
               }
               
           }
           return sb.ToString();           
       }
  
        private static string ProcessTitle(string Title)
        {
            return ConvertUrlName(Title);
        }       
        #endregion 

        /// <summary>
        /// 联盟转向URL
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        public static string GetUnionRefUrl(int siteid, string resourceUrl)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/reurl?id=");
            sb.Append(siteid);
            sb.Append("&ref=");
            sb.Append(System.Web.HttpUtility.UrlEncode(resourceUrl));             
            return sb.ToString().ToLower();
        }
        #region 分类列表URL转换
        /// <summary>
        /// 一级分类的URL(格式:http://www.digview.com/books)
        /// </summary>
        /// <param name="FirstCategoryUrlCode"></param>
        /// <returns></returns>
        public static string GetFirstCategoryUrl(string FirstCategoryUrlCode)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            if (!string.IsNullOrEmpty(FirstCategoryUrlCode))
            {
                sb.Append("/");
                sb.Append(FirstCategoryUrlCode);
            }
            return sb.ToString().ToLower();
        }
        /// <summary>
        /// 得到分类列表URL(格式:http://www.digview.com/books/fiction--121210-0-1-30-1.html)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string GetCategoryListUrl(CategoryListPage clp)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(clp.FirstCategoryUrlCode);
            if (clp.CategoryID > 100)
            {
                sb.Append("/");
                sb.Append(clp.CategoryUrlCode);
                sb.Append("--");
                sb.Append(clp.CategoryID);
                sb.Append("-");
                if (clp.ListType == "0")
                    clp.ListType = "grid";
                sb.Append(clp.ListType);
                sb.Append("-");
                sb.Append(clp.Sort);
                sb.Append("-");
                sb.Append(clp.BrandID);
                sb.Append("-");
                sb.Append(clp.PageSize);
                sb.Append("-");
                sb.Append(clp.PageIndex);
                sb.Append(".html");
            }
            return sb.ToString().ToLower();
        }
        public static string GetCategoryListUrl(string FirstCategoryUrlCode, string CategoryUrlCode, int CategoryID)
        {
            return GetCategoryListUrl(new CategoryListPage(FirstCategoryUrlCode,CategoryUrlCode,CategoryID));
        }
        public class CategoryListPage
        {
            /// <summary>
            /// 一级分类名称转换而成的UrlCode
            /// </summary>
            public string FirstCategoryUrlCode;
            /// <summary>
            /// 分类ID
            /// </summary>
            public int CategoryID;
            /// <summary>
            /// 分类名称转换的UrlCode
            /// </summary>
            public string CategoryUrlCode;
            /// <summary>
            /// 列表格式(0,list ; 1,table)默认0
            /// </summary>
            public string ListType="grid";
            /// <summary>
            /// 排序方式(1,价格从高到底;2,价格从低到高;3,商家数;4,评价高到低;0,受欢迎度(目前暂时用User view值)
            /// </summary>
            public int Sort=0;
            /// <summary>
            /// 品牌ID
            /// </summary>
            public int BrandID=0;
            /// <summary>
            /// 分页大小
            /// </summary>
            public int PageSize=15;
            /// <summary>
            /// 当前页数
            /// </summary>
            public int PageIndex=1;
            public CategoryListPage()
            {
            }
            public CategoryListPage(string firstcategoryurlcode, string categoryurlcode,int categoryid)
            {
                FirstCategoryUrlCode = firstcategoryurlcode;
                CategoryUrlCode = categoryurlcode;
                CategoryID = categoryid;
            }
            public CategoryListPage(string firstcategoryurlcode, string categoryurlcode, int categoryid, string listType,int sort,int brandid,int pagesize,int pageindex)
            {
                FirstCategoryUrlCode = firstcategoryurlcode;
                CategoryUrlCode = categoryurlcode;
                CategoryID = categoryid;
                ListType = listType;
                Sort = sort;
                BrandID = brandid;
                PageSize = pagesize;
                PageIndex = pageindex;
            }
        }
         
        /// <summary>
        /// 得到品牌产品列表URL(格式:http://www.digview.com/computer-cup/brand-12-121211-1.html)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public static string GetBrandProductList(string CategoryUrlCode, int BrandID,int CategoryID,int PageIndex)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(CategoryUrlCode);
            sb.Append("/brand-");
            sb.Append(BrandID.ToString());
            sb.Append("-");
            sb.Append(CategoryID.ToString());
            sb.Append("-");
            sb.Append(PageIndex.ToString());
            sb.Append(".html");
            return sb.ToString().ToLower();
        }
        #endregion

        #region 搜索列表Url 
        public static string GetSearchUrl(string word)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/search?q=");
            sb.Append(word);            
            return sb.ToString().ToLower();
        }
        public static string GetSearchUrl(string word,int CategoryID)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/search?q=");
            sb.Append(word);
            if (CategoryID > 0)
            {
                sb.Append("&c=");
                sb.Append(CategoryID);
            }           
            return sb.ToString().ToLower();
        }
        public static string GetSearchUrl(string word, string listType, int CategoryID, int PageIndex, int PageSize)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/search?q=");
            sb.Append(word);
            if (CategoryID > 0)
            {
                sb.Append("&c=");
                sb.Append(CategoryID);
            }
            if (PageIndex > 1)
            {
                sb.Append("&p=");
                sb.Append(PageIndex);
            }
            if (PageSize > 15)
            {
                sb.Append("&s=");
                sb.Append(PageSize);
            }
            if (listType != "list")
            {
                sb.Append("&t=");
                sb.Append(listType);
            }
            return sb.ToString().ToLower();
        }
        #endregion

        #region Deals列表Url
        public static string GetDealsUrl(string word)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/deals?q=");
            sb.Append(word);
            return sb.ToString().ToLower();
        }
        public static string GetDealsUrl(string word, int firstCategoryID)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/deals?q=");
            sb.Append(word);
            if (firstCategoryID > 0)
            {
                sb.Append("&c=");
                sb.Append(firstCategoryID);
            }
            return sb.ToString().ToLower();
        }
        public static string GetDealsUrl(string word, string listType, int firstCategoryID, int PageIndex, int PageSize)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/deals?q=");
            sb.Append(word);
            if (firstCategoryID > 0)
            {
                sb.Append("&c=");
                sb.Append(firstCategoryID);
            }
            if (PageIndex > 1)
            {
                sb.Append("&p=");
                sb.Append(PageIndex);
            }
            if (PageSize > 15)
            {
                sb.Append("&s=");
                sb.Append(PageSize);
            }
            if (listType != "list")
            {
                sb.Append("&t=");
                sb.Append(listType);
            }
            return sb.ToString().ToLower();
        }
        #endregion

        #region 新闻URL转换
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="CategoryUrlCode"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string GetNewsListUrl(int CategoryID, string CategoryUrlCode, int PageIndex)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(CategoryUrlCode);
            sb.Append("/news_list-");
            sb.Append(CategoryID.ToString());
            sb.Append("-");
            sb.Append(PageIndex.ToString());
            sb.Append(".html");
            return sb.ToString().ToLower(); 
        }
        /// <summary>
        /// 新闻页面Url(http://www.digview.com/book/news-124.html)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="CategoryUrlCode"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string GetNewsShowUrl(string CategoryUrlCode, int NewsID)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SiteDomain);
            sb.Append("/");
            sb.Append(CategoryUrlCode);
            sb.Append("/news_show-");
            sb.Append(NewsID.ToString());
            sb.Append(".html");
            return sb.ToString().ToLower();
        }
        #endregion

        #region 用户目录
        public static string GetUserFavoriteXml(int UserID)
        {
            return GetUserDir(UserID) + "favorite.xml";
        }
        /// <summary>
        /// 得到用户数据目录(例:d:\digview.com\User\0\0\100\)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static string GetUserDir(int UserID)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = UserID / 1000000;
            second = (UserID % 1000000) / 1000;
            third = UserID % 1000;
            StringBuilder sb = new StringBuilder("");
            sb.Append(Configs.SitePath);
            sb.Append("User\\");
            sb.Append(first.ToString());
            sb.Append("\\");
            sb.Append(second.ToString());
            sb.Append("\\");
            sb.Append(third.ToString());
            sb.Append("\\");
            return Common.CommonFun.CheckDirectory(sb.ToString());
        }
        #endregion

        #region 站点地图路径
        /// <summary>
        /// 得到站点地图XML文件URL(使用GZIP压缩后的地址)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetSiteMapUrl(int CategoryID, int page)
        {
            return Configs.SiteDomain + "/sitemap/sitemap_" + CategoryID + "_" + page + ".xml.gz";
        }
        /// <summary>
        ///得到站点地图文件存放地址
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetSiteMapPath(int CategoryID, int page)
        {
            if (CategoryID == 0 && page == 0)
            {
                return CommonFun.CheckDirectory(Configs.SitePath + "sitemap") + "\\sitemap.xml";
            }
            else
            {
                return CommonFun.CheckDirectory(Configs.SitePath + "sitemap") + "\\sitemap_" + CategoryID + "_" + page + ".xml";
            }
        }
        public static string GetSiteMapRootPath()
        {
             return Configs.SitePath + "sitemap"; 
        }

        #endregion
    }
}