using System;
using Database;

namespace DAL
{
    public partial class UniversityRepository : IDisposable
    {
        private UniversityDbContext _db;

        public UniversityRepository()
        {
            _db = UniversityDbContext.Create();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}