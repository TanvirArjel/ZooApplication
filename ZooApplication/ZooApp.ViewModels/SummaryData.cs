using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.ViewModels
{
    public class SummaryData
    {
        public IEnumerable<FoodSummaryViewModel> FoodSummaryViewModels { get; set; }
        public IEnumerable<FoodSummayByAnimalViewModel> FoodSummayByAnimalViewModels { get; set; }
    }
}
