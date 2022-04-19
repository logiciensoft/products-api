using System.Collections.Generic;
using Products.Models;
using Products.Providers;
using Xunit;

namespace Products.Tests
{
    public class UnitTest1
    {
        private readonly List<ProductDto> _products;

        [Fact]
        public void Test_Products_Filter_With_Query_Parameter()
        {
            var appHelper = new AppHelper();

            var maxPrice = 11;
            var size = "large";

            var response = appHelper.FilterProducts(maxPrice, size, _products);

            Assert.Single(response);
        }

        [Fact]
        public void Test_Most_Common_Words_In_Products_Description()
        {
            var appHelper = new AppHelper();

            var text = "What you see is not what is real";
            var skip = 1;
            var take = 1;

            var response = appHelper.GetMostCommonWords(text, skip, take);

            Assert.Single(response);
        }

        [Fact]
        public void Test_Products_Filter_Object_In_Response()
        {
            var appHelper = new AppHelper();

            var response = appHelper.GetFilter(_products);

            Assert.Equal(12, response.MaxPrice);
            Assert.Equal(10, response.MinPrice);
            Assert.Equal(3, response.Sizes.Length);
        }


        public UnitTest1()
        {
            _products = new List<ProductDto>()
            {
                new(){
                    Title = "A Red Trouser",
                    Price= 10,
                    Sizes = new List<string>()
                    {
                        "small", "medium", "large"
                    },
                    Description = "This trouser perfectly pairs with a green shirt."
                },
                new(){
                    Title= "A Green Trouser",
                    Price= 11,
                    Sizes= new List<string>{
                    "small"
                    },
                    Description= "This trouser perfectly pairs with a blue shirt."
                },
                new(){
                    Title= "A Blue Trouser",
                    Price= 12,
                    Sizes= new List<string>{
                    "medium"
                    },
                    Description= "This trouser perfectly pairs with a red shirt."
                },
                
            };
        }
    }
}