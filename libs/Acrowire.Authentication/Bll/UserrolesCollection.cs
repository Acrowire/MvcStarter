namespace Acrowire.Bll {
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Collections.Concurrent;
using System;
    
    public partial class UserrolesCollection : Collection<Userroles> 
    {
        public static ConcurrentDictionary<Int32, Userroles> Cache = new ConcurrentDictionary<int, Userroles>();

        public static Bll.UserrolesCollection GetAll()
        {
            var results = new Bll.UserrolesCollection();

            var list = Bll.Userroles.GetAll();

            foreach (var item in list)
            {
                if (UserrolesCollection.Cache.ContainsKey(item.Id.Value) == false)
                {
                    item.Roles = Bll.Roles.Load(item.RoleId);
                    item.Users = Bll.Users.Load(item.UserId);
                    Cache.TryAdd(item.Id.Value, item);
                }

                results.Add(UserrolesCollection.Cache[item.Id.Value]);
            }

            return results;
        }
    }
}
