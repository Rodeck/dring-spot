using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public virtual ICollection<CategoryPlace> Places { get; set; }

        public Category()
        {
            Places = new HashSet<CategoryPlace>();
        }
    }
}