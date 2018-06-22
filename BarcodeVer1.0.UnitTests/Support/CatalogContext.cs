using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVer1._0.UnitTests.Support
{
    public class CatalogContext
    {
        public CatalogContext()
        {
            ReferenceProject = new ReferenceProjectList();
        }

        public ReferenceProjectList ReferenceProject { get; set; }
    }
}
