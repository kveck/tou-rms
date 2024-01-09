USE touResourceDatabase
GO

CREATE TABLE organization(
org_id	int	IDENTITY(1,1),
org_name varchar(256),
org_code char(6) NOT NULL,
next_resource_id int DEFAULT 1,
website_url varchar(2083),
email varchar(320),
phone char(10),
fax char(10),


CONSTRAINT pk_org_id PRIMARY KEY(org_id),
-- light weight check that phone is all nubmers, data layer will do specific check
CONSTRAINT valid_phone CHECK (phone like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_fax CHECK (fax like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_next_res_id CHECK (next_resource_id > 0)
);

GO

CREATE TABLE organization_address(
id int IDENTITY(1,1),
org_id int NOT NULL,
street1 varchar(256),
street2 varchar(256),
state_code char(2),
zip char(5),
zip_ext char(4),
country varchar(256),

CONSTRAINT pk_org_address_id PRIMARY KEY(id),
CONSTRAINT fk_organization_id FOREIGN KEY (org_id) REFERENCES organization(org_id),
-- light weight check that zip and zip extension are all nubmers, data layer will do specific check
CONSTRAINT valid_org_zip CHECK (zip like '[0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_org_zip_ext CHECK (zip_ext like '[0-9][0-9][0-9][0-9]')
);

GO

-- table contains all resources (programs) offered by an organization
CREATE TABLE resource_program (
id int IDENTITY(1,1),
resource_id char(10) NOT NULL,
org_id int NOT NULL,
name varchar(256),
resource_url varchar(2083),
description varchar(512),
cost varchar(128),
notes varchar(max),
status varchar(25) NOT NULL,
timestamp_last_update datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC
timestamp_created datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC

CONSTRAINT pk_resource_program PRIMARY KEY(id),
CONSTRAINT fk_resource_organization_id FOREIGN KEY (org_id) REFERENCES organization(org_id),
);

GO

CREATE TABLE resource_contact(
contact_id int IDENTITY(1,1),
title varchar(25),
first_name varchar(128),
middle_name varchar(128),
last_name varchar(128),
suffix varchar(25),
org_title varchar(128),
phone char(10),
phone_ext varchar(10),
mobile char(10),
fax char(10),
email varchar(320), 

CONSTRAINT pk_resource_contact PRIMARY KEY(contact_id),
CONSTRAINT valid_contact_phone CHECK (phone like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_contact_mobile CHECK (mobile like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
CONSTRAINT valid_contact_fax CHECK (fax like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
);
GO

-- Junction (cross-ref) table for resource and contact
-- allows for mapping a single contact to multiple resources
CREATE TABLE resource_contact_xref(
id int IDENTITY(1,1),
contact_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_contact_xref PRIMARY KEY(id),
CONSTRAINT fk_resource_id_contact FOREIGN KEY (contact_id) REFERENCES resource_contact(contact_id),
CONSTRAINT fk_contact_id_resource FOREIGN KEY (resource_id) REFERENCES resource_program(id),
-- constraint so each resource cannot have duplicate contacts
CONSTRAINT uq_resource_contact UNIQUE(contact_id, resource_id)
);
GO


CREATE TABLE resource_language(
language_id int IDENTITY(1,1),
language_name varchar(128) NOT NULL,

CONSTRAINT pk_resource_language PRIMARY KEY(language_id)
);
GO

INSERT INTO resource_language(language_name) VALUES ('English'),('Spanish'),('Russian'),('Korean');
GO

-- Junction (cross-ref) table for language and resource
CREATE TABLE resource_language_xref(
id int IDENTITY(1,1),
language_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_language_xref PRIMARY KEY(id),
CONSTRAINT fk_language_id FOREIGN KEY (language_id) REFERENCES resource_language(language_id),
CONSTRAINT fk_resource_id_language FOREIGN KEY (resource_id) REFERENCES resource_program(id),
-- constraint so each resource cannot have duplicate languages
CONSTRAINT uq_resource_language UNIQUE(language_id, resource_id)
);
GO

-- Junction (cross-ref) table for language and resource contact
CREATE TABLE contact_language_xref(
id int IDENTITY(1,1),
language_id int NOT NULL,
contact_id int NOT NULL,

CONSTRAINT pk_contact_language_xref PRIMARY KEY(id),
CONSTRAINT fk_language_id_contact FOREIGN KEY (language_id) REFERENCES resource_language(language_id),
CONSTRAINT fk_contact_id_language FOREIGN KEY (contact_id) REFERENCES resource_contact(contact_id),
-- constraint so each contact cannot have duplicate languages
CONSTRAINT uq_contact_language UNIQUE(language_id, contact_id)
);
GO


CREATE TABLE resource_application_type(
id int IDENTITY(1,1),
application_type varchar(128) NOT NULL,

CONSTRAINT pk_resource_application_type PRIMARY KEY(id)
);
GO

INSERT INTO resource_application_type(application_type) 
	VALUES 
	('In-Person'),
	('Phone'),
	('Online'),
	('Website');
GO

-- Junction (cross-ref) table for application type and resource
CREATE TABLE resource_application_type_xref(
id int IDENTITY(1,1),
application_type_id int NOT NULL,
resource_id int NOT NULL,

CONSTRAINT pk_resource_application_type_xref PRIMARY KEY(id),
CONSTRAINT fk_application_type_id FOREIGN KEY (application_type_id) REFERENCES resource_application_type(id),
CONSTRAINT fk_resource_program_id_application_type FOREIGN KEY (resource_id) REFERENCES resource_program(id),
-- constraint so each resource cannot have duplicate application types
CONSTRAINT uq_resource_application_type UNIQUE(application_type_id, resource_id)
);
GO
