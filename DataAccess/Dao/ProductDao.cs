using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class ProductDao
    {
        private static ProductDao instance = null;
        public static readonly object instanceLock = new object();
        private static Prn231CoffeeProjectContext dbcontext = new Prn231CoffeeProjectContext();

        public static ProductDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ProductDao();
                }
                dbcontext = new Prn231CoffeeProjectContext();
                return instance;
            }
        }
        public IEnumerable<Product> listProduct()
        {
            var listMem = new List<Product>();
            try
            {
                listMem = dbcontext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listMem;

        }
        public Product findById(int Id)
        {
            Product product = new Product();
            try
            {
                product = dbcontext.Products.SingleOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return product;
        }
        public IEnumerable<Product> findByCate(int cateId)
        {
            var product = new List<Product>();
            try
            {
                product = dbcontext.Products.Where(x => x.CategoryId == cateId).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return product;
        }
        public bool addProduct(Product product)
        {
            try
            {
                dbcontext.Products.Add(product);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public bool updateProduct(Product product)
        {
            try
            {
                Product pro = findById(product.Id);
                if (pro != null)
                {
                    dbcontext.Entry(pro).CurrentValues.SetValues(product);
                    dbcontext.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public bool deleteProduct(Product product)
        {
            try
            {
                Product pro = findById(product.Id);
                if (pro != null)
                {
                    dbcontext.Products.Remove(pro);
                    dbcontext.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public IEnumerable<Product> searchProduct(int? Id, string? name, decimal? price, double? weight)
        {
            var listSearch = new List<Product>();
            //int Id;
            //decimal price;
            //int stock;
            try
            {
                //if (idS == "" && name == "" && priceS == "" && stockS == "")
                //{
                //    listSearch = dbcontext.Products.ToList();
                //}
                //else if (idS == "" && name == "" && priceS == "" && stockS != "")
                //{
                //    stock = Convert.ToInt32(stockS);
                //    listSearch = dbcontext.Products.Where(x => x.UnitsInStock == stock).ToList();
                //}
                //else if (idS != "" && name == "" && priceS == "" && stockS == "")
                //{
                //    Id = Convert.ToInt32(idS);
                //    listSearch = dbcontext.Products.Where(x => x.ProductId == Id).ToList();
                //}
                //else if (idS == "" && name != "" && priceS == "" && stockS == "")
                //{
                //    listSearch = dbcontext.Products.Where(x => x.ProductName.Contains(name)).ToList();
                //}
                //else if (idS == "" && name == "" && priceS != "" && stockS == "")
                //{
                //    price = Convert.ToDecimal(priceS);
                //    listSearch = dbcontext.Products.Where(x => x.UnitPrice == price).ToList();
                //}
                //else
                //{
                //    Id = Convert.ToInt32(idS);
                //    stock = Convert.ToInt32(stockS);
                //    price = Convert.ToDecimal(priceS);
                //    listSearch = dbcontext.Products.Where(x => x.ProductId == Id && x.ProductName.Contains(name) && x.UnitPrice == price && x.UnitsInStock == stock).ToList();
                //}
                listSearch = dbcontext.Products.Where(x => x.Id == Id ||
                                                      x.Name.Contains(name) ||
                                                      x.Price == price ||
                                                      x.Weight == weight ||
                                                      x.Id == Id && x.Name.Contains(name) && x.Price == price && x.Weight == weight)
                                                      .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listSearch;
        }
    }
}
