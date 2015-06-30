using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
using FZ.Spider.DAL.Data.Search;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class AdProduct : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAdCategory();
                BindData();
            }
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("请选择广告系列", "0"));
        }

        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField = "AdGroupName";
            ddlAdGroup.DataValueField = "AdGroupID";
            ddlAdGroup.DataBind();
            ddlAdGroup.Items.Insert(0, new ListItem("请选择广告组...", "0"));
        }
        protected void BindAdList()
        {
            ddlAds.DataSource = DAds.GetAdsList(CommonFun.StrToInt(ddlAdGroup.SelectedValue),1);
            ddlAds.DataTextField = "AdTitle";
            ddlAds.DataValueField = "AdID";
            ddlAds.DataBind();
            ddlAds.Items.Insert(0, new ListItem("选择广告", "0"));
        }
        protected void BindData()
        {
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";
            qe.TempTableColumns = "  adp.*,pro.FullName,pro.CategoryID,pro.ImageType,ps.minPrice,ps.MaxPrice,pro.UPCOrISBN,pl.Price,pl.UsedPrice,pl.OrgPrice,pl.RentPrice,pl.SiteID,pl.ResourceUrl,ps.CommentCount,ps.Score,ps.ScoreUsers as ScoreCount,ps.ShopCount  ";
                
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            if(ddlAds.SelectedValue!="0")
                qe.Conditions = " where adp.ADID=" + ddlAds.SelectedValue + "  ";
            qe.Orderby = " adp.ordid desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_SEM_AdProductList adp left join TB_SEARCH_PRODUCT as pro on adp.ProductID=Pro.ProductID left join TB_SEARCH_PRODUCTList as pl on adp.ProductID=pl.ProductID  left join tb_search_productshow as ps on adp.ProductID=ps.ProductID  ";
            qe.TotalRecord = 0;

            gvDataList.DataSource = DAdProduct.GetAdProductList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
            
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        { 
            int AdID = CommonFun.StrToInt(ddlAds.SelectedValue);
            if (AdID == 0)
            {
                Alert("请选择广告");
                return;
            }
            string[] ProductIDs = txtProductIDs.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pid in ProductIDs)
            {
                string IDStr= pid.Trim();
                int productID=0;
                if (StringHelper.IsNumberByStr(IDStr))
                {
                    productID = CommonFun.StrToInt(IDStr);
                }
                else if(IDStr.Length == 10)
                {
                    productID = DProductList.GetProductIDByASIN(IDStr);
                }
                if (productID == 0 || DAdProduct.Exist(AdID, productID)) continue;
                DAdProduct.Add(AdID, productID);
            } 
            Cancel();
            BindData();               
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EAdProduct eAdProduct = (EAdProduct)e.Row.DataItem;
                Literal lit = (Literal)e.Row.FindControl("litFullName");              

                lit.Text ="<a href=\""+UrlHelper.GetProductUrl(eAdProduct.ProductID,FZ.Spider.Cache.DictionaryCache.GetCategory(eAdProduct.CategoryID).FirstCategoryName,eAdProduct.FullName)+"\" target=_blank >"+eAdProduct.FullName+"</a>";
                if(eAdProduct.OrgPrice>0&&eAdProduct.Price>0&&eAdProduct.OrgPrice>eAdProduct.Price)
                {
                    Literal litDiscount = (Literal)e.Row.FindControl("litDiscount");
                    litDiscount.Text =Convert.ToInt32(((eAdProduct.OrgPrice - eAdProduct.Price) / eAdProduct.OrgPrice)*100)+"%";
                }
            }
        }
       
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        { 
            CheckBox cbox = (CheckBox)gvDataList.Rows[e.RowIndex].FindControl("OrdID");
            int ordid = CommonFun.StrToInt(cbox.Text);  
            if (ordid > 0)
            {
                DAdProduct.Delete(ordid); 
                BindData();
            }
        }
        protected void gvDataList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDataList.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvDataList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDataList.EditIndex = -1;
            BindData();
        }

        protected void ddlAdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdGroup();
        }
        protected void Cancel()
        { 
            txtProductIDs.Text = "";
            
            this.btnSave.Text = "添加";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void ddlAdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdList();
        }

        protected void ddlAds_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnDeleteAdProduct_Click(object sender, EventArgs e)
        {
            string scids = GetGridViewCheckBoxList(gvDataList, "ordid");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            DAdProduct.Delete(scids);
            BindData();
        }
    }
}