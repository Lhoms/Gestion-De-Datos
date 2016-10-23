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
IF OBJECT_ID('NUL.Consulta', 'U') IS NOT NULL
		DROP TABLE NUL.Consulta;
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
IF OBJECT_ID('NUL.Cancelacion', 'U') IS NOT NULL
		DROP TABLE NUL.Cancelacion;
IF OBJECT_ID('NUL.Tipo_cancelacion', 'U') IS NOT NULL
		DROP TABLE NUL.Tipo_cancelacion;
IF OBJECT_ID('NUL.Bono', 'U') IS NOT NULL
		DROP TABLE NUL.Bono;
IF OBJECT_ID('NUL.Historial_plan_med', 'U') IS NOT NULL
		DROP TABLE NUL.Historial_plan_med;
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
IF OBJECT_ID('NUL.Usuario', 'U') IS NOT NULL
		DROP TABLE NUL.Usuario;
IF OBJECT_ID('NUL.Tipo_doc', 'U') IS NOT NULL
		DROP TABLE NUL.Tipo_doc;		


GO



--Creación de tablas
CREATE TABLE NUL.Tipo_doc
(
		doc_id 		 numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		doc_descrip  varchar(255)
	);

CREATE TABLE NUL.Usuario
(
		user_id				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		user_tipodoc		numeric(18,0),
		user_username 		varchar(255) NOT NULL,
		user_pass	 		varchar(255) NOT NULL,
		user_log_fallidos	tinyint DEFAULT 0,
		user_habilitado  	bit DEFAULT 1,

		CONSTRAINT FK_tipo_doc_usuario FOREIGN KEY (user_tipodoc) REFERENCES NUL.Tipo_doc(doc_id)
	);

CREATE TABLE NUL.Persona
(
		pers_id 			numeric(18,0) PRIMARY KEY,
		pers_nombre 		varchar(255) NOT NULL,
		pers_apellido 		varchar(255) NOT NULL,
		pers_tipo_doc		numeric(18,0) DEFAULT 1,
		pers_doc 			numeric(18,0) NOT NULL,
		pers_dire 			varchar(255),
		pers_tel			numeric(18,0),
		pers_mail 			varchar(255),
		pers_sexo 			char(1) DEFAULT 'M',
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
		rol_habilitado  	bit DEFAULT 1
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
		plan_id 				numeric(18,0) PRIMARY KEY,
		plan_descrip 			varchar(255),
		plan_precio_bono_cons	numeric(18,2),
		plan_precio_bono_farm   numeric(18,2), 										
	);

CREATE TABLE NUL.Afiliado
(
		afil_id 				numeric(18,0) PRIMARY KEY,
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

CREATE TABLE NUL.Historial_plan_med
(
		histo_plan_id			 numeric(18,0),
		histo_afil_id			 numeric(18,0),
		histo_fecha_id			 date,
		histo_descrip			 varchar(255),

		CONSTRAINT pk_plan_med PRIMARY KEY (histo_plan_id,histo_afil_id,histo_fecha_id),

		CONSTRAINT FK_histo_plan_id FOREIGN KEY (histo_plan_id) REFERENCES NUL.Plan_medico (plan_id),
		CONSTRAINT FK_histo_afil_id FOREIGN KEY (histo_afil_id) REFERENCES NUL.Afiliado (afil_id)
	);

CREATE TABLE NUL.Bono
(
		bono_id 				numeric(18,0) PRIMARY KEY,
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

CREATE TABLE NUL.Tipo_esp
(
		tipo_esp_id 			numeric(18,0) PRIMARY KEY,
		tipo_esp_descrip 		varchar(255)
	);

CREATE TABLE NUL.Especialidad
(
		esp_id 				numeric(18,0) PRIMARY KEY,
		esp_tipo			numeric(18,0),
		esp_descrip 		varchar(255),

		CONSTRAINT FK_especialidad_esp FOREIGN KEY (esp_tipo) REFERENCES NUL.Tipo_esp (tipo_esp_id)
	);

CREATE TABLE NUL.Profesional
(
		prof_id				numeric(18,0) PRIMARY KEY,
		prof_matric			numeric(18,0)
	);

CREATE TABLE NUL.Turno
(
		turno_id			 	numeric(18,0) PRIMARY KEY,
		turno_afiliado		 	numeric(18,0),
		turno_profesional	 	numeric(18,0),
		turno_especialidad	 	numeric(18,0),
		turno_fecha_hora	 	datetime,
		turno_llegada		 	time,

		CONSTRAINT FK_turno_afiliado 	 FOREIGN KEY (turno_afiliado) 	  REFERENCES NUL.Afiliado (afil_id),
		CONSTRAINT FK_turno_profesional  FOREIGN KEY (turno_profesional)  REFERENCES NUL.Profesional (prof_id),
		CONSTRAINT FK_turno_especialidad FOREIGN KEY (turno_especialidad) REFERENCES NUL.Especialidad (esp_id)
	);

CREATE TABLE NUL.Consulta
(
		cons_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		cons_turno_id			numeric(18,0),
		cons_bono_usado		 	numeric(18,0),
		cons_fecha_hora			datetime,
		cons_sintomas			varchar(255),
		cons_enfermedades		varchar(255),

		CONSTRAINT FK_bono_usado  FOREIGN KEY (cons_bono_usado) REFERENCES NUL.Bono (bono_id),
		CONSTRAINT FK_turno       FOREIGN KEY (cons_turno_id)   REFERENCES NUL.Turno (turno_id)
	);

CREATE TABLE NUL.Profesional_especialidad
(
		esp_id 				numeric(18,0),
		prof_id				numeric(18,0),

		CONSTRAINT pk_Profesional_esp PRIMARY KEY (esp_id,prof_id),

		CONSTRAINT FK_especialidad_pe FOREIGN KEY (esp_id) REFERENCES NUL.Especialidad (esp_id),
		CONSTRAINT FK_profesional_pe  FOREIGN KEY (prof_id)  REFERENCES NUL.Profesional (prof_id)
	);

CREATE TABLE NUL.Dia
(
		dia_id 					numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		dia_nombre				varchar(255)
	);

CREATE TABLE NUL.Agenda
(
		agenda_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		agenda_prof_id			numeric(18,0),
		agenda_prof_esp_id		numeric(18,0),
		agenda_disp_desde 		date,
		agenda_disp_hasta		date,

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




--Migración de datos
BEGIN TRANSACTION	

INSERT INTO NUL.Tipo_doc (doc_descrip) VALUES
			('DNI'),
			('PAS'),
			('CI'),
			('LC'),
			('LE');

INSERT INTO NUL.Usuario (user_tipodoc, user_username, user_pass)
(
	SELECT DISTINCT 1, M.Paciente_Dni, HASHBYTES('SHA2_256','w23e') 
	FROM gd_esquema.Maestra M
	WHERE M.Paciente_Mail IS NOT NULL
);

INSERT INTO NUL.Usuario (user_tipodoc, user_username, user_pass)
(
	SELECT DISTINCT 1, M.Medico_Dni, HASHBYTES('SHA2_256','w23e')
	FROM gd_esquema.Maestra M
	WHERE M.Medico_Mail IS NOT NULL
);

INSERT INTO NUL.Usuario (user_tipodoc, user_username, user_pass) VALUES
			(1, 'admin', HASHBYTES('SHA2_256','w23e'))


INSERT INTO NUL.Persona (pers_id ,pers_nombre, pers_apellido, pers_doc, pers_dire, pers_tel, pers_mail, pers_fecha_nac)
(
		SELECT DISTINCT M.Paciente_Dni,  M.Paciente_Nombre, M.Paciente_Apellido, M.Paciente_Dni, M.Paciente_Direccion, M.Paciente_Telefono,
						M.Paciente_Mail, M.Paciente_Fecha_Nac
		FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
		WHERE U.user_tipodoc  = 1
);		                                             



INSERT INTO NUL.Funcionalidad (func_descrip) VALUES
			('ABM Rol'),
			('Abm Afiliado'),
			('Compra de bonos'),
			('Pedir turno'),
			('Registro de llegada para atención médica '),
			('Registrar resultado para atención médica'),
			('Cancelar atención médica'),
			('Listado estadístico'),
			('Crear agenda');

INSERT INTO NUL.Rol (rol_descrip) VALUES
			('Administrativo'),
			('Afiliado'),
			('Profesional');



INSERT INTO NUL.Rol_funcionalidad(rol_id, func_id) VALUES
			(1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),
			(2,3),(2,4),(2,7),
			(3,5),(3,6),(3,7);


-- JM
INSERT INTO NUL.User_rol(rol_id, user_id)
(
	SELECT 1, user_id FROM NUL.Usuario 
	WHERE user_username = 'admin' );

INSERT INTO NUL.User_rol(rol_id, user_id)
(
	SELECT DISTINCT 2, user_id
	FROM gd_esquema.Maestra M
	JOIN NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
	                  AND U.user_tipodoc = 1

);

INSERT INTO NUL.User_rol(rol_id, user_id)
(
	SELECT DISTINCT 3, user_id
	FROM gd_esquema.Maestra M
	JOIN NUL.Usuario U ON CAST(M.Medico_Dni AS CHAR) = U.user_username
	                  AND U.user_tipodoc = 1
);

INSERT INTO NUL.Estado(estado_descrip) VALUES
		('Soltero'),
		('Casado'),
		('Viudo'),
		('Concubinato'),
		('Divorciado');

INSERT INTO NUL.Plan_medico(plan_id, plan_descrip, plan_precio_bono_cons, plan_precio_bono_farm)
( 
  SELECT DISTINCT M.Plan_Med_Codigo, M.Plan_Med_Descripcion, M.Plan_Med_Precio_Bono_Consulta, M.Plan_Med_Precio_Bono_Farmacia
    FROM gd_esquema.Maestra M
);

INSERT INTO NUL.Afiliado(afil_id, afil_estado, afil_plan_med, afil_nro_afiliado, afil_familiares, afil_nro_consulta)
(
	SELECT DISTINCT U.user_id, '1', M.Plan_Med_Codigo, (M.Paciente_Dni*100), '0', ISNULL((SELECT COUNT(M2.Bono_Consulta_Numero)
																							FROM gd_esquema.Maestra M2
																							WHERE M2.Paciente_Mail = M.Paciente_Mail
																							  AND M2.Bono_Consulta_Numero IS NOT NULL 
																							  AND M2.Turno_Numero IS NULL
																							GROUP BY M2.Paciente_Mail),0)
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												 AND U.user_tipodoc  = 1
);


INSERT INTO NUL.Bono_compra(bonoc_id_usuario, bonoc_fecha, bonoc_cantidad, bonoc_monto_total) 
(
	SELECT U.user_id, M.Compra_Bono_Fecha, COUNT(M.Bono_Consulta_Numero), (COUNT(*) * M.Plan_Med_Precio_Bono_Consulta) 
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
											     AND U.user_tipodoc = 1
	WHERE M.Bono_Consulta_Numero IS NOT NULL AND M.Turno_Numero IS NULL
	GROUP BY U.user_id, M.Compra_Bono_Fecha, M.Plan_Med_Precio_Bono_Consulta
	--HAVING M.Compra_Bono_Fecha IS NOT NULL
);

INSERT INTO NUL.Historial_plan_med(histo_plan_id, histo_afil_id, histo_fecha_id, histo_descrip)
(
	SELECT DISTINCT M.Plan_Med_Codigo, U.user_id, MIN(M.Turno_Fecha), 'Sin descripcion'
    FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												AND U.user_tipodoc  = 1
	GROUP BY M.Plan_Med_Codigo, U.user_id );

/*  */
INSERT INTO NUL.Bono(bono_id, bono_compra, bono_plan, bono_nro_consulta, bono_usado)
(
	SELECT DISTINCT M.Bono_Consulta_Numero, BC.bonoc_id, M.Plan_Med_Codigo, COUNT(BC.bonoc_id) + 1, 1
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												 AND U.user_tipodoc  = 1
	                          JOIN  NUL.Bono_compra BC ON U.user_id = BC.Bonoc_id_usuario
													  AND BC.bonoc_fecha = M.Compra_Bono_Fecha
	WHERE M.Bono_Consulta_Numero IS NOT NULL AND M.Turno_Numero IS NULL
	GROUP BY M.Bono_Consulta_Numero, BC.bonoc_id, M.Plan_Med_Codigo
);

INSERT INTO NUL.Tipo_cancelacion(tipo_cancel_detalle) VALUES
('Cancelada por el afiliado'),
('Cancelada por el médico');

/* Cancelación queda vacía por ahora */

INSERT INTO NUL.Tipo_esp(tipo_esp_id, tipo_esp_descrip)
(
	SELECT DISTINCT M.Tipo_Especialidad_Codigo, M.Tipo_Especialidad_Descripcion
			FROM gd_esquema.Maestra M
			WHERE M.Tipo_Especialidad_Codigo IS NOT NULL
			GROUP BY M.Tipo_Especialidad_Codigo, M.Tipo_Especialidad_Descripcion

);

INSERT INTO NUL.Especialidad(esp_id, esp_tipo, esp_descrip)
(
	SELECT DISTINCT M.Especialidad_Codigo, M.Tipo_Especialidad_Codigo, M.Especialidad_Descripcion
			FROM gd_esquema.Maestra M
			WHERE M.Especialidad_Codigo IS NOT NULL
);

INSERT INTO NUL.Profesional (prof_id, prof_matric)
(
	SELECT DISTINCT U.user_id, 0
	FROM gd_esquema.Maestra M JOIN NUL.Usuario U ON CAST(M.Medico_Dni AS CHAR) = U.user_username
											 AND U.user_tipodoc  = 1

);

INSERT INTO NUL.Profesional_Especialidad (esp_id, prof_id)
(
	SELECT DISTINCT M.Especialidad_Codigo, U.user_id
	FROM gd_esquema.Maestra M JOIN NUL.Usuario U ON CAST(M.Medico_Dni AS CHAR) = U.user_username
												AND U.user_tipodoc  = 1

);

INSERT INTO NUL.Turno(turno_id, turno_afiliado, turno_profesional, turno_especialidad, turno_fecha_hora, turno_llegada)
(
	SELECT DISTINCT M.Turno_Numero, U.user_id, U2.user_id, E.esp_id, M.Turno_Fecha, CAST(M.Turno_Fecha as time)
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U  ON CAST(M.Paciente_Dni AS CHAR)= U.user_username
												  AND U.user_tipodoc  = 1
							  JOIN  NUL.Usuario U2 ON CAST(M.Medico_Dni AS CHAR) = U2.user_username
												  AND U2.user_tipodoc  = 1
							  JOIN  NUL.Especialidad E ON M.Especialidad_Codigo = E.esp_id
);

INSERT INTO NUL.Consulta(cons_bono_usado, cons_turno_id, cons_fecha_hora, cons_sintomas, cons_enfermedades)
(
	SELECT DISTINCT M.Bono_Consulta_Numero, M.Turno_Numero, M.Turno_Fecha, M.Consulta_Sintomas, M.Consulta_Enfermedades
	FROM gd_esquema.Maestra M
	WHERE M.Consulta_Enfermedades  IS NOT NULL
	   OR M.Consulta_Sintomas  IS NOT NULL
);

INSERT INTO NUL.Dia(dia_nombre) VALUES
	('Lunes'),
	('Martes'),
	('Miércoles'),
	('Jueves'),
	('Viernes'),
	('Sábado'),
	('Domingo');

INSERT INTO NUL.Agenda(agenda_prof_id, agenda_prof_esp_id, agenda_disp_desde, agenda_disp_hasta)
(
	SELECT DISTINCT U.user_id, M.Especialidad_Codigo, MIN(CAST(M.Turno_Fecha as date)), MAX(CAST(M.Turno_Fecha as date))
	FROM gd_esquema.Maestra M JOIN NUL.Usuario U ON CAST(M.Medico_Dni AS CHAR) = U.user_username
												AND U.user_tipodoc  = 1
	GROUP BY U.user_id, M.Especialidad_Codigo
);

INSERT INTO NUL.Agenda_dia(dia_id, agenda_id, dia_hora_inicio, dia_hora_fin)
(
	SELECT DISTINCT DATEPART(dw, M.Turno_Fecha), A.agenda_id, MIN(CAST(M.Turno_Fecha as time)), MAX(CAST(M.Turno_Fecha as time))
	FROM gd_esquema.Maestra M JOIN NUL.Usuario U ON M.Medico_Dni = U.user_username
												AND U.user_tipodoc  = 1
							  JOIN NUL.Agenda  A ON A.agenda_prof_id = U.user_id
							                    AND A.agenda_prof_esp_id = M.Especialidad_Codigo
	GROUP BY DATEPART(dw, M.Turno_Fecha), A.agenda_id
	HAVING DATEPART(dw, M.Turno_Fecha) <> 7

);

COMMIT TRANSACTION


-- Re-enable constraints for all tables:
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'

GO


--stored procedures

CREATE PROCEDURE NUL.get_tipo_doc

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT doc_descrip FROM NUL.Tipo_doc

END
GO


CREATE PROCEDURE NUL.get_roles_disponibles

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT rol_descrip FROM NUL.Rol

END
GO

