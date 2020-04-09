using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Models
{
    public class FileModel
    {
        public HttpPostedFileBase file { get; set; }
        public string password { get; set; }

        public bool uploaded { get; set; }
    }
}