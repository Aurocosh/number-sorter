﻿using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class NewConsecutiveListProcessorLineControl : ReactiveUserControl<NewConsecutiveListProcessorLineViewModel>
    {
        public NewConsecutiveListProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Size, x => x.SizeUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Step, x => x.StepUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Start, x => x.StartUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
