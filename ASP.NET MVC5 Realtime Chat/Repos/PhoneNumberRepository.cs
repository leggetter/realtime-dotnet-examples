using ASP.NET_MVC5_Realtime_Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5_Realtime_Chat.Repos
{

    public class PhoneNumberRepository
    {
        ChatEntities _entities;

        public PhoneNumberRepository()
        {
            _entities = new ChatEntities();
        }

        public bool Create(PhoneNumber phoneNumber)
        {
            var createNewNumber = _entities.PhoneNumbers
                                        .Any(p => p.number == phoneNumber.number) == false;
            if (createNewNumber) {
                phoneNumber.timestamp = DateTime.Now;
                _entities.PhoneNumbers.Add(phoneNumber);
                _entities.SaveChanges();
            }
            return createNewNumber;
        }

        internal List<PhoneNumber> GetAll()
        {
            return _entities.PhoneNumbers.ToList();
        }

        internal bool NumberExists(string fromNumber)
        {
            return _entities.PhoneNumbers
                                        .Any(p => p.number == fromNumber) == true;
        }
    }
}