using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;

namespace soilMate_UI
{
    class WeatherData
    {
        public float tmin, tmax;
    }
    class Plant
    {
        public string name ;
        public struct Nutrients
        {

            public int Ni, P, K;
            public float soilPH;

        }
        public Nutrients nutrients = new Nutrients();
        int water;
        int water_sec;
        int temp_pos_max = 0;
        public int temp_base = 0;

        public int temp_opt_max = 0;
        public int temp_opt_min = 0;


        int humidity;
        int days_matu;
        int yield;
        int GDD_bestscenerio;

        int getYield()
        {


            return yield;

        }

        int temp_pos_avg;
        public int temp_opt_avg;

        int getAverage(int max, int min)
        {

            return (max + min) / 2;
        }

        public Plant(int Ni, int P, int K, int hum, int temp_min, int temp_max, int temp_bs, int ph,string input_name)
        {
            name = input_name;
            nutrients.Ni = Ni;
            nutrients.P = P;
            nutrients.K = K;
            humidity = hum;
            temp_opt_avg = getAverage(temp_max, temp_min);
            temp_opt_min = temp_min;
            temp_opt_max = temp_max;
            nutrients.soilPH = ph;
            temp_base = temp_bs;
            GDD_bestscenerio = temp_opt_avg - temp_base;
            
        }


    }

    class plots
    {
        const float p_falloff = 0.3f, k_falloff = 0.3f, n_falloff = 0.4f;
        class plotType
        {
            public long longitude, latitude;
            public int P, N;

            public plotType(long input_longitude, long input_latitude)
            {
                longitude = input_longitude;
                latitude = input_latitude;

                var d = new Dictionary<string, object>
            {
                {"longitude", longitude},
                {"latitude", latitude}
            };
            }
        }
        List<plotType> plotData = new List<plotType>();

        public long findPlot(long longitude, long latitude)
        {
            plotData.Add(new plotType(4884436418200305, 23188847769990053));
            plotData.Add(new plotType(468244774552231, 2182672493772014));

            long diff_total_global = long.MaxValue, current_index = -1;

            for (int i = 0; i < plotData.Count; i++)
            {
                long diff_long, diff_lat, diff_total;

                diff_long = longitude - plotData[i].longitude;
                diff_lat = latitude - plotData[i].latitude;

                diff_total = (long)Math.Sqrt(diff_lat * diff_lat + diff_long * diff_long); //distance

                if (diff_total < diff_total_global)
                {
                    diff_total_global = diff_total;
                    current_index = i;
                }
            }
            return current_index;
        }

        public WeatherData getWeatherData(long longitude, long latitude)
        {
            WeatherData weatherData = new WeatherData();
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\\Users\\Noob5431\\AppData\\Local\\Programs\\Python\\Python39\\python.exe";
            var script = @"C:\\Users\\Noob5431\\Desktop\\soilMate\\soilMate_UI\\weather.py";

            psi.Arguments = $"\"{script}\" \"{ longitude}\" \"{latitude}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var results = "";

            using (var process = Process.Start(psi))
            {
                results = process.StandardOutput.ReadToEnd();
            }

            string string_tmin = "";
            string string_tmax = "";
            int i = 0;
            for (; results[i] != ' '; i++)
            {
                string_tmin += results[i];
            }
            i += 3;
            for (; results[i] != ' '; i++)
            {
                string_tmax += results[i];
            }
            float tmin = float.Parse(string_tmin);
            float tmax = float.Parse(string_tmax);

            weatherData.tmin = tmin;
            weatherData.tmax = tmax;

            return weatherData;
        }

        public float checkCompatibility(float ph, float t_min, float t_max, Plant plant)
        {
            /*float p_proc = plotData[plot_index].P / plant.nutrients.P;
            float n_proc = plotData[plot_index].N / plant.nutrients.Ni;

            if (p_proc < p_falloff || n_proc < n_falloff)
                return 0;

            float final_proc = p_proc * n_proc;
            return (int)(final_proc * 100);*/
            float t_average = (t_max + t_min) / 2;
            float efficiency = (ph / plant.nutrients.soilPH)*(plant.temp_opt_avg/t_average);

            
            if (plant.temp_base > t_min)
                return 0;
            return efficiency;
        }
    }

    class CalculateResult
    {
        public Plant plant1, plant2, plant3;
    }

    class Program1
    {
        public CalculateResult Calculate(long longitude, long latitude, int id)
        {
            CalculateResult result = new CalculateResult();

            Plant wheat = new Plant(26, 14, 16, 50, 12, 25, 4, 64,"Wheat");
            Plant barley = new Plant(23, 11, 22, 15, 12, 25, 4, 67,"Barley");
            Plant corn = new Plant(27, 13, 17, 90, 21, 30, 10, 63,"Corn");
            Plant sugar_beat = new Plant(5, 2, 6, 70, 24, 28, 1, 7,"Sugarbeet");
            Plant Potato = new Plant(5, 3, 7, 55, 15, 20, 7, 62,"Potato");
            Plant Sunflower = new Plant(36, 17, 50, 7, 25, 28, 7, 6,"Sunflower");
            Plant Canola = new Plant(51, 36, 44, 60, 12, 30, 5, 6,"Canola");
            Plant Flax_oil = new Plant(59, 17, 72, 100, 0, 8, -3, 5,"FlaxOil"); 
            Plant Beans = new Plant(59, 13, 25, 40, 18, 29, 12, 6,"Beans");
            Plant GreenPeas = new Plant(61, 17, 28, 75, 13, 18, 4, 7,"Greenpeas");
            Plant SoyBean = new Plant(70, 22, 34, 75, 21, 29, 10, 6,"Soybean");
            Plant Flax_fiber = new Plant(11, 7, 13, 100, 0, 8, -3, 5,"FlaxFiber");
            Plant Lucerne = new Plant(8, 2, 6, 40, 18, 28, 1, 5,"Lucerne");
            Plant Clover = new Plant(6, 1, 5, 90, 5, 28, -1, 6,"Clover");
            Plant[] plants = new Plant[14];
            plants[0] = wheat;
            plants[1] = barley;
            plants[2] = corn;
            plants[3] = sugar_beat;
            plants[4] = Potato;
            plants[5] = Flax_oil;
            plants[6] = Beans;
            plants[7] = GreenPeas;
            plants[8] = SoyBean;
            plants[9] = Flax_fiber;
            plants[10] = Lucerne;
            plants[11] = Clover;
            plants[12] = Sunflower;
            plants[13] = Canola;

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Noob5431\Desktop\soilMate\soilMate_UI\New_Text_Document.txt");

            plots a = new plots();
            WeatherData current_wd = a.getWeatherData(longitude, latitude);

            float current_ph = float.Parse(lines[id]);

            float max_efficiency = 0;
            float efficiency;
            Plant current_max_plant = new Plant(0, 0, 0, 0, 0, 0, 0, 0,"");
            int max_index = 0;
            for (int i = 0; i < 14; i++)
            {
                efficiency = a.checkCompatibility(current_ph, current_wd.tmin, current_wd.tmax, plants[i]);
                if (efficiency > max_efficiency)
                {
                    max_efficiency = efficiency;
                    current_max_plant = plants[i];
                    max_index = i;
                }
            }
            result.plant1 = current_max_plant;
            plants[max_index] = plants[13];

            max_efficiency = 0;
            for (int i = 0; i < 13; i++)
            {
                efficiency = a.checkCompatibility(current_ph, current_wd.tmin, current_wd.tmax, plants[i]);
                if (efficiency > max_efficiency)
                {
                    max_efficiency = efficiency;
                    current_max_plant = plants[i];
                    max_index = i;
                }
            }
            result.plant2 = current_max_plant;
            plants[max_index] = plants[12];

            max_efficiency = 0;
            for (int i = 0; i < 12; i++)
            {
                efficiency = a.checkCompatibility(current_ph, current_wd.tmin, current_wd.tmax, plants[i]);
                if (efficiency > max_efficiency)
                {
                    max_efficiency = efficiency;
                    current_max_plant = plants[i];
                    max_index = i;
                }
            }
            result.plant3 = current_max_plant;

            return result;
        }
    }

}
