USE touResourceDatabase
GO

CREATE TABLE rms.organization(
org_id	int	IDENTITY(1,1),
org_name nvarchar(512),
website_url nvarchar(2083),
email nvarchar(320),
phone char(10),
fax char(10),


CONSTRAINT pk_org_id PRIMARY KEY(org_id),
-- light weight check that phone is all nubmers, data layer will do specific check
CONSTRAINT valid_phone CHECK (phone like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_fax CHECK (fax like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
);

GO

CREATE TABLE rms.organization_address(
id int IDENTITY(1,1),
org_id int NOT NULL,
street1 nvarchar(512),
street2 nvarchar(512),
state_code char(2),
zip char(5),
zip_ext char(4),
country nvarchar(256),

CONSTRAINT pk_org_address_id PRIMARY KEY(id),
CONSTRAINT fk_organization_id FOREIGN KEY (org_id) REFERENCES rms.organization(org_id),
-- light weight check that zip and zip extension are all nubmers, data layer will do specific check
CONSTRAINT valid_org_zip CHECK (zip like '[0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_org_zip_ext CHECK (zip_ext like '[0-9][0-9][0-9][0-9]')
);

GO

-- table contains resource process time categories
CREATE TABLE rms.resource_process_time(
id int IDENTITY(1,1),
time_category nvarchar(50) NOT NULL,

CONSTRAINT pk_resource_process_id PRIMARY KEY(id),
CONSTRAINT uq_time_category UNIQUE(time_category)
);
GO

INSERT INTO rms.resource_process_time(time_category) 
	VALUES 
	('Immediate'),
	('24 hours'),
	('48 hours'),
	('1 Week (7 Days/5 Working Days)'),
	('1 Month'),
	('More than a month');
GO

-- table contains resource status categories
CREATE TABLE rms.resource_status(
id int IDENTITY(1,1),
status nvarchar(50) NOT NULL,

CONSTRAINT pk_resource_status_id PRIMARY KEY(id),
CONSTRAINT uq_status UNIQUE(status)
);
GO

INSERT INTO rms.resource_status(status) 
	VALUES 
	('Verified'),
	('Deleted'),
	('Pending');
GO

-- table contains the resource (program) description
-- stored in a seperate table because it needs to use a nvarchar(max) field and does not need to be queried
CREATE TABLE rms.resource_program_description(
id int IDENTITY(1,1),
resource_detail_id INT NOT NULL,
description nvarchar(max),
CONSTRAINT pk_resource_program_description PRIMARY KEY(id),
CONSTRAINT uq_resource_id_description UNIQUE(resource_detail_id)
);
GO

-- table contains the resource (program) internal notes by CR
-- stored in a seperate table because it needs to use a nvarchar(max) field and does not need to be queried
CREATE TABLE rms.resource_program_note(
id int IDENTITY(1,1),
resource_detail_id INT NOT NULL,
internal_notes nvarchar(max),
CONSTRAINT pk_resource_program_notes PRIMARY KEY(id),
CONSTRAINT uq_resource_detail_id_notes UNIQUE(resource_detail_id)
);
GO

-- table contains the resource (program) internal notes by CR
-- stored in a seperate table because it needs to use a nvarchar(max) field and does not need to be queried
CREATE TABLE rms.resource_program_steps(
id int IDENTITY(1,1),
resource_detail_id INT NOT NULL,
process_steps nvarchar(max),
CONSTRAINT pk_resource_program_steps PRIMARY KEY(id),
CONSTRAINT uq_resource_id_steps UNIQUE(resource_detail_id)
);
GO


-- table contains the resource (program) details
CREATE TABLE rms.resource_program_detail (
id int IDENTITY(1,1),
resource_id int NOT NULL,
process_time_id int,
description_id int,
process_steps_id int,
internal_notes_id int,
cost nvarchar(128),
customer_service_rating tinyint, 
obtainability_rating tinyint, 

CONSTRAINT pk_resource_program_detail PRIMARY KEY(id),
CONSTRAINT uq_resource_id_detail UNIQUE(resource_id),
CONSTRAINT uq_process_time_id_detail UNIQUE(process_time_id),
CONSTRAINT uq_resource_description_id_detail UNIQUE(description_id),
CONSTRAINT uq_resource_steps_id_detail UNIQUE(process_steps_id),
CONSTRAINT uq_resource_notes_id_detail UNIQUE(internal_notes_id),
);
GO

-- table contains all resources (programs) offered by an organization
CREATE TABLE rms.resource_program (
id int IDENTITY(10000,1),
resource_code INT NOT NULL,
org_id int NOT NULL,
detail_id int NOT NULL, 
name nvarchar(256),
resource_url nvarchar(3000),
status_id int NOT NULL,
timestamp_last_update datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC
timestamp_created datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC

CONSTRAINT pk_resource_program PRIMARY KEY(id),
CONSTRAINT fk_resource_organization_id FOREIGN KEY (org_id) REFERENCES rms.organization(org_id),
CONSTRAINT uq_org_id UNIQUE(org_id),
CONSTRAINT fk_resource_detial_id FOREIGN KEY (detail_id) REFERENCES rms.resource_program_detail(id),
CONSTRAINT uq_detail_id UNIQUE(detail_id),
CONSTRAINT fk_resource_status_id FOREIGN KEY (status_id) REFERENCES rms.resource_status(id),
CONSTRAINT uq_status_id_resource UNIQUE(status_id),
CONSTRAINT uq_resource_code UNIQUE(resource_code)
);
GO

ALTER TABLE rms.resource_program_description
ADD CONSTRAINT fk_resource_id_description FOREIGN KEY (resource_detail_id) REFERENCES rms.resource_program_detail(id);
GO

ALTER TABLE rms.resource_program_note
ADD CONSTRAINT fk_resource_id_notes FOREIGN KEY (resource_detail_id) REFERENCES rms.resource_program_detail(id);
GO

ALTER TABLE rms.resource_program_steps
ADD CONSTRAINT fk_resource_id_steps FOREIGN KEY (resource_detail_id) REFERENCES rms.resource_program_detail(id);
GO

ALTER TABLE rms.resource_program_detail
ADD CONSTRAINT fk_resource_id_detail FOREIGN KEY (resource_id) REFERENCES rms.resource_program(id);

ALTER TABLE rms.resource_program_detail
ADD CONSTRAINT fk_resource_process_time_id_detail FOREIGN KEY (process_time_id) REFERENCES rms.resource_process_time(id);

ALTER TABLE rms.resource_program_detail
ADD CONSTRAINT fk_resource_description_id_detail  FOREIGN KEY (description_id) REFERENCES rms.resource_program_description(id);

ALTER TABLE rms.resource_program_detail
ADD CONSTRAINT fk_resource_steps_id_detail  FOREIGN KEY (process_steps_id) REFERENCES rms.resource_program_steps(id);

ALTER TABLE rms.resource_program_detail
ADD CONSTRAINT fk_internal_notes_id_detail  FOREIGN KEY (internal_notes_id) REFERENCES rms.resource_program_note(id);
GO

CREATE TABLE rms.resource_contact(
contact_id int IDENTITY(1,1),
title nvarchar(25),
first_name nvarchar(128),
middle_name nvarchar(128),
last_name nvarchar(128),
suffix nvarchar(25),
org_title nvarchar(128),
phone char(10),
phone_ext nvarchar(10),
mobile char(10),
fax char(10),
email nvarchar(320), 

CONSTRAINT pk_resource_contact PRIMARY KEY(contact_id),
CONSTRAINT valid_contact_phone CHECK (phone like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_contact_mobile CHECK (mobile like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_contact_fax CHECK (fax like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
);
GO

-- Junction (cross-ref) table for resource and contact
-- allows for mapping a single contact to multiple resources
CREATE TABLE rms.resource_contact_xref(
id int IDENTITY(1,1),
contact_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_contact_xref PRIMARY KEY(id),
CONSTRAINT fk_resource_id_contact FOREIGN KEY (contact_id) REFERENCES rms.resource_contact(contact_id),
CONSTRAINT fk_contact_id_resource FOREIGN KEY (resource_id) REFERENCES rms.resource_program(id),
-- constraint so each resource cannot have duplicate contacts
CONSTRAINT uq_resource_contact UNIQUE(contact_id, resource_id)
);
GO


CREATE TABLE rms.resource_language(
language_id int IDENTITY(1,1),
language_name nvarchar(128) NOT NULL,

CONSTRAINT pk_resource_language PRIMARY KEY(language_id)
);
GO

INSERT INTO rms.resource_language(language_name) VALUES ('English'),('Spanish'),('Russian'),('Korean');
GO

-- Junction (cross-ref) table for language and resource
CREATE TABLE rms.resource_language_xref(
id int IDENTITY(1,1),
language_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_language_xref PRIMARY KEY(id),
CONSTRAINT fk_language_id FOREIGN KEY (language_id) REFERENCES rms.resource_language(language_id),
CONSTRAINT fk_resource_id_language FOREIGN KEY (resource_id) REFERENCES rms.resource_program(id),
-- constraint so each resource cannot have duplicate languages
CONSTRAINT uq_resource_language UNIQUE(language_id, resource_id)
);
GO

-- Junction (cross-ref) table for language and resource contact
CREATE TABLE rms.contact_language_xref(
id int IDENTITY(1,1),
language_id int NOT NULL,
contact_id int NOT NULL,

CONSTRAINT pk_contact_language_xref PRIMARY KEY(id),
CONSTRAINT fk_language_id_contact FOREIGN KEY (language_id) REFERENCES rms.resource_language(language_id),
CONSTRAINT fk_contact_id_language FOREIGN KEY (contact_id) REFERENCES rms.resource_contact(contact_id),
-- constraint so each contact cannot have duplicate languages
CONSTRAINT uq_contact_language UNIQUE(language_id, contact_id)
);
GO


CREATE TABLE rms.resource_application_type(
id int IDENTITY(1,1),
application_type nvarchar(128) NOT NULL,

CONSTRAINT pk_resource_application_type PRIMARY KEY(id)
);
GO

INSERT INTO rms.resource_application_type(application_type) 
	VALUES 
	('In-Person'),
	('Phone'),
	('Online'),
	('Website');
GO

-- Junction (cross-ref) table for application type and resource
CREATE TABLE rms.resource_application_type_xref(
id int IDENTITY(1,1),
application_type_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_application_type_xref PRIMARY KEY(id),
CONSTRAINT fk_application_type_id FOREIGN KEY (application_type_id) REFERENCES rms.resource_application_type(id),
CONSTRAINT fk_resource_program_id_application_type FOREIGN KEY (resource_id) REFERENCES rms.resource_program(id),
-- constraint so each resource cannot have duplicate application types
CONSTRAINT uq_resource_application_type UNIQUE(application_type_id, resource_id)
);
GO