CREATE EXTENSION tablefunc;

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
SELECT regexp_replace(value , '(\w)([A-Z])','\1 \2','g')
$BODY$
LANGUAGE SQL
IMMUTABLE
RETURNS NULL ON NULL INPUT;
ALTER FUNCTION split_pascal OWNER TO db_user;

CREATE OR REPLACE VIEW benchmark_parameters AS 
	SELECT id AS benchmark_id,
		substring(param from '(^[^=]+)+=.*') AS name,
		substring(param from '[^=]+=(.*)') AS value
	FROM (
		SELECT id,
			unnest(regexp_matches(parameters, '(?:^|&|;)([^=]+=[^&|;]+)', 'g')) AS param
		FROM benchmark
	) AS t;
ALTER TABLE benchmark_parameters OWNER TO db_user;

CREATE OR REPLACE VIEW report_sort_names AS 
	SELECT id AS report_id,
		cap_first(lower(split_pascal(substring(type from '^(.*)Benchmarks')))) AS sort_name
	FROM report_data;
ALTER TABLE report_sort_names OWNER TO db_user;
 
CREATE OR REPLACE VIEW benchmark_method_names AS 
	SELECT id AS benchmark_id,
		cap_first(lower(split_pascal(replace(method,'_',' ')))) AS method_name
	FROM benchmark;
ALTER TABLE benchmark_method_names OWNER TO db_user;

CREATE OR REPLACE VIEW benchmark_overview AS 
	SELECT b.report_id,
		rsn.sort_name,
		bmn.method_name,
		bp.value,
		ROUND(b.statistics_median::numeric, 2) AS median,
		ROUND(b.statistics_mean::numeric, 2) AS mean,
		ROUND(b.statistics_standard_error::numeric, 2) AS standard_error,
		ROUND(b.statistics_standard_deviation::numeric, 2) AS standard_deviation,
		memory_bytes_allocated_per_operation AS memory_allocated,
		memory_gen0_collections,
		memory_gen1_collections,
		memory_gen2_collections
	FROM benchmark AS b
	JOIN report_sort_names AS rsn ON rsn.report_id = b.report_id
	JOIN benchmark_method_names AS bmn ON bmn.benchmark_id = b.id
	JOIN benchmark_parameters AS bp ON bp.benchmark_id = b.id
		AND bp.name = 'size';
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
	created;
ALTER TABLE most_recent_reports_by_type_and_env OWNER TO db_user;

CREATE OR REPLACE VIEW sort_report_comparassion_by_time AS 
SELECT row_number() OVER( PARTITION BY method_name,value ORDER BY mean) AS position,
	sort_name,
	method_name,
	value,
	mean,
	round(mean / (min(mean) OVER( PARTITION BY method_name,value ORDER BY mean)),2) AS compared_to_best,
	memory_allocated,
	memory_gen0_collections,
	memory_gen1_collections,
	memory_gen2_collections,
	standard_error,
	standard_deviation,
	host_environment_info_id
FROM most_recent_reports_by_type_and_env AS mrr
JOIN benchmark_overview AS bo ON bo.report_id = mrr.report_id
ORDER BY method_name,
	value,
	mean ASC;
ALTER TABLE sort_report_comparassion_by_time OWNER TO db_user;

CREATE OR REPLACE VIEW sort_report_comparassion_by_memory AS 
SELECT row_number() OVER( PARTITION BY method_name,value ORDER BY memory_allocated,mean) AS position,
	sort_name,
	method_name,
	value,
	memory_allocated,
	mean,
	memory_gen0_collections,
	memory_gen1_collections,
	memory_gen2_collections,
	standard_error,
	standard_deviation,
	host_environment_info_id
FROM most_recent_reports_by_type_and_env AS mrr
JOIN benchmark_overview AS bo ON bo.report_id = mrr.report_id
ORDER BY method_name,
	value,
	memory_allocated,
	mean ASC;
ALTER TABLE sort_report_comparassion_by_memory OWNER TO db_user;

CREATE OR REPLACE VIEW recent_sort_position_sums_by_time AS
	SELECT sort_name,
		position,
		mean,
		memory_allocated,
		first_count,
		second_count,
		third_count,
		fourth_count,
		fifth_count
	FROM (
		SELECT sort_name,
			SUM(position) AS position,
			SUM(mean) AS mean,
			SUM(memory_allocated) AS memory_allocated,
			COUNT(CASE WHEN position = 1 THEN 1 END) AS first_count,
			COUNT(CASE WHEN position = 2 THEN 1 END) AS second_count,
			COUNT(CASE WHEN position = 3 THEN 1 END) AS third_count,
			COUNT(CASE WHEN position = 4 THEN 1 END) AS fourth_count,
			COUNT(CASE WHEN position = 5 THEN 1 END) AS fifth_count
		FROM sort_report_comparassion_by_time
		GROUP BY sort_name
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
		fifth_count
	FROM (
		SELECT sort_name,
			SUM(position) AS position,
			SUM(memory_allocated) AS memory_allocated,
			SUM(mean) AS mean,
			COUNT(CASE WHEN position = 1 THEN 1 END) AS first_count,
			COUNT(CASE WHEN position = 2 THEN 1 END) AS second_count,
			COUNT(CASE WHEN position = 3 THEN 1 END) AS third_count,
			COUNT(CASE WHEN position = 4 THEN 1 END) AS fourth_count,
			COUNT(CASE WHEN position = 5 THEN 1 END) AS fifth_count
		FROM sort_report_comparassion_by_memory
		GROUP BY sort_name
	) AS t
	ORDER BY position;
ALTER TABLE recent_sort_position_sums_by_memory OWNER TO db_user;

CREATE OR REPLACE VIEW top_three_sorts_by_time AS
	SELECT *
	FROM crosstab(
	  'SELECT format(''%s (%s)'', method_name, value),
			position,
			sort_name	
		FROM sort_report_comparassion_by_time
		WHERE position < 4
	   ORDER by 1,2',
		
	  'select m from generate_series(1,3) m'
	)
	AS ct(method text, first text, second text, third text);
ALTER TABLE top_three_sorts_by_time OWNER TO db_user;

CREATE OR REPLACE VIEW top_three_sorts_by_memory AS
	SELECT *
	FROM crosstab(
	  'SELECT format(''%s (%s)'', method_name, value),
			position,
			sort_name	
		FROM sort_report_comparassion_by_memory
		WHERE position < 4
	   ORDER by 1,2',
		
	  'select m from generate_series(1,3) m'
	)
	AS ct(method text, first text, second text, third text);
ALTER TABLE top_three_sorts_by_memory OWNER TO db_user;

