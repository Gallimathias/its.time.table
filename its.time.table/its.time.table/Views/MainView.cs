using its.time.table.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace its.time.table.Views
{
    public class MainView : MasterDetailPage
    {
        public MainView()
        {
            setup();
        }

        private void setup()
        {
            Detail = new ContentPage
            {
                Content = new StackLayout
                {
                    Children = { }
                }
            };

            Master = new ContentPage { Title = "AppName", Content = new StackLayout { Children = { } } };

            var service = new GetTimeTable_Service();

        }
    }
}
