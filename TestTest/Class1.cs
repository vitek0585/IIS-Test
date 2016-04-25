﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOutUser.Models;
using Xunit;

namespace TestTest
{
    class UserTest
    {
        public int SubId { get; set; }
        public string SubName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Users { get; set; }
    }
    public class Class1
    {
        [Fact]
        public void MyFunction()
        {
            var c = new ApplicationDbContext();
            var ubers = c.Users.Where(r => r.Roles.Any(ro => ro.RoleId == "1")).Select(
                u => new UserTest()
                {
                    SubId = u.SubscriptionId,
                    SubName = u.Subscription.Name,
                    Email = u.Email
                }).ToList();

            var result = c.Users.GroupBy(u => u.SubscriptionId)
                .Select(u => new
                {
                    id = u.Key,
                    Users = u.Select(r => r.Email)
                }).ToList();

            ubers.ForEach(u => u.Users = result.First(r => r.id == u.SubId).Users.Where(us=>us!=u.Email));

        }

    }
}