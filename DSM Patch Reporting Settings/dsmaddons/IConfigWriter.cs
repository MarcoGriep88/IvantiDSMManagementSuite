using DSMAddons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMAddons
{
    public interface IConfigWriter
    {
        void WriteConfig(Setting setting);
        Setting ReadConfig();
    }
}
