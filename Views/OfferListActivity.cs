using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Binding.Views;

namespace Test_Notissimus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class OfferListActivity : MvxActivity<OfferListVM>
	{
		private MvxListView OffersListView => _offersListView ?? (_offersListView = FindViewById<MvxListView>(Resource.Id.Offers));
		private MvxListView _offersListView;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);
			
			var set = CreateBindingSet();
			set.Bind(OffersListView)
				.For(olv => olv.ItemsSource)
				.To(vm => vm.OfferIds);
			set.Bind(OffersListView)
				.For(olv => olv.ItemClick)
				.To(vm => vm.ShowOfferCommand);
			set.Apply();
		}
	}
}