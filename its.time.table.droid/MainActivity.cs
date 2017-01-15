using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using System.Collections.Generic;

namespace its.time.table.droid
{
    [Activity(Label = "its.time.table.droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Core.Service service;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            service = new Core.Service();
            var timeTable = service.TimeTable;

            var list = new List<KeyValuePair<int, Core.Hour>>();

            foreach (var item in timeTable.hours)
            {
                foreach (var hour in item.Value)
                {
                    list.Add(new KeyValuePair<int, Core.Hour>(item.Key, hour));
                }
            }


            string[] array = new string[list.Count];

            //list.Sort((a, b) => a.Value.startTime.CompareTo(b.Value.startTime));
            list = list.OrderBy(v => v.Value.startTime).ThenBy(v => v.Key).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var key = list[i].Key;
                var hour = list[i].Value;

                array[i] = $"{key}: {hour.Subject}, {hour.Teacher} Room: {hour.Room} {hour.startTime.ToString("HH:mm")} - {hour.endTime.ToString("HH:mm dd.MM.yy")}";
            }

            var listView = FindViewById<ListView>(Resource.Id.main_listView);
            listView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.Main, Resource.Id.main_TextView, array);
        }

        
    }
}

