using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }


        public IQueryable<ProductCategory> Colection()
        {
            return productCategories.AsQueryable();
        }


        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }


        public void Insert(ProductCategory product)
        {
            productCategories.Add(product);
        }


        public void Update(ProductCategory product)
        {
            ProductCategory productToUpdate = productCategories.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }


        public ProductCategory Find(string Id)
        {
            ProductCategory productToFind = productCategories.Find(p => p.Id == Id);

            if (productToFind != null)
            {
                return productToFind;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }


        public void Delete(string Id)
        {
            ProductCategory productToDelete = productCategories.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                productCategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }
    }
}
