﻿--DECLARE @ConstraintName nvarchar(200)
--SELECT @ConstraintName = KCU.CONSTRAINT_NAME
--FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
--INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
--	ON KCU.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
--	AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
--	AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
--WHERE
--	KCU.TABLE_NAME = 'ApplicationVersionMonths' AND
--	KCU.COLUMN_NAME = 'VersionId'
--IF @ConstraintName IS NOT NULL
--BEGIN
--	EXEC('ALTER TABLE ApplicationVersionMonths DROP CONSTRAINT ' + @ConstraintName)
--END;
--ALTER TABLE ApplicationVersionMonths WITH CHECK ADD CONSTRAINT FK_ApplicationVersionMonths_ApplicationVersions FOREIGN KEY (VersionId) REFERENCES ApplicationVersions (Id) ON DELETE CASCADE;



--SELECT @ConstraintName = KCU.CONSTRAINT_NAME
--FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
--INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
--	ON KCU.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
--	AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
--	AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
--WHERE
--	KCU.TABLE_NAME = 'ApplicationVersions' AND
--	KCU.COLUMN_NAME = 'ApplicationId'
--IF @ConstraintName IS NOT NULL
--BEGIN
--	EXEC('ALTER TABLE ApplicationVersions DROP CONSTRAINT ' + @ConstraintName)
--END;
--ALTER TABLE ApplicationVersions WITH CHECK ADD CONSTRAINT FK_ApplicationVersions_Applications FOREIGN KEY (ApplicationId) REFERENCES Applications (Id) ON DELETE CASCADE;

--SELECT @ConstraintName = KCU.CONSTRAINT_NAME
--FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
--INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
--	ON KCU.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
--	AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
--	AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
--WHERE
--	KCU.TABLE_NAME = 'IncidentVersions' AND
--	KCU.COLUMN_NAME = 'IncidentId'
--IF @ConstraintName IS NOT NULL
--BEGIN
--	EXEC('ALTER TABLE IncidentVersions DROP CONSTRAINT ' + @ConstraintName)
--END;
--ALTER TABLE IncidentVersions WITH CHECK ADD CONSTRAINT FK_IncVersions_Incidents FOREIGN KEY (IncidentId) REFERENCES Incidents (Id) ON DELETE CASCADE;

--create table ErrorReportCollectionProperties
--(
--	Id int identity not null primary key,
--	ReportId int not null constraint FK_ErrorReportCollectionProperties_ErrorReports REFERENCES ErrorReports(Id) ON DELETE CASCADE,
--	Name varchar(50) not null,
--	PropertyName varchar(50) not null,
--	Value nvarchar(MAX)  not null
--);


--create table ErrorReportCollectionInbound
--(
--	Id int identity not null primary key,
--	ReportId int not null constraint FK_ErrorReportCollectionInbound_ErrorReports REFERENCES ErrorReports(Id) ON DELETE CASCADE,
--	Body nvarchar(max) not null
--);
