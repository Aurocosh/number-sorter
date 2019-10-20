using Microsoft.Win32;
using NumberSorter.Domain.Interactions;
using NumberSorter.Domain.Utility;
using System.Windows;
using WinColorDialog = System.Windows.Forms.ColorDialog;
using WinDialogResult = System.Windows.Forms.DialogResult;

namespace NumberSorter.Interactions
{
    static class WpfInteractions
    {
        public static void RegisterInteractions()
        {
            DialogInteractions.PickAnotherColor.RegisterHandler(context =>
           {
               var colorDialog = new WinColorDialog();
               colorDialog.Color = context.Input.ConvertToDrawing();
               if (colorDialog.ShowDialog() == WinDialogResult.OK)
                   context.SetOutput(colorDialog.Color.ConvertToMedia());
               else
                   context.SetOutput(context.Input);
           });

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
