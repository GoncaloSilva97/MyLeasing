
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;
        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Owners.Any())
            {
                AddProduct("João", "Então", "Rua Xica Nº37, 8C");
                AddProduct("São", "Santos","Rua da Tia Nº12, 4A");
                AddProduct("Andre", "Ré", "Rua Nunca Nº3, 6H");
                AddProduct("Tomas", "Razz", "Rua Da America Nº9, 7C");
                AddProduct("Sofia", "Cotovia", "Rua Tranca Nº19, 6P");

                AddProduct("João", "Tomé", "Rua Rica Treta Nº1");
                AddProduct("Otávio", "Reis", "Rua Xafarica Nº3");
                AddProduct("Diana", "Gaio", "Rua Do Calhambec Nº54, 2B");
                AddProduct("Maria", "Suzana", "Rua Do Autocarro Nº1289");
                AddProduct("Xana", "Tiago", "Rua Sem Nome Nº2");
               
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string Fname, string Lname, string Add)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(100000),
                FirstName = Fname,
                LastName = Lname,
                FixPhone = _random.Next(100000000),
                Address = Add,
                CellPhone = _random.Next(100000000)
            });
        }
    }
}
