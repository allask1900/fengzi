using FZ.Spider.DAL.Entity.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace FZ.Spider.DAL.Collection
{
	public class CSite:List<ESite>
	{
        private int m_CategoryID = 0;
        public int CategoryID
        {
            set { m_CategoryID = value; }
            get { return m_CategoryID; }
        }
	}
}
