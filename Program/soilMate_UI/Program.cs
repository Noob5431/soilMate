using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace soilMate_UI
{

    
    public class MapMarker
    {
        public string longitude;
        public string latitude;
        public string id;
    }
    internal static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            MapMarker mapMarker = new MapMarker();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            Application.Run(form1);
            mapMarker.longitude = form1.longitude.ToString();
            mapMarker.latitude = form1.latitude.ToString();
            mapMarker.id = form1.id.ToString();
            
            
        }
    }

    
    


}
