using Parquet;
using Parquet.Data;
using Parquet.Data.Rows;
using System;
using System.Linq;

namespace ParquetSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // parquetファイル読込
            Table table = ParquetReader.ReadTableFromFile("./data/sample.parquet");

            //スキーマ情報の表示
            ShowSchema(table.Schema);

            Console.WriteLine();

            //行データを表示
            ShowRows(table);
        }

        static void ShowSchema(Schema schema)
        {
            Console.WriteLine("---------- Schema ----------");

            foreach (var field in schema.GetDataFields())
            {
                Console.WriteLine($"Name={field.Name}, SchemaType={field.DataType}");
            }
        }

        static void ShowRows(Table table)
        {
            Console.WriteLine("---------- Rows ----------");

            foreach (var row in table)
            {
                string values = String.Join(",", row.Values.Select(v =>
                 {
                     if(v is byte[])
                     {
                         return BitConverter.ToString((byte[])v);
                     }
                     return v.ToString();
                 }).ToArray());

                Console.WriteLine(values);
            }
        }
    }
}
