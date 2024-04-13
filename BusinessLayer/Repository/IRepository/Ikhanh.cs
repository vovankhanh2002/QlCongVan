using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.IRepository
{
    public interface Ikhanh
    {
        Task SendEmailCV(List<string> toAddress, string subject, string htmlMessage, string attachmentFilePath = null);
    }
}
