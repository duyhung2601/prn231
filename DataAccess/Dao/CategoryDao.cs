using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class CategoryDao
    {
        private static CategoryDao instance = null;
        public static readonly object instanceLock = new object();
        private static Prn231CoffeeProjectContext dbcontext = new Prn231CoffeeProjectContext();

        public static CategoryDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new CategoryDao();
                }
                dbcontext = new Prn231CoffeeProjectContext();
                return instance;
            }
        }
        public IEnumerable<Category> listCategories()
        {
            var listCate = new List<Category>();
            try
            {
                listCate = dbcontext.Categories.Include(c => c.Products).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCate;
        }
    }
}
