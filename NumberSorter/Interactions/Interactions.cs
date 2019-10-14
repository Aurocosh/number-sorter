using Microsoft.Win32;
using NumberSorter.Domain.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.Interactions
{
    static class Interactions
    {
        static Interactions()
        {
            DialogInteractions.AskYesNoQuestion.RegisterHandler(context =>
            {
                var result = MessageBox.Show(context.Input.Text, context.Input.Header, MessageBoxButton.YesNo, MessageBoxImage.Question);
                context.SetOutput(result == MessageBoxResult.Yes);
            });

            DialogInteractions.FindFileToOpenWithType.RegisterHandler(context =>
            {
                var dialog = new OpenFileDialog { Filter = context.Input };
                if (dialog.ShowDialog() == true)
                    context.SetOutput(dialog.FileName);
                else
                    context.SetOutput("");
            });
            DialogInteractions.FindFileToSaveWithType.RegisterHandler(context =>
            {
                var dialog = new SaveFileDialog { Filter = context.Input };
                if (dialog.ShowDialog() == true)
                    context.SetOutput(dialog.FileName);
                else
                    context.SetOutput("");
            });
        }
    }
}
