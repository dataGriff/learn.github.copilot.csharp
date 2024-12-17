using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
    class QuarterlyIncomeReport
    {
        static void Main(string[] args)
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();

            // call the GenerateSalesData method
            SalesData[] salesData = report.GenerateSalesData();
            
            // call the QuarterlySalesReport method
            report.QuarterlySalesReport(salesData);
        }

        /* public struct SalesData includes the following fields: date sold, department name, product ID, quantity sold, unit price */
        public struct SalesData
        {
            public DateOnly dateSold;
            public string departmentName;
            public string productID;
            public int quantitySold;
            public double unitPrice;
            public double baseCost;
            public int volumeDiscount;
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[1000];
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                salesData[i].dateSold = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = ProdDepartments.departmentNames[random.Next(ProdDepartments.departmentNames.Length)];

            int indexOfDept = Array.IndexOf(ProdDepartments.departmentNames, salesData[i].departmentName);
            string deptAbb = ProdDepartments.departmentAbbreviations[indexOfDept];
            string firstDigit = (indexOfDept + 1).ToString();
            string nextTwoDigits = random.Next(1, 100).ToString("D2");
            string sizeCode = new string[] { "XS", "S", "M", "L", "XL" }[random.Next(0, 5)];
            string colorCode = new string[] { "BK", "BL", "GR", "RD", "YL", "OR", "WT", "GY" }[random.Next(0, 8)];
            string manufacturingSite = ManufacturingSites.manufacturingSites[random.Next(0, ManufacturingSites.manufacturingSites.Length)];

                salesData[i].productID = $"{deptAbb}-{firstDigit}-{nextTwoDigits}-{sizeCode}-{colorCode}-{manufacturingSite}";
                salesData[i].quantitySold = random.Next(1, 101);
                salesData[i].unitPrice = random.Next(25, 300) + random.NextDouble();
                salesData[i].baseCost = salesData[i].unitPrice * (1 - (random.Next(5, 21) / 100.0));
                salesData[i].volumeDiscount = (int)(salesData[i].quantitySold * 0.1);
            }

            return salesData;
        }

        /// <summary>
        /// The <c>ProdDepartments</c> struct contains arrays of department names and their corresponding abbreviations.
        /// </summary>
        /// <remarks>
        /// This struct provides static arrays for department names and their abbreviations, which can be used for categorizing products.
        /// </remarks>
        /// <example>
        /// <code>
        /// string menswear = ProdDepartments.departmentNames[0]; // "Menswear"
        /// string menswearAbbreviation = ProdDepartments.departmentAbbreviations[0]; // "MENS"
        /// </code>
        /// </example>
        public struct ProdDepartments
        {
            public static string[] departmentNames = new string[]
            {
                "Menswear",
                "Womenswear",
                "Childrenswear",
                "Footwear",
                "Accessories",
                "Sportswear",
                "Underwear",
                "Outerwear"
            };

            public static string[] departmentAbbreviations = new string[]
            {
                "MENS",
                "WMNS",
                "CHLD",
                "FTWR",
                "ACCS",
                "SPRT",
                "UNDR",
                "OUTR"
            };
        }

        public struct ManufacturingSites
        {
            public static string[] manufacturingSites = new string[]
            {
                "US1", "US2", "US3",
                "CA1", "CA2", "CA3",
                "MX1", "MX2", "MX3", "MX4"
            };
        }

        public void QuarterlySalesReport(SalesData[] salesData)
        {
            // create dictionaries to store the quarterly sales data and profit data
            Dictionary<string, double> quarterlySales = new Dictionary<string, double>
            {
            { "Q1", 0 },
            { "Q2", 0 },
            { "Q3", 0 },
            { "Q4", 0 }
            };

            Dictionary<string, double> quarterlyProfit = new Dictionary<string, double>
            {
            { "Q1", 0 },
            { "Q2", 0 },
            { "Q3", 0 },
            { "Q4", 0 }
            };

            // create nested dictionaries to store sales and profit data by department
            Dictionary<string, Dictionary<string, double>> departmentSales = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, double>> departmentProfit = new Dictionary<string, Dictionary<string, double>>();

            foreach (string department in ProdDepartments.departmentNames)
            {
            departmentSales[department] = new Dictionary<string, double>
            {
                { "Q1", 0 },
                { "Q2", 0 },
                { "Q3", 0 },
                { "Q4", 0 }
            };

            departmentProfit[department] = new Dictionary<string, double>
            {
                { "Q1", 0 },
                { "Q2", 0 },
                { "Q3", 0 },
                { "Q4", 0 }
            };
            }

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
            // calculate the total sales and profit for each quarter
            string quarter = GetQuarter(data.dateSold.Month);
            double totalSales = data.quantitySold * data.unitPrice;
            double totalCost = data.quantitySold * data.baseCost;
            double profit = totalSales - totalCost;

            quarterlySales[quarter] += totalSales;
            quarterlyProfit[quarter] += profit;

            departmentSales[data.departmentName][quarter] += totalSales;
            departmentProfit[data.departmentName][quarter] += profit;
            }

            // display the quarterly sales report
            Console.WriteLine("Quarterly Sales Report");
            Console.WriteLine("----------------------");
            foreach (var quarter in quarterlySales.OrderBy(q => q.Key))
            {
            double sales = quarter.Value;
            double profit = quarterlyProfit[quarter.Key];
            double profitPercentage = (profit / sales) * 100;

            Console.WriteLine("{0}: Sales: {1:C}, Profit: {2:C}, Profit Percentage: {3:F2}%", quarter.Key, sales, profit, profitPercentage);
            }

            // display the quarterly sales report by department
            Console.WriteLine("\nQuarterly Sales Report by Department");
            Console.WriteLine("------------------------------------");
            foreach (var department in departmentSales)
            {
            Console.WriteLine("Department: {0}", department.Key);
            foreach (var quarter in department.Value.OrderBy(q => q.Key))
            {
                double sales = quarter.Value;
                double profit = departmentProfit[department.Key][quarter.Key];
                double profitPercentage = (profit / sales) * 100;

                Console.WriteLine("  {0}: Sales: {1:C}, Profit: {2:C}, Profit Percentage: {3:F2}%", quarter.Key, sales, profit, profitPercentage);
            }
            }
        }

        public string GetQuarter(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return "Q1";
            }
            else if (month >= 4 && month <= 6)
            {
                return "Q2";
            }
            else if (month >= 7 && month <= 9)
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }
    }
}
