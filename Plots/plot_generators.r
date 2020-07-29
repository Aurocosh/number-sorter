require("stringr")

generate_all_sort_barplots  <- function(connection, save_folder, extra_filter = "", pixelsPerRecord = 50) {
	query_string <- str_interp("SELECT DISTINCT dataset_name FROM sort_report_comparassion_by_time WHERE true ${extra_filter} ORDER BY dataset_name ")

	query_result <- dbSendQuery(connection, query_string)
	result <- dbFetch(query_result)
	dbClearResult(query_result)

	DataSetNames <- result[,c('dataset_name')]
	for (DataSetName in DataSetNames)
	{
		generate_sort_barplot(connection, save_folder, DataSetName, extra_filter, pixelsPerRecord)
	}
}

generate_sort_barplot <- function(connection, save_folder, dataset_name, extra_filter = "", pixelsPerRecord = 50) {
	query_string <- str_interp("SELECT * FROM sort_report_comparassion_by_time WHERE dataset_name = '${dataset_name}' ${extra_filter}")
	
	query_result <- dbSendQuery(connection, query_string)
	result <- dbFetch(query_result)
	dbClearResult(query_result)
	
	SortNames <- result[,c('sort_name')]
	Means <- result[,c('mean')]
	
	record_count <- length(Means)
	plot_height <- pixelsPerRecord * record_count
	plot_width <- plot_height * 1.8
	
	file_path <- paste(save_folder,"/",dataset_name,".png", sep = "")
	png(file = file_path, width = plot_width, height = plot_height)
	
	par(las=1)
	
	par(mai=c(1,3,1,1))
	barplot(Means, names.arg = SortNames, main = dataset_name, horiz = TRUE, cex.names = 1)
	dev.off()
}