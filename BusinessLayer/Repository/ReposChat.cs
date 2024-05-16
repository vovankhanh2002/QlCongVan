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
    public class ReposChat : Repository<Tb_Chat>, IChat
    {
        public ReposChat(DBContextCV dbContext) : base(dbContext)
        {
        }
    }
}
