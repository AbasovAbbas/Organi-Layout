using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.Entity
{
    public class AppInfo
    {
        public int Id { get; set; }
        public string AppTitle { get; set; }
        public string LogoPath { get; set; }
        public string ImageLink { get; set; }
        public string HashTag { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string OpenTime { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string Githublink { get; set; }
        public string TwitterLink { get; set; }
        public string PinterestLink { get; set; }
    }
}
