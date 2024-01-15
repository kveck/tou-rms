USE touResourceDatabase
GO
DROP TABLE IF EXISTS resource_contact_xref;
DROP TABLE IF EXISTS region_taxonomy_contact_xref;
DROP TABLE IF EXISTS region_taxonomy_resource_xref;
DROP TABLE IF EXISTS service_taxonomy_resource_xref;
DROP TABLE IF EXISTS situation_taxonomy_resource_xref;
DROP TABLE IF EXISTS resource_application_type_xref;
DROP TABLE IF EXISTS resource_language_xref;
DROP TABLE IF EXISTS contact_language_xref;
DROP TABLE IF EXISTS user_resource_xref;
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
DROP TABLE IF EXISTS user_resource_favorites;
DROP TABLE IF EXISTS user_resource_rating;
DROP TABLE IF EXISTS user_security;
DROP TABLE IF EXISTS user_preference;
DROP TABLE IF EXISTS user_detail;
DROP TABLE IF EXISTS user_vss;
DROP TABLE IF EXISTS user_vss_role;
GO
DROP TABLE IF EXISTS region_taxonomy;
DROP TABLE IF EXISTS situation_taxonomy;
DROP TABLE IF EXISTS service_taxonomy;
DROP TABLE IF EXISTS resource_application_type;
DROP TABLE IF EXISTS resource_language;
DROP TABLE IF EXISTS resource_contact;
GO
DROP TABLE IF EXISTS rms.region_taxonomy;
DROP TABLE IF EXISTS rms.situation_taxonomy;
DROP TABLE IF EXISTS rms.service_taxonomy;
DROP TABLE IF EXISTS rms.resource_application_type;
DROP TABLE IF EXISTS rms.resource_language;
DROP TABLE IF EXISTS rms.resource_contact;
GO
DROP TABLE IF EXISTS rms.resource_program;
DROP TABLE IF EXISTS rms.resource_program;
DROP TABLE IF EXISTS rms.resource_program_details;
DROP TABLE IF EXISTS rms.resource_process_time;
GO

DROP TABLE IF EXISTS rms.organization_address;
DROP TABLE IF EXISTS rms.organization;
GO
