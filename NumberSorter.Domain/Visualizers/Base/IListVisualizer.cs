﻿using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Container;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Base.Visualizers
{
    public interface IListVisualizer
    {
        WriteableBitmap Init(SortLog<int> sortLog, int width, int height);
        int Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet);
    }
}
