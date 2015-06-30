using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FZ.Spider.Web.WebControl
{
    /// <summary>
    /// DataList 的摘要说明
    /// </summary>
    [ToolboxData(@"<{0}:DataList runat='server'></{0}:DataList>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class DataList : System.Web.UI.WebControls.DataList, IPostBackEventHandler
    {
        public event EventHandler<DataListPageEventArgs> PageIndexChanging;

        #region 属性
        public int RecordCount
        {
            get
            {
                object o = ViewState["RecordCount"];

                return o == null ? 0 : Convert.ToInt32(o);
            }
            set
            {
                ViewState["RecordCount"] = value;
            }
        }

        public virtual int PageIndex
        {
            get
            {
                object o = ViewState["PageIndex"];

                return o == null ? 0 : Convert.ToInt32(o);
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }

        [DefaultValue(10)]
        public virtual int PageSize
        {
            get
            {
                object o = ViewState["PageSize"];

                return o == null ? 10 : Convert.ToInt32(o);
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }

        private PagerSettings _settings;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public PagerSettings PagerSettings
        {
            get
            {
                if (_settings == null)
                    _settings = new PagerSettings();
                if (base.IsTrackingViewState)
                    ((IStateManager)_settings).TrackViewState();

                return this._settings;
            }
        }

        public bool EnablePaging
        {
            get
            {
                object o = ViewState["EnablePaging"];

                return o == null ? false : Convert.ToBoolean(o);
            }
            set
            {
                ViewState["EnablePaging"] = value;
            }
        }

        #endregion

        protected virtual void OnPageIndexChanging(object sender, DataListPageEventArgs e)
        {
            if (PageIndexChanging != null)
            {
                PageIndexChanging(sender, e);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.RenderBeginTag(writer);

            if (EnablePaging)
            {
                Table table = new Table();
                TableRow row = new TableRow();
                table.Rows.Add(row);
                TableCell cell = new TableCell();

                row.RenderBeginTag(writer);
                cell.RenderBeginTag(writer);

                base.RenderContents(writer);

                cell.RenderEndTag(writer);
                row.RenderEndTag(writer);

                TableRow rowpager = new TableRow();

                //计算页链接并加入到单元格中
                DataListPager pager = new DataListPager(PagerSettings, PageIndex, RecordCount, PageSize, this);

                rowpager.Cells.Add(pager);

                rowpager.RenderBeginTag(writer);

                pager.RenderControl(writer);

                rowpager.RenderEndTag(writer);

            }
            else
            {
                base.RenderContents(writer);
            }


            this.RenderEndTag(writer);
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            string[] args = eventArgument.Split(Constant.ARGUMENT_SPLITTER);

            if (args == null || args.Length < 1)
                return;

            string name = args[0];

            if (name == Constant.PAGE_ARGUMENT)
            {
                int index = 0;
                string argvalue = args[1];
                bool isInt = int.TryParse(argvalue, out index);

                if (isInt)
                {
                    DataListPageEventArgs arg = new DataListPageEventArgs();
                    arg.NewPageIndex = index;

                    OnPageIndexChanging(this, arg);
                }
            }
        }

        protected override object SaveViewState()
        {
            object o = base.SaveViewState();
            object osetting = ((IStateManager)PagerSettings).SaveViewState();
            Pair p = new Pair(o, osetting);

            return p;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Pair p = (Pair)savedState;
                base.LoadViewState(p.First);
                ((IStateManager)PagerSettings).LoadViewState(p.Second);
            }
            else
            {
                base.LoadViewState(null);
            }
        }
    }

    public class DataListPageEventArgs : EventArgs
    {
        public int NewPageIndex
        {
            get;
            set;
        }
    }
    /// <summary>
    /// DataPager 的摘要说明
    /// </summary>
    public class DataPager : TableCell
    {
        private int _pageIndex;
        private int _recordCount;
        private int _pageSize;
        private int _pageCount;
        private PagerSettings _settings;

        public DataPager(PagerSettings setting, int pageIndex, int recordCount, int pageSize)
        {
            _settings = setting;
            _pageIndex = pageIndex;
            _recordCount = recordCount;
            _pageSize = pageSize;

            _pageCount = _recordCount % _pageSize == 0 ? _recordCount / _pageSize : _recordCount / _pageSize + 1;

            //生成分页
            GenerateDataPage();
        }

        private void GenerateDataPage()
        {
            if (_settings.Mode == PagerButtons.NextPrevious || _settings.Mode == PagerButtons.NextPreviousFirstLast)
            {
                GeneratePrevNextPage();
            }
            else if (_settings.Mode == PagerButtons.Numeric || _settings.Mode == PagerButtons.NumericFirstLast)
            {
                GenerateNumericPage();
            }
        }

        private void GeneratePrevNextPage()
        {
            GeneratePage(false);
        }

        private void GenerateNumericPage()
        {
            GeneratePage(false);
        }

        private void GeneratePage(bool generateNumber)
        {
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            if (_recordCount > 0)
            {
                this.Controls.Add(new LiteralControl("共&nbsp;"));
                this.Controls.Add(new LiteralControl(_recordCount.ToString()));
                this.Controls.Add(new LiteralControl("&nbsp;条记录&nbsp;&nbsp;每页&nbsp;"));
                this.Controls.Add(new LiteralControl(_pageSize.ToString()));
                this.Controls.Add(new LiteralControl("&nbsp;条记录&nbsp;&nbsp;"));
            }


            this.Controls.Add(new LiteralControl((_pageIndex + 1).ToString()));
            this.Controls.Add(new LiteralControl("/"));
            this.Controls.Add(new LiteralControl(_pageCount.ToString()));
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

            LinkButton btnFrist = new LinkButton();
            LinkButton btnPrev = new LinkButton();
            LinkButton btnNext = new LinkButton();
            LinkButton btnLast = new LinkButton();

            if (!String.IsNullOrEmpty(_settings.FirstPageImageUrl))
            {
                btnFrist.Text = "<img src='" + ResolveUrl(_settings.FirstPageImageUrl) + "' border='0'/>";
            }
            else
            {
                btnFrist.Text = "首页";
            }
            btnFrist.CommandName = Constant.PAGE_ARGUMENT;
            btnFrist.CommandArgument = Constant.FIRST_PAGE;
            btnFrist.Font.Underline = false;

            if (!String.IsNullOrEmpty(_settings.PreviousPageImageUrl))
            {
                btnPrev.Text = "<img src='" + ResolveUrl(_settings.PreviousPageImageUrl) + "' border='0'/>";
            }
            else
            {
                btnPrev.Text = "上一页";
            }
            btnPrev.CommandName = Constant.PAGE_ARGUMENT;
            btnPrev.CommandArgument = Constant.PREV_PAGE;
            btnPrev.Font.Underline = false;

            if (!String.IsNullOrEmpty(_settings.NextPageImageUrl))
            {
                btnNext.Text = "<img src='" + ResolveUrl(_settings.NextPageImageUrl) + "' border='0'/>";
            }
            else
            {
                btnNext.Text = "下一页";
            }
            btnNext.CommandName = Constant.PAGE_ARGUMENT;
            btnNext.CommandArgument = Constant.NEXT_PAGE;
            btnNext.Font.Underline = false;

            if (!String.IsNullOrEmpty(_settings.LastPageImageUrl))
            {
                btnLast.Text = "<img src='" + ResolveUrl(_settings.LastPageImageUrl) + "' border='0'/>";
            }
            else
            {
                btnLast.Text = "尾页";
            }
            btnLast.CommandName = Constant.PAGE_ARGUMENT;
            btnLast.CommandArgument = Constant.LAST_PAGE;
            btnLast.Font.Underline = false;

            if (this._pageIndex <= 0)
            {
                btnFrist.Enabled = btnPrev.Enabled = false;
            }
            else
            {
                btnFrist.Enabled = btnPrev.Enabled = true;
            }

            this.Controls.Add(btnFrist);
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            this.Controls.Add(btnPrev);
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            if (generateNumber)//是否生成数字页码
            {
                GenerateNumber();
            }

            if (this._pageIndex >= _pageCount - 1)
            {
                btnNext.Enabled = btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = btnLast.Enabled = true;
            }
            this.Controls.Add(btnNext);
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            this.Controls.Add(btnLast);
            this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        }

        private void GenerateNumber()
        {
            // 当前页左边显示的数字分页按钮的数量
            int rightCount = (int)(_settings.PageButtonCount / 2);
            // 当前页右边显示的数字分页按钮的数量
            int leftCount = _settings.PageButtonCount % 2 == 0 ? rightCount - 1 : rightCount;
            for (int i = 0; i < _pageCount; i++)
            {
                if (_pageCount > _settings.PageButtonCount)
                {
                    if (i < _pageIndex - leftCount && _pageCount - 1 - i > _settings.PageButtonCount - 1)
                    {
                        continue;
                    }
                    else if (i > _pageIndex + rightCount && i > _settings.PageButtonCount - 1)
                    {
                        continue;
                    }
                }

                if (i == _pageIndex)
                {
                    this.Controls.Add(new LiteralControl("<span style='color:red;font-weight:bold'>" + (i + 1).ToString() + "</span>"));
                }
                else
                {
                    LinkButton lb = new LinkButton();
                    lb.Text = (i + 1).ToString();
                    lb.CommandName = Constant.PAGE_ARGUMENT;
                    lb.CommandArgument = (i + 1).ToString();


                    this.Controls.Add(lb);
                }

                this.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            }
        }
    }
    /// <summary>
    /// DataListPager 的摘要说明
    /// </summary>
    public class DataListPager : DataPager
    {
        public DataListPager(PagerSettings setting, int pageIndex, int recordCount, int pageSize, DataList dal)
            : base(setting, pageIndex, recordCount, pageSize)
        {
            LinkButton btn = null;
            int _pageCount = recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;
            int index;

            foreach (Control control in this.Controls)
            {
                if (control is LinkButton)
                {
                    btn = (LinkButton)control;

                    if (btn.Enabled)
                    {
                        string argvalue = btn.CommandArgument;

                        bool isInt = int.TryParse(argvalue, out index);
                        string arg = string.Empty;
                        if (isInt)
                        {
                            index--;
                        }
                        else
                        {
                            switch (argvalue)
                            {
                                case Constant.FIRST_PAGE: index = 0;
                                    break;
                                case Constant.PREV_PAGE: index = pageIndex - 1 < 0 ? 0 : pageIndex - 1;
                                    break;
                                case Constant.NEXT_PAGE: index = pageIndex + 1 > _pageCount - 1 ? _pageCount - 1 : pageIndex + 1;
                                    break;
                                case Constant.LAST_PAGE: index = _pageCount - 1;
                                    break;
                            }
                        }

                        arg = Constant.PAGE_ARGUMENT + Constant.ARGUMENT_SPLITTER + index;
                        btn.Attributes.Add(HtmlTextWriterAttribute.Href.ToString(),
                                            dal.Page.ClientScript.GetPostBackClientHyperlink(dal, arg));
                    }

                }
            }
        }
    }

    /// <summary>
    /// 常量类
    /// </summary>
    public class Constant
    {
        public const string FIRST_PAGE = "First";
        public const string PREV_PAGE = "Prev";
        public const string NEXT_PAGE = "Next";
        public const string LAST_PAGE = "Last";
        public const string PAGE_ARGUMENT = "Page";
        public const char ARGUMENT_SPLITTER = '$';
    }
}
