# install.packages("RPostgres")

library(DBI)
source("plot_generators.r")

connection <- dbConnect(RPostgres::Postgres(),dbname = 'sort_benchmark_data', 
                 host = 'localhost',
                 port = 5432,
                 user = 'user',
                 password = 'password')
