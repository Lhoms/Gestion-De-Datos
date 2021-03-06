USE GD2C2016
GO

CREATE SCHEMA [NUL]
GO

--Drop de tablas
BEGIN TRANSACTION

IF OBJECT_ID('NUL.Dias_cancelados', 'U') IS NOT NULL
		DROP TABLE NUL.Dias_cancelados;
IF OBJECT_ID('NUL.Historial_baja', 'U') IS NOT NULL
		DROP TABLE NUL.Historial_baja;
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
		afil_nro_afiliado		numeric(18,0) UNIQUE,
		afil_familiares 		tinyint DEFAULT 0,
		afil_nro_consulta 		numeric(18,0) DEFAULT 0,
		afil_titular			numeric(18,0) DEFAULT NULL,

		CONSTRAINT FK_afiliado_raiz FOREIGN KEY (afil_titular) REFERENCES NUL.Afiliado (afil_id),
		CONSTRAINT FK_afiliado_estado FOREIGN KEY (afil_estado) REFERENCES NUL.Estado (estado_id),
		CONSTRAINT FK_afiliado_plan_med FOREIGN KEY (afil_plan_med) REFERENCES NUL.Plan_medico (plan_id)
	);

CREATE TABLE NUL.Bono_compra
(
		bonoc_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		bonoc_id_usuario  		numeric(18,0),
		bonoc_fecha		  		datetime,
		bonoc_fecha_impresion   datetime,
		bonoc_cantidad  		numeric(18,0),
		bonoc_monto_total  		numeric(16,2),

		CONSTRAINT FK_bono_compra FOREIGN KEY (bonoc_id_usuario) REFERENCES NUL.Afiliado (afil_id)
	);

CREATE TABLE NUL.Historial_plan_med
(
		histo_id 				 numeric(18,0) IDENTITY(1,1),
		histo_plan_id			 numeric(18,0),
		histo_afil_id			 numeric(18,0),
		histo_fecha_id			 date,
		histo_descrip			 varchar(255),

		CONSTRAINT pk_plan_med PRIMARY KEY (histo_id,histo_plan_id,histo_afil_id,histo_fecha_id),

		CONSTRAINT FK_histo_plan_id FOREIGN KEY (histo_plan_id) REFERENCES NUL.Plan_medico (plan_id),
		CONSTRAINT FK_histo_afil_id FOREIGN KEY (histo_afil_id) REFERENCES NUL.Afiliado (afil_id)
	);

CREATE TABLE NUL.Bono
(
		bono_id 				numeric(18,0) PRIMARY KEY,	--no es identity porque ya vienen valores y no desde 1,
		bono_compra 			numeric(18,0),				--asique se va a manejar con un max(bono_id) + 1;
		bono_plan				numeric(18,0),
		bono_nro_consulta	    numeric(18,0),
		bono_usado			    bit,
		bono_comprador			numeric(18,0),

		CONSTRAINT FK_bono_bono_compra FOREIGN KEY (bono_compra) REFERENCES NUL.Bono_compra (bonoc_id),
		CONSTRAINT FK_bono_plan FOREIGN KEY (bono_plan) REFERENCES NUL.Plan_medico (plan_id),
		CONSTRAINT FK_bono_comprador  FOREIGN KEY (bono_comprador) REFERENCES NUL.Afiliado (afil_id)
	);

CREATE TABLE NUL.Tipo_cancelacion
(
		tipo_cancel_id 				numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
		tipo_cancel_detalle 		varchar(255)
	);

CREATE TABLE NUL.Cancelacion
(
		cancel_turno_id 			numeric(18,0) PRIMARY KEY,
		cancel_tipo					numeric(18,0),
		cancel_detalle				varchar(255),
		cancel_fecha				datetime,

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


CREATE TABLE NUL.Historial_baja
(
		baja_user_id 			numeric(18,0),
		baja_fecha_id			DateTime,

		CONSTRAINT pk_histo_baja PRIMARY KEY (baja_user_id,baja_fecha_id),
		CONSTRAINT FK_user_baja FOREIGN KEY (baja_user_id) REFERENCES NUL.Usuario (user_id)
	);     

CREATE TABLE NUL.Dias_cancelados
(
		canceld_id			    numeric(18,0) IDENTITY(1,1),
		canceld_prof			numeric(18,0),
		canceld_dia				date,

		CONSTRAINT pk_dias_cancel PRIMARY KEY (canceld_id,canceld_prof,canceld_dia),
		CONSTRAINT FK_dc_profesional FOREIGN KEY (canceld_prof) REFERENCES NUL.Profesional (prof_id)
	);   	                   
GO
COMMIT TRANSACTION

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
		SELECT DISTINCT U.user_id,  M.Paciente_Nombre, M.Paciente_Apellido, M.Paciente_Dni, M.Paciente_Direccion, M.Paciente_Telefono,
						M.Paciente_Mail, M.Paciente_Fecha_Nac
		FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
		WHERE U.user_tipodoc  = 1
);

INSERT INTO NUL.Persona (pers_id ,pers_nombre, pers_apellido, pers_doc, pers_dire, pers_tel, pers_mail, pers_fecha_nac)
(
		SELECT DISTINCT U.user_id,  M.Medico_Nombre, M.Medico_Apellido, M.Medico_Dni, M.Medico_Direccion, M.Medico_Telefono,
						M.Medico_Mail, M.Medico_Fecha_Nac
		FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Medico_Dni AS CHAR) = U.user_username
		WHERE U.user_tipodoc  = 1
);				                                             



INSERT INTO NUL.Funcionalidad (func_descrip) VALUES
			('ABM Rol'),					--1
			('Abm Afiliado'),				--2
			('Compra de bonos'),			--3
			('Pedir turno'),				--4
			('Registro de llegada para atención médica '), --5
			('Registrar resultado para atención médica'),  --6
			('Cancelar atención médica'),	--7
			('Listado estadístico'),		--8
			('Crear agenda');				--9

INSERT INTO NUL.Rol (rol_descrip) VALUES
			('Administrativo'),		--1
			('Afiliado'),			--2
			('Profesional');		--3



INSERT INTO NUL.Rol_funcionalidad(rol_id, func_id) VALUES
			(1,1),(1,2),(1,3),(1,4),(1,5),(1,8),
			(2,3),(2,4),(2,7),
			(3,6),(3,7),(3,9);

 
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
	SELECT DISTINCT U.user_id, '1', M.Plan_Med_Codigo, (M.Paciente_Dni*10+1)*100+1, '0', ISNULL((SELECT COUNT(M2.Bono_Consulta_Numero)
																							FROM gd_esquema.Maestra M2
																							WHERE M2.Paciente_Dni = M.Paciente_Dni
																							  AND M2.Bono_Consulta_Numero IS NOT NULL 
																							  AND M2.Turno_Numero IS NULL
																							GROUP BY M2.Paciente_Dni),0)
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												 AND U.user_tipodoc  = 1
);


INSERT INTO NUL.Bono_compra(bonoc_id_usuario, bonoc_fecha, bonoc_fecha_impresion, bonoc_cantidad, bonoc_monto_total) 
(
	SELECT U.user_id, M.Compra_Bono_Fecha, M.Bono_Consulta_Fecha_Impresion, COUNT(M.Bono_Consulta_Numero), (COUNT(*) * M.Plan_Med_Precio_Bono_Consulta) 
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
											     AND U.user_tipodoc = 1
	WHERE M.Bono_Consulta_Numero IS NOT NULL AND M.Turno_Numero IS NULL
	GROUP BY U.user_id, M.Compra_Bono_Fecha, M.Bono_Consulta_Fecha_Impresion, M.Plan_Med_Precio_Bono_Consulta
);

INSERT INTO NUL.Historial_plan_med(histo_plan_id, histo_afil_id, histo_fecha_id, histo_descrip)
(
	SELECT DISTINCT M.Plan_Med_Codigo, U.user_id, MIN(M.Turno_Fecha), 'Sin descripcion'
    FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												AND U.user_tipodoc  = 1
	GROUP BY M.Plan_Med_Codigo, U.user_id );

/*  */
INSERT INTO NUL.Bono(bono_id, bono_compra, bono_plan, bono_nro_consulta, bono_usado, bono_comprador)
(
	SELECT DISTINCT M.Bono_Consulta_Numero, BC.bonoc_id, M.Plan_Med_Codigo, 0, 1, U.user_id
	FROM gd_esquema.Maestra M JOIN  NUL.Usuario U ON CAST(M.Paciente_Dni AS CHAR) = U.user_username
												 AND U.user_tipodoc  = 1
	                         JOIN  NUL.Bono_compra BC ON U.user_id = BC.Bonoc_id_usuario
													  AND BC.bonoc_fecha = M.Compra_Bono_Fecha
	WHERE M.Bono_Consulta_Numero IS NOT NULL AND M.Turno_Numero IS NULL
	GROUP BY M.Bono_Consulta_Numero, BC.bonoc_id, M.Plan_Med_Codigo, U.user_id
);

--Seteo el numero de bono correlativamente por afiliado

UPDATE B1
SET bono_nro_consulta = (SELECT COUNT(*) FROM NUL.Bono B2
						 WHERE B1.bono_comprador = B2.bono_comprador
									  AND B1.bono_id <= B2.bono_id)
FROM NUL.Bono B1

INSERT INTO NUL.Tipo_cancelacion(tipo_cancel_detalle) VALUES
('Cancelada por el afiliado'),
('Cancelada por el médico'),
('Usuario dado de baja');

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
	SELECT DISTINCT DATEPART(dw, M.Turno_Fecha), A.agenda_id, MIN(CAST(M.Turno_Fecha as time)), dateadd(MI, 30, MAX(CAST(M.Turno_Fecha as time)))
	FROM gd_esquema.Maestra M JOIN NUL.Usuario U ON M.Medico_Dni = U.user_username
												AND U.user_tipodoc  = 1
							  JOIN NUL.Agenda  A ON A.agenda_prof_id = U.user_id
							                    AND A.agenda_prof_esp_id = M.Especialidad_Codigo
	GROUP BY DATEPART(dw, M.Turno_Fecha), A.agenda_id
	--HAVING DATEPART(dw, M.Turno_Fecha) <> 7

);

COMMIT TRANSACTION


BEGIN TRANSACTION

--Drop y creacion de Vistas

IF OBJECT_ID ('NUL.v_esp_canceladas', 'V') IS NOT NULL  
	DROP VIEW NUL.v_esp_canceladas ; 
GO
CREATE VIEW NUL.v_esp_canceladas(esp_id, esp_descrip, tipo_esp_id, tipo_esp_descrip, cant, anio, semestre, mes)
AS
	SELECT E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, COUNT(C.cancel_turno_id) AS cant, YEAR(C.cancel_fecha) as anio,
			 CASE WHEN MONTH(C.cancel_fecha) < 7 THEN 1 ELSE 2 END  as semestre, CASE WHEN MONTH(C.cancel_fecha) < 7 THEN MONTH(C.cancel_fecha) ELSE MONTH(C.cancel_fecha)-6 END  as mes
	  FROM NUL.Especialidad E JOIN NUL.Tipo_esp TE ON TE.tipo_esp_id = E.esp_tipo
							  JOIN NUL.Turno	T  ON T.turno_especialidad = E.esp_id
							  JOIN NUL.Cancelacion C ON C.cancel_turno_id = T.turno_id
	GROUP BY E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, YEAR(C.cancel_fecha), CASE WHEN MONTH(C.cancel_fecha) < 7 THEN 1 ELSE 2 END, MONTH(C.cancel_fecha),
			CASE WHEN MONTH(C.cancel_fecha) < 7 THEN MONTH(C.cancel_fecha) ELSE MONTH(C.cancel_fecha)-6 END
GO
--------------------------------------
IF OBJECT_ID ('NUL.v_prof_consultados', 'V') IS NOT NULL  
	DROP VIEW NUL.v_prof_consultados ; 
GO
CREATE VIEW NUL.v_prof_consultados(fecha,prof_id, pers_nombre, pers_apellido, pers_tipo_doc, pers_doc, esp_id, esp_descrip, tipo_esp_id, tipo_esp_descrip, plan_id, plan_descrip, cant)
AS
	SELECT DATEFROMPARTS(YEAR(C.cons_fecha_hora),MONTH(C.cons_fecha_hora),DAY(C.cons_fecha_hora)) as fecha,P.prof_id, PER.pers_nombre, PER.pers_apellido, PER.pers_tipo_doc, PER.pers_doc, E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, PM.plan_id, PM.plan_descrip, COUNT(C.cons_turno_id) AS cant 
	  FROM NUL.Profesional P JOIN NUL.Profesional_especialidad PE ON PE.prof_id = P.prof_id
							 JOIN NUL.Persona PER ON PER.pers_id = P.prof_id
							 JOIN NUL.Especialidad E ON E.esp_id = PE.esp_id
							 JOIN NUL.Tipo_esp    TE ON TE.tipo_esp_id = E.esp_tipo
							 JOIN NUL.Turno	T  ON T.turno_profesional = P.prof_id
							                  AND T.turno_especialidad = PE.esp_id
							 JOIN NUL.Consulta C ON C.cons_turno_id = T.turno_id
							 JOIN NUL.Bono     B ON B.bono_id = C.cons_bono_usado
							 JOIN NUL.Plan_medico PM ON PM.plan_id = B.bono_plan
	GROUP BY DATEFROMPARTS(YEAR(C.cons_fecha_hora),MONTH(C.cons_fecha_hora),DAY(C.cons_fecha_hora)), P.prof_id, PER.pers_nombre, PER.pers_apellido, PER.pers_tipo_doc, PER.pers_doc, E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, PM.plan_id, PM.plan_descrip
GO
----------------------------------
IF OBJECT_ID ('NUL.v_prof_horas', 'V') IS NOT NULL  
	DROP VIEW NUL.v_prof_horas ; 
GO
CREATE VIEW NUL.v_prof_horas(anio,mes, prof_id, pers_nombre, pers_apellido, pers_tipo_doc, pers_doc, esp_id, esp_descrip, tipo_esp_id, tipo_esp_descrip, plan_id, plan_descrip, cant)
AS
	SELECT YEAR(T.turno_fecha_hora),MONTH(T.turno_fecha_hora),P.prof_id, PER.pers_nombre, PER.pers_apellido, PER.pers_tipo_doc, PER.pers_doc, E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, PM.plan_id, 
		PM.plan_descrip,  COUNT(T.turno_id)*0.5 AS cant 
	  FROM NUL.Profesional P JOIN NUL.Profesional_especialidad PE ON PE.prof_id = P.prof_id
							 JOIN NUL.Persona PER ON PER.pers_id = P.prof_id
							 JOIN NUL.Especialidad E ON E.esp_id = PE.esp_id
							 JOIN NUL.Tipo_esp    TE ON TE.tipo_esp_id = E.esp_tipo
							 JOIN NUL.Turno	T  ON T.turno_profesional = P.prof_id
							                  AND T.turno_especialidad = PE.esp_id
							 JOIN NUL.Consulta C ON C.cons_turno_id = T.turno_id
							 JOIN NUL.Bono     B ON B.bono_id = C.cons_bono_usado
							 JOIN NUL.Plan_medico PM ON PM.plan_id = B.bono_plan
	GROUP BY  YEAR(T.turno_fecha_hora),MONTH(T.turno_fecha_hora), P.prof_id, PER.pers_nombre, PER.pers_apellido, PER.pers_tipo_doc, PER.pers_doc, E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, PM.plan_id, PM.plan_descrip
GO
------------------------------------------------
IF OBJECT_ID ('NUL.v_afil_bonos', 'V') IS NOT NULL  
	DROP VIEW NUL.v_afil_bonos ; 
GO
CREATE VIEW NUL.v_afil_bonos(anio, mes, afil_id, pers_nombre, pers_apellido, pers_tipo_doc, pers_doc, cant, grupo)
AS 
SELECT YEAR(BC.bonoc_fecha), MONTH(BC.bonoc_fecha), A.afil_id, P.pers_nombre, P.pers_apellido, P.pers_tipo_doc, P.pers_doc, SUM(BC.bonoc_cantidad) AS cant, (CASE WHEN A.afil_familiares > 1 OR RIGHT(CAST(A.afil_nro_afiliado as VARCHAR(18)),1) != 1 THEN 'SI' ELSE 'NO' END) AS grupo
	  FROM NUL.Afiliado A JOIN NUL.Persona P ON P.pers_id = A.afil_id
						  JOIN NUL.Bono_compra BC ON BC.bonoc_id_usuario = P.pers_id
	  GROUP BY YEAR(BC.bonoc_fecha), MONTH(BC.bonoc_fecha),A.afil_id, P.pers_nombre, P.pers_apellido, P.pers_tipo_doc, P.pers_doc, (CASE WHEN A.afil_familiares > 1 OR RIGHT(CAST(A.afil_nro_afiliado as VARCHAR(18)),1) != 1 THEN 'SI' ELSE 'NO' END)
GO
------------------------------------------------

IF OBJECT_ID ('NUL.v_esp_bonos', 'V') IS NOT NULL  
	DROP VIEW NUL.v_esp_bonos ; 
GO
CREATE VIEW NUL.v_esp_bonos(anio,mes,esp_id, esp_descrip, tipo_esp_id, tipo_esp_descrip, cant)
AS 
	SELECT YEAR(T.turno_fecha_hora), MONTH(T.turno_fecha_hora), E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip, COUNT(DISTINCT C.cons_id) AS cant
	  FROM NUL.Especialidad E JOIN NUL.Tipo_esp TE ON TE.tipo_esp_id = E.esp_tipo
							  JOIN NUL.Turno	T  ON T.turno_especialidad = E.esp_id
							  JOIN NUL.Consulta C  ON C.cons_turno_id = T.turno_id
	GROUP BY YEAR(T.turno_fecha_hora), MONTH(T.turno_fecha_hora),E.esp_id, E.esp_descrip, TE.tipo_esp_id, TE.tipo_esp_descrip
GO


--Drop y creacion de Trigger


IF OBJECT_ID ('NUL.cancelar_turnos ', 'TR') IS NOT NULL  
	DROP TRIGGER NUL.cancelar_turnos; 
GO


CREATE TRIGGER cancelar_turnos ON NUL.Historial_baja
AFTER INSERT
AS
BEGIN
		
		INSERT INTO NUL.Cancelacion 
			SELECT T.turno_id,3,'Usuario dado de baja',I.baja_fecha_id FROM NUL.Turno T JOIN inserted I
				ON T.turno_afiliado = I.baja_user_id
				WHERE T.turno_fecha_hora >= I.baja_fecha_id
				AND T.turno_id NOT IN (SELECT cancel_turno_id FROM NUL.Cancelacion)
	
END
GO


--Drop stored procedures

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_top5_esp_cancel'))
BEGIN
    DROP PROCEDURE NUL.sp_get_top5_esp_cancel
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_top5_prof_consultados'))
BEGIN
    DROP PROCEDURE NUL.sp_get_top5_prof_consultados
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_top5_prof_horas'))
BEGIN
    DROP PROCEDURE NUL.sp_get_top5_prof_horas
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_top5_afil_bonos'))
BEGIN
    DROP PROCEDURE NUL.sp_get_top5_afil_bonos
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_top5_esp_bonos'))
BEGIN
    DROP PROCEDURE NUL.sp_get_top5_esp_bonos
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_tipo_doc'))
BEGIN
    DROP PROCEDURE NUL.sp_get_tipo_doc
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_usuario'))
BEGIN
    DROP PROCEDURE NUL.sp_get_usuario
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_estados_civiles'))
BEGIN
    DROP PROCEDURE NUL.sp_get_estados_civiles
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_planes'))
BEGIN
    DROP PROCEDURE NUL.sp_get_planes
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_login'))
BEGIN
    DROP PROCEDURE NUL.sp_login
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_roles_disponibles_por_usuario'))
BEGIN
    DROP PROCEDURE NUL.sp_get_roles_disponibles_por_usuario
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_del_usuario'))
BEGIN
    DROP PROCEDURE NUL.sp_del_usuario
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_funciones_por_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_get_funciones_por_rol
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_del_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_del_rol
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_upd_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_upd_rol
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_set_funcion_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_set_funcion_rol
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_new_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_new_rol
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_buscar_usuarios'))
BEGIN
    DROP PROCEDURE NUL.sp_buscar_usuarios
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_del_funciones_rol'))
BEGIN
    DROP PROCEDURE NUL.sp_del_funciones_rol
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_habil_usuario'))
BEGIN
    DROP PROCEDURE NUL.sp_habil_usuario
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_turnos_pedidos'))
BEGIN
    DROP PROCEDURE NUL.sp_get_turnos_pedidos
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_cancelar_turno'))
BEGIN
    DROP PROCEDURE NUL.sp_cancelar_turno
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_set_pedir_turno'))
BEGIN
    DROP PROCEDURE NUL.sp_set_pedir_turno
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_set_resultado_consulta'))
BEGIN
    DROP PROCEDURE NUL.sp_set_resultado_consulta
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_actualizar_plan'))
BEGIN
    DROP PROCEDURE NUL.sp_actualizar_plan
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_new_agenda_profesional'))
BEGIN
    DROP PROCEDURE NUL.sp_new_agenda_profesional
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_new_dia_agenda_profesional'))
BEGIN
    DROP PROCEDURE NUL.sp_new_dia_agenda_profesional
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.agregar_persona'))
BEGIN
    DROP PROCEDURE NUL.agregar_persona
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_set_matricula_profesional'))
BEGIN
    DROP PROCEDURE NUL.sp_set_matricula_profesional
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_validar_bono'))
BEGIN
    DROP PROCEDURE NUL.sp_validar_bono
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_set_llegada'))
BEGIN
    DROP PROCEDURE NUL.sp_set_llegada
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_new_bono'))
BEGIN
    DROP PROCEDURE NUL.sp_new_bono
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_get_disp_profesional'))
BEGIN
    DROP PROCEDURE NUL.sp_get_disp_profesional
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_turnos_profesional'))
BEGIN
    DROP PROCEDURE NUL.sp_turnos_profesional
END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.agregar_afiliado'))
BEGIN
    DROP PROCEDURE NUL.agregar_afiliado
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_agregar_a_grupo_familiar'))
BEGIN
    DROP PROCEDURE NUL.sp_agregar_a_grupo_familiar
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.agregar_usuario'))
BEGIN
    DROP PROCEDURE NUL.agregar_usuario
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_modificar_usuario'))
BEGIN
    DROP PROCEDURE NUL.sp_modificar_usuario
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_asignar_rol_afiliado'))
BEGIN
    DROP PROCEDURE NUL.sp_asignar_rol_afiliado
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID('NUL.sp_cancelar_dia'))
BEGIN
    DROP PROCEDURE NUL.sp_cancelar_dia
END
GO


--Creacion de los stored procedures

CREATE PROCEDURE NUL.sp_get_top5_esp_cancel(@anio numeric(18,0), @semestre numeric(18,0),@mes numeric(18,0))
AS
BEGIN

	SELECT TOP 5 * FROM NUL.v_esp_canceladas
		WHERE anio = @anio AND
		  semestre = @semestre AND
		  mes = @mes
	ORDER BY cant DESC

END
GO

CREATE PROCEDURE NUL.sp_get_top5_prof_consultados(@plan_id numeric(18,0), @anio numeric(18,0), @semestre numeric(18,0),@mes numeric(18,0))
AS
BEGIN
	DECLARE @mes_semestre int;
	IF @semestre = 1 
		SET @mes_semestre = @mes;
	ELSE IF @semestre = 2 
		SET @mes_semestre = @mes + 6;
			
	SELECT TOP 5 * FROM NUL.v_prof_consultados V
	WHERE V.plan_id = @plan_id AND YEAR(V.fecha) = @anio AND MONTH(V.fecha) = @mes_semestre 

END
GO

CREATE PROCEDURE NUL.sp_get_top5_prof_horas(@plan_id numeric(18,0), @esp_id numeric(18,0), @anio numeric(18,0), @semestre numeric(18,0),@mes numeric(18,0))
AS
BEGIN
		
	SELECT TOP 5 * FROM NUL.v_prof_horas V
	WHERE plan_id = @plan_id
	  AND esp_id = @esp_id AND V.anio = @anio AND V.mes = (@semestre-1)*6+@mes
	ORDER BY cant ASC
	
END
GO


CREATE PROCEDURE NUL.sp_get_top5_afil_bonos(@anio numeric(18,0), @semestre numeric(18,0),@mes numeric(18,0))
AS
BEGIN
		
	SELECT TOP 5 * FROM NUL.v_afil_bonos V
	WHERE  V.anio = @anio AND V.mes = (@semestre-1)*6+@mes
	ORDER BY cant DESC

END
GO


CREATE PROCEDURE NUL.sp_get_top5_esp_bonos(@anio numeric(18,0), @semestre numeric(18,0),@mes numeric(18,0))
AS
BEGIN
		
	SELECT TOP 5 * FROM NUL.v_esp_bonos V
	WHERE V.anio = @anio AND V.mes = (@semestre-1)*6+@mes
	ORDER BY cant DESC

END
GO

CREATE PROCEDURE NUL.sp_get_tipo_doc

AS
BEGIN

	SELECT doc_id, doc_descrip FROM NUL.Tipo_doc

END
GO


CREATE PROCEDURE NUL.sp_get_usuario (@id numeric(18,0))
AS
BEGIN	
	
	SELECT * 
	FROM NUL.Usuario U
	WHERE U.user_id = @id

END
GO

CREATE PROCEDURE NUL.sp_get_estados_civiles
AS
BEGIN	
	
	SELECT estado_id, estado_descrip FROM NUL.Estado

END
GO


CREATE PROCEDURE NUL.sp_get_planes
AS
BEGIN	
	
	SELECT plan_id, plan_descrip FROM NUL.Plan_medico

END
GO

CREATE PROCEDURE NUL.sp_login(@username varchar(255), @tipo_doc numeric(18,0), @pass varchar(255), @result int output, @error varchar(255) output, @id numeric(18,0) output)
AS
BEGIN
	declare @user_id numeric(18,0)
	declare @user_pass varchar(255)
	declare @user_log_fallidos tinyint
	declare @user_habilitado bit
	
	declare curs CURSOR for
	SELECT user_id, user_pass, user_log_fallidos, user_habilitado FROM NUL.Usuario
	WHERE user_username = @username AND user_tipodoc = @tipo_doc
	for update

	open curs
	fetch next from curs into @user_id, @user_pass, @user_log_fallidos, @user_habilitado

	set @result = 1

	if @@FETCH_STATUS != 0
		set @error = 'Usuario inexistente'
	else
		if @user_habilitado != 1
			set @error = 'Usuario inhabilitado'
		else
				if @user_log_fallidos >= 3
					set @error = 'Usuario bloqueado'
				else
					if HASHBYTES('SHA2_256',@pass) != @user_pass
						begin
							update NUL.Usuario set user_log_fallidos = @user_log_fallidos + 1 where current of curs
							set @error = 'Password incorrecto. Quedan ' + CONVERT(varchar(5), ( 3 -  ( @user_log_fallidos + 1)))
						end
					else
						begin
							update NUL.Usuario set user_log_fallidos = 0 where current of curs
							set @result = 0
							set @id = @user_id
						end

END
GO

CREATE PROCEDURE NUL.sp_get_roles_disponibles_por_usuario(@id numeric(18,0))
AS
BEGIN
	SELECT R.rol_id, R.rol_descrip
	  FROM NUL.Usuario U JOIN NUL.User_rol UR ON UR.user_id = U.user_id
	                     JOIN NUL.Rol R ON R.rol_id = UR.rol_id
	  WHERE U.user_id = @id
		AND R.rol_habilitado = 1
END
GO

CREATE PROCEDURE NUL.sp_del_usuario(@id numeric(18,0), @fecha DateTime, @result int output)
AS 
BEGIN
	UPDATE NUL.Usuario SET user_habilitado = 0
	WHERE user_id = @id


	INSERT INTO NUL.Historial_baja(baja_user_id, baja_fecha_id)
		VALUES (@id, @fecha);	 		


	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_habil_usuario(@id numeric(18,0), @result int output)
AS 
BEGIN
	UPDATE NUL.Usuario SET user_habilitado = 1
	WHERE user_id = @id

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_get_funciones_por_rol(@id numeric(18,0))
AS 
BEGIN
	SELECT F.func_id, F.func_descrip
		FROM NUL.Rol_funcionalidad RF JOIN Funcionalidad F ON F.func_id = RF.func_id
		WHERE RF.rol_id = @id
END
GO

CREATE PROCEDURE NUL.sp_del_rol(@id numeric(18,0), @result int output)
AS 
BEGIN
	set @result = 0


	DELETE FROM NUL.User_rol
	WHERE rol_id = @id
	set @result = @@ERROR

    
	if @result = 0
		begin
			UPDATE NUL.Rol SET rol_habilitado = 0
			WHERE rol_id = @id

			set @result = @@ERROR
		end
END
GO

CREATE PROCEDURE NUL.sp_upd_rol(@id numeric(18,0), @descrip varchar(255), @habilitado bit, @result int output)
AS 
BEGIN
	UPDATE NUL.Rol SET rol_descrip = @descrip,
					   rol_habilitado = @habilitado
				 WHERE rol_id = @id
	
	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_set_funcion_rol(@id numeric(18,0), @id_func numeric(18,0), @result int output)
AS 
BEGIN
	INSERT INTO NUL.Rol_funcionalidad(rol_id, func_id) 
	VALUES (@id,@id_func)

	set @result = @@ERROR

END
GO

CREATE PROCEDURE NUL.sp_del_funciones_rol(@id numeric(18,0), @result int output)
AS
BEGIN
	DELETE FROM NUL.Rol_funcionalidad WHERE rol_id = @id

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_new_rol(@descrip varchar(255), @id_new numeric(38,0) output)
AS 
BEGIN
	INSERT INTO NUL.Rol(rol_descrip)
	VALUES (@descrip)
	if @@ERROR = 0
		set @id_new = @@IDENTITY

END
GO

CREATE PROCEDURE NUL.sp_buscar_usuarios(@username varchar(255), @tipo_doc varchar(255), @nombre varchar(255), @apellido varchar(255), @documento varchar(255), @direccion varchar(255), @telefono varchar(255), @mail varchar(255), @sexo varchar(255), @fechaNac varchar(255), @plan varchar(255), @nroAfiliado varchar(255))
AS
BEGIN
	SELECT U.user_id, U.user_username, U.user_tipodoc, P.pers_nombre, P.pers_apellido, P.pers_doc, P.pers_dire, P.pers_tel, P.pers_mail, P.pers_sexo, P.pers_fecha_nac, A.afil_plan_med, A.afil_nro_afiliado, U.user_habilitado, A.afil_estado
	  FROM NUL.Usuario U JOIN NUL.Persona P ON P.pers_id = U.user_id
	                     JOIN NUL.Afiliado A ON A.afil_id = P.pers_id
	 WHERE U.user_username LIKE @username
	   AND U.user_tipodoc  LIKE @tipo_doc
	   AND P.pers_nombre   LIKE @nombre
	   AND P.pers_apellido LIKE @apellido
	   AND P.pers_doc	   LIKE @documento
	   AND P.pers_dire     LIKE @direccion
	   AND P.pers_tel      LIKE @telefono
	   AND P.pers_mail     LIKE @mail
	   AND P.pers_sexo     LIKE @sexo
	   AND P.pers_fecha_nac LIKE @fechaNac
	   AND A.afil_plan_med LIKE @plan
	   AND A.afil_nro_afiliado LIKE @nroAfiliado
END
GO

CREATE PROCEDURE NUL.sp_get_turnos_pedidos(@user_id numeric(18,0), @desde datetime, @hasta datetime, @prof_id numeric(18,0), @esp_id numeric(18,0))
AS
BEGIN
	SELECT * FROM NUL.Turno T
	WHERE T.turno_afiliado = @user_id
	  AND T.turno_fecha_hora >= @desde
	  AND T.turno_fecha_hora <= @hasta
	  AND T.turno_profesional = @prof_id
	  AND T.turno_especialidad = @esp_id
	  AND T.turno_id NOT IN  (SELECT cancel_turno_id FROM NUL.Cancelacion)
	  AND T.turno_id NOT IN (SELECT cons_turno_id FROM NUL.Consulta)
END
GO

CREATE PROCEDURE NUL.sp_cancelar_turno(@id_turno numeric(18,0), @tipo_cancel numeric(18,0), @detalle varchar(255), @fecha datetime, @result int output)
AS
BEGIN
	INSERT INTO NUL.Cancelacion(cancel_turno_id, cancel_tipo, cancel_detalle, cancel_fecha)
	VALUES(@id_turno, @tipo_cancel, @detalle, @fecha)

	set @result = @@ERROR
END
GO


CREATE PROCEDURE NUL.sp_cancelar_dia(@id_prof numeric(18,0),@dia_cancelado datetime, @dia_cancelacion datetime)
AS
BEGIN
 
	INSERT INTO NUL.dias_cancelados (canceld_prof, canceld_dia) values (@id_prof, @dia_cancelado);

END
GO


CREATE PROCEDURE NUL.sp_set_pedir_turno(@afil_id numeric(18,0), @prof_id numeric(18,0), @esp_id numeric(18,0), @fecha datetime, @result int output)
AS 
BEGIN
	DECLARE @id numeric(18,0) = (SELECT MAX(turno_id)+1 FROM NUL.Turno)

	INSERT INTO NUL.Turno(turno_id, turno_afiliado, turno_profesional, turno_especialidad, turno_fecha_hora)
	VALUES(@id, @afil_id,@prof_id,@esp_id,@fecha)

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_set_resultado_consulta(@cons_id numeric(18,0), @sintomas varchar(255), @enfermedades varchar(255), @result int output)
AS
BEGIN
	UPDATE NUL.Consulta SET cons_enfermedades = @enfermedades,
							cons_sintomas	  = @sintomas
					  WHERE cons_id = @cons_id

    set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.agregar_usuario(@user_username varchar(255), @user_tipodoc numeric(18,0),@user_pass varchar(255),@id numeric(18,0) output)
AS
BEGIN
	
	INSERT INTO NUL.Usuario(user_username,user_tipodoc, user_pass) values (@user_username,@user_tipodoc, HASHBYTES('SHA2_256',@user_pass))
	SET @id = @@IDENTITY

END
GO


CREATE PROCEDURE NUL.sp_new_agenda_profesional(@prof_id numeric(18,0), @esp_id numeric(18,0), @desde datetime, @hasta datetime, @id_new numeric(18,0) output)
AS
BEGIN
	INSERT INTO NUL.Agenda(agenda_prof_id, agenda_prof_esp_id, agenda_disp_desde, agenda_disp_hasta)
	VALUES(@prof_id, @esp_id, @desde, @hasta)

	set @id_new = @@IDENTITY

END
GO

CREATE PROCEDURE NUL.sp_new_dia_agenda_profesional(@dia_id numeric(18,0), @agenda_id numeric(18,0), @hora_desde time, @hora_hasta time, @result int output)
AS
BEGIN
	INSERT INTO NUL.Agenda_dia(dia_id, agenda_id, dia_hora_inicio, dia_hora_fin)
	VALUES(@dia_id, @agenda_id, @hora_desde, @hora_hasta)

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_set_matricula_profesional(@prof_id numeric(18,0), @matric numeric(18,0), @result int output)
AS
BEGIN
	UPDATE NUL.Profesional SET prof_matric = @matric
	WHERE prof_id = @prof_id

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_validar_bono(@bono_id numeric(18,0), @result int output, @nroAfiliado varchar(255))
AS
BEGIN
SELECT * FROM NUL.Bono B JOIN NUL.Bono_compra BC ON BC.bonoc_id = B.bono_compra
								    JOIN NUL.Afiliado A ON BC.bonoc_id_usuario = A.afil_id
	WHERE B.bono_id = @bono_id
	  AND A.afil_nro_afiliado LIKE @nroAfiliado  --nroAfiliado llega con 'raiz _ _ _' 
	  AND B.bono_usado = 0
	  AND B.bono_plan = A.afil_plan_med

	set @result = @@ERROR
END
GO

CREATE PROCEDURE NUL.sp_set_llegada(@user_id numeric(18,0), @id_turno numeric(18,0), @fecha datetime, @hora_llegada time, @bono_id numeric(18,0), @result int output)
AS
BEGIN
	UPDATE NUL.Turno SET turno_llegada = @hora_llegada
	WHERE turno_id = @id_turno

	set @result = @@ERROR

	if @result = 0
		begin

		INSERT INTO NUL.Consulta (cons_turno_id, cons_bono_usado, cons_fecha_hora, cons_sintomas, cons_enfermedades)
		VALUES (@id_turno, @bono_id, @fecha, ' ', '  ' );
		
		set @result = @@ERROR

		if @result = 0
		   begin
 			
 			DECLARE @nro_cons numeric(18,0) = (SELECT afil_nro_consulta + 1 FROM NUL.Afiliado WHERE afil_id = @user_id);

			UPDATE NUL.Bono SET bono_usado = 1, bono_nro_consulta = @nro_cons,
							    bono_plan = (SELECT afil_plan_med FROM NUL.Afiliado WHERE afil_id = @user_id)
			WHERE bono_id = @bono_id

			UPDATE NUL.Afiliado SET afil_nro_consulta = @nro_cons

			set @result = @@ERROR

		   end

		end
END
GO

CREATE PROCEDURE NUL.sp_new_bono(@id_user numeric(18,0), @fecha datetime, @cantidad numeric(18,0), @monto numeric(16,2), @plan numeric(18,0), @result int output)
AS
BEGIN
	INSERT INTO NUL.Bono_compra(bonoc_id_usuario, bonoc_fecha, bonoc_fecha_impresion, bonoc_cantidad, bonoc_monto_total)
	VALUES(@id_user, @fecha, @fecha, @cantidad, @monto)

	DECLARE @cant int = 0
	DECLARE @new_id int = (SELECT MAX(bono_id)+1 FROM NUL.Bono) --no tiene identity
	DECLARE @compra_id int = @@IDENTITY

	if @@ERROR = 0

	WHILE @cant < @cantidad 	--va a agregar bono por bono segun @cantidad
	BEGIN
		INSERT INTO NUL.Bono(bono_id, bono_compra, bono_plan, bono_nro_consulta, bono_usado)
		VALUES(@new_id, @compra_id, @plan, 0, 0)
		

		set @cant = @cant + 1	--avanzo
		set @new_id = (SELECT MAX(bono_id)+1 FROM NUL.Bono)
	END

	set @result = @@ERROR

END
GO

CREATE PROCEDURE NUL.sp_get_disp_profesional(@id_prof numeric(18,0), @esp_id numeric(18,0), @fec_inicio datetime, @fec_fin datetime)
AS
BEGIN

with FECHAS(fecha) AS (
			SELECT CAST(@fec_inicio as datetime) fecha
			UNION ALL
			SELECT DATEADD(mi, 30, fecha) fecha
			FROM FECHAS
			WHERE fecha < @fec_fin 
		),
		--Seleccion de los turnos que tiene el medico actualmente disponibles
TURNOS AS (SELECT * FROM NUL.Turno 
		WHERE turno_profesional = @id_prof AND turno_fecha_hora BETWEEN @fec_inicio AND @fec_fin
		AND turno_id NOT IN 
			(SELECT cancel_turno_id FROM NUL.Cancelacion C JOIN NUL.Tipo_cancelacion TC ON C.cancel_tipo = TC.tipo_cancel_id 
				WHERE TC.tipo_cancel_detalle != 'Cancelada por el médico' ))

SELECT F.fecha,A.agenda_id, DATEPART(DW,F.fecha) as dia from FECHAS F
JOIN NUL.Agenda A ON F.fecha BETWEEN A.agenda_disp_desde AND A.agenda_disp_hasta
JOIN NUL.Agenda_dia AD ON AD.agenda_id = A.agenda_id AND CONVERT(time,F.fecha) >= AD.dia_hora_inicio AND CONVERT(time,F.fecha)< AD.dia_hora_fin AND DATEPART(DW,F.fecha) = AD.dia_id
WHERE A.agenda_prof_id = @id_prof AND A.agenda_prof_esp_id = @esp_id 
AND F.fecha NOT IN (SELECT turno_fecha_hora FROM TURNOS) AND CONVERT(date,F.fecha) NOT IN (SELECT DISTINCT canceld_dia FROM NUL.Dias_cancelados WHERE canceld_prof = @id_prof)
ORDER BY fecha,A.agenda_id


OPTION(MAXRECURSION 500)
END
GO


CREATE PROCEDURE NUL.sp_turnos_profesional(@prof NUMERIC(18,0), @fecha_desde DATETIME, @fecha_hasta DATETIME)
AS
BEGIN

SELECT * FROM NUL.Turno t 
WHERE t.turno_profesional = @prof AND t.turno_fecha_hora between @fecha_desde AND @fecha_hasta
		AND T.turno_id NOT IN  (SELECT cancel_turno_id FROM NUL.Cancelacion)
		AND T.turno_id NOT IN (SELECT cons_turno_id FROM NUL.Consulta)
END
GO

CREATE PROCEDURE NUL.sp_agregar_a_grupo_familiar(@user_id numeric(18,0), @titular numeric(18,0), @nro_familiar int, @fecha DateTime)
AS
BEGIN
	DECLARE @ult_afil numeric(18,0);
	DECLARE @nuevo_titular numeric(18,0) = (SELECT afil_id FROM NUL.Afiliado WHERE afil_nro_afiliado = @titular)
	DECLARE @cantidad_fam tinyint = (SELECT afil_familiares FROM NUL.Afiliado WHERE afil_nro_afiliado = @titular)
	DECLARE @plan_nuevo numeric(18,0) = (SELECT afil_plan_med FROM NUL.Afiliado WHERE afil_nro_afiliado = @titular)

	IF @nro_familiar = 0
		SET @ult_afil = @titular;
	ELSE 

		SELECT @ult_afil = ISNULL(MAX(afil_nro_afiliado), @titular+1)
		FROM NUL.Afiliado 
		WHERE afil_titular = @nuevo_titular;
	
	UPDATE NUL.Afiliado SET afil_nro_afiliado = @ult_afil+1 WHERE afil_id = @user_id;
	UPDATE NUL.Afiliado SET afil_titular = @nuevo_titular WHERE afil_id = @user_id;

	UPDATE NUL.Afiliado SET afil_plan_med = @plan_nuevo WHERE afil_id = @user_id;

	INSERT INTO NUL.Historial_plan_med (histo_plan_id, histo_afil_id, histo_fecha_id, histo_descrip)
						VALUES(@plan_nuevo, @user_id, @fecha, 'Por cambio de grupo familiar');

	UPDATE NUL.Afiliado SET afil_familiares = @cantidad_fam + 1 WHERE afil_id = @nuevo_titular;

END
GO



CREATE PROCEDURE NUL.agregar_persona(@pers_id numeric(18,0),@pers_nombre varchar(255), @pers_apellido varchar(255),@pers_doc varchar(255),
		@pers_dire varchar(255), @pers_tel numeric(18,2),@pers_mail varchar(255), @pers_fecha_nac datetime,@pers_sexo char)

AS
BEGIN

 INSERT INTO NUL.Persona (pers_id,pers_nombre, pers_apellido, pers_doc, pers_dire, pers_tel, pers_mail, pers_fecha_nac,pers_sexo)
	values(@pers_id,@pers_nombre, @pers_apellido, @pers_doc, @pers_dire, @pers_tel, @pers_mail, @pers_fecha_nac,@pers_sexo)

END
GO


CREATE PROCEDURE NUL.agregar_afiliado(@afil_id numeric(18,0),@afil_estado numeric(18,0),@afil_plan_med numeric(18,0),@afil_nro_afiliado numeric(18,0))
AS
BEGIN

INSERT INTO NUL.Afiliado(afil_id,afil_estado,afil_plan_med,afil_nro_afiliado) values 
	(@afil_id,@afil_estado,@afil_plan_med,@afil_nro_afiliado)

END
GO

CREATE PROCEDURE NUL.sp_actualizar_plan(@afil numeric(18,0),@plan numeric(18,0),@motivo varchar(255),@fecha DateTime,@error int output)
AS
BEGIN
	
	DECLARE @afil_dependiente numeric(18,0)
	DECLARE @motivo2 varchar(255) = 'Titular cambia plan'

	DECLARE afil CURSOR FOR (SELECT afil_id FROM NUL.Afiliado WHERE afil_titular = @afil)


	UPDATE NUL.Afiliado SET afil_plan_med = @plan WHERE afil_id = @afil

	INSERT INTO NUL.Historial_plan_med (histo_plan_id, histo_afil_id, histo_fecha_id, histo_descrip)
						VALUES(@plan, @afil, @fecha, @motivo);


	OPEN afil
	FETCH NEXT FROM afil INTO @afil_dependiente
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE NUL.Afiliado SET afil_plan_med = @plan WHERE afil_id = @afil_dependiente

		INSERT INTO NUL.Historial_plan_med (histo_plan_id, histo_afil_id, histo_fecha_id, histo_descrip)
					VALUES(@plan, @afil_dependiente, @fecha, @motivo2);


		FETCH NEXT FROM afil INTO @afil_dependiente
	END
	CLOSE afil
	DEALLOCATE afil

	set @error = @@ERROR

END
GO

CREATE PROCEDURE NUL.sp_modificar_usuario(@user_id numeric(18,0),@pers_dire varchar(255),
	 @pers_tel numeric(18,0), @pers_mail varchar(255), @pers_sexo char(1) , @afil_estado numeric(18,0))
AS
BEGIN
	UPDATE NUL.Persona SET
		pers_dire = @pers_dire,
		pers_tel = @pers_tel,
		pers_mail = @pers_mail,
		pers_sexo = @pers_sexo
	WHERE pers_id = @user_id

	UPDATE NUL.Afiliado SET
		afil_estado = @afil_estado
		WHERE afil_id = @user_id
END
GO

CREATE PROCEDURE NUL.sp_asignar_rol_afiliado(@user_id numeric(18,0))
AS
BEGIN

	INSERT INTO NUL.User_rol (rol_id, user_id) VALUES (2,@user_id)

END
GO

COMMIT TRANSACTION
