using AccsessLayer;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class ReposPTNhan : Repository<Tb_PTNhan>, IPTNhan
    {
        public ReposPTNhan(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
