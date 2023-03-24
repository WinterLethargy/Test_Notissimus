using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

namespace Test_Notissimus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : Activity
    {
		public OfferListVM OfferListVM { get; } = new OfferListVM();
		private ListView OffersListView => _offersListView ?? (_offersListView = FindViewById<ListView>(Resource.Id.Offers));
		private ListView _offersListView;

		protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			await OfferListVM.InitAsync();
			
			OffersListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, OfferListVM.OfferIds);
			OffersListView.ItemClick += OffersListView_ItemClick;
		}

		private void OffersListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var id = ((TextView)e.View).Text;
			var offerJson = OfferListVM.GetOfferJson(int.Parse(id));

			var intent = new Intent(this, typeof(OfferActivity));

			intent.PutExtra("id", id);
			intent.PutExtra("offer_json", offerJson);

			StartActivity(intent);
		}
	}
}