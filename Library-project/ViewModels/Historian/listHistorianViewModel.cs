using Library_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.ViewModels.Historian
{
    public class listHistorianViewModel
    {
        public List<historians> allHistorians { get; set; } = new List<historians>();
    }

}
