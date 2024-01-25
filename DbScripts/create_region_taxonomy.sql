-- This file creates the region taxonomy TABLE rms.and the resource junction (cross-ref) table
-- Regions are areas available to the resources
-- values include nation-wide, state, city, county, zip

USE touResourceDatabase
GO

DROP TABLE IF EXISTS rms.region_taxononomy_resource_xref;
DROP TABLE IF EXISTS rms.region_taxonomy;
GO

CREATE TABLE rms.region_taxonomy(
taxonomy_id int IDENTITY(1,1),
region nvarchar(126) NOT NULL,
taxonomy_left int NOT NULL,
taxonomy_right int NOT NULL,

CONSTRAINT pk_region_taxonomy PRIMARY KEY(taxonomy_id),
CONSTRAINT uq_region_taxonomy_left UNIQUE(taxonomy_left),
CONSTRAINT uq_region_taxonomy_right UNIQUE(taxonomy_right),

);
GO

-- Junction (cross-ref) TABLE rms.for region taxonomy and resource
CREATE TABLE rms.region_taxonomy_resource_xref(
id int IDENTITY(1,1),
region_taxonomy_id int NOT NULL, 
resource_id int NOT NULL,

CONSTRAINT pk_regiontaxon_resource_xref PRIMARY KEY(id),
CONSTRAINT fk_region_taxon_id FOREIGN KEY (region_taxonomy_id) REFERENCES rms.region_taxonomy(taxonomy_id),
CONSTRAINT fk_resource_program_id_region_taxonomy FOREIGN KEY (resource_id) REFERENCES rms.resource_program(id)
);
GO

-- Junction (cross-ref) TABLE rms.for region taxonomy and resource contact
CREATE TABLE rms.region_taxonomy_contact_xref(
id int IDENTITY(1,1),
region_taxonomy_id int NOT NULL,
contact_id int NOT NULL,

CONSTRAINT pk_regiontaxon_contact_xref PRIMARY KEY(id),
CONSTRAINT fk_region_taxon_id_contact FOREIGN KEY (region_taxonomy_id) REFERENCES rms.region_taxonomy(taxonomy_id),
CONSTRAINT fk_contact_id_region FOREIGN KEY (contact_id) REFERENCES rms.resource_contact(id),
-- constraint so each contact cannot have duplicate regions
CONSTRAINT uq_contact_region UNIQUE(region_taxonomy_id, contact_id)
);
GO