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
    public class ReposCVDEN : Repository<Tb_CVDEN>, ICVDEN
    {
        public ReposCVDEN(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
