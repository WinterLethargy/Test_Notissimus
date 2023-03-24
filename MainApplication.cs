using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using System;

namespace Test_Notissimus
{
	[Application]
	public class MainApplication : MvxAndroidApplication<Setup, App>
	{
		public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}
	}
}