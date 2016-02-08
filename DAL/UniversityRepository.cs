using System;
using Database;

namespace DAL
{

    /// <summary>
    /// There are main repository that implements interfaces to get access for all database entities.
    /// </summary>
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