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
Activo bit DEFAULT (1),
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
Nombre varchar(255),
Apellido varchar(255),
Nro_Afiliado int, -- numero de afiliado ENTERO = ID_Afiliado *100 mas 1, 2 o el digito q corresponda
Cantidad_Consultas int,
Direccion varchar(255),
Telefono numeric(18,0),
Mail varchar(255),
Fecha_Nac DateTime,
DNI numeric(18,0)FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(DNI_Usuario),
Sexo char(1),
ID_Estado_Civil int,
Familiares_A_Cargo int,
ID_Plan int, 
Fecha_Baja DateTime,
PRIMARY KEY (ID_Afiliado) );

create table YOU_SHALL_NOT_CRASH.CONSULTA( 
ID_CONSULTA NUMERIC IDENTITY,
ID_TURNO NUMERIC,
ID_PROFESIONAL NUMERIC

PRIMARY KEY (ID_CONSULTA))

create table YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA(
ID_ITEM NUMERIC IDENTITY,
ID_CONSULTA NUMERIC,
ID_SINTOMA NUMERIC,

PRIMARY KEY (ID_ITEM),
FOREIGN KEY (ID_CONSULTA) REFERENCES  you_shall_not_crash.CONSULTA(ID_CONSULTA)
)

create table YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA(
ID_ITEM NUMERIC IDENTITY,
ID_CONSULTA NUMERIC,
ID_ENFERMEDAD NUMERIC,

PRIMARY KEY (ID_ITEM),
FOREIGN KEY (ID_CONSULTA) REFERENCES  you_shall_not_crash.CONSULTA(ID_CONSULTA)
)

create table YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD(
CODIGO_TIPO_ESPECIALIDAD int,
DESCRIPCION VARCHAR(255),

PRIMARY KEY (CODIGO_TIPO_ESPECIALIDAD)
)

create table YOU_SHALL_NOT_CRASH.ESPECIALIDAD(
CODIGO_ESPECIALIDAD NUMERIC(18,0),
DESCRIPCION VARCHAR(255),
CODIGO_TIPO_ESPECIALIDAD int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD(CODIGO_TIPO_ESPECIALIDAD),

PRIMARY KEY (CODIGO_ESPECIALIDAD),
)

create table YOU_SHALL_NOT_CRASH.PROFESIONAL(
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
ACTIVO bit, --SI/NO

PRIMARY KEY (ID_PROFESIONAL),
FOREIGN KEY (DNI) REFERENCES you_shall_not_crash.USUARIO(DNI_USUARIO)
)

create table YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL(
CODIGO_ESPECIALIDAD NUMERIC,
ID_PROFESIONAL NUMERIC,

FOREIGN KEY (CODIGO_ESPECIALIDAD) REFERENCES  you_shall_not_crash.ESPECIALIDAD(CODIGO_ESPECIALIDAD),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES  you_shall_not_crash.PROFESIONAL(ID_PROFESIONAL)
)

create table YOU_SHALL_NOT_CRASH.RECETA(
ID_RECETA NUMERIC IDENTITY,
ID_CONSULTA NUMERIC,

PRIMARY KEY (ID_RECETA),
FOREIGN KEY (ID_CONSULTA) REFERENCES you_shall_not_crash.CONSULTA(ID_CONSULTA)
)

create table YOU_SHALL_NOT_CRASH.SINTOMA(
ID_SINTOMA NUMERIC IDENTITY(1,1),
DESCRIPCION VARCHAR (255),

PRIMARY KEY (ID_SINTOMA)
)

create table YOU_SHALL_NOT_CRASH.ENFERMEDAD(
ID_ENFERMEDAD NUMERIC IDENTITY(1,1),
DESCRIPCION VARCHAR (255),

PRIMARY KEY (ID_ENFERMEDAD)
)

CREATE TABLE YOU_SHALL_NOT_CRASH.PLAN_MEDICO (
ID_Plan int,
Descripcion varchar(255), 
Precio_plan numeric(18,2),
Precio_bono_consulta numeric(18,2),
Precio_bono_farmacia numeric(18,2),

PRIMARY KEY(ID_Plan) );

CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_CONSULTA (
ID_Bono_Consulta int,
Fecha_Emision datetime,
ID_Afiliado int ,
Numero_Consulta_Afiliado int,
ID_Plan int

PRIMARY KEY(ID_Bono_Consulta),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Plan) REFERENCES YOU_SHALL_NOT_CRASH.Plan_Medico(ID_Plan)
);

CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_FARMACIA (
ID_Bono_Farmacia numeric(18,0) ,
Fecha_Emision datetime,
ID_Afiliado int ,
ID_Plan int,
ID_Receta_Medica NUMERIC ,
Fecha_Prescripcion_Medica datetime,
Fecha_Vencimiento datetime

PRIMARY KEY(ID_Bono_Farmacia),
FOREIGN KEY (ID_Plan) REFERENCES YOU_SHALL_NOT_CRASH.Plan_Medico(ID_Plan),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Receta_Medica) REFERENCES YOU_SHALL_NOT_CRASH.RECETA(ID_Receta)
);

CREATE TABLE YOU_SHALL_NOT_CRASH.MEDICAMENTO (
ID_Medicamento int identity(1,1),
Descripcion varchar(255),

PRIMARY KEY(ID_Medicamento) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (
ID_Item int identity (1,1),
ID_Bono_Farmacia numeric(18,0),
ID_Medicamento int ,
Cantidad int,

PRIMARY KEY(ID_Item),
FOREIGN KEY (ID_Bono_Farmacia) REFERENCES YOU_SHALL_NOT_CRASH.BONO_FARMACIA(ID_Bono_Farmacia),
FOREIGN KEY (ID_Medicamento) REFERENCES YOU_SHALL_NOT_CRASH.MEDICAMENTO(ID_Medicamento) )

create table YOU_SHALL_NOT_CRASH.TURNO(
ID_TURNO NUMERIC IDENTITY,
NUMERO NUMERIC(18,0),
ID_PROFESIONAL NUMERIC,
ID_AFILIADO INT,
FECHA DATETIME,
FECHA_LLEGADA DATETIME,
ID_Bono_Consulta INT,
Cancelado bit DEFAULT (0), 

PRIMARY KEY (ID_TURNO),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL(ID_PROFESIONAL),
FOREIGN KEY (ID_AFILIADO) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_AFILIADO),
FOREIGN KEY (ID_Bono_Consulta) REFERENCES YOU_SHALL_NOT_CRASH.BONO_CONSULTA(ID_Bono_Consulta)
)

CREATE TABLE YOU_SHALL_NOT_CRASH.ESTADO_CIVIL (
ID_Estado_Civil int identity(1,1),
Descripcion varchar(255),
PRIMARY KEY(ID_Estado_Civil) );

--Cancelaciones: Se tomará para la migracion el día 01/01/2013 como fecha default de cancelacion.

CREATE TABLE YOU_SHALL_NOT_CRASH.CANCELACION_TURNO (
ID_Cancelacion int identity(1,1),
Tipo_Cancelacion varchar(30),
Detalle varchar(255),
Fecha datetime,
ID_Turno numeric

PRIMARY KEY (ID_Cancelacion),
FOREIGN KEY (ID_Turno) REFERENCES YOU_SHALL_NOT_CRASH.TURNO (ID_TURNO))

CREATE TABLE YOU_SHALL_NOT_CRASH.COMPRA_BONO (
Id_compra int identity(1,1),
Id_Afiliado int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
Cant_Bonos_Consulta int,
Cant_Bonos_Farmacia int,
Monto numeric(18,2)
PRIMARY KEY (ID_COMPRA))

CREATE TABLE YOU_SHALL_NOT_CRASH.ITEM_COMPRA_BONO (
id_Item int identity(1,1),
id_compra int,
id_bono int,
tipo_bono char(1),
precio_unitario numeric(18,2)

PRIMARY KEY (id_item),
FOREIGN KEY (ID_compra) REFERENCES YOU_SHALL_NOT_CRASH.compra_bono (id_compra))

 
--AGREGAMOS LAS FOREING KEYS FALTANTES:
 
ALTER TABLE YOU_SHALL_NOT_CRASH.AFILIADO
ADD FOREIGN KEY (ID_Estado_Civil)
REFERENCES YOU_SHALL_NOT_CRASH.ESTADO_CIVIL(ID_Estado_Civil);

ALTER TABLE YOU_SHALL_NOT_CRASH.AFILIADO
ADD FOREIGN KEY (ID_Plan)
REFERENCES YOU_SHALL_NOT_CRASH.PLAN_MEDICO(ID_Plan);

ALTER TABLE YOU_SHALL_NOT_CRASH.CONSULTA
ADD FOREIGN KEY (ID_TURNO) REFERENCES you_shall_not_crash.TURNO(ID_TURNO);

ALTER TABLE YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA
ADD FOREIGN KEY (ID_SINTOMA) REFERENCES you_shall_not_crash.SINTOMA(ID_SINTOMA);

ALTER TABLE YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA
ADD FOREIGN KEY (ID_ENFERMEDAD) REFERENCES you_shall_not_crash.ENFERMEDAD(ID_ENFERMEDAD);


---------------------------------------------------------------------------------------------------------------

--COMPLETADO TABLAS

INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Soltero/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Casado/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Viudo/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Concubinato');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Divorciado/a');

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
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Listado estadistico');

--Declaramos las variables con los codigos de los roles, que luego seran utilizadas
declare @cod_afiliado int
set @cod_afiliado=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Afiliado')

declare @cod_profesional int
set @cod_profesional=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Profesional')

declare @cod_administrativo int
set @cod_administrativo=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Administrativo')

--Asignar funcionalidades al Administrador
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_administrativo, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD
;

--Agrego dos funcionalidades mas que no pertenecen al Administrador
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registro de resultado de atencion medica');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Generar receta');

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
Descripcion='Registro de resultado de atencion medica'
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

--Creamos un usuario administrador y le asignamos todos los roles
INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO 
values ('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 0)
;

INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_administrativo, 0)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_afiliado, 0)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_profesional, 0)
;

--PLAN MEDICO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.PLAN_MEDICO (ID_Plan,Descripcion,Precio_bono_consulta,Precio_bono_farmacia)
SELECT DISTINCT Plan_Med_Codigo, Plan_Med_Descripcion, Plan_Med_Precio_Bono_Consulta, Plan_Med_Precio_Bono_Farmacia 
FROM gd_esquema.Maestra
;

--Por defecto se carga sexo masculino, digito familiar 1, estado civil soltero, familiares a cargo 0
INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO (Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac, DNI, ID_Plan, Sexo, ID_Estado_Civil, Familiares_A_Cargo, Cantidad_Consultas)
SELECT DISTINCT Paciente_Nombre, Paciente_Apellido, Paciente_Direccion, Paciente_Telefono, Paciente_Mail, Paciente_Fecha_Nac, Paciente_Dni, Plan_Med_Codigo, 'M', 1, 0, 0
FROM gd_esquema.Maestra
where Paciente_Dni is not NULL
;

--creamos un afiliado para el admin
INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO (Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac, DNI, ID_Plan, Sexo, ID_Estado_Civil, Familiares_A_Cargo, Cantidad_Consultas) values ('admin', 'admin', '', '454545454', '', '1991/09/07', 0, 555555, 'M', 1, 0, 0)


--Al numero de afiliado para la migracion le asignamos el mismo valor del ID, ya que consideramos que ninguno tiene familiares asignados
UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Nro_Afiliado = ((ID_Afiliado*100) +1);

--creamos un profesional para el admin
INSERT INTO YOU_SHALL_NOT_CRASH.PROFESIONAL (Nombre, APELLIDO, DNI, DIRECCION, TELEFONO, MAIL, FECHA_NAC, SEXO, ACTIVO) values ('admin', 'ADMIN',0,'casa admin', '45454545', '', '1991/09/07', 'M', 1)

--BONO CONSULTA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_CONSULTA (ID_Bono_Consulta, Fecha_Emision, ID_Afiliado, ID_Plan)
SELECT DISTINCT (Bono_Consulta_Numero), Bono_Consulta_Fecha_Impresion, (select ID_Afiliado from YOU_SHALL_NOT_CRASH.AFILIADO where DNI = Paciente_Dni), (select ID_Plan from YOU_SHALL_NOT_CRASH.AFILIADO where DNI = Paciente_Dni)
FROM gd_esquema.Maestra
WHERE Bono_Consulta_Fecha_Impresion IS NOT NULL AND 
	  Bono_Consulta_Numero IS NOT NULL 
;

--BONO FARMACIA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_FARMACIA(Fecha_Emision,ID_Bono_Farmacia, ID_Afiliado, ID_Plan, Fecha_Vencimiento)
SELECT DISTINCT Bono_Farmacia_Fecha_Impresion, Bono_Farmacia_Numero, (select ID_Afiliado from YOU_SHALL_NOT_CRASH.AFILIADO where DNI = Paciente_Dni), (select ID_Plan from YOU_SHALL_NOT_CRASH.AFILIADO where DNI = Paciente_Dni), (Bono_Farmacia_Fecha_Impresion + 60)	
FROM gd_esquema.Maestra
WHERE Bono_Farmacia_Fecha_Impresion IS NOT NULL AND
	  Bono_Farmacia_Numero IS NOT NULL;	  
	  
--MEDICAMENTO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.MEDICAMENTO (Descripcion)
SELECT DISTINCT Bono_Farmacia_Medicamento 
FROM gd_esquema.Maestra 
WHERE Bono_Farmacia_Medicamento IS NOT NULL
;

--ITEM BONO FARMACIA---------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (ID_Bono_Farmacia, ID_Medicamento, Cantidad)
SELECT Bono_Farmacia_Numero, (select ID_Medicamento from YOU_SHALL_NOT_CRASH.MEDICAMENTO where Descripcion=Bono_Farmacia_Medicamento), 1
FROM gd_esquema.Maestra
WHERE Bono_Farmacia_Numero IS NOT NULL AND
	  Bono_Farmacia_Medicamento IS NOT NULL
;

--PROFESIONAL--------------------
insert into YOU_SHALL_NOT_CRASH.PROFESIONAL(NOMBRE,APELLIDO,DNI,DIRECCION,TELEFONO,MAIL,FECHA_NAC,ACTIVO) --matricula y sexo null
select distinct Medico_Nombre,Medico_Apellido,Medico_Dni,Medico_Direccion,Medico_Telefono,Medico_Mail,Medico_Fecha_Nac,1
from gd_esquema.Maestra
where Medico_Dni is not null

--TIPO_ESPECIALIDAD-------------------------
insert into YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD
select distinct Tipo_Especialidad_Codigo,Tipo_Especialidad_Descripcion
from gd_esquema.Maestra
where Tipo_Especialidad_Codigo is not null
order by Tipo_Especialidad_Codigo

--ESPECIALIDAD-------------------------
insert into YOU_SHALL_NOT_CRASH.ESPECIALIDAD(CODIGO_ESPECIALIDAD,DESCRIPCION,CODIGO_TIPO_ESPECIALIDAD)
select distinct Especialidad_Codigo,Especialidad_Descripcion,Tipo_Especialidad_Codigo
from gd_esquema.Maestra
where Especialidad_Codigo is not null
order by Especialidad_Codigo;

--ESPECIALIDAD_PROFESIONAL-------------------------
insert into YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL
select distinct e.CODIGO_ESPECIALIDAD,p.ID_PROFESIONAL
from you_shall_not_crash.PROFESIONAL P join gd_esquema.Maestra M on p.DNI=m.Medico_Dni join you_shall_not_crash.ESPECIALIDAD E on M.Especialidad_Codigo=e.CODIGO_ESPECIALIDAD

--SINTOMA------------------------
insert into YOU_SHALL_NOT_CRASH.SINTOMA(DESCRIPCION)
select distinct Consulta_Sintomas
from gd_esquema.Maestra
where Consulta_Sintomas is not null;

--ENFERMEDAD------------------------
insert into YOU_SHALL_NOT_CRASH.ENFERMEDAD(DESCRIPCION)
select distinct Consulta_Enfermedades
from gd_esquema.Maestra
where Consulta_Enfermedades is not null;

--TURNO-----------------------------------------------------------
go
create view YOU_SHALL_NOT_CRASH.turnos_temp as (
SELECT Turno_Numero a, Max(p.ID_PROFESIONAL) b, Max(a.ID_Afiliado) c, Max(Turno_Fecha) d,
 CASE
    WHEN Max(Bono_Consulta_Numero) is not null
     THEN dateadd(MINUTE, -15, Max(Turno_Fecha))
     ELSE NULL
     END as llegada,  M.Bono_Consulta_Numero,
 0 as f
--suponemos que a los que tienen bono consulta asignado fueron atendidos, por lo que llegaron 15 min antes
from gd_esquema.Maestra M join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
where Turno_Numero is not null
group by Turno_Numero, M.Bono_Consulta_Numero
);
go

insert into YOU_SHALL_NOT_CRASH.TURNO(NUMERO, ID_PROFESIONAL, ID_AFILIADO, FECHA, FECHA_LLEGADA, ID_Bono_Consulta, Cancelado) 
(Select distinct turno_numero, Max(p.ID_PROFESIONAL) prof, Max(a.ID_Afiliado) af, Max(Turno_Fecha) d,
(select MAX(llegada)
			from YOU_SHALL_NOT_CRASH.turnos_temp t2
			where Turno_Numero = t2.a
			group by a),
			(select max(Bono_Consulta_Numero)
					from YOU_SHALL_NOT_CRASH.turnos_temp t2
					where Turno_Numero = t2.a
					group by a), 0
from gd_esquema.Maestra M join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
where Turno_Numero is not null
group by Turno_Numero);

--CONSULTA---------------------------
insert into YOU_SHALL_NOT_CRASH.CONSULTA
select distinct t.id_turno, t.ID_PROFESIONAL
from gd_esquema.Maestra m join you_shall_not_crash.TURNO t on m.Turno_Numero=t.NUMERO
where FECHA_LLEGADA is not null;

--SINTOMA_CONSULTA-----------------------------------------
insert into YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA
select distinct d.ID_CONSULTA,s.ID_SINTOMA
from you_shall_not_crash.TURNO t join you_shall_not_crash.CONSULTA d on t.ID_TURNO=d.ID_TURNO join gd_esquema.Maestra m on t.NUMERO=m.Turno_Numero join you_shall_not_crash.SINTOMA s on s.DESCRIPCION=m.Consulta_Sintomas
where Consulta_Sintomas is not null and FECHA_LLEGADA is not null
order by 1

--ENFERMEDAD_CONSULTA-----------------------------------------
insert into YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA
select distinct d.ID_CONSULTA,e.ID_ENFERMEDAD
from you_shall_not_crash.TURNO t join you_shall_not_crash.CONSULTA d on t.ID_TURNO=d.ID_TURNO join gd_esquema.Maestra m on t.NUMERO=m.Turno_Numero join you_shall_not_crash.ENFERMEDAD e on e.DESCRIPCION=m.Consulta_Enfermedades
where Consulta_Enfermedades is not null and FECHA_LLEGADA is not null
order by 1

--RECETA----------------------------FALTA
insert into YOU_SHALL_NOT_CRASH.RECETA
select c.ID_CONSULTA
from you_shall_not_crash.CONSULTA c

--TURNO------------------------------------------------------------

INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_TURNO
SELECT 
   CASE
    WHEN FECHA>GETDATE()
     THEN 'CANCELA_PROFESIONAL'
     ELSE 'CANCELA_PACIENTE'
     END as Tipo_Cancelacion,
   CASE
    WHEN FECHA>GETDATE()
     THEN 'CAMBIO DE AGENDA DEL PROFESIONAL'
     ELSE 'SIN MOTIVO'
     END as Detalle,
   '01/01/2013' Fecha, ID_TURNO as IdTurno 
FROM YOU_SHALL_NOT_CRASH.TURNO
WHERE (FECHA_LLEGADA IS NULL AND FECHA<GETDATE()) OR (datepart(dw,FECHA)=7 AND FECHA>GETDATE()) ;

---------------------------------------------------------------------------------------


-- Se consideran "cancelados" los turnos anteriores a la fecha actual a los que no se haya presentado el paciente.
-- Se considera que al no tener detallado los motivos el tipo de cancelacion se asume "por el paciente" y "sin motivo" en
-- el detalle.
-- Por nueva restriccion de dominio se cancelan todos los turnos agendados para días Domingos con tipo de cancelacion
-- "por el profesional" con motivo "cambio de agenda del profesional".
 
--Ahora actualizo el booleano de los turnos que fueron cancelados
UPDATE YOU_SHALL_NOT_CRASH.TURNO SET Cancelado=1 WHERE ID_TURNO IN (SELECT ID_TURNO FROM YOU_SHALL_NOT_CRASH.CANCELACION_TURNO)


--AGENDA-----

CREATE TABLE YOU_SHALL_NOT_CRASH.AGENDA (
Id_Agenda int identity(1,1),	
Id_Profesional numeric,
Fecha_Inicio datetime,
Fecha_Fin datetime,

PRIMARY KEY (Id_Agenda),
FOREIGN KEY (Id_Profesional) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL (ID_Profesional));

CREATE TABLE YOU_SHALL_NOT_CRASH.ITEM_AGENDA (
ID_Item int identity(1,1),
ID_Agenda int,
Fecha date,
Hora_Inicio time,
Hora_Fin time,
ID_TURNO numeric

PRIMARY KEY (Id_Item),
FOREIGN KEY (ID_Agenda) REFERENCES YOU_SHALL_NOT_CRASH.AGENDA (Id_Agenda),
FOREIGN KEY (ID_Turno) REFERENCES YOU_SHALL_NOT_CRASH.Turno (Id_Turno));

INSERT INTO YOU_SHALL_NOT_CRASH.AGENDA
SELECT p.Id_Profesional, MIN(Convert(char(10),turno_fecha, 103)), MAX(Convert(char(10),turno_fecha, 103))
FROM  gd_esquema.Maestra join YOU_SHALL_NOT_CRASH.PROFESIONAL p on gd_esquema.Maestra.Medico_Dni=p.DNI
WHERE TURNO_FECHA IS NOT NULL AND Medico_Dni IS NOT NULL
GROUP BY p.Id_Profesional
order by 1

INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_AGENDA 
SELECT (a.ID_Agenda), cast (CONVERT(datetime, DATEDIFF(d, 0, Turno_Fecha), 102) as date), 
cast((turno_fecha) as time), 
cast(DATEADD(minute,30,TURNO_FECHA) as time), t.ID_TURNO
FROM  gd_esquema.Maestra 
	join YOU_SHALL_NOT_CRASH.PROFESIONAL p on gd_esquema.Maestra.Medico_Dni=p.DNI 
	join YOU_SHALL_NOT_CRASH.AGENDA a on p.ID_PROFESIONAL = a.Id_Profesional
	join YOU_SHALL_NOT_CRASH.TURNO t on Turno_Numero = t.NUMERO
WHERE TURNO_FECHA IS NOT NULL AND Medico_Dni IS NOT NULL AND datepart(dw,TURNO_FECHA)!=7  and t.Cancelado = 0
GROUP BY p.Id_Profesional, a.Id_Agenda, Turno_Fecha,t.ID_TURNO
order by 1,2,3


--CARGA INICIAL DE CANTIDAD DE CONSULTAS
UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Cantidad_Consultas=(SELECT COUNT(DISTINCT T.ID_Bono_Consulta) 
															FROM YOU_SHALL_NOT_CRASH.TURNO T
															WHERE T.ID_AFILIADO=AFILIADO.ID_AFILIADO)
															

--Tabla para registrar dias cancelados por un profesional
/*
CREATE TABLE YOU_SHALL_NOT_CRASH.CANCELACION_DIA (
ID_CANCELACION_DIA int identity(1,1),
ID_PROFESIONAL numeric(18,0),
DiaHora_inicio DATETIME,
DiaHora_Fin DATETIME,

PRIMARY KEY (ID_CANCELACION_DIA),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL (ID_PROFESIONAL));
*/
--COMPRA DE BONOS
GO
create view YOU_SHALL_NOT_CRASH.compras_temp as (

SELECT ROW_NUMBER() OVER( ORDER BY afi ) AS id, afi, cant_cons, cant_farm, Precio_bono, id_bono
FROM(
select c.ID_Afiliado as afi, 1 as cant_cons, 0 as cant_farm, p.Precio_bono_consulta as precio_bono, c.ID_Bono_Consulta as id_bono
from YOU_SHALL_NOT_CRASH.BONO_CONSULTA c join YOU_SHALL_NOT_CRASH.PLAN_MEDICO p on (c.ID_Plan = p.ID_Plan)
union all
select f.ID_Afiliado, 0 as cant_cons, 1 as cant_farm, p.Precio_bono_farmacia, f.ID_Bono_Farmacia as id_bono
from YOU_SHALL_NOT_CRASH.BONO_FARMACIA f join YOU_SHALL_NOT_CRASH.PLAN_MEDICO p on (f.ID_Plan = p.ID_Plan)

) as id);
GO

insert into YOU_SHALL_NOT_CRASH.COMPRA_BONO 
select afi, cant_cons, cant_farm, Precio_bono 
from YOU_SHALL_NOT_CRASH.compras_temp order by id
;

insert into YOU_SHALL_NOT_CRASH.ITEM_COMPRA_BONO
select ID, id_bono, case when cant_cons > 0 then 'c' else 'f' end, precio_bono 
from YOU_SHALL_NOT_CRASH.compras_temp 
order by cant_cons,id

--LE doy especialidad al admin
insert into YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL values (10029, 1)


---------------------------------------------------------------------
----------------------FUNCIONES Y SPS--------------------------------
---------------------------------------------------------------------
GO

CREATE FUNCTION YOU_SHALL_NOT_CRASH.Split(@String varchar(150), @Delimiter char(1))
RETURNS @Results table (word varchar(50))
AS
BEGIN
DECLARE @INDEX INT

DECLARE @SLICE varchar(200)
-- Asignar 1 a la variable que utilizaremos en el loop para no iniciar en 0. 
SELECT @INDEX = 1

WHILE @INDEX !=0
BEGIN
-- Obtenemos el índice de la primera ocurrencia del split de caracteres. 
SELECT @INDEX = CHARINDEX(@Delimiter,@STRING)
-- Ahora ponemos todo a la izquierda de el slice de la variable. 
IF @INDEX != 0
SELECT @SLICE = LEFT(@STRING,@INDEX - 1)
ELSE
SELECT @SLICE = @STRING

insert into @Results(word) values(@SLICE)

SELECT @STRING = RIGHT(@STRING,LEN(@STRING) - @INDEX)
-- Salimos del loop si terminamos la búsqueda 
IF LEN(@STRING) = 0 BREAK
END

RETURN
END
GO

create procedure YOU_SHALL_NOT_CRASH.login(@usuario nvarchar(255), @pass nvarchar(255), @respuesta nvarchar(255) output)
AS 
BEGIN                  
 
declare @existeUsuario INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario);                   
                   
if (@existeUsuario = 1)
begin                                          
	declare @cantidadIntentosFallidos INT = (SELECT Intentos_Fallidos FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario);
	    
	if (@cantidadIntentosFallidos < 3)
	begin
		declare @existeUsuarioyContraseña INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario and Pass = @pass);
		
		if (@existeUsuarioyContraseña = 1)
		begin
			UPDATE YOU_SHALL_NOT_CRASH.USUARIO SET Intentos_Fallidos=0 WHERE Username = @usuario;
			declare @existeRol INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO u join YOU_SHALL_NOT_CRASH.ROL_USUARIO ru on (u.DNI_Usuario = ru.DNI_Usuario) WHERE Username = @usuario)
			
			if (@existeRol = 0)
			begin
				set @respuesta='El usuario no tiene asignado un rol, o el rol ha sido inhabilitado'
			end
			else
			begin			
				set @respuesta='abrir sesion'
			end

		end
		else
		begin
			UPDATE YOU_SHALL_NOT_CRASH.USUARIO SET Intentos_Fallidos=(Intentos_Fallidos+1) WHERE Username = @usuario;
			set @cantidadIntentosFallidos = (@cantidadIntentosFallidos + 1);
			declare @cantidadIntentosFallidosString nvarchar(255) = @cantidadIntentosFallidos;
			
			set @respuesta = 'Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: ' + (@cantidadIntentosFallidosString);
		end

	end
	else
	begin
		set @respuesta = 'Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos';
	end  
end
else
begin 
set @respuesta = 'No existe el usuario, vuelva a intentarlo';                              
end

END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Insertar_Rol(@nombreRol nvarchar(255), @respuesta int output)
AS
BEGIN
	IF EXISTS(SELECT * FROM YOU_SHALL_NOT_CRASH.Rol WHERE Rol.Descripcion = @nombreRol)
	BEGIN
		set @respuesta = -1
	END
	ELSE
	BEGIN
		INSERT INTO YOU_SHALL_NOT_CRASH.Rol VALUES (@nombreRol, 1)
		set @respuesta = (SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.Rol WHERE Rol.Descripcion = @nombreRol)
	END
END
GO

CREATE FUNCTION YOU_SHALL_NOT_CRASH.F_Roles ()
RETURNS TABLE
AS
RETURN (SELECT 0 as RN,'No seleccionado' as NombreRol
	    UNION
	    SELECT ROW_NUMBER() OVER(ORDER BY r.Descripcion ASC) as RN,r.Descripcion FROM YOU_SHALL_NOT_CRASH.ROL r
	    );
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Baja_Logica_Rol(@nombreRol varchar(255))
AS
BEGIN
UPDATE YOU_SHALL_NOT_CRASH.ROL SET Activo=0 WHERE Descripcion = @nombreRol

declare @codigoRol int = (SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion = @nombreRol)

DELETE FROM YOU_SHALL_NOT_CRASH.ROL_USUARIO WHERE ID_Rol = @codigoRol

END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Traer_Info_Afiliado(@nombreUsuario varchar(255), @nroAfiliado int output, @precioConsulta numeric(18,2) output, @precioFarmacia numeric(18,2) output, @idAfiliado int output, @idPlan int output)
AS
BEGIN
set @idAfiliado = (select a.ID_Afiliado 
					from YOU_SHALL_NOT_CRASH.afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) 
					where u.Username = @nombreUsuario)
					
set @idPlan = (select a.ID_Plan 
					from YOU_SHALL_NOT_CRASH.afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) 
					where u.Username = @nombreUsuario)					


set @nroAfiliado = (select Nro_Afiliado
					from YOU_SHALL_NOT_CRASH.afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) 
					where u.Username = @nombreUsuario)
					
set @precioConsulta = (select Precio_Bono_Consulta 
						from YOU_SHALL_NOT_CRASH.PLAN_MEDICO pm join 
						YOU_SHALL_NOT_CRASH.afiliado a on (pm.ID_Plan = a.ID_Plan) join
						YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) 
						where u.Username = @nombreUsuario)
 
set @precioFarmacia = (select Precio_Bono_Farmacia
						from YOU_SHALL_NOT_CRASH.PLAN_MEDICO pm join 
						YOU_SHALL_NOT_CRASH.afiliado a on (pm.ID_Plan = a.ID_Plan) join
						YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) 
						where u.Username = @nombreUsuario) 
END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Comprar_Bonos(@fechaActual datetime, @idAfiliado int, @idPlan int, @cantBonosConsulta int, @cantBonosFarmacia int, @idCompra int)
AS
BEGIN
declare @IdConsulta int = ((select max(ID_Bono_consulta) from YOU_SHALL_NOT_CRASH.BONO_CONSULTA) + 1)
declare @IdFarmacia int = ((select max(ID_Bono_Farmacia) from YOU_SHALL_NOT_CRASH.BONO_FARMACIA) + 1)
	
	WHILE @cantBonosConsulta <> 0
	BEGIN		
		INSERT INTO YOU_SHALL_NOT_CRASH.BONO_CONSULTA (ID_Bono_Consulta, Fecha_Emision, ID_Afiliado, ID_Plan) 
		values (@IdConsulta, @fechaActual, @idAfiliado, @idPlan)
		
		INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_COMPRA_BONO 
		SELECT @idCompra, @IdConsulta, 'c', Precio_bono_consulta
		from YOU_SHALL_NOT_CRASH.PLAN_MEDICO
		where ID_Plan = @idPlan
		
		SET @cantBonosConsulta = (@cantBonosConsulta - 1)
		SET @IdConsulta = (@IdConsulta + 1)
	END
	
		WHILE @cantBonosFarmacia <> 0
	BEGIN
		INSERT INTO YOU_SHALL_NOT_CRASH.BONO_FARMACIA(ID_Bono_Farmacia, Fecha_Emision, ID_Afiliado, ID_Plan, Fecha_Vencimiento) 
		values (@IdFarmacia, @fechaActual, @idAfiliado, @idPlan, @fechaActual+60)
		
		INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_COMPRA_BONO 
		SELECT @idCompra, @IdFarmacia, 'f', Precio_bono_farmacia
		from YOU_SHALL_NOT_CRASH.PLAN_MEDICO
		where ID_Plan = @idPlan
		
		SET @cantBonosFarmacia = (@cantBonosFarmacia - 1)
		SET @IdFarmacia = (@IdFarmacia + 1)
	END

END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Registrar_Compra (@idAfiliado int, @cantBonosConsulta int, @cantBonosFarmacia int, @monto numeric(18,2), @idCompra int output)
AS
BEGIN
INSERT INTO YOU_SHALL_NOT_CRASH.COMPRA_BONO values (@idAfiliado,@cantBonosConsulta,@cantBonosFarmacia,@monto)
set @idCompra = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Insertar_Agenda(@idProfesional numeric, @fechaInicio datetime, @fechaFin datetime, @respuesta int output)
AS
BEGIN

declare @fi datetime
declare @ff datetime
declare @flag int
DECLARE CUR CURSOR 
FOR SELECT Fecha_Inicio, Fecha_Fin 
	from YOU_SHALL_NOT_CRASH.AGENDA 
	where Id_Profesional = @idProfesional

OPEN cur	
FETCH cur INTO @fi, @ff
WHILE @@FETCH_STATUS = 0
BEGIN
	if (@fechaInicio between @fi and @ff) or (@fechaFin between @fi and @ff)
		set @flag = 1		
	FETCH cur INTO @fi, @ff
END

CLOSE cur
DEALLOCATE cur

if @flag = 1
BEGIN
	set @respuesta = -1
END
ELSE
BEGIN
	declare @fechaInicioCasted date= @fechaInicio
	declare @fechaFinCasted date= @fechaFin
	
	INSERT INTO YOU_SHALL_NOT_CRASH.AGENDA VALUES (@idProfesional, @fechaInicioCasted, @fechaFinCasted)
	SET @respuesta = (select ID_Agenda 
					from YOU_SHALL_NOT_CRASH.AGENDA 
					where Id_Profesional = @idProfesional and
						Fecha_Inicio = @fechaInicioCasted and 
						Fecha_Fin = @fechaFinCasted)		
END
END
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Insertar_turno (@fechaCompleta dateTime, @profesional numeric, @afiliado int, @fecha dateTime, @horaInicio time)
AS
BEGIN 
	declare @numeroTurno numeric(18,0) = (SELECT MAX(NUMERO) FROM YOU_SHALL_NOT_CRASH.TURNO) +1
	
	BEGIN TRANSACTION
	INSERT INTO YOU_SHALL_NOT_CRASH.TURNO (NUMERO, ID_PROFESIONAL, ID_AFILIADO, FECHA, Cancelado)
	values (@numeroTurno,@profesional,@afiliado,@fechaCompleta,0)
	
	declare @idAgenda int = (select Id_Agenda from YOU_SHALL_NOT_CRASH.AGENDA 
							where Id_Profesional = @profesional and @fechaCompleta BETWEEN Fecha_Inicio-1 and Fecha_Fin+1)
	declare @idTurno numeric = (select id_turno from YOU_SHALL_NOT_CRASH.TURNO
								where NUMERO = @numeroTurno)
	
	UPDATE YOU_SHALL_NOT_CRASH.ITEM_AGENDA SET ID_TURNO = (@idTurno)
	where ID_Agenda = @idAgenda and Fecha = @fecha and Hora_Inicio = @horaInicio
		
	if ( @@ERROR != 0)
	BEGIN 
	rollback 
	END
	ELSE BEGIN 
	commit
	END			
END
GO


create procedure YOU_SHALL_NOT_CRASH.dni_profesional_nuevo(@dni nvarchar(255), @resultado INT output)
AS 
BEGIN       
	declare @dni2 numeric(18,0) = CONVERT(numeric(18,0),@dni);  
      
	IF EXISTS(select p.DNI from YOU_SHALL_NOT_CRASH.PROFESIONAL P where p.DNI=@dni2)
	BEGIN
		SET @resultado = 1
	END
	ELSE
	BEGIN
		SET @resultado = 0
	END
END
go

create procedure YOU_SHALL_NOT_CRASH.insertar_prof_espec(@dni nvarchar(255),@especialidad nvarchar(255))
AS
BEGIN
	DECLARE @dni2 numeric(18,0) = CONVERT(numeric(18,0),@dni)
	DECLARE @prof_id numeric(18,0)
	DECLARE @esp_id numeric(18,0)
	
	set @prof_id=(select p.ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL p where p.DNI=@dni2)
	set @esp_id=(select e.CODIGO_ESPECIALIDAD from YOU_SHALL_NOT_CRASH.ESPECIALIDAD e where @especialidad=e.DESCRIPCION)
	insert into YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL(CODIGO_ESPECIALIDAD, ID_PROFESIONAL)
	values(@esp_id,@prof_id)
END
GO

-----------POR  AHORA DEJO COMO USUARIO NOM.AP.FECHA-NAC
CREATE PROCEDURE YOU_SHALL_NOT_CRASH.insertar_profesional(@nombre varchar(255),@apellido varchar(255),@dni int,@direccion varchar(255),@matricula int,@fecha_nac dateTime,@sexo varchar(9),@mail varchar(255), @telefono int)
AS
BEGIN

	INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO(Username,Pass,DNI_Usuario,Intentos_Fallidos) 
	VALUES (@nombre+@apellido+CONVERT(VARCHAR,YEAR(@fecha_nac)), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', @dni, 0)

	declare @cod_profesional int
	set @cod_profesional=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Profesional')	
	INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO(ID_Rol,DNI_Usuario)
	VALUES(@cod_profesional,@dni)
	
	declare @dni_profesional numeric(18,0)
	set @dni_profesional=(SELECT DNI_Usuario FROM YOU_SHALL_NOT_CRASH.USUARIO  WHERE DNI_Usuario=@dni)
	
	INSERT INTO YOU_SHALL_NOT_CRASH.PROFESIONAL(NOMBRE,APELLIDO,DNI,DIRECCION,TELEFONO,MAIL,FECHA_NAC,SEXO,MATRICULA,ACTIVO)
	VALUES(@nombre,@apellido, (SELECT DNI_Usuario FROM YOU_SHALL_NOT_CRASH.USUARIO  WHERE DNI_Usuario=@dni_profesional),@direccion,convert(numeric(18,0),@telefono),@mail,CONVERT(datetime, @fecha_nac+' '+'00:00:00'),@sexo,@matricula,1)
END
go


----PARA LA BAJA LOGICA DE UN PROF, TAMB SE DAN DE BAJA EN FORMA LOGICA LOS TURNOS
CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Baja_Logica_Profesional(@dni nvarchar(9))
AS
BEGIN
	declare @dni2 numeric(18,0)=convert(numeric(18,0),@dni)
	
	UPDATE YOU_SHALL_NOT_CRASH.PROFESIONAL SET Activo=0 WHERE DNI = @dni2
	--BAJA LOGICA DE TURNO Y AGREGO LOS CANCELADOS A CANCELACION_TURNO
	UPDATE YOU_SHALL_NOT_CRASH.TURNO SET Cancelado=1 WHERE ID_PROFESIONAL=(select p.ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL p WHERE P.DNI=@dni2)
	INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_TURNO 
	SELECT 'CANCELA_PROFESIONAL','PROFESIONAL DADO DE BAJA',convert(datetime,getdate(),120), t.ID_TURNO from YOU_SHALL_NOT_CRASH.TURNO t left join YOU_SHALL_NOT_CRASH.CANCELACION_TURNO ct on t.ID_TURNO=ct.ID_Turno join YOU_SHALL_NOT_CRASH.PROFESIONAL p on t.ID_PROFESIONAL=p.ID_PROFESIONAL where t.Cancelado=1 and ct.ID_Cancelacion is null and p.DNI=@dni2 and t.FECHA_LLEGADA is null
	
	UPDATE YOU_SHALL_NOT_CRASH.PROFESIONAL SET Activo=0 WHERE Id_Profesional=(select p.ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL p WHERE P.DNI=@dni2)

END
GO


create procedure YOU_SHALL_NOT_CRASH.modificar_profesional(@nombre varchar(255),@apellido varchar(255),@dni int,@direccion varchar(255),@matricula int,@fecha_nac nvarchar(255),@sexo varchar(9),@mail varchar(255), @telefono int,@activo bit,@resu int output)
AS
BEGIN

		UPDATE YOU_SHALL_NOT_CRASH.PROFESIONAL SET NOMBRE=@nombre,APELLIDO=@apellido,DIRECCION=@direccion,TELEFONO=@telefono,MAIL=@mail,FECHA_NAC=CONVERT(datetime, @fecha_nac+' '+'00:00:00'),SEXO=@sexo,MATRICULA=@matricula,ACTIVO=@activo WHERE @dni=YOU_SHALL_NOT_CRASH.PROFESIONAL.DNI

	SET @resu = 1;
END--SI SE QUISIERA ACTIVAR DE NUEVO ALL PROF, LOS TURNOS Q HABIAN SIDO CANCELADOS SEGUIRAN ESTANDO CANCELADOS
go

--EN MODIFICAR_PROF SOLO SE TRAEN LOS PROFESIONALES ACTIVOS(EN 1)
create procedure YOU_SHALL_NOT_CRASH.modificar_prof_espec(@dni nvarchar(255),@especialidad nvarchar(255))
AS
BEGIN
	DECLARE @dni2 numeric(18,0) = CONVERT(numeric(18,0),@dni)
	DECLARE @prof_id numeric(18,0)
	DECLARE @esp_id numeric(18,0)
	
	set @prof_id=(select p.ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL p where p.DNI=@dni2)
	set @esp_id=(select e.CODIGO_ESPECIALIDAD from YOU_SHALL_NOT_CRASH.ESPECIALIDAD e where @especialidad=e.DESCRIPCION)
	insert into YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL(CODIGO_ESPECIALIDAD, ID_PROFESIONAL)
	values(@esp_id,@prof_id)
END
GO

create procedure YOU_SHALL_NOT_CRASH.eliminar_prof_espec(@dni nvarchar(255))
AS
BEGIN

	declare @id_p numeric(18,0)
	set @id_p=(select distinct p.ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep on ep.ID_PROFESIONAL=p.ID_PROFESIONAL where p.DNI=@dni)
	DELETE FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL WHERE ID_PROFESIONAL=@id_p
END
GO 

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Cancelar_turno_afiliado(@id_turno numeric(18,0), @fecha dateTime, @motivo nvarchar(255), @retorno int output)
AS
BEGIN 
	declare @diaTurno date = (select FECHA from YOU_SHALL_NOT_CRASH.TURNO where ID_TURNO = @id_turno)	
	SET @retorno = 0
	declare @diaActual date = @fecha
	
	IF (@diaTurno = @diaActual)
	BEGIN	
		SET @retorno = 1	--no se cancela por no tener un dia de anticipacion
	END
	ELSE
	BEGIN	
		UPDATE YOU_SHALL_NOT_CRASH.TURNO
		SET Cancelado = 1
		WHERE ID_TURNO = @id_turno
		
		INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_TURNO values ('CANCELA_PACIENTE', @motivo, @fecha, @id_turno)
		
		UPDATE YOU_SHALL_NOT_CRASH.ITEM_AGENDA
		SET ID_TURNO = NULL
		WHERE ID_TURNO = @id_turno
	END
END
GO


CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Cancelar_dia_rango(@id_profesional int,@DiaHora_inicio dateTime,@DiaHora_Fin dateTime, @fechaActual dateTime, @motivo nvarchar(255))
AS
BEGIN TRANSACTION
	--Hago un update del campo Cancelado de los turnos que esten dentro del margen de horarios que se puso.
	UPDATE YOU_SHALL_NOT_CRASH.TURNO SET Cancelado = 1 WHERE ID_PROFESIONAL = @id_profesional AND (FECHA between @DiaHora_inicio and @DiaHora_Fin)
	
	declare @idAgenda int = (select Id_Agenda from YOU_SHALL_NOT_CRASH.AGENDA 
							where Id_Profesional = @id_profesional and @DiaHora_inicio BETWEEN Fecha_Inicio-1 and Fecha_Fin+1)
	
	
	--pongo null en los time slot agenda
	declare @horaInicio time = @DiaHora_inicio
	declare @horaFin time = @DiaHora_fin
	declare @fechaSinHora date = @DiaHora_inicio
	
	UPDATE YOU_SHALL_NOT_CRASH.ITEM_AGENDA
	SET ID_TURNO = NULL
	WHERE ID_Agenda = @idAgenda and Fecha = @fechaSinHora and Hora_Inicio BETWEEN @horaInicio AND @horaFin
	
	--Hago un insert en la tabla de Cancelacion_Dia
	--INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_DIA (ID_PROFESIONAL,DiaHora_inicio,DiaHora_Fin) VALUES (@id_profesional,@DiaHora_inicio,@DiaHora_Fin)
	
	--cursor para insertar las cancelaciones muy feo usar cancelacion_dia
	declare @idTurno numeric
	declare turnosParaAnular cursor
	for select id_turno from YOU_SHALL_NOT_CRASH.TURNO 
		where ID_PROFESIONAL = @id_profesional and FECHA BETWEEN @DiaHora_inicio AND @DiaHora_Fin
		
	open turnosParaAnular
	fetch turnosParaAnular into @idTurno 
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_TURNO VALUES ('CANCELA_PROFESIONAL', @motivo, @fechaActual, @idTurno)
		fetch turnosParaAnular into @idTurno
	END
	CLOSE turnosParaAnular
	DEALLOCATE turnosParaAnular
	
	
	if ( @@ERROR != 0)
	BEGIN 
		rollback 
	END
	ELSE BEGIN 
		commit
		END
GO


CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Ingresar_bono_y_llegada(@id_bono_ingresado int,@numero_de_consulta_a_ingresar int,@id_turno int, @fecha_llegada dateTime,@idafiliado int)
AS
BEGIN TRANSACTION
	--Hago un update del turno registrando la hora de llegada
	UPDATE YOU_SHALL_NOT_CRASH.TURNO SET FECHA_LLEGADA = @fecha_llegada, ID_Bono_Consulta = @id_bono_ingresado WHERE ID_TURNO = @id_turno
	--Hago un update del bono consulta usado, agregando el numero de consulta que le corresponde
	UPDATE YOU_SHALL_NOT_CRASH.BONO_CONSULTA SET Numero_Consulta_Afiliado = @numero_de_consulta_a_ingresar WHERE ID_Bono_Consulta = @id_bono_ingresado
	--Hago un update de la cantidad de consultas realizadas en la tabla de afiliados
	UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Cantidad_Consultas = Cantidad_Consultas + 1 WHERE ID_Afiliado = @idafiliado
	--CREO LA CONSULTA DE ESE TURNO
	INSERT INTO YOU_SHALL_NOT_CRASH.CONSULTA (ID_PROFESIONAL,ID_TURNO) 
	SELECT ID_PROFESIONAL, @id_turno FROM YOU_SHALL_NOT_CRASH.TURNO WHERE ID_TURNO=@id_turno
	if ( @@ERROR != 0)
	BEGIN 
		rollback 
	END
	ELSE BEGIN 
		commit
	END
GO


CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Insertar_receta
AS
BEGIN TRANSACTION
	--Inserto una nueva consulta
	INSERT INTO YOU_SHALL_NOT_CRASH.RECETA(ID_CONSULTA) VALUES (NULL)
	
	if ( @@ERROR != 0)
	BEGIN 
		rollback 
	END
	ELSE BEGIN 
		commit
	END
GO

--FUNCIONES PARA LOS LISTADOS
CREATE FUNCTION YOU_SHALL_NOT_CRASH.Top5_Especialidades_Mas_Canceladas_En (@anio int, @mesInicial int, @mesFinal int)
RETURNS TABLE
AS
RETURN (select top 5 e.DESCRIPCION as Especialidad, COUNT(*) as Cancelaciones
		from YOU_SHALL_NOT_CRASH.CANCELACION_TURNO c join 
			YOU_SHALL_NOT_CRASH.TURNO t on (c.ID_Turno = t.ID_TURNO) join
			YOU_SHALL_NOT_CRASH.PROFESIONAL p on (t.ID_PROFESIONAL = p.ID_PROFESIONAL) join
			YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep on (p.ID_PROFESIONAL = ep.ID_PROFESIONAL) join
			YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on (ep.CODIGO_ESPECIALIDAD = e.CODIGO_ESPECIALIDAD)
		where (YEAR(c.Fecha) = @anio) and (MONTH(c.fecha) BETWEEN @mesInicial and @mesFinal)
		group by e.DESCRIPCION
		order by 2 desc
);
GO

CREATE FUNCTION YOU_SHALL_NOT_CRASH.Top5_Bonos_Farmacia_Vencidos_En (@anio int, @mesInicial int, @mesFinal int, @fechaActual datetime)
RETURNS TABLE
AS
RETURN (select top 5 a.Nombre + ' ' + a.Apellido as Afiliado, COUNT(*) as Bonos_Vencidos
		from YOU_SHALL_NOT_CRASH.BONO_FARMACIA b join 
			YOU_SHALL_NOT_CRASH.AFILIADO a on (b.ID_Afiliado = a.ID_Afiliado)
		where b.Fecha_Vencimiento < @fechaActual and ID_Receta_Medica is null and
			(YEAR(b.Fecha_Vencimiento) = @anio) and (MONTH(b.Fecha_Vencimiento) BETWEEN @mesInicial and @mesFinal)
		group by a.ID_Afiliado, a.Nombre, a.Apellido
		order by 2 desc
);
GO

CREATE FUNCTION YOU_SHALL_NOT_CRASH.Top5_Especialidades_Que_Mas_Recetaron_En (@anio int, @mesInicial int, @mesFinal int)
RETURNS TABLE
AS
RETURN (select top 5 e.DESCRIPCION as Especialidad, COUNT(*) as Bonos_Recetados
		from YOU_SHALL_NOT_CRASH.BONO_FARMACIA b join
			 YOU_SHALL_NOT_CRASH.RECETA r on (b.ID_Receta_Medica = r.ID_RECETA) join
			 YOU_SHALL_NOT_CRASH.CONSULTA c on (r.ID_CONSULTA = c.ID_CONSULTA) join
			 YOU_SHALL_NOT_CRASH.TURNO t on (c.ID_TURNO = t.ID_TURNO) join --para obtener la fecha en que receto		
			 YOU_SHALL_NOT_CRASH.PROFESIONAL p on (c.ID_PROFESIONAL = p.ID_PROFESIONAL) join
			 YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep on (p.ID_PROFESIONAL = ep.ID_PROFESIONAL) join
			 YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on (ep.CODIGO_ESPECIALIDAD = e.CODIGO_ESPECIALIDAD)
		where (YEAR(t.FECHA) = @anio) and (MONTH(t.FECHA) BETWEEN @mesInicial and @mesFinal)
		group by e.DESCRIPCION
		order by 2 desc
);
GO

CREATE FUNCTION YOU_SHALL_NOT_CRASH.Top10_Afiliado_Que_Uso_Bonos_De_Otro_En (@anio int, @mesInicial int, @mesFinal int)
RETURNS TABLE
AS
RETURN (select top 10 a.Nombre + ' ' + a.Apellido as Afiliado , SUM(
			case when  bc.ID_Afiliado <> t.ID_AFILIADO and bf.id_Afiliado <> t.ID_AFILIADO	then 2 else 1 end) as Cantidad_Bonos

		from YOU_SHALL_NOT_CRASH.BONO_CONSULTA bc join 
			YOU_SHALL_NOT_CRASH.TURNO t on (bc.ID_Bono_Consulta = t.ID_Bono_Consulta) join
			YOU_SHALL_NOT_CRASH.AFILIADO a on (t.ID_AFILIADO = a.ID_Afiliado) join
			YOU_SHALL_NOT_CRASH.CONSULTA c on (c.id_turno = t.id_turno) join
			YOU_SHALL_NOT_CRASH.RECETA r on (c.ID_CONSULTA = r.ID_CONSULTA) join
			YOU_SHALL_NOT_CRASH.BONO_FARMACIA bf on (r.ID_RECETA = bf.ID_RECETA_MEDICA)			
		where (bc.ID_Afiliado <> t.ID_AFILIADO or bf.id_Afiliado <> t.ID_AFILIADO)	and(
			(YEAR(t.FECHA) = @anio) and (MONTH(t.FECHA) BETWEEN @mesInicial and @mesFinal))	
		group by a.ID_Afiliado, a.Nombre, a.Apellido	
		order by 2 desc
);
GO

CREATE PROCEDURE YOU_SHALL_NOT_CRASH.Insertar_Item_Agenda(@idAgenda int, @fechaInicio datetime, @fechaFin datetime, @horaInicio time, @horaFin time, @numeroDia int)
AS
BEGIN
declare @fechaAAgregar date
if (datepart(dw,@fechaInicio) <= @numeroDia)
begin
	set @fechaAAgregar = dateAdd(Day,ABS((datepart(dw,@fechaInicio)) - @numeroDia), @fechaInicio)
end
else
begin
	set @fechaAAgregar  = dateAdd(Day,ABS(7-ABS((datepart(dw,@fechaInicio)) - @numeroDia)), @fechaInicio)
end

declare @horaAAgregar time = @horaInicio

WHILE @fechaAAgregar <= @fechaFin
BEGIN
set @horaAAgregar = @horaInicio

	WHILE @horaAAgregar < @horaFin
	BEGIN
		INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_AGENDA (ID_Agenda,Fecha,Hora_Inicio,Hora_Fin) values
		(@idAgenda, @fechaAAgregar, cast(@horaAAgregar as time), cast(DATEADD(minute,30,@horaAAgregar) as time))
		set @horaAAgregar = DATEADD(minute,30,@horaAAgregar)
	END	

SET @fechaAAgregar = DATEADD(Day,7,@fechaAAgregar)
END

END
GO

---------------------------------------------------------------------
-----------------------------TRIGGERS--------------------------------
---------------------------------------------------------------------
GO
CREATE TRIGGER NUEVO_AFILIADO ON YOU_SHALL_NOT_CRASH.AFILIADO
AFTER INSERT
AS BEGIN

DECLARE @NRO INT

SET @NRO = (SELECT (ID_AFILIADO * 100) FROM YOU_SHALL_NOT_CRASH.AFILIADO WHERE DNI=(SELECT TOP 1 DNI FROM INSERTED order by Nro_Afiliado))
--ASIGNO EL NRO DE AFILIADO
UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Nro_Afiliado=(Nro_Afiliado + @NRO) WHERE DNI IN (SELECT DNI FROM inserted)
--AGREGO ROL DE AFILIADO
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO SELECT 3, DNI FROM inserted
 
END; 
GO

--ACTUALIZA EL NUMERO DE CONSULTA DE LOS AFILIADOS
UPDATE
    YOU_SHALL_NOT_CRASH.BONO_CONSULTA
SET
    YOU_SHALL_NOT_CRASH.BONO_CONSULTA.Numero_Consulta_Afiliado = numeros_consulta.numero_consulta
FROM   
	(SELECT Bono_Consulta_Numero, Max(a.ID_Afiliado) IDaFILIADO, Max(Turno_Fecha) FECHA, 
    (1+(SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.TURNO T2 WHERE T2.ID_AFILIADO=Max(a.ID_Afiliado) AND Cancelado=0 and t2.FECHA<Max(Turno_Fecha))) numero_consulta
	from gd_esquema.Maestra M join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
	where Turno_Numero is not null AND M.Bono_Consulta_Numero IS NOT NULL
	group by Turno_Numero, M.Bono_Consulta_Numero
	)numeros_consulta
	
where YOU_SHALL_NOT_CRASH.BONO_CONSULTA.ID_Bono_Consulta = numeros_consulta.Bono_Consulta_Numero

GO
--actualizo los id de receta en los bonos que fueron usados.
DECLARE bonos CURSOR
FOR
select distinct r.ID_RECETA, m.Bono_Farmacia_Numero, m.Turno_Fecha from gd_esquema.Maestra m join you_shall_not_crash.TURNO t on m.Turno_Numero=t.NUMERO join 
YOU_SHALL_NOT_CRASH.CONSULTA c on (t.ID_TURNO = c.ID_TURNO) join YOU_SHALL_NOT_CRASH.RECETA r on (c.ID_CONSULTA = r.ID_CONSULTA) 
where FECHA_LLEGADA is not null and Bono_Farmacia_Numero is not null 

DECLARE @rec INT
DECLARE @bon INT
DECLARE @dia DATETIME
OPEN bonos 

FETCH bonos INTO @rec, @bon, @dia

WHILE @@FETCH_STATUS=0
 BEGIN
 UPDATE YOU_SHALL_NOT_CRASH.BONO_FARMACIA SET ID_Receta_Medica=@rec, Fecha_Prescripcion_Medica=@dia WHERE ID_Bono_Farmacia=@bon
 FETCH bonos INTO @rec, @bon, @dia
 END
 
CLOSE bonos
DEALLOCATE bonos
------------------------------------------------------------


