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
    public class ReposHop : Repository<Tb_Hop>, IHop
    {
        public ReposHop(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
