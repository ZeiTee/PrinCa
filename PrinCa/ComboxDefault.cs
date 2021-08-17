using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrinCa
{
    public class ComboxDefault
    {
        public void SetDefaultValue(string item, ComboBox cbx)
        {
            int indexer = 0;
            foreach (string ci in cbx.Items)
            {
                if (ci == item)
                    break;
                else
                    indexer++;
            }
            cbx.SelectedIndex = indexer;
        }
    }
}
