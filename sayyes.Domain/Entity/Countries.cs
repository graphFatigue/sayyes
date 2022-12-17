using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;

namespace sayyes.Domain.Entity
{
    public class Countries
    {
        public List<string> cultureList { get; set; }

        public void SetList() 
        { 
            cultureList = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    
                    RegionInfo region = new RegionInfo(culture.LCID);

                    
                    if (!(this.cultureList.Contains(region.EnglishName)))
                    {
                       
                        this.cultureList.Add(region.EnglishName);
                    }
                }
                catch (ArgumentException ex)
                {
                   
                    continue;
                }
            }
            cultureList.Sort();

            StringBuilder sb = new StringBuilder();
            foreach (var country in cultureList)
            {
                sb.AppendLine(country);
            }

            Console.WriteLine(sb.ToString());
        }
           
    }
    
}

