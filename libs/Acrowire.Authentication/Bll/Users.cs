using System;
using System.Linq;
using System.Collections;

namespace Acrowire.Bll {
    
    
    public partial class Users
    {

        public static Users GetByEmail(string email)
        {
            Users user = new Users();

            try
            {
                var results = Users.GetAll()
                            .Where(u => u.Email.ToLower() == email.ToLower() && u.Active == true);

                if (results.Any())
                {
                    user = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return user;
        }

        public static Users GetByUserName(string username)
        {
            Users user = new Users();

            try
            {
                var results = Users.GetAll()
                            .Where(u => u.Username.ToLower() == username.ToLower() && u.Active == true);

                if (results.Any())
                {
                    user = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return user;
        }

        public static Users GetById(Guid PublicID)
        {
            Users user = new Users();

            try
            {
                var results = Users.GetAll()
                            .Where(u => u.Publicid == PublicID && u.Active == true);

                if (results.Any())
                {
                    user = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return user;
        }

        public static Users GetById(int id)
        {
            Users user = new Users();

            try
            {
                var results = Users.GetAll()
                            .Where(u => u.Id == id && u.Active == true);

                if (results.Any())
                {
                    user = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return user;
        }
    }
}
