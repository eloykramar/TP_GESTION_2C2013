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






----------------------------------------------------------------------------------------------------------------------





create table you_shall_not_crash.DIAGNOSTICO( 
ID_DIAGNOSTICO NUMERIC IDENTITY,
ID_TURNO NUMERIC,
ID_PROFESIONAL NUMERIC,
DESCRIPCION nvarchar(255)

PRIMARY KEY (ID_DIAGNOSTICO),
--FOREIGN KEY (ID_TURNO) REFERENCES you_shall_not_crash.TURNO(ID_TURNO)
)

GO 


create table you_shall_not_crash.ITEM_DIAGNOSTICO(
ID_ITEM NUMERIC IDENTITY,
ID_DIAGNOSTICO NUMERIC,
ID_SINTOMA NUMERIC,

PRIMARY KEY (ID_ITEM),
FOREIGN KEY (ID_DIAGNOSTICO) REFERENCES  you_shall_not_crash.DIAGNOSTICO(ID_DIAGNOSTICO),
--FOREIGN KEY (ID_SINTOMA) REFERENCES you_shall_not_crash.SINTOMA(ID_SINTOMA)
)



create table you_shall_not_crash.TIPO_ESPECIALIDAD(
CODIGO_TIPO_ESPECIALIDAD NUMERIC(18,0),
DESCRIPCION VARCHAR(255),


PRIMARY KEY (CODIGO_TIPO_ESPECIALIDAD)
)



create table you_shall_not_crash.ESPECIALIDAD(
CODIGO_ESPECIALIDAD NUMERIC(18,0),
DESCRIPCION VARCHAR(255),
CODIGO_TIPO_ESPECIALIDAD int,


PRIMARY KEY (CODIGO_ESPECIALIDAD),
)

create table you_shall_not_crash.PROFESIONAL(
ID_PROFESIONAL NUMERIC IDENTITY,
--ID_USUARIO NUMERIC,
NOMBRE VARCHAR(255),
APELLIDO VARCHAR(255),
DNI NUMERIC(18,0),
DIRECCION VARCHAR(255),
TELEFONO NUMERIC(18,0),
MAIL VARCHAR(255),
FECHA_NAC DATETIME,
SEXO VARCHAR(9),
MATRICULA INT, --VER COMO VAMOS A CREAR LA MATRICULA, POR AHORA NULL
ACTIVO CHAR(3), --SI/NO

PRIMARY KEY (ID_PROFESIONAL),
FOREIGN KEY (DNI) REFERENCES you_shall_not_crash.USUARIO(DNI_USUARIO)
)


create table you_shall_not_crash.ESPECIALIDAD_PROFESIONAL(
CODIGO_ESPECIALIDAD NUMERIC,
ID_PROFESIONAL NUMERIC,

FOREIGN KEY (CODIGO_ESPECIALIDAD) REFERENCES  you_shall_not_crash.ESPECIALIDAD(CODIGO_ESPECIALIDAD),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES  you_shall_not_crash.PROFESIONAL(ID_PROFESIONAL)
)


create table you_shall_not_crash.RECETA(
ID_RECETA NUMERIC IDENTITY,
ID_DIAGNOSTICO NUMERIC,

PRIMARY KEY (ID_RECETA),
FOREIGN KEY (ID_DIAGNOSTICO) REFERENCES you_shall_not_crash.DIAGNOSTICO(ID_DIAGNOSTICO)
)


create table you_shall_not_crash.SINTOMA(
ID_SINTOMA NUMERIC IDENTITY,
DESCRIPCION VARCHAR (255),

PRIMARY KEY (ID_SINTOMA)
)


create table you_shall_not_crash.TURNO(
ID_TURNO NUMERIC IDENTITY,
NUMERO NUMERIC(18,0),
ID_PROFESIONAL NUMERIC,
ID_AFILIADO INT,
FECHA DATETIME,
FECHA_LLEGADA DATETIME,

PRIMARY KEY (ID_TURNO),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL(ID_PROFESIONAL),
FOREIGN KEY (ID_AFILIADO) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_AFILIADO)
)

--PROFESIONAL--------------------
insert into you_shall_not_crash.PROFESIONAL(NOMBRE,APELLIDO,DNI,DIRECCION,TELEFONO,MAIL,FECHA_NAC) --activo,matricula y sexo null
select distinct Medico_Nombre,Medico_Apellido,Medico_Dni,Medico_Direccion,Medico_Telefono,Medico_Mail,Medico_Fecha_Nac
from gd_esquema.Maestra
where Medico_Dni is not null

--ESPECIALIDAD_PROFESIONAL-------------------------
insert into you_shall_not_crash.ESPECIALIDAD_PROFESIONAL
select distinct e.CODIGO_ESPECIALIDAD,p.ID_PROFESIONAL
from you_shall_not_crash.PROFESIONAL P join gd_esquema.Maestra M on p.DNI=m.Medico_Dni join you_shall_not_crash.ESPECIALIDAD E on M.Especialidad_Codigo=e.CODIGO_ESPECIALIDAD

--TIPO_ESPECIALIDAD-------------------------
insert into you_shall_not_crash.TIPO_ESPECIALIDAD
select distinct Tipo_Especialidad_Codigo,Tipo_Especialidad_Descripcion
from gd_esquema.Maestra
where Tipo_Especialidad_Codigo is not null
order by Tipo_Especialidad_Codigo

--ESPECIALIDAD-------------------------
insert into you_shall_not_crash.ESPECIALIDAD(CODIGO_ESPECIALIDAD,DESCRIPCION,CODIGO_TIPO_ESPECIALIDAD)
select distinct Especialidad_Codigo,Especialidad_Descripcion,Tipo_Especialidad_Codigo
from gd_esquema.Maestra
where Especialidad_Codigo is not null
order by Especialidad_Codigo

--SINTOMA------------------------
insert into you_shall_not_crash.SINTOMA
select distinct Consulta_Sintomas
from gd_esquema.Maestra
where Consulta_Sintomas is not null

--TURNO-----------------------------------------------------------
insert into you_shall_not_crash.TURNO --ACLARACION!!: EN ESTA TABLA HAY NRO DE TURNOS DUPLICADOS; ANTES DE LA CONSULTA (CMPO ENFERMEDADES NULL) Y DSP DE LA CONSULTA (CAMPO ENFERMEDADES C ENFERMEDAD) MISMO NRO
select Turno_Numero,p.ID_PROFESIONAL,a.ID_Afiliado,Turno_Fecha,dateadd(MINUTE, -15, Turno_Fecha)--suponemos que a los que se le diagnostico una enfermedad fueron atendidos, por lo que llegaron 15 min antes
from gd_esquema.Maestra M join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
where Turno_Numero is not null and Consulta_Enfermedades is not null
union
select Turno_Numero,p.ID_PROFESIONAL,a.ID_Afiliado,Turno_Fecha,NULL
from gd_esquema.Maestra m join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
where Turno_Numero is not null and Consulta_Enfermedades is null
order by Turno_Numero 

--DIAGNOSTICO---------------------------
insert into you_shall_not_crash.DIAGNOSTICO
select distinct t.id_turno, t.ID_PROFESIONAL,m.Consulta_Enfermedades
from gd_esquema.Maestra m join you_shall_not_crash.TURNO t on m.Turno_Numero=t.NUMERO
where Consulta_Enfermedades is not null and FECHA_LLEGADA is not null


--ITEM_DIAGNOSTICO-----------------------------------------
insert into you_shall_not_crash.ITEM_DIAGNOSTICO
select distinct d.ID_DIAGNOSTICO,s.ID_SINTOMA
from you_shall_not_crash.TURNO t join you_shall_not_crash.DIAGNOSTICO d on t.ID_TURNO=d.ID_TURNO join gd_esquema.Maestra m on t.NUMERO=m.Turno_Numero join you_shall_not_crash.SINTOMA s on s.DESCRIPCION=m.Consulta_Sintomas
where Consulta_Enfermedades is not null and FECHA_LLEGADA is not null


--RECETA----------------------------
insert into you_shall_not_crash.RECETA
select d.ID_DIAGNOSTICO
from you_shall_not_crash.DIAGNOSTICO d





-----------------------------------------------------------------------------------------------





CREATE TABLE YOU_SHALL_NOT_CRASH.PLAN_MEDICO (
ID_Plan numeric(18,0),
Descripcion varchar(255), 
Precio_plan numeric(18,2),
Precio_bono_consulta numeric(18,2),
Precio_bono_farmacia numeric(18,2),

PRIMARY KEY(ID_Plan) );


CREATE TABLE YOU_SHALL_NOT_CRASH.MEDICAMENTO (
ID_Medicamento int identity(1,1),
Descripcion varchar(255),

PRIMARY KEY(ID_Medicamento) );


CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_CONSULTA (
ID_Bono_Consulta numeric(18,0),
Fecha_Emision datetime,
ID_Afiliado int ,
ID_Turno NUMERIC ,

PRIMARY KEY(ID_Bono_Consulta),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Turno) REFERENCES YOU_SHALL_NOT_CRASH.TURNO(ID_Turno) );


CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_FARMACIA (
ID_Bono_Farmacia numeric(18,0),
Fecha_Emision datetime,
ID_Afiliado int ,
ID_Receta_Medica NUMERIC ,
Fecha_Prescripcion_Medica datetime,
--ACA NO DEBERIA IR LA FECHA DE VENCIMIENTO DEL BONO?

PRIMARY KEY(ID_Bono_Farmacia),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Receta_Medica) REFERENCES YOU_SHALL_NOT_CRASH.RECETA(ID_Receta) );


CREATE TABLE YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (
ID_Bono_Farmacia numeric(18,0),
ID_Item int ,
ID_Medicamento int ,
Cantidad int,

PRIMARY KEY(ID_Bono_Farmacia,ID_Item),
FOREIGN KEY (ID_Bono_Farmacia) REFERENCES YOU_SHALL_NOT_CRASH.BONO_FARMACIA(ID_Bono_Farmacia),
FOREIGN KEY (ID_Medicamento) REFERENCES YOU_SHALL_NOT_CRASH.MEDICAMENTO(ID_Medicamento) );






--PLAN MEDICO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.PLAN_MEDICO (ID_Plan,Descripcion,Precio_bono_consulta,Precio_bono_farmacia)
SELECT DISTINCT Plan_Med_Codigo, Plan_Med_Descripcion, Plan_Med_Precio_Bono_Consulta, Plan_Med_Precio_Bono_Farmacia 
FROM gd_esquema.Maestra
;


--MEDICAMENTO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.MEDICAMENTO (Descripcion)
SELECT DISTINCT Bono_Farmacia_Medicamento 
FROM gd_esquema.Maestra 
WHERE Bono_Farmacia_Medicamento IS NOT NULL
;

--BONO CONSULTA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_CONSULTA (ID_Bono_Consulta, Fecha_Emision)
SELECT DISTINCT Bono_Consulta_Numero, Bono_Consulta_Fecha_Impresion
FROM gd_esquema.Maestra
WHERE Bono_Consulta_Fecha_Impresion IS NOT NULL AND 
	  Bono_Consulta_Numero IS NOT NULL

;

--BONO FARMACIA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_FARMACIA(Fecha_Emision,ID_Bono_Farmacia)
SELECT DISTINCT Bono_Farmacia_Fecha_Impresion, Bono_Farmacia_Numero 
FROM gd_esquema.Maestra
WHERE Bono_Farmacia_Fecha_Impresion IS NOT NULL AND
	  Bono_Farmacia_Numero IS NOT NULL
;  


--ITEM BONO FARMACIA---------------------------------------------
--INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (ID_Bono_Farmacia)
--SELECT Bono_Farmacia_Numero FROM gd_esquema.Maestra
--WHERE Bono_Farmacia_Numero IS NOT NULL AND
--	  Bono_Farmacia_Medicamento IS NOT NULL
--;
