using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Test_Notissimus
{
	public class OfferListVM : MvxViewModel
	{
		private IOfferWebProvider _offerWebProvider;
		private IMvxNavigationService _navigationService;
		private XmlNode _offerList;

		public MvxObservableCollection<string> OfferIds { get; } = new MvxObservableCollection<string>();
		private ICommand _showOfferCommand;
		public ICommand ShowOfferCommand => _showOfferCommand ?? (_showOfferCommand = new MvxAsyncCommand<string>(ShowOffer));

		public OfferListVM(IOfferWebProvider offerWedProvider, IMvxNavigationService navigationService)
		{
			_offerWebProvider = offerWedProvider;
			_navigationService = navigationService;
		}

		public async override Task Initialize()
		{
			await base.Initialize();

			_offerList = await _offerWebProvider.GetOffersXml();

			var offers = _offerList.SelectNodes("//offer");
			var offerIds = offers.Cast<XmlNode>()
								 .Select(of => of.Attributes["id"].Value)
								 .ToArray();

			OfferIds.AddRange(offerIds);
		}
		public Task ShowOffer(string offerId)
		{
			return Task.Run(async () =>
			{
				var idNum = int.Parse(offerId);
				var offerJson = GetOfferJson(idNum);
				await _navigationService.Navigate<OfferVM, string>(offerJson);
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
