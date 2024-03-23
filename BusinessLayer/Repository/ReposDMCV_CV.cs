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
    public class ReposDMCV_CV : Repository<Tb_DMCV_CV>, IDMCV_CV
    {
        public ReposDMCV_CV(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
