# run init first then use following functions to generate plots

save_folder <- "D:\\" #Folder where plots should be saved

# Generate plot for a specific data set
generate_sort_barplot(connection,save_folder,"Random unsorted (10000)") 

# Generate plot for all data sets
generate_all_sort_barplots(connection,save_folder)

# Generate plot for all data sets but only include best 20 sorts
generate_all_sort_barplots(connection,save_folder,"AND position <= 20") 

# Generate plot for all data sets but only include results of quick sort algorithms
generate_all_sort_barplots(connection,save_folder,"AND sort_name ILIKE '%quick sort%'") 

# dbDisconnect(connection)