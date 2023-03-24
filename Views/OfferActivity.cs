using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;

namespace Test_Notissimus
{
	[Activity(Label = "@string/offer")]
	internal class OfferActivity : MvxActivity<OfferVM>
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.activity_offer);
		}
	}
}