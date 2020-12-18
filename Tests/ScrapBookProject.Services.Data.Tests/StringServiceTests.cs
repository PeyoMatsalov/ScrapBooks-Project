namespace ScrapBookProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class StringServiceTests
    {
        [Fact]
        public void ConvertCollectionOfIntToStringForChartJSShouldWorkCorrectly()
        {
            var service = new StringService();

            var intList = new List<int>() { 1, 32, 2, 5 };
            string expectedResult = "[1,32,2,5]";
            var actualResult = service.ConvertCollectionOfIntToStringForChartJS(intList);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ConvertCollectionOfStringToStringForChartJSShouldWorkCorrectly()
        {
            var service = new StringService();

            var stringList = new List<string>() { "test", "this", "test", "this" };
            string expectedResult = "[\"test\",\"this\",\"test\",\"this\"]";
            var actualResult = service.ConvertCollectionOfStringToStringForChartJS(stringList);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
