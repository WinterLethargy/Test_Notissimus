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
		public ICommand ShowOfferCommand => _showOfferCommand ?? (_showOfferCommand = new MvxAsyncCommand<string>(ShowOfferAsync));

		public OfferListVM(IOfferWebProvider offerWedProvider, IMvxNavigationService navigationService)
		{
			_offerWebProvider = offerWedProvider;
			_navigationService = navigationService;
		}
		public async override Task Initialize()
		{
			await base.Initialize();
			await UpdateOfferIdsAsync();
		}
		private Task UpdateOfferIdsAsync()
		{
			return Task.Run(UpdateOfferIds);
		}
		private async Task UpdateOfferIds()
		{
			_offerList = await _offerWebProvider.GetOffersXml();

			var offers = _offerList.SelectNodes("//offer");
			var offerIds = offers.Cast<XmlNode>()
								 .Select(of => of.Attributes["id"].Value)
								 .ToArray();

			OfferIds.Clear();
			OfferIds.AddRange(offerIds);
		}
		private Task ShowOfferAsync(string offerId)
		{
			return Task.Run(() => ShowOffer(offerId));
		}
		private void ShowOffer(string offerId)
		{
			var idNum = int.Parse(offerId);
			var offerJson = GetOfferJson(idNum);
			_navigationService.Navigate<OfferVM, string>(offerJson);
		}
		private string GetOfferJson(int id)
		{
			var offerXml = _offerList.SelectSingleNode($"//offer[@id=\"{id}\"]");

			if (offerXml == null)
				return string.Empty;

			var offerJson = JsonConvert.SerializeObject(offerXml, Newtonsoft.Json.Formatting.Indented);

			return offerJson;
		}
	}
}
