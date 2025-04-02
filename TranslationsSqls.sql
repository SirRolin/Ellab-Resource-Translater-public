
-- See EMSuite entries
-- select * from Translation where SystemEnum = 1;


-- See Valsuite entries 
--select * from Translation where SystemEnum = 0;

-- Duplicates
/*
WITH added_row_number AS (
  SELECT
    *,
    ROW_NUMBER() OVER(PARTITION BY "Key" + LanguageCode + ResourceName ORDER BY ID ASC) AS row_number
  FROM Translation
)
select *
FROM added_row_number
WHERE row_number <> 1;
*/

-- Delete All 
-- Delete from Translation;

-- Test Fetch Command
SELECT a.ResourceName, a.LanguageCode, a.Comment, b.ChangedText FROM Translation a INNER JOIN ChangedTranslation b ON a.ID = b.TranslationID;
