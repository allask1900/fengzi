using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FZ.Spider.Configuration;
namespace FZ.Spider.Common
{
    public class UrlHelper
    {
        #region ��ƷĿ¼ת��
        /// <summary>
        /// �õ���Ʒ�������Ŀ¼(��:d:\digview.com\www.digview.com\Product\1\120\)
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

        #region Ŀ¼URLת��
        /// <summary>
        /// �õ���Ʒ�������Ŀ¼URL(��:http://www.digview.com/Product/1/120/100/)
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
        /// �õ�ͼƬ�������Ŀ¼URL(��:http://Image.digview.com/1/120/100/)
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
 
        #region ͼƬURLת�� 
        /// <summary>
        /// �õ���Ʒ����ͼƬUrl(��ʽ:http://image.digview.com/1/120/100/1120100_s.jpg)
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        public static string GetSmallImageUrl(int ProductID,int ImageType)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(GetImageFileUrl(ProductID));
            ///����ץȡ���ϵ���ͼƬ���ƴ�
            ///��allstoreprice_movieandmusic..tb_product���� ��192743�������ͼƬ�Ļ���ͼƬ·�����䵫������Ӧ�ø�Ϊ ProductID - 19000000
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
        /// �õ���Ʒ��ͼƬUrl(��ʽ:http://image.digview.com/1/120/100/1120100_b.jpg)
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

        #region ͼƬ·��ת��
        /// <summary>
        /// �õ�ͼƬ���·��(ֻ��һ�� ��ͼ Сͼ)
        /// </summary>
        /// <param name="ProductID">��ƷID</param>
        /// <param name="ImageType">0: ֻ��һ��; 1:Сͼ; 2:��ͼ</param>
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
        /// ͼƬ���Ŀ¼ (D:\Project\3jbi\image.digview.com\12\123\234\)
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
        /// ͼƬ���Ŀ¼(D:\Project\3jbi\image.digview.com\12\123\234)����ɾ������ͼƬ
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

        #region ��ƷXML�����ļ�·��ת��
        /// <summary>
        /// �õ���Ʒ��ͬ�̼�XML�����ļ�·��(��ʽd:\www.digview.com\product\1\102\12\shoplist.xml)
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>        
        public static string GetProductList(int ProductID)
        {
            return GetProductDir(ProductID)+"shoplist.xml";
        }
        /// <summary>
        /// �õ���Ʒ����XML�����ļ�·��(��ʽd:\www.digview.com\product\1\102\12\Specifications.xml)
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>        
        public static string GetProductSpecifications(int ProductID)
        {
            return GetProductDir(ProductID) + "Specifications.xml";
        }
        /// <summary>
        /// �õ���ƷXML(�����ò�Ʒ������Ϣ)�����ļ�·��(��ʽd:\www.digview.com\product\1\102\12\100102012.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductXml(int ProductID)
        {
            return GetProductDir(ProductID) + ProductID+".xml";
        }
        /// <summary>
        /// �õ���Ʒ��ϢXML�����ļ�·��(��ʽd:\www.digview.com\product\1\102\12\info.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductInfo(int ProductID)
        {
            return GetProductDir(ProductID) + "info.xml";
        }
        /// <summary>
        /// �õ���Ʒ����XML�����ļ�·��(��ʽd:\digview.com\www.digview.com\product\1\102\12\comment.xml)
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>        
        public static string GetProductCommentPath(int ProductID)
        {
            return GetProductDir(ProductID) + "comment.xml";
        }
        /// <summary>
        /// �õ�վ������XML�����ļ�·��(��ʽd:\digview.com\www.digview.com\site\1\SiteComment.xml)
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

        #region վ�����ݻ���XML�ļ�·��
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

        #region ��ƷURLת��
        /// <summary>
        /// �õ���Ʒת����·��(��ʽhttp://www.digview.com/computers-cpu/product-1230045.html)
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
       /// ���ַ���ת��������url(��:Health & Beauty====>health-beauty),����������֮������ֻ��һ��"-"�ָ���
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
        /// ����ת��URL
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
        #region �����б�URLת��
        /// <summary>
        /// һ�������URL(��ʽ:http://www.digview.com/books)
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
        /// �õ������б�URL(��ʽ:http://www.digview.com/books/fiction--121210-0-1-30-1.html)
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
            /// һ����������ת�����ɵ�UrlCode
            /// </summary>
            public string FirstCategoryUrlCode;
            /// <summary>
            /// ����ID
            /// </summary>
            public int CategoryID;
            /// <summary>
            /// ��������ת����UrlCode
            /// </summary>
            public string CategoryUrlCode;
            /// <summary>
            /// �б��ʽ(0,list ; 1,table)Ĭ��0
            /// </summary>
            public string ListType="grid";
            /// <summary>
            /// ����ʽ(1,�۸�Ӹߵ���;2,�۸�ӵ͵���;3,�̼���;4,���۸ߵ���;0,�ܻ�ӭ��(Ŀǰ��ʱ��User viewֵ)
            /// </summary>
            public int Sort=0;
            /// <summary>
            /// Ʒ��ID
            /// </summary>
            public int BrandID=0;
            /// <summary>
            /// ��ҳ��С
            /// </summary>
            public int PageSize=15;
            /// <summary>
            /// ��ǰҳ��
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
        /// �õ�Ʒ�Ʋ�Ʒ�б�URL(��ʽ:http://www.digview.com/computer-cup/brand-12-121211-1.html)
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

        #region �����б�Url 
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

        #region Deals�б�Url
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

        #region ����URLת��
        /// <summary>
        /// �����б�
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
        /// ����ҳ��Url(http://www.digview.com/book/news-124.html)
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

        #region �û�Ŀ¼
        public static string GetUserFavoriteXml(int UserID)
        {
            return GetUserDir(UserID) + "favorite.xml";
        }
        /// <summary>
        /// �õ��û�����Ŀ¼(��:d:\digview.com\User\0\0\100\)
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

        #region վ���ͼ·��
        /// <summary>
        /// �õ�վ���ͼXML�ļ�URL(ʹ��GZIPѹ����ĵ�ַ)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetSiteMapUrl(int CategoryID, int page)
        {
            return Configs.SiteDomain + "/sitemap/sitemap_" + CategoryID + "_" + page + ".xml.gz";
        }
        /// <summary>
        ///�õ�վ���ͼ�ļ���ŵ�ַ
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