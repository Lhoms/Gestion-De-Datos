USE GD2C2016
GO

--CREATE SCHEMA [NUL]
--GO

-- Disable constraints for all tables:
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'

-- Re-enable constraints for all tables:
--EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'


--Drop de tablas por si existen

IF OBJECT_ID('NUL.Agenda_dia', 'U') IS NOT NULL
		DROP TABLE NUL.Agenda_dia;
IF OBJECT_ID('NUL.Agenda', 'U') IS NOT NULL
		DROP TABLE NUL.Agenda;
IF OBJECT_ID('NUL.Dia', 'U') IS NOT NULL
		DROP TABLE NUL.Dia;
IF OBJECT_ID('NUL.Turno', 'U') IS NOT NULL
		DROP TABLE NUL.Turno;
IF OBJECT_ID('NUL.Profesional_especialidad', 'U') IS NOT NULL
		DROP TABLE NUL.Profesional_especialidad;
IF OBJECT_ID('NUL.Profesional', 'U') IS NOT NULL
		DROP TABLE NUL.Profesional;
IF OBJECT_ID('NUL.Especialidad', 'U') IS NOT NULL
		DROP TABLE NUL.Especialidad;
IF OBJECT_ID('NUL.Tipo_esp', 'U') IS NOT NULL
		DROP TABLE NUL.Tipo_esp;
IF OBJECT_ID('NUL.Consulta', 'U') IS NOT NULL
		DROP TABLE NUL.Consulta;
IF OBJECT_ID('NUL.Cancelacion', 'U') IS NOT NULL
		DROP TABLE NUL.Cancelacion;
IF OBJECT_ID('NUL.Tipo_cancelacion', 'U') IS NOT NULL
		DROP TABLE NUL.Tipo_cancelacion;
IF OBJECT_ID('NUL.Bono', 'U') IS NOT NULL
		DROP TABLE NUL.Bono;
IF OBJECT_ID('NUL.Bono_compra', 'U') IS NOT NULL
		DROP TABLE NUL.Bono_compra;
IF OBJECT_ID('NUL.Afiliado', 'U') IS NOT NULL
		DROP TABLE NUL.Afiliado;
IF OBJECT_ID('NUL.Plan_medico', 'U') IS NOT NULL
		DROP TABLE NUL.Plan_medico;
IF OBJECT_ID('NUL.Estado', 'U') IS NOT NULL
		DROP TABLE NUL.Estado;
IF OBJECT_ID('NUL.User_rol', 'U') IS NOT NULL
		DROP TABLE NUL.User_rol;
IF OBJECT_ID('NUL.Rol_funcionalidad', 'U') IS NOT NULL
		DROP TABLE NUL.Rol_funcionalidad;
IF OBJECT_ID('NUL.Rol', 'U') IS NOT NULL
		DROP TABLE NUL.Rol;
IF OBJECT_ID('NUL.Funcionalidad', 'U') IS NOT NULL
		DROP TABLE NUL.Funcionalidad;
IF OBJECT_ID('NUL.Persona', 'U') IS NOT NULL
		DROP TABLE NUL.Persona;
IF OBJECT_ID('NUL.Tipo_doc', 'U') IS NOT NULL
		DROP TABLE NUL.Tipo_doc;		
IF OBJECT_ID('NUL.Usuario', 'U') IS NOT NULL
		DROP TABLE NUL.Usuario;

GO



--Creación de tablas
CREATE TABLE NUL.Usuario
(
		user_id				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		user_username 		varchar(255),
		user_pass	 		varchar(255),
		user_log_fallidos	tinyint,
		user_habilitado  	bit
	);


CREATE TABLE NUL.Tipo_doc
(
		doc_id 		 numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		doc_descrip  varchar(255)
	);

CREATE TABLE NUL.Persona
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

		CONSTRAINT FK_tipo_doc FOREIGN KEY (pers_tipo_doc) REFERENCES NUL.Tipo_doc (doc_id)
	);

CREATE TABLE NUL.Funcionalidad
(
		func_id 			numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		func_descrip		varchar(255)
	);

CREATE TABLE NUL.Rol
(
		rol_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		rol_descrip			varchar(255),
		rol_habilitado  	bit
	);

CREATE TABLE NUL.Rol_funcionalidad
(
		rol_id 				numeric(18,0),
		func_id				numeric(18,0),


		CONSTRAINT pk_Rol_funcionalidad PRIMARY KEY (rol_id,func_id),

		CONSTRAINT FK_rol_id_rf 	FOREIGN KEY (rol_id) REFERENCES NUL.Rol (rol_id),
		CONSTRAINT FK_func_id_rf	FOREIGN KEY (func_id) REFERENCES NUL.Funcionalidad (func_id)
	);

CREATE TABLE NUL.User_rol
(
		rol_id 				numeric(18,0),
		user_id 			numeric(18,0),

		CONSTRAINT pk_User_rol PRIMARY KEY (rol_id,user_id),

		CONSTRAINT FK_rol_id_ur  FOREIGN KEY (rol_id)  REFERENCES NUL.Rol (rol_id),
		CONSTRAINT FK_user_id_ur FOREIGN KEY (user_id) REFERENCES NUL.Usuario (user_id)
	);

CREATE TABLE NUL.Estado
(
		estado_id 			numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		estado_descrip		varchar(255),
	);

CREATE TABLE NUL.Plan_medico
(
		plan_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		plan_descrip 			varchar(255),
		plan_precio_bono_cons	numeric(18,2),
		plan_precio_bono_farm   numeric(18,2),
	);

CREATE TABLE NUL.Afiliado
(
		afil_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		afil_estado 			numeric(18,0),
		afil_plan_med 			numeric(18,0),
		afil_nro_afiliado		numeric(18,0),
		afil_familiares 		tinyint,
		afil_nro_consulta 		numeric(18,0),

		CONSTRAINT FK_afiliado_estado FOREIGN KEY (afil_estado) REFERENCES NUL.Estado (estado_id),
		CONSTRAINT FK_afiliado_plan_med FOREIGN KEY (afil_plan_med) REFERENCES NUL.Plan_medico (plan_id)
	);

CREATE TABLE NUL.Bono_compra
(
		bonoc_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		bonoc_id_usuario  		numeric(18,0),
		bonoc_fecha		  		datetime,
		bonoc_cantidad  		numeric(18,0),
		bonoc_monto_total  		numeric(16,2),

		CONSTRAINT FK_bono_compra FOREIGN KEY (bonoc_id_usuario) REFERENCES NUL.Afiliado (afil_id)
	);

CREATE TABLE NUL.Bono
(
		bono_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		bono_compra 			numeric(18,0),
		bono_plan				numeric(18,0),
		bono_nro_consulta	    numeric(18,0),
		bono_usado			    bit,

		CONSTRAINT FK_bono_bono_compra FOREIGN KEY (bono_compra) REFERENCES NUL.Bono_compra (bonoc_id),
		CONSTRAINT FK_bono_plan FOREIGN KEY (bono_plan) REFERENCES NUL.Plan_medico (plan_id)
	);

CREATE TABLE NUL.Tipo_cancelacion
(
		tipo_cancel_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		tipo_cancel_detalle 		varchar(255)
	);

CREATE TABLE NUL.Cancelacion
(
		cancel_turno_id 			numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		cancel_tipo					numeric(18,0),
		cancel_detalle				varchar(255),

		CONSTRAINT FK_cancelacion_tipo FOREIGN KEY (cancel_tipo) REFERENCES NUL.Tipo_cancelacion (tipo_cancel_id)
	);

CREATE TABLE NUL.Consulta
(
		cons_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		cons_fecha_hora			datetime,
		cons_sintomas			varchar(255),
		cons_enfermedades		varchar(255),
	);

CREATE TABLE NUL.Tipo_esp
(
		tipo_esp_id 			numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		tipo_esp_descrip 		varchar(255)
	);

CREATE TABLE NUL.Especialidad
(
		esp_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		esp_tipo			numeric(18,0),
		esp_descrip 		varchar(255),

		CONSTRAINT FK_especialidad_esp FOREIGN KEY (esp_tipo) REFERENCES NUL.Tipo_esp (tipo_esp_id)
	);

CREATE TABLE NUL.Profesional
(
		prof_id				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		prof_matric			numeric(18,0)
	);

CREATE TABLE NUL.Profesional_especialidad
(
		esp_id 				numeric(18,0),
		prof_id				numeric(18,0),

		CONSTRAINT pk_Profesional_esp PRIMARY KEY (esp_id,prof_id),

		CONSTRAINT FK_especialidad_pe FOREIGN KEY (esp_id) REFERENCES NUL.Especialidad (esp_id),
		CONSTRAINT FK_profesional_pe  FOREIGN KEY (prof_id)  REFERENCES NUL.Profesional (prof_id)
	);

CREATE TABLE NUL.Turno
(
		turno_id			 	numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		turno_afiliado		 	numeric(18,0),
		turno_profesional	 	numeric(18,0),
		turno_especialidad	 	numeric(18,0),
		turno_bono_usado	 	numeric(18,0),
		turno_consulta		 	numeric(18,0),
		turno_fecha_hora	 	datetime,
		turno_llegada		 	time,

		CONSTRAINT FK_turno_afiliado 	 FOREIGN KEY (turno_afiliado) 	  REFERENCES NUL.Afiliado (afil_id),
		CONSTRAINT FK_turno_profesional  FOREIGN KEY (turno_profesional)  REFERENCES NUL.Profesional (prof_id),
		CONSTRAINT FK_turno_especialidad FOREIGN KEY (turno_especialidad) REFERENCES NUL.Especialidad (esp_id),
		CONSTRAINT FK_bono_usado 		 FOREIGN KEY (turno_bono_usado)	  REFERENCES NUL.Bono (bono_id),
		CONSTRAINT FK_consulta 			 FOREIGN KEY (turno_consulta) 	  REFERENCES NUL.Consulta (cons_id)
	);



CREATE TABLE NUL.Dia
(
		dia_id 					numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		plan_nombre				varchar(255)
	);

CREATE TABLE NUL.Agenda
(
		agenda_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		agenda_prof_id			numeric(18,0),
		agenda_disp_desde 		date,
		agenda_desp_hasta		date,

		CONSTRAINT FK_agenda_profesional FOREIGN KEY (agenda_prof_id) REFERENCES NUL.Profesional (prof_id)
	);

CREATE TABLE NUL.Agenda_dia
(
		dia_id 					numeric(18,0),
		agenda_id				numeric(18,0),
		dia_hora_inicio 		time,
		dia_hora_fin 			time,

		CONSTRAINT pk_agenda_dia PRIMARY KEY (dia_id,agenda_id),

		CONSTRAINT FK_dia_id FOREIGN KEY (dia_id) REFERENCES NUL.Dia (dia_id),
		CONSTRAINT FK_agenda_id  FOREIGN KEY (agenda_id)  REFERENCES NUL.Agenda (agenda_id)
	);

GO

-- Re-enable constraints for all tables:
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'

GO