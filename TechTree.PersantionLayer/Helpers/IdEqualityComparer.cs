using System.Diagnostics.CodeAnalysis;
using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.Helpers
{

    public class AppUserEmail_EqualityComparer : IEqualityComparer<ApplicationUser>
    {
        public bool Equals(ApplicationUser x, ApplicationUser y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Email == y.Email;
        }



        public int GetHashCode([DisallowNull] ApplicationUser obj)
        {
            if (obj is null) return 0;
            return obj.Email.GetHashCode();
        }

    }

    public class AppUserUserName_EqualityComparer : IEqualityComparer<ApplicationUser>
    {
        public bool Equals(ApplicationUser x, ApplicationUser y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.UserName == y.UserName;
        }



        public int GetHashCode([DisallowNull] ApplicationUser obj)
        {
            if (obj is null) return 0;
            return obj.UserName.GetHashCode();
        }

    }

}
