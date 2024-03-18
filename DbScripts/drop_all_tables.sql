USE touResourceDatabase
GO

-- drop fk constraints
ALTER TABLE [rms].[resource_program] DROP CONSTRAINT [fk_resource_detail_id]
GO
ALTER TABLE [rms].[profile_taxonomy_resource_xref] DROP CONSTRAINT [fk_tou_profile_taxon_id]
GO
ALTER TABLE [rms].[profile_taxonomy_resource_xref] DROP CONSTRAINT [fk_resource_id_tou_profile_taxonomy]
GO
ALTER TABLE [rms].[resource_program_steps] DROP CONSTRAINT [fk_resource_id_steps]
GO
ALTER TABLE [rms].[resource_program_status] DROP CONSTRAINT [fk_status_type_id]
GO
ALTER TABLE [rms].[resource_program_status] DROP CONSTRAINT [fk_resource_id_status]
GO
ALTER TABLE [rms].[resource_program_note] DROP CONSTRAINT [fk_resource_id_notes]
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
ALTER TABLE [rms].[resource_language_xref] DROP CONSTRAINT [fk_resource_id_language]
GO
ALTER TABLE [rms].[resource_language_xref] DROP CONSTRAINT [fk_language_id]
GO
ALTER TABLE [rms].[resource_contact_xref] DROP CONSTRAINT [fk_resource_id_contact]
GO
ALTER TABLE [rms].[resource_contact_xref] DROP CONSTRAINT [fk_contact_id_resource]
GO
ALTER TABLE [rms].[resource_code_legacy] DROP CONSTRAINT [fk_resource_id_legacy]
GO
ALTER TABLE [rms].[resource_application_type_xref] DROP CONSTRAINT [fk_resource_program_id_application_type]
GO
ALTER TABLE [rms].[resource_application_type_xref] DROP CONSTRAINT [fk_application_type_id]
GO
ALTER TABLE [rms].[resource_activity_detail] DROP CONSTRAINT [fk_activity_id_detail]
GO
ALTER TABLE [rms].[resource_activity] DROP CONSTRAINT [fk_resource_id_activity]
GO
ALTER TABLE [rms].[resource_activity] DROP CONSTRAINT [fk_activity_type_id]
GO
ALTER TABLE [rms].[resource_activity] DROP CONSTRAINT [fk_activity_detail_id]
GO
ALTER TABLE [rms].[organization_address] DROP CONSTRAINT [fk_organization_id]
GO
ALTER TABLE [rms].[contact_language_xref] DROP CONSTRAINT [fk_language_id_contact]
GO
ALTER TABLE [rms].[contact_language_xref] DROP CONSTRAINT [fk_contact_id_language]
GO

DROP TABLE IF EXISTS rms.resource_contact_xref;
DROP TABLE IF EXISTS rms.region_taxonomy_contact_xref;
DROP TABLE IF EXISTS rms.region_taxonomy_resource_xref;
DROP TABLE IF EXISTS rms.service_taxonomy_resource_xref;
DROP TABLE IF EXISTS rms.situation_taxonomy_resource_xref;
DROP TABLE IF EXISTS [rms].[profile_taxonomy_resource_xref];
DROP TABLE IF EXISTS rms.resource_application_type_xref;
DROP TABLE IF EXISTS rms.resource_language_xref;
DROP TABLE IF EXISTS rms.contact_language_xref;
DROP TABLE IF EXISTS rms.user_resource_xref;
GO
DROP TABLE IF EXISTS rms.region_taxonomy;
DROP TABLE IF EXISTS rms.situation_taxonomy;
DROP TABLE IF EXISTS rms.service_taxonomy;
DROP TABLE IF EXISTS [rms].[profile_taxonomy];
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

DROP TABLE IF EXISTS rms.resource_activity_type;
DROP TABLE IF EXISTS rms.resource_activity_detail;
DROP TABLE IF EXISTS rms.resource_activity;
DROP TABLE IF EXISTS rms.resource_program_status;
DROP TABLE IF EXISTS rms.resource_status_type;
DROP TABLE IF EXISTS rms.resource_code_legacy;
DROP TABLE IF EXISTS rms.resource_program_detail;
DROP TABLE IF EXISTS rms.resource_program;
GO

DROP TABLE IF EXISTS rms.organization_address;
DROP TABLE IF EXISTS rms.organization;
GO
