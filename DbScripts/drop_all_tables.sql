USE touResourceDatabase
GO

-- drop fk constraints
ALTER TABLE [rms].[resource_program_steps] DROP CONSTRAINT [fk_resource_id_steps]
GO
ALTER TABLE [rms].[resource_program_detail] DROP CONSTRAINT [fk_resource_steps_id_detail]
GO
ALTER TABLE [rms].[resource_program_detail] DROP CONSTRAINT [fk_resource_process_time_id_detail]
GO
ALTER TABLE [rms].[resource_program_detail] DROP CONSTRAINT [fk_resource_id_detail]
GO
ALTER TABLE [rms].[resource_program_detail] DROP CONSTRAINT [fk_resource_description_id_detail]
GO
ALTER TABLE [rms].[resource_program_detail] DROP CONSTRAINT [fk_internal_notes_id_detail]
GO
ALTER TABLE [rms].[resource_program_description] DROP CONSTRAINT [fk_resource_id_description]
GO
ALTER TABLE [rms].[resource_program] DROP CONSTRAINT [fk_resource_status_id]
GO
ALTER TABLE [rms].[resource_program] DROP CONSTRAINT [fk_resource_organization_id]
GO
ALTER TABLE [rms].[resource_program] DROP CONSTRAINT [fk_resource_detial_id]
GO

DROP TABLE IF EXISTS rms.resource_contact_xref;
DROP TABLE IF EXISTS rms.region_taxonomy_contact_xref;
DROP TABLE IF EXISTS rms.region_taxonomy_resource_xref;
DROP TABLE IF EXISTS rms.service_taxonomy_resource_xref;
DROP TABLE IF EXISTS rms.situation_taxonomy_resource_xref;
DROP TABLE IF EXISTS rms.resource_application_type_xref;
DROP TABLE IF EXISTS rms.resource_language_xref;
DROP TABLE IF EXISTS rms.contact_language_xref;
DROP TABLE IF EXISTS rms.user_resource_xref;
GO
DROP TABLE IF EXISTS rms.region_taxonomy;
DROP TABLE IF EXISTS rms.situation_taxonomy;
DROP TABLE IF EXISTS rms.service_taxonomy;
DROP TABLE IF EXISTS rms.resource_application_type;
DROP TABLE IF EXISTS rms.resource_process_time;
DROP TABLE IF EXISTS rms.resource_status;
DROP TABLE IF EXISTS rms.resource_language;
DROP TABLE IF EXISTS rms.resource_contact;
GO

DROP TABLE IF EXISTS rms.resource_program_description;
DROP TABLE IF EXISTS rms.resource_program_note;
DROP TABLE IF EXISTS rms.resource_program_steps;
DROP TABLE IF EXISTS rms.resource_process_time;
GO

DROP TABLE IF EXISTS rms.resource_program_detail;
DROP TABLE IF EXISTS rms.resource_program;
GO

DROP TABLE IF EXISTS rms.organization_address;
DROP TABLE IF EXISTS rms.organization;
GO