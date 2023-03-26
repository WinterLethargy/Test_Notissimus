using Java.Lang;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test_Notissimus
{
	public class OfferVM : MvxViewModel<string>
	{
		IMvxNavigationService _navigationService;

		private string _title;
		public string Title 
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
				RaisePropertyChanged();
			} 
		}
		private string _offerJson;
		public string OfferJson
		{
			get
			{
				return _offerJson;
			}
			set
			{
				_offerJson = value;
				RaisePropertyChanged();
			}
		}

		private ICommand _backCommand;
		public ICommand BackCommand => _backCommand ?? (_backCommand = new MvxAsyncCommand(BackAsync));

		public OfferVM(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public override void Prepare(string offerJson)
		{
			OfferJson = offerJson;
		}
		public override async Task Initialize()
		{
			await base.Initialize();
			await InitTitleAsync();
		}
		private Task InitTitleAsync()
		{
			return Task.Run(InitTitle);
		}
		private void InitTitle() 
		{
			var offerJObject = JObject.Parse(OfferJson);
			var id = offerJObject["offer"]["@id"].Value<string>();
			Title = $"Offer {id}";
		}
		private Task BackAsync()
		{
			return Task.Run(() => _navigationService.Close(this));
		}
	}
}