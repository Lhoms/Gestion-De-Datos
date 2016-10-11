USE GD2C2016
GO


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