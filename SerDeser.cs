using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducPractVer2._00
{
    internal class SerDeser
    {
        public static T DeserialiseObject<T>()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                try
                {
                    T obj = JsonConvert.DeserializeObject<T>(json);
                    return obj;

                }
                catch (Exception)
                {
                    return default(T);
                    throw;
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
