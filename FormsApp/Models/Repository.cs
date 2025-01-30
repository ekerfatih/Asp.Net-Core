using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models {
    public static class Repository {
        private static readonly List<Product> _products = new List<Product>();
        private static readonly List<Category> _categories = new List<Category>();
        public static List<Product> Products { get { return _products; } }
        public static List<Category> Categories { get { return _categories; } }

        static Repository() {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductId = 1, CategoryId = 1, Name = "IPhone 14", Price = 40000, IsActive = true, Image = "1.jpg" });
            _products.Add(new Product { ProductId = 2, CategoryId = 1, Name = "IPhone 15", Price = 50000, IsActive = true, Image = "2.jpg" });
            _products.Add(new Product { ProductId = 3, CategoryId = 1, Name = "IPhone 16", Price = 60000, IsActive = true, Image = "3.jpg" });
            _products.Add(new Product { ProductId = 4, CategoryId = 1, Name = "IPhone 17", Price = 70000, IsActive = true, Image = "4.jpg" });
            _products.Add(new Product { ProductId = 5, CategoryId = 2, Name = "Macbook Air", Price = 80000, IsActive = true, Image = "5.jpg" });
            _products.Add(new Product { ProductId = 6, CategoryId = 2, Name = "Macbook Pro", Price = 90000, IsActive = true, Image = "6.jpg" });
        }

        public static void CreateProduct(Product product) {
            _products.Add(product);
        }

        public static void EditProduct(Product updatedProduct) {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null) {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;
            }

        }
        public static void DeleteProduct(Product entity) {
            if (entity != null) {
                _products.Remove(entity);
            }
        }
    }
}
