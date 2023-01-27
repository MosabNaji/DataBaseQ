using DataBaseQ.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseQ.Web.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int PostCount { get; set; }
        public DateTime? LastPostAded { get; set; }

    }
}
