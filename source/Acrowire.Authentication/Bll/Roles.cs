using System;
using System.Linq;
using System.Collections;

namespace Acrowire.Bll {
    
    
    public partial class Roles 
    {

        public static Roles GetByName(String RoleName)
        {
            Roles role = new Roles();

            try
            {
                var results = Roles.GetAll()
                                   .Where(r => r.Active == true)
                                   .Where(r => r.Name.ToLower() == RoleName.ToLower());
                if (results.Any())
                {
                    role = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return role;
        }

        public static Roles GetById(Guid PublicId)
        {
            Roles role = new Roles();

            try
            {
                var results = Roles.GetAll()
                                   .Where(r => r.Active == true)
                                   .Where(r => r.Publicid == PublicId);

                if (results.Any())
                {
                    role = results.First();
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return role;
        }

    }
}
