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
    public class ReposThongke : Repository<Tb_Thongke>, IThongke
    {
        public ReposThongke(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
