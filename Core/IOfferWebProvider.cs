using System.Threading.Tasks;
using System.Xml;

namespace Test_Notissimus
{
	public interface IOfferWebProvider
	{
		Task<XmlNode> GetOffersXml();
	}
}