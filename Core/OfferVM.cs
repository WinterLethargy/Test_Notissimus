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
		public ICommand BackCommand => _backCommand ?? (_backCommand = new MvxAsyncCommand(Back));

		public OfferVM(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public override void Prepare(string offerJson)
		{
			var offerJObjeck = JObject.Parse(offerJson);
			var id = offerJObjeck["offer"]["@id"].Value<string>();
			Title = $"Offer {id}";
			OfferJson = offerJson;
		}
		public Task Back()
		{
			return Task.Run(() => _navigationService.Close(this));
		}
	}
}