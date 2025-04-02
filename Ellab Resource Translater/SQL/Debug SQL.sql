
--  See EMSuite entries
-- SELECT a.* from Translation a where a.SystemEnum = 1;


-- See Valsuite entries 
 select a.* from Translation a where a.Comment like '%#AI%';



-- DELETE a FROm Translation a where (a."Key" LIKE '%.Font') or ISNUMERIC(a.Text) = 1;
-- select * from Translation where SystemEnum = 0;

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


-- Get all manually translated Resources
-- SELECT a."Key", a.ResourceName, a.LanguageCode, a.Comment, b.ChangedText, b.ID as "ChangedID" FROM Translation a JOIN ChangedTranslation b ON a.ID = b.TranslationID where a.SystemEnum = 1;


-- Testing The Deletion before upload (but as Select)
-- SELECT a.* FROM Translation a where a.SystemEnum = 1 AND NOT a.ID in (Select ct.TranslationID from ChangedTranslation ct);

-- Testing The Deletion after updating local data
-- DELETE FROM ChangedTranslation WHERE ChangedTranslation.TranslationID in (SELECT ID FROM Translation where SystemEnum = 1);


-- Demonstration of ChangeTranslation

-- see ChangeTranslations
-- SELECT ct.* FROM Changedtranslation ct;

-- add ChangeTranslation of subject
-- insert INTO ChangedTranslation (ChangedText, TranslationID) SELECT t.Text + ' changed Text', t.ID from Translation t where t.SystemEnum = 1 and t."Key" = 'CannotUpdateComment' and t.LanguageCode = 'EN';

-- see chosen subject in translation
-- select t.* from Translation t where t.SystemEnum = 1 and t."Key" = 'CannotUpdateComment' and t.LanguageCode = 'EN';

-- This is what the program sees when fetching changes
/*
WITH changedTranslationsLatest AS (
  SELECT
    *,
    ROW_NUMBER() OVER(PARTITION BY TranslationID ORDER BY ID DESC) AS ReverseRowNumber
  FROM ChangedTranslation
)

SELECT a."Key", a.ResourceName, a.LanguageCode, a.Comment, COALESCE(b.ChangedText, a."Text") AS "ChangedText", ISNULL(b.ID, -1) AS "ChangedID", a.ID
FROM Translation a left JOIN changedTranslationsLatest b 
ON a.ID = b.TranslationID 
WHERE (b.ReverseRowNumber = 1 or b.ID IS NULL) 
--AND COALESCE(b.ChangedText, a."Text") != '' 
AND a.SystemEnum = 0
AND a.LanguageCode in ('EN', 'DE')

and a."Key" = '$this.Text'
--*/



-- SELECT a.* from Translation a JOIN (SELECT * from Translation where LanguageCode = 'EN' and SystemEnum = 1) b on a.ResourceName = b.ResourceName and a.Text = b.Text and a."Key" = b."Key" where a.LanguageCode <> 'EN' and a.SystemEnum = 1;