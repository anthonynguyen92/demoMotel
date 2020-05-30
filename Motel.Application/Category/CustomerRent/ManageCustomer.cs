﻿using Microsoft.EntityFrameworkCore;
using Motel.Application.Category.CustomerRent.Dtos;
using Motel.Application.Dtos;
using Motel.EntityDb.EF;
using Motel.EntityDb.Entities;
using Motel.Utilities.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Application.Category.CustomerRent
{
    public class ManageCustomer : IManageCustomer
    {
        private readonly MotelDbContext _context;
        
        public ManageCustomer(MotelDbContext context)
        {
            _context = context;
        }
        private List<string> IDCustomer
        {
            get
            {
                var result = from c in _context.Customers
                             select c.IDuser;
                return result.ToList();
            }
        }
 
        // valid email - customer - identification - phone number
        public async Task<int> Create(CustomerRequest customer)
        {
            if (IDCustomer.Contains(customer.IDuser))
                return 0;
            else
            {
                var kq = new Customer()
                {
                    IDuser = customer.IDuser,
                    Identification = customer.Identification,
                    Address = customer.Address,
                    Birthdate = customer.Birthdate,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Sex = customer.Sex,
                };
                _context.Customers.Add(kq);
            }
            return await _context.SaveChangesAsync();
        }

        // need return name delete 
        public async Task<int> Delete(string id)
        {
            var result = _context.Customers.Find(id);
            if (result == null) return 0;
            else
                _context.Customers.Remove(result);
            return await _context.SaveChangesAsync();
        }

        // Find Customer by id
        public async Task<Customer> Find(string id)
        {
            var result = await _context.Customers.FindAsync(id);
            return result;
        }

        // Get all Customer
        public async Task<PagedViewModel<CustomerRequest>> GettAll()
        {
            var request = from c in _context.Customers
                          orderby c.IDuser
                          select c;
            PagedViewModel<CustomerRequest> list = new PagedViewModel<CustomerRequest>()
            {
                Items = request.Select(x => new CustomerRequest()
                {
                    Address = x.Address,
                    Birthdate = x.Birthdate,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Identification = x.Identification,
                    IDuser = x.IDuser,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Sex = x.Sex,
                }).ToList(),
                TotalRecord = await request.CountAsync(),
            };
            return list;
        }

        // Update Customer - all information
        public async Task<int> Update(string id, CustomerRequest customer)
        {
            var request = _context.Customers.Find(customer.IDuser);
            if (request == null) return 0; 
            else
            {
                request.LastName = customer.LastName;
                request.Identification = customer.Identification;
                request.FirstName = customer.FirstName;
                request.Address = customer.Address;
                request.Birthdate = customer.Birthdate;
                request.Email = customer.Email;
                request.Sex = customer.Sex;
                request.PhoneNumber = customer.PhoneNumber;
                _context.Customers.Update(request);
            }
            return await _context.SaveChangesAsync();
        }

        // Update Customer - Address
        public async Task<int> UpdateAddress(string id, string address)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.Address = address;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        // Update Customer - Email
        public async Task<int> UpdateEmail(string id, string email)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.Email = email;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        // Update Customer - Identification
        public async Task<int> UpdateIdentification(string id, string idfi)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.Identification = idfi;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        //Update Customer - First Name - Last Name
        public async Task<int> UpdateName(string id, string fname, string lname)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.FirstName = fname;
                result.LastName = lname;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        // Update Customer - Phone Number
        public async Task<int> UpdatePhoneNumber(string id, string number)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.PhoneNumber = number;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        // Update Customer - Sex
        public async Task<int> UpdateSex(string id, string sex)
        {
            var result = _context.Customers.Find(id);
            if (result == null)
                return 0;
            else
            {
                result.Sex = sex;
                _context.Customers.Update(result);
            }
            return await _context.SaveChangesAsync();
        }

        // Get Customer by Name - no controller yet.
        public async Task<PagedViewModel<CustomerRequest>> GetByFirstName(string name)
        {
            var request = from c in _context.Customers
                         where c.FirstName.Contains(name)
                         select c;
            PagedViewModel<CustomerRequest> list = new PagedViewModel<CustomerRequest>()
            {
                Items = request.Select(x => new CustomerRequest()
                {
                    Address = x.Address,
                    Birthdate = x.Birthdate,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Identification = x.Identification,
                    IDuser = x.IDuser,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Sex = x.Sex,
                }).ToList(),
                TotalRecord = await request.CountAsync(),
            };
            return list;
        }

        public string GetIDbyRequest(CustomerRequest request) =>  request.IDuser;
    }
}
