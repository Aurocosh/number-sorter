
CREATE OR REPLACE FUNCTION cap_first(value text)
	RETURNS text AS
$BODY$
SELECT upper(substring(value from 1 for 1)) || substring(value from 2 for length(value));
$BODY$
LANGUAGE SQL
IMMUTABLE
RETURNS NULL ON NULL INPUT;
ALTER FUNCTION cap_first OWNER TO db_user;

CREATE OR REPLACE FUNCTION split_pascal(value text)
	RETURNS text AS
$BODY$
SELECT regexp_replace(regexp_replace(value , '(\w)([A-Z][a-z])','\1 \2','g'), '([a-z])([A-Z\d])','\1 \2','g')
$BODY$
LANGUAGE SQL
IMMUTABLE
RETURNS NULL ON NULL INPUT;
ALTER FUNCTION split_pascal OWNER TO db_user;

CREATE OR REPLACE VIEW report_sort_names AS 
	SELECT report_id,
		sort_name,
		dense_rank() OVER(ORDER BY sort_name)::int AS sort_name_id
	FROM (
		SELECT id AS report_id,
			cap_first(lower(split_pascal(substring(type from '^(.*)Benchmarks')))) AS sort_name
		FROM report_data	
	) AS t;
ALTER TABLE report_sort_names OWNER TO db_user;
 
CREATE OR REPLACE VIEW benchmark_method_names AS 
	SELECT benchmark_id,
		dataset_name,
		dense_rank() OVER( ORDER BY dataset_name)::int AS dataset_id
	FROM (
		SELECT id AS benchmark_id,
		substring(full_name from 'name:\s\"([^\"]*)\"') AS dataset_name
		FROM benchmark
	) AS t;
ALTER TABLE benchmark_method_names OWNER TO db_user;

CREATE OR REPLACE VIEW benchmark_overview AS 
	SELECT b.report_id,
		rsn.sort_name,
		bmn.dataset_name,
		ROUND(b.statistics_median::numeric, 2) AS median,
		ROUND(b.statistics_mean::numeric, 2) AS mean,
		ROUND(b.statistics_standard_error::numeric, 2) AS standard_error,
		ROUND(b.statistics_standard_deviation::numeric, 2) AS standard_deviation,
		memory_bytes_allocated_per_operation AS memory_allocated,
		memory_gen0_collections,
		memory_gen1_collections,
		memory_gen2_collections,
		bmn.dataset_id,
		rsn.sort_name_id
	FROM benchmark AS b
	JOIN report_sort_names AS rsn ON rsn.report_id = b.report_id
	JOIN benchmark_method_names AS bmn ON bmn.benchmark_id = b.id;
ALTER TABLE benchmark_overview OWNER TO db_user;

CREATE OR REPLACE VIEW benchmark_types AS 
	SELECT DISTINCT type
	FROM report_data;
ALTER TABLE benchmark_types OWNER TO db_user;

CREATE OR REPLACE VIEW most_recent_reports_by_type_and_env AS 
SELECT DISTINCT ON (type, host_environment_info_id)
	id AS report_id,
	title,
	created,
	host_environment_info_id
FROM report_data
ORDER BY type,
	host_environment_info_id,
	created DESC;
ALTER TABLE most_recent_reports_by_type_and_env OWNER TO db_user;

CREATE OR REPLACE VIEW sort_report_comparassion_by_time AS 
SELECT row_number() OVER( PARTITION BY host_environment_info_id,dataset_name ORDER BY mean) AS position,
	sort_name,
	dataset_name,
	mean,
	round(mean / (min(mean) OVER( PARTITION BY host_environment_info_id,dataset_name ORDER BY mean)),2) AS compared_to_best,
	memory_allocated,
	memory_gen0_collections,
	memory_gen1_collections,
	memory_gen2_collections,
	standard_error,
	standard_deviation,
	host_environment_info_id,
	dataset_id,
	sort_name_id
FROM most_recent_reports_by_type_and_env AS mrr
JOIN benchmark_overview AS bo ON bo.report_id = mrr.report_id
ORDER BY host_environment_info_id,
	dataset_name,
	mean ASC;
ALTER TABLE sort_report_comparassion_by_time OWNER TO db_user;

CREATE OR REPLACE VIEW sort_report_comparassion_by_memory AS 
SELECT row_number() OVER( PARTITION BY host_environment_info_id,dataset_name ORDER BY memory_allocated,mean) AS position,
	sort_name,
	dataset_name,
	memory_allocated,
	mean,
	memory_gen0_collections,
	memory_gen1_collections,
	memory_gen2_collections,
	standard_error,
	standard_deviation,
	host_environment_info_id,
	dataset_id,
	sort_name_id
FROM most_recent_reports_by_type_and_env AS mrr
JOIN benchmark_overview AS bo ON bo.report_id = mrr.report_id
ORDER BY host_environment_info_id,
	dataset_name,
	memory_allocated,
	mean ASC;
ALTER TABLE sort_report_comparassion_by_memory OWNER TO db_user;

CREATE OR REPLACE VIEW public.sort_report_comparassion_by_memory_pretty AS
	SELECT sort_report_comparassion_by_memory."position",
		sort_report_comparassion_by_memory.sort_name,
		sort_report_comparassion_by_memory.dataset_name,
		to_char(sort_report_comparassion_by_memory.mean, '999G999G999G999'::text) AS mean,
		sort_report_comparassion_by_memory.memory_allocated,
		pg_size_pretty(sort_report_comparassion_by_memory.memory_allocated::numeric) AS memory_pretty,
		sort_report_comparassion_by_memory.memory_gen0_collections,
		sort_report_comparassion_by_memory.memory_gen1_collections,
		sort_report_comparassion_by_memory.memory_gen2_collections,
		sort_report_comparassion_by_memory.standard_error,
		sort_report_comparassion_by_memory.standard_deviation,
		sort_report_comparassion_by_memory.host_environment_info_id,
		dataset_id,
		sort_name_id
	FROM sort_report_comparassion_by_memory;
ALTER TABLE public.sort_report_comparassion_by_memory_pretty OWNER TO db_user;

CREATE OR REPLACE VIEW public.sort_report_comparassion_by_time_pretty AS
	SELECT sort_report_comparassion_by_time."position",
		sort_report_comparassion_by_time.sort_name,
		sort_report_comparassion_by_time.dataset_name,
		to_char(sort_report_comparassion_by_time.mean, '999G999G999G999'::text) AS mean,
		sort_report_comparassion_by_time.compared_to_best,
		sort_report_comparassion_by_time.memory_allocated,
		pg_size_pretty(sort_report_comparassion_by_time.memory_allocated::numeric) AS memory_pretty,
		sort_report_comparassion_by_time.memory_gen0_collections,
		sort_report_comparassion_by_time.memory_gen1_collections,
		sort_report_comparassion_by_time.memory_gen2_collections,
		sort_report_comparassion_by_time.standard_error,
		sort_report_comparassion_by_time.standard_deviation,
		sort_report_comparassion_by_time.host_environment_info_id,
		dataset_id,
		sort_name_id
	FROM sort_report_comparassion_by_time;
ALTER TABLE public.sort_report_comparassion_by_time_pretty OWNER TO db_user;

CREATE OR REPLACE VIEW recent_sort_position_sums_by_time AS
	SELECT sort_name,
		position,
		mean,
		memory_allocated,
		first_count,
		second_count,
		third_count,
		fourth_count,
		fifth_count,
		host_environment_info_id
	FROM (
		SELECT host_environment_info_id,
			sort_name,
			SUM(position) AS position,
			SUM(mean) AS mean,
			SUM(memory_allocated) AS memory_allocated,
			COUNT(CASE WHEN position = 1 THEN 1 END) AS first_count,
			COUNT(CASE WHEN position = 2 THEN 1 END) AS second_count,
			COUNT(CASE WHEN position = 3 THEN 1 END) AS third_count,
			COUNT(CASE WHEN position = 4 THEN 1 END) AS fourth_count,
			COUNT(CASE WHEN position = 5 THEN 1 END) AS fifth_count
		FROM sort_report_comparassion_by_time
		GROUP BY host_environment_info_id,
			sort_name
	) AS t
	ORDER BY position;
ALTER TABLE recent_sort_position_sums_by_time OWNER TO db_user;

CREATE OR REPLACE VIEW recent_sort_position_sums_by_memory AS
	SELECT sort_name,
		position,
		memory_allocated,
		mean,
		first_count,
		second_count,
		third_count,
		fourth_count,
		fifth_count,
		host_environment_info_id
	FROM (
		SELECT host_environment_info_id,
			sort_name,
			SUM(position) AS position,
			SUM(memory_allocated) AS memory_allocated,
			SUM(mean) AS mean,
			COUNT(CASE WHEN position = 1 THEN 1 END) AS first_count,
			COUNT(CASE WHEN position = 2 THEN 1 END) AS second_count,
			COUNT(CASE WHEN position = 3 THEN 1 END) AS third_count,
			COUNT(CASE WHEN position = 4 THEN 1 END) AS fourth_count,
			COUNT(CASE WHEN position = 5 THEN 1 END) AS fifth_count
		FROM sort_report_comparassion_by_memory
		GROUP BY host_environment_info_id,
			sort_name
	) AS t
	ORDER BY position;
ALTER TABLE recent_sort_position_sums_by_memory OWNER TO db_user;

CREATE OR REPLACE VIEW previous_sort_report_comparassion_by_time AS 
SELECT report_id,
	row_number() OVER( PARTITION BY host_environment_info_id,dataset_name,sort_name ORDER BY mean) AS position,
	sort_name,
	dataset_name,
	mean,
	round(mean / (min(mean) OVER( PARTITION BY host_environment_info_id,dataset_name,sort_name ORDER BY mean)),2) AS compared_to_best,
	memory_allocated,
	memory_gen0_collections,
	memory_gen1_collections,
	memory_gen2_collections,
	standard_error,
	standard_deviation,
	host_environment_info_id,
	dataset_id,
	sort_name_id
FROM report_data AS rd
JOIN benchmark_overview AS bo ON bo.report_id = rd.id
ORDER BY host_environment_info_id,
	dataset_name,
	sort_name,
	report_id DESC;
ALTER TABLE previous_sort_report_comparassion_by_time OWNER TO db_user;
