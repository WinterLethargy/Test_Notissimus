using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Test_Notissimus
{
	internal class OfferWebProvider : IOfferWebProvider
	{
		public string RemoteStorageUrl { get; } = "http://partner.market.yandex.ru/pages/help/YML.xml";
		public int RemoteStorageEncoding { get; } = 1251;
		public async Task<XmlNode> GetOffersXml()
		{
			var enc = Encoding.GetEncoding(RemoteStorageEncoding);
			var client = new WebClient() { Encoding = enc };
			var xdoc = new XmlDocument();

			string resp;
			try
			{
				resp = await client.DownloadStringTaskAsync(RemoteStorageUrl);
			}
			catch (Exception ex)
			{
				return xdoc;
			}

			xdoc.LoadXml(resp);

			var offerListXml = xdoc.SelectSingleNode("/yml_catalog/shop/offers");

			return offerListXml;
		}
	}
}