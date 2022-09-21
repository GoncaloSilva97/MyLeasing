
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


            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lessee");





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
                    UserName = "dalton.fury120@gmail.com"
                    


                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }


                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }





            if (!_context.Owners.Any())
            {
                await AddOwner("Joao", "Entao", "Rua Xica Nº37, 8C");
                await AddOwner("Sao", "Santos","Rua da Tia Nº12, 4A");
                await AddOwner("Andre", "Re", "Rua Nunca Nº3, 6H");
                await AddOwner("Tomas", "Razz", "Rua Da America Nº9, 7C");
                await AddOwner("Sofia", "Cotovia", "Rua Tranca Nº19, 6P");

                await AddOwner("Joao", "Tome", "Rua Rica Treta Nº1");
                await AddOwner("Otavio", "Reis", "Rua Xafarica Nº3");
                await AddOwner("Diana", "Gaio", "Rua Do Calhambec Nº54, 2B");
                await AddOwner("Maria", "Suzana", "Rua Do Autocarro Nº1289");
                await AddOwner("Xana", "Tiago", "Rua Sem Nome Nº2");             
                await _context.SaveChangesAsync();
            }


            if (!_context.Lessee.Any())
            {
                await AddLessee("Ruy", "Charls", "Rua Kapa Nº33, 1C");
                await AddLessee("Simon", "Nothing", "Rua da Gama Nº1, 4A");
                await AddLessee("Andrew", "Pyke", "Rua Never Nº1, 6E");
                await AddLessee("Tomy", "Mouse", "Rua Da Alemanha Nº245, 1C");
                await AddLessee("Sofya", "Cast", "Rua Da Caixa Nº5, 3P");
                await _context.SaveChangesAsync();
            }
        }





        private async Task AddOwner(string Fname, string Lname, string Add)
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
                NormalizedUserName = Fname + "." + Lname + "@gmail.com",
                //PhoneNumber = _random.Next(100000000).ToString()
            };

            await _userHelper.AddUserAsync(user, "123456");

            await _userHelper.AddUserToRoleAsync(user, "Owner");


            _context.Owners.Add(new Owner
            {
                Document = ran.ToString(),
                FirstName = Fname,
                LastName = Lname,
                FixPhone = _random.Next(100000000),
                Address = Add,
                CellPhone = _random.Next(100000000),
                User = user
            });
        }

        private async Task AddLessee(string Fname, string Lname, string Add)
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
                NormalizedUserName = Fname + "." + Lname + "@gmail.com",
                //PhoneNumber = _random.Next(100000000).ToString()
            };

            await _userHelper.AddUserAsync(user, "123456");

            await _userHelper.AddUserToRoleAsync(user, "Lessee");



            _context.Lessee.Add(new Lessee
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
