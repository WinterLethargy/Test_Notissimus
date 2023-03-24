using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Notissimus
{
	[Activity(Label = "@string/offer")]
	internal class OfferActivity : Activity
	{
		private TextView OfferTitle => _offerTitle ?? (_offerTitle = FindViewById<TextView>(Resource.Id.OfferTitle));
		private TextView _offerTitle;
		private TextView OfferJson => _offerJson ?? (_offerTitle = FindViewById<TextView>(Resource.Id.OfferJson));
		private TextView _offerJson;
		private Button BackButton => _backButton ?? (_backButton = FindViewById<Button>(Resource.Id.button_offer_back));
		private Button _backButton;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.activity_offer);

			OfferTitle.Text = Intent.GetStringExtra("id");
			OfferJson.Text = Intent.GetStringExtra("offer_json");

			BackButton.Click += BackButton_Click;
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			Finish();
		}
	}
}