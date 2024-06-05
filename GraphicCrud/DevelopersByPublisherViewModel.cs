using GraphicCrud.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphicCrud
{
    public class DevelopersByPublisherViewModel : ObservableRecipient
    {
        public RestCollection<Developer> DevelopersByPublisher { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public DevelopersByPublisherViewModel()
        {
            if (!IsInDesignMode)
            {
                DevelopersByPublisher = new RestCollection<Developer>("http://localhost:57640/", "Stat/DevelopersByPublisherName/Electronic Arts");
            }
        }
    }
}
