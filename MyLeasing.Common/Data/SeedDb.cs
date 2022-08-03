
using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Helperes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            var user = await _userHelper.GetUserByEmailAsync("dalton.fury120@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Document = _random.Next(100000000).ToString(),
                    FirstName = "Gonçalo",
                    LastName = "Silva",
                    Address = "Cinel",
                    Email = "dalton.fury120@gmail.com",
                    UserName = "dalton.fury120@gmail.com",
                    PhoneNumber = "123456789"


                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!_context.Owners.Any())
            {
                AddOwner("Joao", "Entao", "Rua Xica Nº37, 8C");
                AddOwner("Sao", "Santos","Rua da Tia Nº12, 4A");
                AddOwner("Andre", "Re", "Rua Nunca Nº3, 6H");
                AddOwner("Tomas", "Razz", "Rua Da America Nº9, 7C");
                AddOwner("Sofia", "Cotovia", "Rua Tranca Nº19, 6P");

                AddOwner("Joao", "Tome", "Rua Rica Treta Nº1");
                AddOwner("Otavio", "Reis", "Rua Xafarica Nº3");
                AddOwner("Diana", "Gaio", "Rua Do Calhambec Nº54, 2B");
                AddOwner("Maria", "Suzana", "Rua Do Autocarro Nº1289");
                AddOwner("Xana", "Tiago", "Rua Sem Nome Nº2");
               
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string Fname, string Lname, string Add)
        {
            string ran = _random.Next(100000).ToString();
            var user = new User
            {
                Document = ran.ToString(),
                FirstName = Fname,
                LastName = Lname,
                Address = Add,
                Email = Fname + "." + Lname + "@gmail.com",
                UserName = Fname + "." + Lname + "@gmail.com",
                NormalizedEmail = Fname + "." + Lname + "@gmail.com",
                PhoneNumber = _random.Next(100000000).ToString()
            };

            _userHelper.AddUserAsync(user, "123456");

            _context.Owners.Add(new Owner
            {
                Document = ran,
                FirstName = Fname,
                LastName = Lname,
                FixPhone = _random.Next(100000000),
                Address = Add,
                CellPhone = _random.Next(100000000),
                User = user
            });
        }
    }
}
