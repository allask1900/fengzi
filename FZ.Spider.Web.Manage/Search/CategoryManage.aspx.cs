using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;


using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Data.Search;
using System.Collections.Generic;

namespace FZ.Spider.Web.Manage.Search
{
    public partial class CategoryManage : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindCategory_1();
                BindCategory_2();
                BindCategory_3();
            }
        }

        #region bindData
        protected void BindCategory_1()
        {
            List<ECategory> categoryList = DCategory.GetList(0, 0);

            this.dropCategory_1.DataTextField = "CategoryName";
            this.dropCategory_1.DataValueField = "CategoryID";
            this.dropCategory_1.DataSource = categoryList;
            this.dropCategory_1.DataBind();
            this.dropCategory_1.Items.Insert(0, new ListItem("请选择", "0"));

            this.dropFirstCategory.DataTextField = "CategoryName";
            this.dropFirstCategory.DataValueField = "CategoryID";
            this.dropFirstCategory.DataSource = categoryList;
            this.dropFirstCategory.DataBind();
            this.dropFirstCategory.Items.Insert(0, new ListItem("请选择", "0"));
        }
        protected void BindCategory_2()
        {
            string categoryID_1 = this.dropCategory_1.SelectedValue;
            if (categoryID_1 != null && categoryID_1 != "0")
            {
                List<ECategory> categoryList = DCategory.GetList(Convert.ToInt32(categoryID_1), 1); 
                this.dropCategory_2.DataTextField = "CategoryName";
                this.dropCategory_2.DataValueField = "CategoryID";
                this.dropCategory_2.DataSource = categoryList;
                this.dropCategory_2.DataBind();
                this.dropCategory_2.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindCategory_3()
        {
            string categoryID_2 = this.dropCategory_2.SelectedValue;
            if (categoryID_2 != null && categoryID_2 != "0" && categoryID_2 != "")
            {
                List<ECategory> categoryList = DCategory.GetList(Convert.ToInt32(categoryID_2), 2);                
                this.dropCategory_3.DataTextField = "CategoryName";
                this.dropCategory_3.DataValueField = "CategoryID";
                this.dropCategory_3.DataSource = categoryList;
                this.dropCategory_3.DataBind();
                this.dropCategory_3.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindCategory_4()
        {
            string categoryID_3 = this.dropCategory_3.SelectedValue;
            if (categoryID_3 != null && categoryID_3 != "0" && categoryID_3 != "")
            {
                List<ECategory> categoryList = DCategory.GetList(Convert.ToInt32(categoryID_3), 3);     
                this.dropCategory_4.DataTextField = "CategoryName";
                this.dropCategory_4.DataValueField = "CategoryID";
                this.dropCategory_4.DataSource = categoryList;
                this.dropCategory_4.DataBind();
                this.dropCategory_4.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        #endregion

        #region ClickButton
        /// <summary>
        /// 大类添加或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCategory_1_Click(object sender, EventArgs e)
        {
            if (this.txtCategoryName_1.Text == "")
            {
                return;
            }
            ECategory eCategory = new ECategory();
            eCategory.ParentCategoryID = 0;
            eCategory.CategoryLevel = 0;
            eCategory.CategoryName = txtCategoryName_1.Text.Trim();
            if (eCategory.CategoryName == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            eCategory.HasChild = false;
            if (this.dropCategory_1.SelectedValue != "0")
            {
                eCategory.CategoryID = Convert.ToInt32(dropCategory_1.SelectedValue);
                if (DCategory.ExistsCategoryNameForUpdate(eCategory))
                {
                      Alert("不能修改,存在相同名称!");
                    return;
                }
                DCategory.Update(eCategory);
            }
            else
            {
                eCategory.CategoryID =0;
                if (DCategory.ExistsCategoryNameForAdd(eCategory)>0)
                {
                    Alert("不能添加,存在相同名称!");
                    return;
                }
                DCategory.Add(eCategory);
            }
            BindCategory_1();
            this.txtCategoryName_1.Text = "";
        }
        protected void btnCategory_2_Click(object sender, EventArgs e)
        {
            if (this.dropCategory_1.SelectedValue == "0")
            {
                Alert("未选父类!");
                return;
            }
            ECategory eCategory = new ECategory();
            eCategory.CategoryLevel = 1;
            eCategory.CategoryName =txtCategoryName_2.Text.Trim();
            if (eCategory.CategoryName == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            eCategory.HasChild = cBoxCategory_2.Checked;
            eCategory.ParentCategoryID = Convert.ToInt32(dropCategory_1.SelectedValue);
            if (dropCategory_2.SelectedValue != "0" && dropCategory_2.SelectedValue != "")
            {
                eCategory.CategoryID = Convert.ToInt32(dropCategory_2.SelectedValue);
                if (DCategory.ExistsCategoryNameForUpdate(eCategory))
                {
                    Alert("不能修改,存在相同名称!");
                    return;
                }
                DCategory.Update(eCategory);
            }
            else
            {
                //用于批量提交
                string[] CategoryNameS = txtCategoryName_2.Text.Trim().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < CategoryNameS.Length; i++)
                {
                    eCategory.CategoryName = CategoryNameS[i].Trim();
                    if (DCategory.ExistsCategoryNameForAdd(eCategory) > 0)
                    {
                        Alert("不能添加,存在相同名称!");
                        return;
                    }
                    DCategory.Add(eCategory);
                }
            }
            BindCategory_2();
            this.txtCategoryName_2.Text = "";
        }
        protected void btnCategory_3_Click(object sender, EventArgs e)
        {
            if (dropCategory_2.SelectedValue == "")
            {
                Alert("未选父类!");
                return;
            }
            
            ECategory eCategory = new ECategory();
            eCategory.CategoryLevel = 2;
            eCategory.CategoryName = txtCategoryName_3.Text.Trim();
            if (eCategory.CategoryName == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            eCategory.HasChild = cBoxCategory_3.Checked;
            eCategory.ParentCategoryID = Convert.ToInt32(dropCategory_2.SelectedValue);
            if (dropCategory_3.SelectedValue != "0" && dropCategory_3.SelectedValue != "")
            {
                eCategory.CategoryID = Convert.ToInt32(dropCategory_3.SelectedValue);
                if (DCategory.ExistsCategoryNameForUpdate(eCategory))
                {
                    Alert("不能修改,存在相同名称!");
                    return;
                }
                DCategory.Update(eCategory); 
            }
            else
            {
                //用于批量提交
                string[] CategoryNameS = txtCategoryName_3.Text.Trim().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < CategoryNameS.Length; i++)
                {
                    if (CategoryNameS[i].Trim() != "")
                    {
                        eCategory.CategoryName = CategoryNameS[i].Trim();
                        if (DCategory.ExistsCategoryNameForAdd(eCategory)>0)
                        {
                            Alert("不能添加,存在相同名称!");                            
                        }
                        DCategory.Add(eCategory);
                    }
                }
                txtCategoryName_3.Text = string.Empty;
            }
            BindCategory_3();
            this.txtCategoryName_3.Text = "";
        }
        protected void btnCategory_4_Click(object sender, EventArgs e)
        {
            if (dropCategory_3.SelectedValue == "0")
            {
                Alert("未选父类!");
                return;
            }             
            ECategory eCategory = new ECategory();
            eCategory.CategoryLevel = 3;
            eCategory.CategoryName = txtCategoryName_4.Text.Trim();
            if (eCategory.CategoryName == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            eCategory.HasChild = false;
            eCategory.ParentCategoryID = Convert.ToInt32(dropCategory_3.SelectedValue);
            if (dropCategory_4.SelectedValue != "0" && dropCategory_4.SelectedValue != "")
            {
                eCategory.CategoryID = Convert.ToInt32(dropCategory_4.SelectedValue);
                if (DCategory.ExistsCategoryNameForUpdate(eCategory))
                {
                    Alert("不能修改,存在相同名称!");
                    return;
                }
                DCategory.Update(eCategory);
            }
            else
            {
                //用于批量提交
                string[] CategoryNameS = txtCategoryName_4.Text.Trim().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < CategoryNameS.Length; i++)
                {
                    eCategory.CategoryName = CategoryNameS[i].Trim();
                    if (DCategory.ExistsCategoryNameForAdd(eCategory) > 0)
                    {
                        Alert("不能添加,存在相同名称!");
                        return;
                    }
                    DCategory.Add(eCategory);
                }
            }
            BindCategory_4();
            this.txtCategoryName_4.Text = "";
        }
        #endregion

        #region SelectedIndexChanged
        protected void dropCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            ECategory eCategory = DCategory.GetEntity(Convert.ToInt32(dropCategory_1.SelectedItem.Value));             
            this.txtCategoryName_1.Text = eCategory.CategoryName;
            BindCategory_2();
        }
        protected void dropCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ECategory eCategory = DCategory.GetEntity(Convert.ToInt32(dropCategory_2.SelectedItem.Value));
            this.txtCategoryName_2.Text = eCategory.CategoryName;          
            this.cBoxCategory_2.Checked = eCategory.HasChild;
            BindCategory_3();
        }
        protected void dropCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ECategory eCategory = DCategory.GetEntity(Convert.ToInt32(dropCategory_3.SelectedItem.Value));
            this.txtCategoryName_3.Text = eCategory.CategoryName;
            BindCategory_4();
        }
        protected void dropCategory_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ECategory eCategory = DCategory.GetEntity(Convert.ToInt32(dropCategory_4.SelectedItem.Value));
            this.txtCategoryName_4.Text = eCategory.CategoryName;             
        }

        protected void dropFirstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dropFirstCategory.SelectedItem != null && dropFirstCategory.SelectedValue != "0")
            {
                ECategory eCategory = DCategory.GetEntity(Convert.ToInt32(dropFirstCategory.SelectedValue));
                this.txtHotWord.Text = eCategory.HotWord;
                this.txtKeyWord.Text = eCategory.KeyWord;
                this.txtPageDescription.Text = eCategory.Description;
                this.txtPageTitle.Text = eCategory.PageTitle;
            }
        }
        #endregion

        protected void bntMetaInfo_Click(object sender, EventArgs e)
        {
            if (this.dropFirstCategory.SelectedItem != null && dropFirstCategory.SelectedValue != "0")
            {
                ECategory eCategory = new ECategory();
                eCategory.CategoryID = Convert.ToInt32(dropFirstCategory.SelectedValue);
                eCategory.HotWord= this.txtHotWord.Text;
                eCategory.KeyWord= this.txtKeyWord.Text;
                eCategory.Description= this.txtPageDescription.Text ;
                eCategory.PageTitle = this.txtPageTitle.Text;
                DCategory.UpdateMetaInfo(eCategory);
                this.txtHotWord.Text = "";
                this.txtKeyWord.Text = "";
                this.txtPageDescription.Text = "";
                this.txtPageTitle.Text = "";
            }
        } 
    }
}