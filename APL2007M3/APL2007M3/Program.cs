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

        // public struct SalesData. Include the following fields: date sold, department name, product ID, quantity sold, unit price
        public struct SalesData
        {
            public DateTime dateSold;
            public string departmentName;
            public string productID;
            public int quantitySold;
            public double unitPrice;
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[1000];
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                salesData[i].dateSold = new DateTime(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = "Department " + random.Next(1, 11);
                salesData[i].productID = "Product " + random.Next(1, 101);
                salesData[i].quantitySold = random.Next(1, 101);
                salesData[i].unitPrice = random.Next(1, 101);
            }
            return salesData;
        }

        public void QuarterlySalesReport(SalesData[] salesData)
        {

            // create a dictionary to store the quarterly sales data
            Dictionary<string, double> quarterlySales = new Dictionary<string, double>();

            // iterate through the sales data
            foreach (SalesData sale in salesData)
            {
                // calculate the total sales for each quarter
                string quarter = GetQuarter(sale.dateSold);
                double totalSales = sale.quantitySold * sale.unitPrice;
                if (quarterlySales.ContainsKey(quarter))
                {
                    quarterlySales[quarter] += totalSales;
                }
                else
                {
                    quarterlySales.Add(quarter, totalSales);
                }
            }

            // print the quarterly sales report
            Console.WriteLine("Quarterly Sales Report");
            Console.WriteLine("----------------------");
            foreach (KeyValuePair<string, double> quarter in quarterlySales)
            {
                Console.WriteLine("{0}: ${1}", quarter.Key, quarter.Value);
            }
        }

        public string GetQuarter(DateTime date)
        {
            int month = date.Month;
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