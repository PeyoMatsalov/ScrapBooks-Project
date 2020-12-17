namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;

    public interface IStringService
    {
        string ConvertCollectionOfIntToStringForChartJS(ICollection<int> intList);

        string ConvertCollectionOfStringToStringForChartJS(ICollection<string> strList);
    }
}
