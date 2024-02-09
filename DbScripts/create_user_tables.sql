USE touResourceDatabase
GO

CREATE TABLE [rms].user_vss_role(
id	int	IDENTITY(1,1),
user_role nvarchar(25) NOT NULL

CONSTRAINT pk_user_role_id PRIMARY KEY(id),
CONSTRAINT uq_role UNIQUE(user_role)
);

GO

INSERT INTO [rms].user_vss_role (user_role) VALUES
('Admin'),
('TOU User'),
('Community Responder'),
('Resource User'),
('Developer'),
('Test');
GO

CREATE TABLE [rms].user_vss(
id	int	IDENTITY(1,1),
resource_user_id nvarchar(256) NOT NULL, -- Unique, Stores salt value for hashing password, UUID
role_id int NOT NULL,
username nvarchar(25) NOT NULL,
timestamp_last_update datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC
timestamp_created datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC

CONSTRAINT pk_user_id PRIMARY KEY(id),
CONSTRAINT fk_role_id FOREIGN KEY(role_id) REFERENCES [rms].user_vss_role(id),
CONSTRAINT uq_user_id UNIQUE(resource_user_id),
CONSTRAINT uq_username UNIQUE(username)
);

GO

CREATE TABLE [rms].user_detail(
id	int	IDENTITY(1,1),
resource_user_id int NOT NULL,
first_name nvarchar(50),
last_name nvarchar(50),
email nvarchar(320) NOT NULL,
phone char(10),
photo_location nvarchar(250), --URL of location in Azure blob storage

CONSTRAINT pk_user_details_id PRIMARY KEY(id),
CONSTRAINT fk_user_id_details FOREIGN KEY(resource_user_id) REFERENCES [rms].user_vss(id),
CONSTRAINT uq_detail_user_id UNIQUE(resource_user_id),
-- light weight check that phone is all nubmers, data layer will do specific check
CONSTRAINT valid_user_phone CHECK (phone like '[2-9][0-9][0-9][1-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

GO

CREATE TABLE [rms].user_security(
id	int	IDENTITY(1,1),
resource_user_id int NOT NULL, -- Maps to the resource_user.id field. I want to avoid the salt in the same table as the password
password binary(576) NOT NULL, -- Salted hashed (SHA-256) password - salt value comes from resource_user.user_id field.
timestamp_last_login datetime2(3) DEFAULT(getutcdate()) NOT NULL, -- stored value is UTC

CONSTRAINT pk_user_security_id PRIMARY KEY(id),
CONSTRAINT fk_user_id_security FOREIGN KEY(resource_user_id) REFERENCES [rms].user_vss(id),
CONSTRAINT uq_user_id_security UNIQUE(resource_user_id)
);
GO


-- used to track user 'favorites' 
CREATE TABLE [rms].user_resource_favorites(
id	int	IDENTITY(1,1),
resource_user_id int NOT NULL,
resource_id int NOT NULL,
notes nvarchar(512)

CONSTRAINT pk_user_resource_favorites_id PRIMARY KEY(id),
CONSTRAINT fk_user_id_resource_favorites FOREIGN KEY(resource_user_id) REFERENCES [rms].user_vss(id),
CONSTRAINT fk_resource_id_favorites FOREIGN KEY (resource_id) REFERENCES [rms].resource_program(id),
-- constraint so each user cannot have duplicate resource favorites
CONSTRAINT uq_user_resource_favorites UNIQUE(resource_user_id, resource_id)
);
GO

-- used to track user ratings for resources
CREATE TABLE [rms].user_resource_rating(
id	int	IDENTITY(1,1),
resource_user_id int NOT NULL,
resource_id int NOT NULL,
rating tinyint NOT NULL,
details nvarchar(512),
timestamp_last_update datetime2(3) DEFAULT(getutcdate()), -- stored value is UTC
timestamp_created datetime2(3) DEFAULT(getutcdate()), -- stored value is UTC

CONSTRAINT pk_user_resource_xref_id PRIMARY KEY(id),
CONSTRAINT fk_user_id_resource_rating FOREIGN KEY(resource_user_id) REFERENCES [rms].user_vss(id),
CONSTRAINT fk_resource_id_user FOREIGN KEY (resource_id) REFERENCES [rms].resource_program(id),
-- constraint so each user cannot have duplicate ratings for each resource
CONSTRAINT uq_user_resource_rating UNIQUE(resource_user_id, resource_id),
CONSTRAINT valid_rating CHECK (rating >=0 AND rating <=5 )
);
GO
