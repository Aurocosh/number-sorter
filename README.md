# Number sorter
Number sorter is a sorting algorithm visualization software written in c# with WPF.

## Features
- More than 65 various sorting algorithm implementations
- Simple random number generator for simple cases
- Complex and flexible system of custom number generators
- Several types of array visualization
- Customizable color schemes for array visualization
- Ability to select what should be displayed in visualization (read, write, comparison)
- Step by step execution or automatic animation of sorting process
- Rewind and fast-forward of visualization. Ability to step forward and backwards by one, a hundred and a thousand steps
- Benchmarking with BenchmarkDotNet
- Extensive unit test coverage

## Algorithms
- Shell sort (4 variations)
- Merge sort (2 subtypes and 14 variations)
- Binary sort (2 variations)
- Multi merge sort (3 variations)
- Quick sort (4 variations)
- Selection sort (2 variations)
- Strand sort (6 variations)
- Bubble sort
- Circle sort
- Cocktail sort
- Comb sort
- Cycle sort
- Gnome sort
- Heap sort
- Insertion sort
- JHeap sort
- OddEven sort
- Pancake sort
- Average bucket sort
- Smooth sort
- Tim sort
- Wiki sort
- Bit radix sort (4 variations)
- Radix msd sort (2 variations)
- American flag sort

## Input data
You can either load numbers you want to sort from a text file or you can generate them inside the program.
If you just want to generate some random numbers then you can use integrated simple number generator. Just specify minimum, maximum and how many numbers to generate.

For more complex cases you can use integrated custom number generator system. Custom number generator allows you to create a template which specifies the rules for number generation that can be used repeatedly to create number set with specific properties. There are several default custom number generators included that you can use. Custom generators allow you to create numbers sets where everything is sorted or almost sorted, number sets with a few unique values and inverted number sets. They also allow you to create number sets where some parts are sorted, some unsorted and some inverted or any combination of the above.

## Visualization
Currently there are eight possible array visualization types
- Columns
- Columns (No spacers)
- Ghostly columns
- Ghostly columns (No spacers)
- Points cloud
- Squares cloud
- Positive columns
- Positive columns (No spacers)

## Visualization components
You can freely choose what part of visualization you want to see or hide. You can toggle reads, writes and . For example if you disable reads and comparisons then only changes in array will be visualized and everything else will be skipped. You can change what components to display at any time.

## Color scheme
This program features customizable visualization color sets. You can change colors of reads, writes, background, normal color of columns and color of comparison. For comparison you can specify colors for smaller, bigger and equal numbers. You can create several color sets and swap between the mat will.

## Visualization execution
You can watch visualization either step by step, manually advancing visualization, or you can turn on animation in which case steps of visualization will advance automatically with specified delay. You can rewind visualization and fast-forward it by one, a hundred and a thousand steps. Rewind and fast-forward work both in manual mode and while animation is enabled.

## Benchmarking
If you want to compare performance of several algorithms then you can use integrated benchmarking. There is a dedicated subproject in solution called NumberSorter.Domain.Benchmark. To perform benchmark build and run this project in 'Release' mode. Console window will appear that lists all available benchmarks. Each benchmark has a number associated with it. Enter numbers of all benchmarks that you want to run separated by comma and press enter. After benchmarking is completed all results will be in folder with benchmark executable called "BenchmarkDotNet.Artifacts".

Benchmarking supports automatic upload of benchmarking results to Postgres database. Copy file data_upload_settings.json to a folder with benchmark executable and change in this file postgresql database connection settings (host, port, database name, role and password. If this file is present and database connection info is correct then after benchmarking all results will be automatically uploaded into database. There is no need to create tables for benchmarking results, they will be created automatically.

## Example
Example visualization of Quick sort: https://imgur.com/6P0OO14

