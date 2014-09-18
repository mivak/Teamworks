namespace MongoDb.Data
{
    using ElectronicStoresSystem.Models;
    using MongoDb.Data.Entities;
    using System;

    public static class MongoParser
    {
        public static MongoCategory ParseToMongoCategory(Category category)
        {
            var result = new MongoCategory();

            result.CategoryId = category.CategoryId;
            result.CategoryName = category.CategoryName;

            return result;
        }

        public static MongoManufacturer ParseToMongoManufacturer(Manufacturer manufacturer)
        {
            var result = new MongoManufacturer();

            result.ManufacturerId = manufacturer.ManufacturerId;
            result.ManufacturerName = manufacturer.ManufacturerName;

            return result;
        }

        public static MongoProduct ParseToMongoProduct(Product product)
        {
            var result = new MongoProduct();

            result.ProductId = product.ProductId;
            result.ProductName = product.ProductName;
            result.ManufacturerId = product.ManufacturerId;
            result.BasePrice = product.BasePrice;
            result.CategoryId = product.CategoryId;

            return result;
        }

        public static Category ParseCategory(MongoCategory category)
        {
            var result = new Category();

            result.CategoryName = category.CategoryName;

            return result;
        }

        public static Manufacturer ParseManufacturer(MongoManufacturer manufacturer)
        {
            var result = new Manufacturer();

            result.ManufacturerName = manufacturer.ManufacturerName;

            return result;
        }


        public static Product ParseProduct(MongoProduct product)
        {
            var result = new Product();

            result.ProductName = product.ProductName;
            result.ManufacturerId = product.ManufacturerId;
            result.BasePrice = product.BasePrice;
            result.CategoryId = product.CategoryId;

            return result;
        }
    }
}
