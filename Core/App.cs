using MvvmCross.ViewModels;
using MvvmCross;

namespace Test_Notissimus
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			Mvx.IoCProvider.RegisterType<IOfferWebProvider, OfferWebProvider>();

			RegisterAppStart<OfferListVM>();
		}
	}
}