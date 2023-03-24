using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Test_Notissimus
{
	public class OfferListVM
	{
		public  List<string> OfferIds { get; }		= new List<string>();
		private OfferWebProvider _offerWebProvider	= new OfferWebProvider();
		private XmlNode _offerList;

		public Task InitAsync()
		{
			return Task.Run(async () =>
			{
				_offerList = await _offerWebProvider.GetOffersXml();

				var offers   = _offerList.SelectNodes("//offer");
				var offerIds = offers.Cast<XmlNode>()
									 .Select(of => of.Attributes["id"].Value)
									 .ToArray();

				OfferIds.AddRange(offerIds);
			});
		}
		public string GetOfferJson(int id)
		{
			var offerXml = _offerList.SelectSingleNode($"//offer[@id=\"{id}\"]");
			
			if (offerXml == null)
				return string.Empty;
			
			var offerJson = JsonConvert.SerializeObject(offerXml, Newtonsoft.Json.Formatting.Indented);

			return offerJson;
		}
	}
}
