using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    // NOTE: this class is used to load settings from appsettings.JSON 
    //  - It is populated in the startup file when we are configuring services
    //  - once we have this we can use dependency injection to get these options in other classes
    public class EmailOptions
    {
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
    }
}
