using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Hubs
{
    public class Chathub : Hub
    {
        public IUnitOfWork UnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Chathub(IUnitOfWork UnitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.UnitOfWork = UnitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.Name != null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
                return base.OnConnectedAsync();
            }
            return base.OnConnectedAsync();
        }
        public async Task SendMessageToAll(string message)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = UnitOfWork.nguoidung.GetById(i => i.Id == userId);
            DateTime ngayHienTai = DateTime.Now;
            if (user != null)
            {
                var objMessage = new Tb_Chat
                {
                    TaikhoangID = userId,
                    Ten = user.Hoten_NV,
                    Noidung = message,
                    Hinh = Convert.ToBase64String(user.Hinh),
                    Ngay = ngayHienTai.Hour +":"+ ngayHienTai.Minute + " " + ngayHienTai.Day + "/" + ngayHienTai.Month +"/"+ ngayHienTai.Year,
                };
                UnitOfWork.chat.Add(objMessage);
                UnitOfWork.Save();
                await Clients.All.SendAsync("ReceiveMessage", objMessage);
            }
        }
    }
}
