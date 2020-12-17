namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;

    public class StringService : IStringService
    {

        public StringService()
        {
        }

        public string ConvertCollectionOfIntToStringForChartJS(ICollection<int> intList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{string.Join(",", intList)}]");
            return sb.ToString();
        }

        public string ConvertCollectionOfStringToStringForChartJS(ICollection<string> strList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[\"{string.Join("\",\"", strList)}\"]");
            return sb.ToString();
        }
    }
}
