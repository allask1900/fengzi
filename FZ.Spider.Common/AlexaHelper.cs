using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FZ.Spider.Common
{
    public class AlexaHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(AlexaHelper).FullName);
        const string alexaUrl = "http://data.alexa.com/data/?cli=10&dat=s&ver=7.0&url=";
        public static int GetAlexaRank(string domain, ref string desc)
        {
            desc = "";
            int rank = 0;
            try
            {
                string code = PageHelper.ReadUrl(alexaUrl + domain, Encoding.UTF8, "alexa.com");
                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.LoadXml(code);
                XmlNode xn = xml.SelectSingleNode("ALEXA/DMOZ/SITE");
                if (xn != null)
                {
                    desc = xn.Attributes["DESC"].Value;
                }
                XmlNode xn_2 = xml.SelectSingleNode("ALEXA/SD/POPULARITY");
                if (xn_2 != null)
                {
                    rank = Convert.ToInt32(xn_2.Attributes["TEXT"].Value);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "("+domain+")", ex);
            }
            return rank;
        }
    }
}
