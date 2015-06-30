using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using FZ.Spider.Common;
using FZ.Spider.Configuration;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.Logging;

namespace FZ.Spider.Spider
{
    public class SpiderContentPage
    {
        private static ILog logger = LogManager.GetLogger("SpiderLog");
        EProduct eProduct = new EProduct();
        private SpiderSite spiderSite;
        public SpiderContentPage(EProduct ep)
        {
            eProduct = ep;
            spiderSite = (SpiderSite)eProduct.SpiderSite;            
        }

        public object Start(object ob)
        {
            long begintime = DateTime.Now.Ticks;
            if (AnalysisProductPage() && (eProduct.Price > 0 || eProduct.UsedPrice > 0))
            {
                if (!eProduct.IsExist && !spiderSite.IsTest)
                {
                    //下载小图片
                    if (eProduct.SamllImageUrl != string.Empty && DownHelper.SaveBinaryFile(eProduct.SamllImageUrl, eProduct.ProductID, 1))
                    {
                        eProduct.ImageType = eProduct.ImageType + 1;
                    }
                    //下载大图片
                    if (eProduct.ImageUrl != string.Empty && DownHelper.SaveBinaryFile(eProduct.ImageUrl, eProduct.ProductID, 2))
                    {
                        eProduct.ImageType = eProduct.ImageType + 2;
                    }
                    SpiderCommon.CheckModel(eProduct);
                    spiderSite.tSiteResource.AddProduct(eProduct);
                    
                }
                if (!spiderSite.IsTest)
                {
                    if (eProduct.CommentList.Count > 0)
                    {
                        spiderSite.cProductComment.AddProductComment(eProduct);
                    }

                    if (eProduct.IsExist && eProduct.IsUpdatePrice)//对于价格在产品页面中提取的商家 重新检测一遍以更新价格(因为其他可在列表中提取价格的商家已更新过价格)
                    {
                        spiderSite.tSiteResource.Exist(eProduct);
                    }
                }
            } 
            logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析产品页耗时(" + Convert.ToInt32((DateTime.Now.Ticks - begintime) / 10000) + ")毫秒, " + eProduct.ResourceUrl + " !"));
            return true;
        }
        /// <summary>
        /// 分析节目页面
        /// </summary>
        /// <returns></returns>
        public bool AnalysisProductPage()
        {
            if (spiderSite.spiderWork.spiderWorkIsEnd) return false;
            if (!spiderSite.eSiteConfig.Product_Bool_IsAnalysis) return true;

            #region 页面代码
            string ItemPageCode = PageHelper.ReadUrl(eProduct.ResourceUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
            if (ItemPageCode == "")
            {
                ItemPageCode = PageHelper.ReadUrl(eProduct.ResourceUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                if (ItemPageCode == "")
                {
                    logger.Info(new LogMessage(spiderSite.eSite.SiteName, "分析产品页(" + eProduct.ResourceUrl + ") 读取页面错误!"));
                    return false;
                }
            }
            spiderSite.spiderStat.ReadContentPageCount++;
            if (ItemPageCode == "")
            {
                spiderSite.spiderStat.ProductReadPageMaxError++;
                if (spiderSite.spiderStat.ProductReadPageMaxError > 20)
                {
                    logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis product page readpage  Continued error 20!"));
                }
                return false;
            }
            if (spiderSite.spiderStat.ProductReadPageMaxError > 0) spiderSite.spiderStat.ProductReadPageMaxError--;
            #region 截取分析内容
            if (spiderSite.eSiteConfig.Product_Str_GetCodeBeginAndEnd != string.Empty)
            {
                string[] Product_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(spiderSite.eSiteConfig.Product_Str_GetCodeBeginAndEnd, true);
                if (Product_Str_GetCodeBeginAndEnd[0] != string.Empty)
                {
                    int beginIndex = ItemPageCode.IndexOf(Product_Str_GetCodeBeginAndEnd[0]);
                    if (beginIndex > 0)
                    {
                        ItemPageCode = ItemPageCode.Substring(beginIndex);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"analysis product page(" +eProduct.ResourceUrl + ") begin substring error!"));
                        return false;
                    }
                }
                if (Product_Str_GetCodeBeginAndEnd.Length == 2)
                {
                    int endIndex = ItemPageCode.LastIndexOf(Product_Str_GetCodeBeginAndEnd[1]);
                    if (endIndex > 0)
                    {
                        ItemPageCode = ItemPageCode.Substring(0, endIndex + Product_Str_GetCodeBeginAndEnd[1].Length + 1);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"analysis product page(" + eProduct.ResourceUrl + ") end substring error!"));
                        return false;
                    }
                }
            }
            #endregion
            #endregion
            ItemPageCode = StringHelper.DelScriptStlye(ItemPageCode);
             
            long begintime = DateTime.Now.Ticks;

            if((spiderSite.PriceStyle == 2 &&eProduct.IsUpdatePrice)||!eProduct.IsExist)
            {
                foreach (ItemConfig item in spiderSite.ProductParameter)
                {
                    string pageCode =  GetCode(item, ItemPageCode);
                  
                    #region 新提取或者更新价格
                    if (item.FieldName == "price")
                    {

                        string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                        if (value != string.Empty)
                        {
                            try
                            {
                                eProduct.Price = Convert.ToSingle(StringHelper.GetLineTextForNumber(value));
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis product page(" + eProduct.ResourceUrl + ")convert price error!"));
                                return false;
                            }
                        }
                    }
                    if (item.FieldName == "usedprice")
                    {
                        string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                        if (value != string.Empty)
                        {
                            try
                            {
                                eProduct.UsedPrice = Convert.ToSingle(StringHelper.GetLineTextForNumber(value));
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis product page(" + eProduct.ResourceUrl + ")convert usedprice error!"));
                                return false;
                            }
                        }
                    }
                    if (item.FieldName == "orgprice")
                    {
                        string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                        if (value != string.Empty)
                        {
                            try
                            {
                                eProduct.OrgPrice = Convert.ToSingle(StringHelper.GetLineTextForNumber(value));
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis product page(" + eProduct.ResourceUrl + ")convert OrgPrice error!"));
                                return false;
                            }
                        }
                    }
                    #endregion

                    #region 新页面分析
                    if (!eProduct.IsExist)
                    {
                        #region 评分数
                        if (item.FieldName == "scorecount")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                eProduct.ScoreCount = CommonFun.StrToInt(StringHelper.GetLineTextForNumber(value));
                            }
                        }
                        #endregion
                        #region 评分总分数
                        if (item.FieldName == "score")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value == string.Empty)
                            {
                                continue;
                            }
                            float avgScore = 0;
                            try
                            {
                                avgScore = Convert.ToSingle(StringHelper.GetLineTextForNumber(value));
                                eProduct.Score = Convert.ToInt32(avgScore * eProduct.ScoreCount);
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis product page(" + eProduct.ResourceUrl + ")convert avgScore error!"));
                            }
                        }
                        #endregion
                        #region  得到产品名称
                        if (item.FieldName == "fullname")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                eProduct.FullName = StringHelper.GetLineTextString(value);
                                if (eProduct.FullName == String.Empty) return false;
                            }
                        }
                        #endregion
                        #region 得到产品品牌
                        if (item.FieldName == "brandname")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                eProduct.BrandName = StringHelper.GetLineTextString(value);
                            }
                            if (eProduct.BrandName != string.Empty && !spiderSite.IsTest)
                            {
                                SpiderCommon.CheckBrand(eProduct);
                            }
                        }
                        #endregion
                        #region  得到产品属性
                        if (item.FieldName == "specifications")
                        {
                            //获取specifications字符
                            string specifications = string.Empty;
                            foreach (ItemConfig.RegexString rs in item.Reg_GetValue)
                            {

                                MatchCollection mc = RegexHelper.MatchCollection(pageCode, rs[0]);
                                foreach (Match mt in mc)
                                {
                                    string att = mt.Groups[item.FieldName].Value;
                                    if (att != string.Empty)
                                        specifications = specifications + "<br>" + att;
                                }
                                if (!item.Reg_GetValue.IsAnd && specifications != string.Empty)
                                    break;
                            }
                            eProduct.Specifications = specifications;
                            eProduct.Specifications = StringHelper.GetDescription(eProduct.Specifications, spiderSite.eSite.SiteName);
                            //正则替换处理specifications
                            if (item.Reg_GetValue_Replace.Count > 0 && eProduct.Specifications != string.Empty)
                            {

                                eProduct.Specifications = GetReplaceValue(eProduct.Specifications, item.Reg_GetValue_Replace);
                            }
                        }
                        #endregion
                        #region 得到产品型号
                        ///图书提取型号 电影 音乐 UPCOrISBN 提取的是Format 
                        ///
                        if (item.FieldName == "isbn10")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                eProduct.UPCOrISBN = StringHelper.GetLineTextStringNoSpace(value);
                            }
                        }

                        if (item.FieldName == "upcorisbn")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                if (eProduct.UPCOrISBN != string.Empty)
                                    eProduct.UPCOrISBN = eProduct.UPCOrISBN + " ";
                                eProduct.UPCOrISBN = StringHelper.GetLineTextStringNoSpace(value);
                            }
                        }

                        if (item.FieldName == "model")
                        {
                            string value = GetMatchGroupValue(pageCode, item.Reg_GetValue, item.FieldName);
                            if (value != string.Empty)
                            {
                                eProduct.Model = StringHelper.GetLineTextStringNoSpace(value);
                            }
                        }
                        #endregion
                        #region 得到产品图片
                        if (item.FieldName == "bigimage")
                        {
                            string bigimage = GetMatchGroupValue(pageCode, item.Reg_GetValue, "bigimage");

                            if (bigimage != string.Empty)
                            {
                                eProduct.ImageUrl = UrlProcess.ChangeUrl(eProduct.ResourceUrl, StringHelper.GetLineTextStringNoSpace(bigimage));
                            }
                        }
                        #endregion
                        #region 得到产品描述
                        if (item.FieldName == "content")
                        {
                            eProduct.Description = GetMatchGroupValue(pageCode, item.Reg_GetValue, "content").Trim();
                            eProduct.Description = StringHelper.GetDescription(eProduct.Description, spiderSite.eSite.SiteName);
                            if (item.Reg_GetValue_Replace.Count > 0 && eProduct.Description != string.Empty)
                            {
                                eProduct.Description = GetReplaceValue(eProduct.Description, item.Reg_GetValue_Replace);
                            }
                        }
                        #endregion
                        #region 图书影视音乐 作者
                        if (item.FieldName == "author")
                        {
                            MatchCollection mcs = RegexHelper.MatchCollection(pageCode, item.Reg_GetValue[0][0]);
                            foreach (Match mc in mcs)//多个作者
                            {
                                if (mc.Groups["author"].Value != "")
                                {
                                    if (eProduct.Author == string.Empty)
                                    {
                                        eProduct.Author = StringHelper.GetLineTextString(mc.Groups["author"].Value);
                                    }
                                    else
                                    {
                                        eProduct.Author = eProduct.Author + " , " + StringHelper.GetLineTextString(mc.Groups["author"].Value);
                                    }
                                }
                            }
                        }
                        if (item.FieldName == "aboutauthor")
                        {
                            string aboutauthor = RegexHelper.GetMatchGroupValue(pageCode, item.Reg_GetValue[0][0], "aboutauthor");
                            if (aboutauthor != string.Empty)
                            {
                                eProduct.AboutAuthor = StringHelper.GetLineTextString(aboutauthor).Trim();
                            }
                            if (item.Reg_GetValue_Replace.Count > 0 && eProduct.AboutAuthor != string.Empty)
                            {
                                eProduct.AboutAuthor = GetReplaceValue(eProduct.AboutAuthor, item.Reg_GetValue_Replace);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            int time = Convert.ToInt32((DateTime.Now.Ticks - begintime) / 10000);
            if (time > 1000)
                logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析产品页耗时" + time + "毫秒。  " + eProduct.ResourceUrl));

            #region 分析产品评论
            AnalysisCommentPage(ItemPageCode);
            #endregion

            //如果产品价格没有提取则分析失败
            if (spiderSite.PriceStyle==2&&eProduct.UsedPrice == 0 && eProduct.Price == 0&&eProduct.RentPrice==0)
            {
                logger.Error(new LogMessage(spiderSite.eSite.SiteName,"analysis product page(" + eProduct.ResourceUrl + ")can't get price or UsedPrice!"));
                return false;
            }
            ////如果产品类型是图书且没有提取到ISBN则分析失败
            //if (eProduct.Model == string.Empty && eProduct.UPCOrISBN == string.Empty && spiderSite.eSite.AnalysisCategoryID == 12)
            //{
            //    logger.Error(new LogMessage(spiderSite.eSite.SiteName,"Error:analysis product page for Book (" + eProduct.ResourceUrl + ")can't get ISBN!"));
            //    return false;
            //}            
            return true;
        }

        public void AnalysisCommentPage(string ItemPageCode)
        {
            //已存在并且更新评论  或者 新的产品
            if ((eProduct.IsExist && spiderSite.ProductCommentConfig.IsUpdateComment) || !eProduct.IsExist)
            { 

                string commentPageUrl = string.Empty;
                if (spiderSite.ProductCommentConfig.Reg_GetCommentPageUrl != string.Empty)
                    commentPageUrl = GetSecondPageUrl(ItemPageCode, spiderSite.ProductCommentConfig.Reg_GetCommentPageUrl);
                if (commentPageUrl == string.Empty)
                    return;
                string firstPageCode = PageHelper.ReadUrl(commentPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                if (firstPageCode == "")
                {
                    firstPageCode = PageHelper.ReadUrl(commentPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                    if (firstPageCode == "")
                    {
                        logger.Info(new LogMessage(spiderSite.eSite.SiteName, "分析评论页面(" + commentPageUrl + ") 读取页面错误!"));
                    }
                }

                int maxPage = CommonFun.StrToInt(RegexHelper.GetMatchGroupValue(firstPageCode, spiderSite.ProductCommentConfig.Reg_GetCommentMaxPageCount, "commentmaxpage"), 1);
                //最多不超过10页
                for (int i = 1; i <= maxPage && i < 10; i++)
                {
                    string PageUrl = commentPageUrl;
                    #region 获取页面代码
                    if (i > 1)
                    {
                        string[] RepalcePno = StringHelper.SplitForReplace_Child(spiderSite.ProductCommentConfig.Replace_CommentPageReplacePno);
                        PageUrl = Regex.Replace(commentPageUrl, RepalcePno[0], RepalcePno[1], RegexOptions.IgnoreCase, TimeSpan.FromSeconds(30)).Replace("[pno]", i.ToString());
                        firstPageCode = PageHelper.ReadUrl(PageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                        if (firstPageCode == "")
                        {
                            firstPageCode = PageHelper.ReadUrl(PageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                            if (firstPageCode == "")
                            {
                                logger.Info(new LogMessage(spiderSite.eSite.SiteName, "分析评论页面(" + PageUrl + ") 读取页面错误!"));
                            }
                        }
                    }
                    if (spiderSite.ProductCommentConfig.Str_CommentPageBeginOrAndCode != string.Empty)
                    {

                        string[] CommentPageBeginAndEnd = StringHelper.SplitForArray(spiderSite.ProductCommentConfig.Str_CommentPageBeginOrAndCode, true);
                        if (CommentPageBeginAndEnd[0] != string.Empty)
                        {
                            int beginIndex = firstPageCode.IndexOf(CommentPageBeginAndEnd[0]);
                            if (beginIndex > 0)
                            {
                                firstPageCode = firstPageCode.Substring(beginIndex);
                            }
                            else
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis productcomment page(" + PageUrl + ") begin substring error!"));
                                return;
                            }
                        }
                        if (CommentPageBeginAndEnd.Length == 2)
                        {
                            int endIndex = firstPageCode.LastIndexOf(CommentPageBeginAndEnd[1]);
                            if (endIndex > 0)
                            {
                                firstPageCode = firstPageCode.Substring(0, endIndex + CommentPageBeginAndEnd[1].Length + 1);
                            }
                            else
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis productcomment page(" + PageUrl + ") end substring error!"));
                                return;
                            }
                        }
                    }

                    #endregion

                    MatchCollection mcComment = RegexHelper.MatchCollection(firstPageCode, spiderSite.ProductCommentConfig.Reg_GetComment);
                    //跳跃抓取评论
                    int step = 2;
                    if (maxPage > 3)
                        step = 3;
                    if (maxPage > 5)
                        step = 4;
                    for (int m = 0; m < mcComment.Count; m = m + step)
                    {
                        Match mc = mcComment[m];
                        EProductComment epc = new EProductComment();
                        epc.SiteID = eProduct.SiteID;
                        epc.ProductID = eProduct.ProductID;
                        epc.UserName = mc.Groups["username"].Value.Trim();
                        epc.Title = mc.Groups["title"].Value.Trim();
                        epc.Comment = mc.Groups["comment"].Value.Trim();
                        if (epc.Comment == string.Empty)
                        {
                            logger.Error(new LogMessage(spiderSite.eSite.SiteName, "抓取评论内容出错")); 
                            break;
                        }
                        string checkindate = mc.Groups["checkindate"].Value.Trim();
                        if(mc.Groups["score"].Value.Trim()!="")
                            epc.Score = Convert.ToInt32(Convert.ToSingle(mc.Groups["score"].Value.Trim()));
                        DateTime dt = DateTime.Now;
                        if (checkindate != string.Empty)
                        {
                            if (!DateTime.TryParse(checkindate, out dt))
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "抓取评论是，日期转换出错(" + checkindate + ")"));
                        }
                        epc.CheckInTime = dt;
                        eProduct.CommentList.Add(epc);
                    }
                }
                if (eProduct.ScoreCount > 3 && eProduct.CommentList.Count == 0)
                    logger.Info(new LogMessage(spiderSite.eSite.SiteName, "抓取评论提取异常( " + commentPageUrl + " 评论总数(" + eProduct.ScoreCount + ");实际抓取评论数(" + eProduct.CommentList.Count + ") )"));  
            }
        }
        /// <summary>
        /// &&&&&代表正则必选执行，下一个以上一个结果为Code；|||||代表如果前面正则执行有结果则不必执行否则需要执行。
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ItemPageCode"></param>
        /// <returns></returns>
        public string GetCode(ItemConfig item, string ItemPageCode) 
        {
            string itempagecode = string.Empty;
            foreach (ItemConfig.RegexString rs in item.Reg_GetCode)//第一层&&&&& or |||||
            { 
                foreach (string reg_getcode in rs)//第二层 &&&& or ||||
                {
                    itempagecode = RegexHelper.GetMatchValue(ItemPageCode, reg_getcode).Trim();
                    if (itempagecode != string.Empty)
                        ItemPageCode = itempagecode;

                    if (!rs.IsAnd && itempagecode != string.Empty)
                        break;
                }
                if (!item.Reg_GetCode.IsAnd && itempagecode != string.Empty)
                    break;

            } 

            //需要第二次读取页面
           if (item.Reg_GetValue_Url != string.Empty)
           {
               string fieldname_url = RegexHelper.GetMatchGroupValue(ItemPageCode, item.Reg_GetValue_Url, "fieldname_url");
               if (fieldname_url != string.Empty)
               {
                   fieldname_url = UrlProcess.ChangeUrl(eProduct.ResourceUrl, StringHelper.GetLineTextString(fieldname_url));
                   ItemPageCode = PageHelper.ReadUrl(fieldname_url, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                   if (ItemPageCode == string.Empty)
                   {
                       logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析产品页面读取 (" + fieldname_url + ")出错!"));
                   }
               } 
           }
           return ItemPageCode;
        }
        /// <summary>
        /// &&&&&代表正则必选执行,结果相加；|||||代表如果前面正则执行有结果则不必执行否则需要执行。
        /// </summary>
        /// <param name="pageCode"></param>
        /// <param name="es"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public string GetMatchGroupValue(string pageCode, ItemConfig.RegexStrings es, string fieldName)
        {
            string value=string.Empty;
            foreach (ItemConfig.RegexString rs in es)
            {
                foreach (string reg in rs)
                {
                    string val = RegexHelper.GetMatchGroupValue(pageCode, reg, fieldName).Trim();
                    value = value + val;
                    if (!rs.IsAnd && val != string.Empty)
                        break;
                }
                if (!es.IsAnd && value != string.Empty)
                    break;
            }
            return value;
        }
        public string GetSecondPageUrl(string ItemPageCode,string pageurl)
        {
            string[] GetCommentPageUrl = pageurl.Trim().Split(new string[] { "&&&&&" }, StringSplitOptions.RemoveEmptyEntries);
            if (GetCommentPageUrl.Length == 0)
                return string.Empty;
            string commentPageUrl = RegexHelper.GetMatchGroupValue(ItemPageCode, GetCommentPageUrl[0].Trim(), "commentpageurl");
            if(commentPageUrl!=string.Empty)
                commentPageUrl=UrlProcess.ChangeUrl(eProduct.ResourceUrl,commentPageUrl);
            for (int i = 1; i < GetCommentPageUrl.Length;i++ )
            {
                ItemPageCode = PageHelper.ReadUrl(commentPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                commentPageUrl = RegexHelper.GetMatchGroupValue(ItemPageCode, GetCommentPageUrl[i].Trim(), "commentpageurl");
                if (commentPageUrl != string.Empty)
                    commentPageUrl = UrlProcess.ChangeUrl(eProduct.ResourceUrl, commentPageUrl);
            }
            return commentPageUrl;
        }
        public string GetReplaceValue(string value, ItemConfig.RegexStrings es)
        { 
            foreach (ItemConfig.RegexString rs in es)
            {
                long begintime = DateTime.Now.Ticks;
                string tempValue = value;
               
                string[] value_Replaces = StringHelper.SplitForReplace_Child(rs[0], true);
                if (value_Replaces.Length == 2)
                {
                    value = Regex.Replace(value, value_Replaces[0], value_Replaces[1],RegexOptions.IgnoreCase,TimeSpan.FromSeconds(30));
                }               
                int time = Convert.ToInt32((DateTime.Now.Ticks - begintime) / 10000);
                if (time > 500)
                    logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析产品页,正则替换耗时" + time + "毫秒。 “ " + value_Replaces + "” “" + tempValue + "”"));                
            }

            return value;
        }       
    }
}
