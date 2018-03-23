using Android.App;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Support.V7.App;

namespace TestBlack.Droid
{
	[Activity(Label = "PSEA", 
	          MainLauncher= true, NoHistory = true, 
	          ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Splash")]
	public class SplashScreen : AppCompatActivity
	{
		public static bool Initialized;

		public SplashScreen()
		{
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			if (Initialized == false) {
				// Create your application here
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				ThreadPool.QueueUserWorkItem (x => SplashTask ());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			} else {
				LoadMainActivity ();
			}
		}

		private async Task SplashTask ()
		{
			await Task.Delay (100);
			LoadMainActivity ();
			Initialized = true;
		}

		private void LoadMainActivity()
		{
			var activity = new Intent (this, typeof(MainActivity));
			if (this.Intent != null && this.Intent.Data != null) {
				activity.SetData (this.Intent.Data);
			}
			StartActivity (activity);
		}
	}
}

