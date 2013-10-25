USE GD2C2013
GO
CREATE SCHEMA YOU_SHALL_NOT_CRASH 
GO

--CREACION TABLAS VACIAS

CREATE TABLE YOU_SHALL_NOT_CRASH.FUNCIONALIDAD (
ID_Funcionalidad int IDENTITY(1,1),
Descripcion varchar(255), 
PRIMARY KEY(ID_Funcionalidad) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL (
ID_Rol int IDENTITY(1,1),
Descripcion varchar(255),
Activo bit, 
PRIMARY KEY(ID_Rol) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD (
ID_Rol int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.ROL(ID_Rol), 
ID_Funcionalidad int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.FUNCIONALIDAD(ID_Funcionalidad), 
PRIMARY KEY (ID_Rol, ID_Funcionalidad) );

CREATE TABLE YOU_SHALL_NOT_CRASH.USUARIO (
Username varchar(255),
Pass varchar(255),
DNI_Usuario numeric(18,0) UNIQUE, -- antes aca estaba ID_USUARIO, pero creo que nos conviene usar el dni, con el ID no se como hacerlo.
Intentos_Fallidos int, 
PRIMARY KEY (Username) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL_USUARIO (
ID_Rol int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.ROL(ID_Rol), 
DNI_Usuario numeric(18,0) FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(DNI_Usuario), 
PRIMARY KEY (ID_Rol, DNI_Usuario) );

CREATE TABLE YOU_SHALL_NOT_CRASH.AFILIADO (
ID_Afiliado int IDENTITY(1,1),
--ID_Usuario int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(ID_Usuario), este campo 
Nombre varchar(255),
Apellido varchar(255),
Nro_Afiliado int,
Digito_Familiar char(2),
Direccion varchar(255),
Telefono numeric(18,0),
Mail varchar(255),
Fecha_Nac DateTime,
DNI numeric(18,0)FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(DNI_Usuario),
Sexo char(1),
ID_Estado_Civil int, --FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.ESTADO_CIVIL(ID_Estado_Civil),
Familiares_A_Cargo int,
ID_Plan int, --FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.PLAN_MEDICO(ID_Plan),
Fecha_Baja DateTime,
PRIMARY KEY (ID_Afiliado) );





---------------------------------------------------------------------------------------------------------------

--COMPLETADO TABLAS

--Creo roles que se pide por enunciado
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Administrativo', 1)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Profesional', 1)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Afiliado', 1)
;

--Funcionalidades
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Rol');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Afiliado');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Profesional');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registrar Agenda del medico');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Compra de bonos');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Pedir turno');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Cancelacion de turno');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registro de llegada para atencion medica');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registro de resultado de atencion medica');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Generar receta');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Listado estadistico');

--Declaramos las variables con los codigos de los roles, que luego seran utilizadas
declare @cod_afiliado int
set @cod_afiliado=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Afiliado')

declare @cod_profesional int
set @cod_profesional=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Profesional')

declare @cod_administrativo int
set @cod_administrativo=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Administrativo')

--Asignar funcionalidades al Afiliado
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_afiliado, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE
Descripcion='Compra de bonos' or
Descripcion='Pedir turno' or
Descripcion='Cancelacion de turno'
;

--Asignar funcionalidades al Profesional
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_profesional, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE
Descripcion='Cancelacion de turno' or
Descripcion='Registro de resultado de atencion medica' or
Descripcion='Generar receta'
;

--Asignar funcionalidades al Administrador
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_administrativo, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD
;

--Por decision del grupo los username seran nombre + apellido + fecha de nacimiento y el pass sera w23e encriptado con SHA-256 para todos
INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO 
SELECT DISTINCT (Paciente_Nombre+Paciente_Apellido+CONVERT(VARCHAR,YEAR(Paciente_Fecha_Nac))), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', Paciente_Dni, 0
FROM gd_esquema.Maestra
where Paciente_Dni is NOT NULL
union
SELECT DISTINCT (Medico_Nombre+Medico_Apellido+CONVERT(VARCHAR,YEAR(Medico_Fecha_Nac))), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', Medico_Dni, 0
FROM gd_esquema.Maestra
where Medico_Dni is NOT NULL
;

--Asignamos los roles a los usuarios creados
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO 
SELECT DISTINCT @cod_afiliado, Paciente_Dni
FROM gd_esquema.Maestra
where Paciente_Dni is NOT NULL
union
SELECT DISTINCT @cod_profesional, Medico_Dni
FROM gd_esquema.Maestra
where Medico_Dni is NOT NULL
;

--Creamos un usuario administrador y le asignamos su correspondiente rol
INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO 
values ('CLINICAFRBA', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 0)
;

INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_administrativo, 0)
;

INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO (Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac, DNI, ID_Plan, Digito_Familiar)
SELECT DISTINCT Paciente_Nombre, Paciente_Apellido, Paciente_Direccion, Paciente_Telefono, Paciente_Mail, Paciente_Fecha_Nac, Paciente_Dni, Plan_Med_Codigo, 01
FROM gd_esquema.Maestra
;




