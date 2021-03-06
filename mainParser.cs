using System;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace csv2prj
{
    public static class MainParser
    {
        public static void ParseCSV(string csvFile)
        {
            List<DataObject> readData = new List<DataObject>();

            using (var reader = new StreamReader(csvFile))
            {
                CsvHelper.Configuration.CsvConfiguration cnfg = 
                    new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
                cnfg.Encoding = System.Text.Encoding.UTF8;
                cnfg.Delimiter = ",";
                cnfg.HasHeaderRecord = true;
                cnfg.NewLine = System.Environment.NewLine;
                cnfg.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;

                using (var csv = new CsvReader(reader, cnfg))
                {
                    //csv.Context.RegisterClassMap<CsvMap>();                    
                    readData = csv.GetRecords<DataObject>().ToList();                
                }
            }

            Console.WriteLine($"readData.Count={readData.Count}");
            for (int i = 0; i < readData.Count; i++)
            {
                Console.WriteLine($"[{i}]: Name={readData[i].Name}; Code={readData[i].Code}; Quantity={readData[i].Quantity};");
            }
        }
    }

    //public class CsvMap : CsvHelper.Configuration.ClassMap<DataObject>
    //{
    //    public CsvMap()
    //    {
    //        Map(m => m.Name).Name("Наименование");
    //        Map(m => m.Code).Name("Обозначение");
    //        Map(m => m.Quantity).Name("Количество");
    //        Map(m => m.Duration).Name("Длительность");
    //    }
    //}
}
