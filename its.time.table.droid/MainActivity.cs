using Android.App;
using Android.Widget;
using Android.OS;

namespace its.time.table.droid
{
    [Activity(Label = "its.time.table.droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            string[] array = new string[1];
            array[0] = "Test";

            var listView = FindViewById<ListView>(Resource.Id.main_listView);
            listView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.Main, Resource.Id.main_TextView, array);
        }
    }
}

