using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Interactions.Views
{
    class FileOpenView : ReactiveObject
    {
        public FileOpenView()
        {
            this.WhenActivated(
                d =>
                {
                    d(this
                        .ViewModel
                        .Confirm
                        .RegisterHandler(
                            async interaction =>
                            {
                                var deleteIt = await this.DisplayAlert(
                                    "Confirm Delete",
                                    $"Are you sure you want to delete '{interaction.Input}'?",
                                    "YES",
                                    "NO");

                                interaction.SetOutput(deleteIt);
                            }));
                });
        }
    }

}
