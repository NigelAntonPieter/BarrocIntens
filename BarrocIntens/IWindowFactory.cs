using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens
{
    public interface IWindowFactory
    {
        Window CreateWindow(string role);
    }
}
