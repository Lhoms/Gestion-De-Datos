USE [GD2C2016]
GO

CREATE SCHEMA [GD_CLINICA] AUTHORIZATION [gd]
GO

--Drop de tablas 
--(proximamente)



--Creación de tablas
CREATE TABLE GD_CLINICA.Usuario
(
		user_id				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		user_username 		varchar(255),
		user_pass	 		varchar(255),
		user_log_fallidos	tinyint,
		user_habilitado  	bit

	);


CREATE TABLE GD_CLINICA.Tipo_doc
(
		doc_id 		 numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		doc_descrip: varchar(255)

	);

CREATE TABLE GD_CLINICA.Persona
(
		pers_id 			numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		pers_nombre 		varchar(255),
		pers_apellido 		varchar(255),
		pers_tipo_doc		numeric(18,0),
		pers_doc 			numeric(18,0),
		pers_dire 			varchar(255),
		pers_tel			numeric(18,0),
		pers_mail 			varchar(255),
		pers_sexo 			char(1),
		pers_fecha_nac 		datetime,

		CONSTRAINT FK_tipo_doc FOREIGN KEY (pers_tipo_doc) REFERENCES GD_CLINICA.Tipo_doc (doc_id)
	);

